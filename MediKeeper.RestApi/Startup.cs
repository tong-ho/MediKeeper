using MediKeeper.RestApi.Entities;
using MediKeeper.RestApi.Extensions;
using MediKeeper.RestApi.Pagination;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MediKeeper.RestApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
            //Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string[] allowedCorsOrigins = Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder
                        .AllowAnyOrigin()
                        .WithExposedHeaders(nameof(PaginationMetadata))
                        .AllowAnyHeader()
                        .AllowAnyMethod());
                        //.AllowCredentials());
            });

            string connectionString = Configuration["ConnectionStrings:MediKeeper"];
            services.AddDbContext<MediKeeperContext>(x =>
                x.UseSqlServer(connectionString));

            //Repositories
            services.AddRepositories();

            //Services
            services.AddCustomServices();

            services.AddSwaggerGenCustom();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            //app.UseHttpsRedirection();


            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.InjectStylesheet("/swagger/swagger-custom.css");
                x.SwaggerEndpoint("/swagger/MediKeeperOpenAPISpecifications/swagger.json", "MediKeeper API");
                x.RoutePrefix = String.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
