using Microsoft.EntityFrameworkCore;
using Project_PetShop.Context;
using Project_PetShop.Models;

namespace Project_PetShop.EndPoints
{
    public static class UsuarioEndPoint
    {
        public static void MapUsuarioEndPoint(this WebApplication app)
        {
            app.MapPost("/Usuario/cadastrarUsuario", async (Usuario usuario, AppDbContext context) =>
            {
                context.Usuarios.Add(usuario);
                await context.SaveChangesAsync();

                return Results.Created($"/cadastrarUsuario/{usuario.Id}", usuario);
            })
                .WithTags("Usuario");

            app.MapGet("/Usuario/pegarUsuarios", async (AppDbContext context) =>
            await context.Usuarios.ToListAsync())
                .WithTags("Usuario");

            app.MapGet("/Usuario/pegarUsuario/{id:int}", async (int id, AppDbContext context) =>
            {
                return await context.Usuarios.FindAsync(id)
                    is Usuario usuario
                            ? Results.Ok(usuario) :
                            Results.NotFound();
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

            app.MapPut("/Usuario/atualizarUsuario/{id:int}", async (int id, Usuario usuario, AppDbContext context) =>
            {
                if (usuario.Id != id)
                {
                    return Results.BadRequest("Id não encontrado");
                }
                var usuarioContext = await context.Usuarios.FindAsync(id);
                if (usuarioContext is null)
                {
                    return Results.NotFound("Id não encontrado");
                }
                usuarioContext.Email = usuario.Email;
                usuarioContext.Password = usuario.Password;
                usuarioContext.userName = usuario.userName;
                await context.SaveChangesAsync();
                return Results.Ok(usuarioContext);
            })
                .WithTags("Usuario");

            app.MapDelete("/Usuario/deletarUsuario/{id:int}", async (int id, AppDbContext context) => 
            {
                var usuario = await context.Usuarios.FindAsync(id);

                if (usuario is null)
                {
                    return Results.NotFound("Usuario não encontrado");
                }

                context.Usuarios.Remove(usuario);
                await context.SaveChangesAsync();
                return Results.NoContent();
            })
                .WithTags("Usuario");
        }
    }
}
