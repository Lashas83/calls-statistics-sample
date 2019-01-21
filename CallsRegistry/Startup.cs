using System;
using System.Linq;
using CallsRegistry.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CallsRegistry
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("executing configure services");

            var connString = Configuration.GetConnectionString("CallsRegistry");

            services.AddDbContext<CallsRegistryContext>(opt => opt.UseSqlServer(connString));
            services.AddTransient<ICallsSummaryStorage, EntityFrameworkCallsSummaryStorage>();

            services.AddMvc(opt =>
            {
                var jsonFormatter = opt.OutputFormatters.OfType<JsonOutputFormatter>().First();
                jsonFormatter.PublicSerializerSettings.Formatting = Formatting.Indented;
                jsonFormatter.PublicSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetService<CallsRegistryContext>())
                {
                    db.Database.Migrate();
                }
            }

            app.UseStaticFiles();
            app.UseMvc(routes => { routes.MapRoute("default", "api/{controller}"); });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "wwwroot";
            });
        }
    }
}
