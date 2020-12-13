using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

[assembly: FunctionsStartup(typeof(AzninjaTodoFn.Helpers.Startup))]
namespace AzninjaTodoFn.Helpers
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder  builder)
        {
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddFilter(level => true);
            });

            builder.Services.AddHttpContextAccessor();

            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddSingleton((s) =>
            {
                MongoClient client = new MongoClient(config[Constants.CONNECTION_STRING]);

                return client;
            });
        }
    }
}