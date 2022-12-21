using eCatalogo.Core.Models;

namespace eCatalogo.UseCases.Catalogo;

public interface IProcuraProduto
{
    IEnumerable<Produto> Execute(string filter = null);
}
