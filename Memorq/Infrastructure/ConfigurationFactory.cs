using Microsoft.Extensions.Configuration;
using System.IO;

namespace Memorq.Infrastructure
{
    public static class ConfigurationFactory
    {
        public static IConfiguration GetConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
    }
}
