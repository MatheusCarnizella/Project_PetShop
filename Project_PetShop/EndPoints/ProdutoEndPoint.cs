using Microsoft.EntityFrameworkCore;
using Project_PetShop.Context;
using Project_PetShop.Models;
using Project_PetShop.Repository;

namespace Project_PetShop.EndPoints
{
    public static class ProdutoEndPoint
    {
        public static void MapProdutoEndPoint(this WebApplication app)
        {
            app.MapPost("/Produto/cadastrarProduto", async(Produto produto, IUnityOfWork context) =>
            {
                await context.ProdutoRepository.Add(produto);
                context.Save();

                return Results.Created($"/cadastrarProduto/{produto.idProduto}", produto);
            })
                .Produces<Produto>(StatusCodes.Status201Created)
                .WithName("CriarUmNovoProduto")
                .WithTags("Produto");

            app.MapGet("/Produto/pegarProduto", async (IUnityOfWork context) =>
            await context.ProdutoRepository.GetItem())
                .Produces<List<Produto>>(StatusCodes.Status200OK)
                .WithTags("Produto");

            app.MapGet("/Produto/pegarProduto/{id:int}", async (int id, IUnityOfWork context) =>
            {
                return await context.ProdutoRepository.GetItemById(p => p.idProduto == id)
                    is Produto produto
                    ? Results.Ok(produto)
                    : Results.NotFound("Produto não encontrado");

            })
                .Produces<List<Produto>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Produto");
            
            app.MapPut("/Produto/atualizarProduto/{id:int}", async (int id, Produto produto, IUnityOfWork context) =>
            {
                var produtoContext = context.ProdutoRepository.Update(produto);

                if (id != produto.idProduto)
                {
                    return Results.BadRequest("Produto não encontrado");
                }

                if (produtoContext == null)
                {
                    return Results.NotFound("Produto não encontrado");
                }

                context.Save();
                await produtoContext;
                return Results.Ok(produtoContext);
            })
                .Produces<Produto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("AtualizarUmProduto")
                .WithTags("Produto");

            app.MapDelete("/Produtos/deletarProdutos/{id:int}", async (int id, IUnityOfWork context) =>
            {
                var produtoContext = await context.ProdutoRepository.GetItemById(p => p.idProduto == id);

                if (produtoContext is null)
                {
                    return Results.NotFound("Produto não encontrado");
                }

                await context.ProdutoRepository.Delete(produtoContext);
                context.Save();
                return Results.Ok(produtoContext);
            })
                .Produces<Produto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("DeletarUmProduto")
                .WithTags("Produto");
        }
    }
}
