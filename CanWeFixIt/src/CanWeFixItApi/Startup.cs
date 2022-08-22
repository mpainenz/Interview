using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using CanWeFixItService;
using CanWeFixItApi.Areas.Instruments.Data;
using CanWeFixItApi.Areas.MarketData.Data;
using CanWeFixItApi.GroupingConvention;

namespace CanWeFixItApi
{
    public class Startup
    {
        // Unused Configuration property.
        // public Startup(IConfiguration configuration)
        // {
        //     Configuration = configuration;
        // }
        // public IConfiguration Configuration { get; } 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Conventions.Add(new GroupingByNamespaceConvention()); // Used to group in Swagger-ui
            });

            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(o =>
            {
                o.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CanWeFixItApi", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "CanWeFixItApi", Version = "v2" });
            });
            services.AddSingleton<IDatabaseService, DatabaseService>();
            services.AddTransient<IInstrumentDataProvider, InstrumentDataProvider>();
            services.AddTransient<IMarketDataDataProvider, MarketDataDataProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CanWeFixItApi v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "CanWeFixItApi v2");
                }
                );
            } else {
                
                // Send generic errors to the client
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected error occurred. Try again later.");
                    });
                });

            }

            // Populate in-memory database with data
            var database = app.ApplicationServices.GetService(typeof(IDatabaseService)) as IDatabaseService;
            database?.SetupDatabase();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}