using eCatalogo.Core.Models;

namespace eCatalogo.UseCases.Catalogo;

public interface IExibeProduto
{
    Produto Execute(int id);
}