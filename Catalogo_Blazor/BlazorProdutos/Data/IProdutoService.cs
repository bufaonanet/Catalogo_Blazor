namespace BlazorProdutos.Data;

public interface IProdutoService
{
    Task<int> Create(Produto produto);
    Task<int> Delete(int Id);
    Task<int> Update(Produto produto);
    Task<Produto> GetById(int Id);
    Task<List<Produto>> ListAll();
}
