﻿namespace Catalogo_Blazor.Shared.Models;

public class UserToken
{
    public string Token { get; set; }
    public string Message { get; set; }
    public DateTime Expiration { get; set; }
}

