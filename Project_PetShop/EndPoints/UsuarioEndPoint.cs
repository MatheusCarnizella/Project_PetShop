using Microsoft.EntityFrameworkCore;
using Project_PetShop.Context;
using Project_PetShop.Models;
using Project_PetShop.Repository;

namespace Project_PetShop.EndPoints
{
    public static class UsuarioEndPoint
    {
        public static void MapUsuarioEndPoint(this WebApplication app)
        {
            app.MapPost("/Usuario/cadastrarUsuario", async (Usuario usuario, IUnityOfWork context) =>
            {
                context.UsuarioRepository.Add(usuario);
                await context.Save();

                return Results.Created($"/cadastrarUsuario/{usuario.Id}", usuario);
            })
                .WithTags("Usuario");

            app.MapGet("/Usuario/pegarUsuarios", async (IUnityOfWork context) =>
            await context.UsuarioRepository.GetItem().ToListAsync())
                .WithTags("Usuario");

            app.MapGet("/Usuario/pegarUsuario/{id:int}", async (int id, IUnityOfWork context) =>
            {
                return await context.UsuarioRepository.GetItemById(a => a.Id == id)
                    is Usuario usuario
                     ? Results.Ok(usuario) 
                     :Results.NotFound();
            })
                .WithTags("Usuario");

            app.MapGet("/Usuario/pegarUsuario/{username}", (string username, AppDbContext context) =>
            {
                var usuarioNome = context.Usuarios.Where(x => x.userName
                                   .ToLower().Contains(username.ToLower()))
                                   .ToList();

                return usuarioNome.Count > 0
                        ? Results.Ok(usuarioNome)
                        : Results.NotFound(Array.Empty<Produto>());

            })
                .WithTags("Usuario");

            app.MapPut("/Usuario/atualizarUsuario/{id:int}", async (int id, Usuario usuario, IUnityOfWork context) =>
            {
                if (usuario.Id != id)
                {
                    return Results.BadRequest("Id não encontrado");
                }

                var usuarioContext = context.UsuarioRepository.Update(usuario);

                if (usuarioContext is null)
                {
                    return Results.NotFound("Id não encontrado");
                }

                await context.Save();
                return Results.Ok(usuarioContext);
            })
                .WithTags("Usuario");

            app.MapDelete("/Usuario/deletarUsuario/{id:int}", async (int id, IUnityOfWork context) => 
            {
                var usuario = await context.UsuarioRepository.GetItemById(a => a.Id == id);

                if (usuario is null)
                {
                    return Results.NotFound("Usuario não encontrado");
                }

                context.UsuarioRepository.Delete(usuario);
                await context.Save();
                return Results.NoContent();
            })
                .WithTags("Usuario");
        }
    }
}
