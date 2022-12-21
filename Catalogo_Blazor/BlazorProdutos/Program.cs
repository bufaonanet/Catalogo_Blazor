using BlazorProdutos.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;


// Add services to the container.
services.AddRazorPages();
services.AddServerSideBlazor();

services.AddScoped<IDapperDAL, DapperDAL>();
services.AddScoped<IProdutoService, ProdutoService>();

var sqlConnectionConfiguration = new SqlConnectionConfiguration(configuration.GetConnectionString("SqlDbContext"));
services.AddSingleton(sqlConnectionConfiguration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
