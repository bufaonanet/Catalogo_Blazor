using eCatalogo.Core.Models;
using eCatalogo.UseCases.Repository;

namespace eCatalogo.UseCases.Catalogo;

public class ExibeProduto : IExibeProduto
{
    private readonly IProdutoRepository _repository;

    public ExibeProduto(IProdutoRepository repository)
    {
        _repository = repository;
    }

    public Produto Execute(int id)
    {
        return _repository.GetProduto(id);
    }
}