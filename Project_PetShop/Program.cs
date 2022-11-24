using Project_PetShop.EndPoints;
using Project_PetShop.Extensions;
using Project_PetShop.Repository;
using Project_PetShop.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

//ConfigureService
builder.AddSwagger();
builder.AddPersistence();
builder.Services.AddCors();

builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();

var app = builder.Build();

//EndPoints
app.MapProdutoEndPoint();
app.MapUsuarioEndPoint();

var enviroment = app.Environment;

//Configure
app.UseExceptionHandling(enviroment)
   .UseSwaggerEndpoints()
   .UseAppCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();