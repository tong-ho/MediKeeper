using AutoMapper;
using MediKeeper.RestApi.Repositories;
using MediKeeper.WebApi.Pagination;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace MediKeeper.RestApi.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IItemGroupRepository, ItemGroupRepository>();

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IPropertyMappingService, PropertyMappingService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static IServiceCollection AddSwaggerGenCustom(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(
                    "MediKeeperOpenAPISpecifications",
                    new OpenApiInfo()
                    {
                        Title = "MediKeeper OpenAPI Specifications",
                        Version = "1",
                        Contact = new OpenApiContact
                        {
                            Email = "tongho@gmail.com",
                            Name = "Tong Ho",
                            Url = new Uri("https://www.linkedin.com/in/tong-ho-0b0a9a13b/")
                        }
                    });


                string xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                x.IncludeXmlComments(xmlCommentsFullPath);

                x.CustomSchemaIds(currentClass =>
                {
                    if (currentClass.Name.EndsWith("Dto"))
                    {
                        return currentClass.Name.Replace("Dto", string.Empty);
                    }
                    return currentClass.Name;
                });
            });

            return services;
        }
    }
}
