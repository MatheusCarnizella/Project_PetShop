using Microsoft.EntityFrameworkCore;
using Project_PetShop.Context;
using Project_PetShop.Models;

namespace Project_PetShop.EndPoints
{
    public static class ProdutoEndPoint
    {
        public static void MapProdutoEndPoint(this WebApplication app)
        {
            app.MapPost("/Produto/cadastrarProduto", async (Produto produto, AppDbContext context) =>
            {
                context.Produtos.Add(produto);
                await context.SaveChangesAsync();

                return Results.Created($"/cadastrarProduto/{produto.idProduto}", produto);
            })
                .Produces<Produto>(StatusCodes.Status201Created)
                .WithName("CriarUmNovoProduto")
                .WithTags("Produto");

            app.MapGet("/Produto/pegarProdutos", async (AppDbContext context) =>
            await context.Produtos.ToListAsync())
                .Produces<List<Produto>>(StatusCodes.Status200OK)
                .WithTags("Produto");

            app.MapGet("/Produto/pegarProduto/{id:int}", async (int id, AppDbContext context) =>
            {
                return await context.Produtos.FindAsync(id)
                    is Produto produto
                    ? Results.Ok(produto)
                    : Results.NotFound("Produto não encontrado");
            })
                .Produces<List<Produto>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Produto");

            app.MapGet("/Produto/pegarProduto/{status}", (string status, AppDbContext context) =>
            {
                var produtoStatus = context.Produtos.Where(x => x.nomeProduto
                                   .ToLower().Contains(status.ToLower()))
                                   .ToList();

                return produtoStatus.Count > 0
                        ? Results.Ok(produtoStatus)
                        : Results.NotFound(Array.Empty<Produto>());

            })
                .Produces<Produto>(StatusCodes.Status200OK)
                .WithName("ProdutoPorUmStatus")
                .WithTags("Produto");

            app.MapGet("/Produto/pegarPerodutosporPagina", async (int nPagina, int tamanhoPagina, AppDbContext context) =>
            {
                await context.Produtos
                .Skip((nPagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
            })
                .Produces<Produto>(StatusCodes.Status200OK)
                .WithName("ProdutoPorUmaPagina")
                .WithTags("Produto");

            app.MapPut("/Produto/atualizarProduto/{id:int}", async (int idproduto, Produto produto, AppDbContext context) =>
            {
                var produtoContext = context.Produtos.SingleOrDefault(a => a.idProduto == idproduto);

                if (produtoContext == null)
                {
                    return Results.NotFound("Produto não encontrado");
                }

                produtoContext.nomeProduto = produto.nomeProduto;
                produtoContext.produtoDescricao = produto.produtoDescricao;
                produtoContext.urlImagem = produto.urlImagem;
                produtoContext.Valor = produto.Valor;
                produtoContext.Quantidade = produto.Quantidade;

                await context.SaveChangesAsync();
                return Results.Ok(produtoContext);
            })
                .Produces<Produto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("AtualizarUmProduto")
                .WithTags("Produto");

            app.MapDelete("/Produtos/deletarProdutos/{id:int}", async (int id, AppDbContext context) =>
            {
                var produtoContext = await context.Produtos.FindAsync(id);

                if (produtoContext is null)
                {
                    return Results.NotFound("Produto não encontrado");
                }

                context.Produtos.Remove(produtoContext);
                await context.SaveChangesAsync();
                return Results.Ok(produtoContext);
            })
                .Produces<Produto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("DeletarUmProduto")
                .WithTags("Produto");
        }
    }
}
