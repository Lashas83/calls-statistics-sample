using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CallsRegistry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            // TODO: here we probably should create some random data...
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}
