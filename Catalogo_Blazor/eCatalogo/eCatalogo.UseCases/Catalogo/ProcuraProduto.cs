using eCatalogo.Core.Models;
using eCatalogo.UseCases.Repository;

namespace eCatalogo.UseCases.Catalogo;

public class ProcuraProduto : IProcuraProduto
{
    private readonly IProdutoRepository _repository;

    public ProcuraProduto(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Produto> Execute(string filter = null)
    {
        return _repository.GetProdutos(filter);
    }
}