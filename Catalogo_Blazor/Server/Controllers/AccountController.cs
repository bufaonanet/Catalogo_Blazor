using Catalogo_Blazor.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Catalogo_Blazor.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = config;
    }

    [HttpGet]
    public string Get() => $"{nameof(AccountController)} :: {DateTime.Now.ToShortTimeString()}";

    [HttpPost("Register")]
    public async Task<ActionResult<UserToken>> Register([FromBody] UserInfo userInfo)
    {
        var user = new IdentityUser
        {
            UserName = userInfo.Email,
            Email = userInfo.Email
        };

        var result = await _userManager.CreateAsync(user, userInfo.Password);
        if (result.Succeeded)
        {
            //inclulir o novo usuario ao perfil User
            await _userManager.AddToRoleAsync(user, "User");

            if (user.Email.StartsWith("admin"))
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return await GenerateTokenAsync(userInfo);
        }

        return BadRequest(new { message = "Senha ou nome do usuário inválidos..." });
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
    {

        var result = await _signInManager.PasswordSignInAsync(
            userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return await GenerateTokenAsync(userInfo);
        }

        return BadRequest(new { message = "Senha ou nome do usuário inválidos..." });
    }

    private async Task<UserToken> GenerateTokenAsync(UserInfo userInfo)
    {
        //var claims = new List<Claim>
        //{
        //    new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
        //    new Claim(ClaimTypes.Name, userInfo.Email),
        //    new Claim("bufa","nanet"),
        //    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        //};

        List<Claim> claims = new()
        {
             new Claim(ClaimTypes.Name, userInfo.Email),
             new Claim("bufa","nanet"),
        };

        var user = await _signInManager.UserManager.FindByEmailAsync(userInfo.Email);
        var roles = await _signInManager.UserManager.GetRolesAsync(user);       

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddHours(2);
        var message = "Token JWT criado com sucesso";

        JwtSecurityToken token = new
        (
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );

        return new UserToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            Message = message
        };
    }
}
