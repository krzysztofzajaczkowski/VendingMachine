using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class Program
    {

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

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
