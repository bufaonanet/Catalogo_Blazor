using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalogo_Blazor.Shared.Models;

public class Produto
{
    public int ProdutoId { get; set; }

    [MaxLength(100)]
    [Required(ErrorMessage = "Informe o nome do produto")]
    public string Nome { get; set; }

    [MaxLength(200)]
    [Required(ErrorMessage = "Informe a descrição do produto")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Informe o preço do produto")]
    [Column(TypeName = "decimal(12,2)")]
    public decimal Preco { get; set; }

    public string ImagemUrl { get; set; }

    public int CategoriaId { get; set; }
    public virtual Categoria Categoria { get; set; }

}