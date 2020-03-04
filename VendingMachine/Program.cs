using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;

namespace VendingMachine
{
    class Program
    {
        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();

            Console.WriteLine("Start");
            var snack = config.GetSection("Products").GetSection("Snacks:0").Get<Snack>();
            Console.WriteLine(snack.Name);
            Console.WriteLine("Stop");
            

            services.AddSingleton(config);

            services.AddTransient<IVendingMachine, VendingMachine>();

            services.AddTransient<ConsoleApplication>();

            return services;
        }

        static void Main(string[] args)
        {

            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<ConsoleApplication>().Run();

            
        }
    }
}
