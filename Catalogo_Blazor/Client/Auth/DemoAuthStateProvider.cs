using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Catalogo_Blazor.Client.Auth;

public class DemoAuthStateProvider : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        await Task.Delay(4000);
        //indicamos se o usuário esta auutenticado e também os seus claims
        var usuario = new ClaimsIdentity(new List<Claim>
        {
            new Claim("chave","valor"),
            new Claim(ClaimTypes.Name,"bufaonanet")
            //new Claim(ClaimTypes.Role,"Admin"),

        });

        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(usuario)));
    }
}