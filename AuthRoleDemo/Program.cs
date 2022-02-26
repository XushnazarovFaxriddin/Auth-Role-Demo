using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthRoleDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("15550107"));
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(x =>
                {
                    x.UseStartup<Startup>();
                    x.UseUrls("http://localhost:4000");
                });
    }
}
