using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CallsRegistry.AcceptanceTests
{
    public class ConfigurationProvider
    {
        public static IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }

    public class CallsRegistryHost : IDisposable
    {
        private readonly IWebHost _webHost;

        public IConfigurationRoot Configuration { get; private set; }

        public CallsRegistryHost()
        {
            Configuration = ConfigurationProvider.GetConfigurationRoot();

            _webHost = WebHost.CreateDefaultBuilder()
                .UseConfiguration(Configuration)
                .UseStartup<Startup>()
                .UseKestrel(opt => opt.ListenLocalhost(1234))
                .Build();

            _webHost.Start();
        }

        public void Dispose()
        {
            _webHost.Dispose();
        }
    }
}