using Dapper;
using System.Data;

namespace BlazorProdutos.Data;

public class ProdutoService : IProdutoService
{
    private readonly IDapperDAL _dapperDal;

    public ProdutoService(IDapperDAL dapperDal)
    {
        _dapperDal = dapperDal;
    }

    public Task<List<Produto>> ListAll()
    {
        var produtos = Task.FromResult(_dapperDal.GetAll<Produto>("select * from [Produtos2]", null, commandType: CommandType.Text));
        return produtos;
    }

    public Task<Produto> GetById(int id)
    {
        var produto = Task.FromResult(_dapperDal.Get<Produto>($"select * from [Produtos2] where ProdutoId2 = {id}", null,
                    commandType: CommandType.Text));
        return produto;
    }

    public Task<int> Create(Produto produto)
    {
        var dbPara = new DynamicParameters();
        dbPara.Add("Nome", produto.Nome, DbType.String);
        dbPara.Add("Descricao", produto.Descricao, DbType.String);
        dbPara.Add("Imagem", produto.Imagem, DbType.String);
        dbPara.Add("Preco", produto.Preco, DbType.Decimal);
        dbPara.Add("Estoque", produto.Estoque, DbType.Int32);

        var produtoId = Task.FromResult(_dapperDal.Insert<int>("[dbo].[SP_Novo_Produto]",
                        dbPara,
                        commandType: CommandType.StoredProcedure));

        return produtoId;
    }

    public Task<int> Delete(int id)
    {
        var deleteProduto = Task.FromResult(_dapperDal.Execute($"Delete [Produtos2] where ProdutoId2 = {id}", null,
                    commandType: CommandType.Text));
        return deleteProduto;
    }   

    

    public Task<int> Update(Produto produto)
    {
        var dbPara = new DynamicParameters();
        dbPara.Add("ProdutoId2", produto.ProdutoId2);
        dbPara.Add("Nome", produto.Nome, DbType.String);
        dbPara.Add("Descricao", produto.Descricao, DbType.String);
        dbPara.Add("Imagem", produto.Imagem, DbType.String);
        dbPara.Add("Preco", produto.Preco, DbType.Decimal);
        dbPara.Add("Estoque", produto.Estoque, DbType.Int32);

        var updateProduto = Task.FromResult(_dapperDal.Update<int>("[dbo].[SP_Atualiza_Produto]",
                        dbPara,
                        commandType: CommandType.StoredProcedure));
        return updateProduto;
    }
}