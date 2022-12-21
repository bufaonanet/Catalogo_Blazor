using Catalogo_Blazor.Server.Context;
using Catalogo_Blazor.Server.Utils;
using Catalogo_Blazor.Shared.Models;
using Catalogo_Blazor.Shared.Recursos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Blazor.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CategoriaController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriaController(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet("todas")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Categoria>>> Get()
    {
        return await _context.Categorias.AsNoTracking().ToListAsync();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<Categoria>>> Get([FromQuery] Paginacao paginacao, [FromQuery] string nome)
    {
        var queriable = _context.Categorias.AsQueryable();

        if (!string.IsNullOrEmpty(nome))
        {
            queriable = queriable.Where(x => x.Nome.ToUpper().Contains(nome.ToUpper()));
        }

        await HttpContext.InserirParametroEmPageResponse(queriable, paginacao.QuantidadePorPagina);

        return await queriable.Paginar(paginacao).ToListAsync();
    }

    [HttpGet("{id:int}", Name = "GetCategoria")]
    [AllowAnonymous]
    public async Task<ActionResult<Categoria>> Get(int id)
    {
        return Ok(await _context.Categorias.FirstOrDefaultAsync(x => x.CategoriaId == id));
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> Post(Categoria categoria)
    {
        _context.Add(categoria);
        await _context.SaveChangesAsync();
        return new CreatedAtRouteResult("GetCategoria", new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut]
    public async Task<ActionResult<Categoria>> Put(Categoria categoria)
    {
        _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Categoria>> Delete(int id)
    {
        var categoria = new Categoria { CategoriaId = id };
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return Ok(categoria);
    }

}
