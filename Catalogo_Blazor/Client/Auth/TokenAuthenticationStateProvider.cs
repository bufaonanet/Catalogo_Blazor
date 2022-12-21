using Catalogo_Blazor.Client.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Catalogo_Blazor.Client.Auth;

public class TokenAuthenticationStateProvider : AuthenticationStateProvider, IAuthorizeService
{
    private readonly IJSRuntime _js;
    private readonly HttpClient _http;
    private static readonly string _tokenKey = "tokenkey";

    public TokenAuthenticationStateProvider(IJSRuntime js, HttpClient http)
    {
        _js = js;
        _http = http;
    }

    private AuthenticationState NotAutehnticate =>
        new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _js.GetFromLocalStorage(_tokenKey);

        if (string.IsNullOrEmpty(token))
        {
            return NotAutehnticate;
        }

        return CreateAuthenticationState(token);
    }

    private AuthenticationState CreateAuthenticationState(string token)
    {
        // colocar o token obtido do localstorage no header do request 
        // na seção Authorization assim poderemos estar autenticando 
        // cada requisição HTTP enviada ao servidor por este cliente
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", token);

        //extrair as claims
        return new AuthenticationState(new ClaimsPrincipal
            (new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer
            .Deserialize<Dictionary<string, object>>(jsonBytes);

        keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

        if (roles != null)
        {
            if (roles.ToString().Trim().StartsWith("["))
            {
                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());
                foreach (var parsedRole in parsedRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
            }
            keyValuePairs.Remove(ClaimTypes.Role);
        }

        claims.AddRange(keyValuePairs.Select(kvp =>
        new Claim(kvp.Key, kvp.Value.ToString())));
        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    public async Task Login(string token)
    {
        try
        {
            await _js.SetInLocalStorage(_tokenKey, token);
            var authState = CreateAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Logout()
    {
        try
        {
            await _js.RemoveFromLocalStorage(_tokenKey);
            _http.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(NotAutehnticate));
        }
        catch (Exception)
        {
            throw;
        }

    }
}
