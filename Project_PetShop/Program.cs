using Microsoft.EntityFrameworkCore;
using Project_PetShop.Context;
using Project_PetShop.EndPoints;
using Project_PetShop.Extensions;

var builder = WebApplication.CreateBuilder(args);

//ConfigureService
builder.AddSwagger();
builder.AddPersistence();
builder.Services.AddCors();

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