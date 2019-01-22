using System;
using CallsRegistry.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.Configure<ConsoleLoggerOptions>(Configuration);
            services.AddOptions<ConsoleLoggerOptions>();

            services.AddDbContext<CallsRegistryContext>(opt => opt
                .UseSqlServer(connString)
            );
            services.AddTransient<ICallsSummaryStorage, EntityFrameworkCallsSummaryStorage>();

            if (generateData)
                services.AddSingleton<IDataGenerator>(new PseudoRandomDataGenerator(new Random(123456)));
            else
                services.AddSingleton<IDataGenerator, NopDataGenerator>();

            services.AddMvcCore()
                .AddJsonFormatters(settings =>
                {
                    settings.Formatting = Formatting.Indented;
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddCors();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeDatabase(app);

            app.UseStaticFiles();
            app.UseMvc(routes => { routes.MapRoute("default", "api/{controller}"); });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "wwwroot";
            });
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetService<CallsRegistryContext>())
                {
                    db.Database.Migrate();

                    var dataGenerator = scope.ServiceProvider.GetService<IDataGenerator>();

                    dataGenerator.Prefill(db).ConfigureAwait(false).GetAwaiter().GetResult();
                }
            }
        }
    }
}
