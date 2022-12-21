using Microsoft.EntityFrameworkCore;

namespace Catalogo_Blazor.Server.Utils;

public static class HttpContextExtensions
{
    public static async Task InserirParametroEmPageResponse<T>
        (this HttpContext context, IQueryable<T> queryable, int quantidadeTotalRegistrosAExibir)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        double quatidadeTotalRegistros = await queryable.CountAsync();
        double totalPaginas = Math.Ceiling(quatidadeTotalRegistros / quantidadeTotalRegistrosAExibir);

        //Salvando as informações no header do response
        context.Response.Headers.Add("quatidadeTotalRegistros", quatidadeTotalRegistros.ToString());
        context.Response.Headers.Add("totalPaginas", totalPaginas.ToString());
    }
}