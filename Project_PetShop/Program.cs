using Microsoft.EntityFrameworkCore;
using Project_PetShop.Context;
using Project_PetShop.EndPoints;
using Project_PetShop.Extensions;
using Project_PetShop.Interfaces;
using Project_PetShop.Models;

var builder = WebApplication.CreateBuilder(args);

//ConfigureService
builder.AddSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.Services.AddScoped<IProduto, Produto>();

var app = builder.Build();

//EndPoints
app.MapProdutoEndPoint();
app.MapUsuarioEndPoint();

var enviroment = app.Environment;

//Configure
app.UseExceptionHandling(enviroment)
   .UseSwaggerEndpoints()
   .UseAppCors();

app.Run();