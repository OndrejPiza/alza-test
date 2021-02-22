using System.Data;
using System.Linq;
using System.Reflection;
using AlzaTest.Services.Products.API.Database;
using AlzaTest.Services.Products.API.Tasks;
using AlzaTest.Services.Utils.Extensions;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace AlzaTest.Services.Products.API
{
    public class Startup
    {
        private const string DatabaseConnectionStringConfigurationName = "Database";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Versioning
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            #endregion

            #region Migrations
            var databaseConnection = Configuration.GetConnectionString("Database");
            services.AddFluentMigratorCore()
                .ConfigureRunner(c => c
                    .AddSqlServer()
                    .WithGlobalConnectionString(databaseConnection)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.All());
            
            services.AddStartupTask<MigrationTask>();
            #endregion
            
            #region Swagger
            services.AddSwaggerGen(c =>
            {
               
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API - V1", Version = "1.0.0." });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Product API - V2", Version = "2.0.0" });
                
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
            #endregion

            #region API Services
            services.AddTransient<IDbConnection>(provider => new SqlConnection(Configuration.GetConnectionString(DatabaseConnectionStringConfigurationName)));
            services.AddScoped<IDatabaseProxy, DatabaseProxy>();
            #endregion
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Swagger
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
                    config.SwaggerEndpoint("/swagger/v2/swagger.json", "Product API V2");
                });
            }
            #endregion

            app.UseRouting();

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
