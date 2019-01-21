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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
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
            var connString = Configuration.GetConnectionString("CallsRegistry");
            var generateData = Configuration.GetValue<bool>("GenerateData");

            var loggerFactory = new LoggerFactory(new ILoggerProvider[]
            {
                new ConsoleLoggerProvider((s, level) => level >= LogLevel.Information, true),
            });

            services.AddDbContext<CallsRegistryContext>(opt => opt.UseSqlServer(connString).UseLoggerFactory(loggerFactory));
            services.AddTransient<ICallsSummaryStorage, EntityFrameworkCallsSummaryStorage>();

            if (generateData)
                services.AddSingleton<IDataGenerator>(new PseudoRandomDataGenerator(new Random(123456)));
            else
                services.AddSingleton<IDataGenerator, NopDataGenerator>();

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

                    var dataGenerator = scope.ServiceProvider.GetService<IDataGenerator>();

                    dataGenerator.Prefill(db).ConfigureAwait(false).GetAwaiter().GetResult();
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
