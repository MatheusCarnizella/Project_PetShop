using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Project_PetShop.Context;

namespace Project_PetShop.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwagger();
            return builder;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project_PetShop", Version = "v1" });
               
            });
            return services;
        }        

        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));           

            return builder;
        }
    }
}
