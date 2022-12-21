using Catalogo_Blazor.Server.Context;
using Catalogo_Blazor.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Blazor.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("categorias/{id:int}")]
    public async Task<ActionResult<List<Produto>>> GetProdutosCategorias(int id)
    {
        return await _context.Produtos.Where(p => p.CategoriaId == id).ToListAsync();
    }

    [HttpGet]
    public async Task<ActionResult<List<Produto>>> Get()
    {
        var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
        return produtos;
    }

    [HttpGet("{id:int}", Name = "GetProduto")]
    public async Task<ActionResult<Produto>> Get(int id)
    {
        return Ok(await _context.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id));
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> Post(Produto produto)
    {
        _context.Add(produto);
        await _context.SaveChangesAsync();
        return new CreatedAtRouteResult("GetProduto", new { id = produto.ProdutoId }, produto);
    }

    [HttpPut]
    public async Task<ActionResult<Produto>> Put(Produto produto)
    {
        _context.Entry(produto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Produto>> Delete(int id)
    {
        var produto = new Produto { ProdutoId = id };
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return Ok(produto);
    }
}
