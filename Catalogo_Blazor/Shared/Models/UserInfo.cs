using System.ComponentModel.DataAnnotations;

namespace Catalogo_Blazor.Shared.Models;

public class UserInfo
{
    [Required(ErrorMessage ="Informe o Email")]
    [EmailAddress(ErrorMessage ="Formato do email inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage ="Informe o Password")]
    public string Password { get; set; }
}

