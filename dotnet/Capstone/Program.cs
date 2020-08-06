using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.DataLoops;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Capstone
{
    public class Program
    {
        //public string connectionString = "Server=.\\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;";
        public static void Main(string[] args)
        {
            CompanySqlDAO companySql = new CompanySqlDAO("Server=.\\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;");
            FinnHubDataLoop dataLoop = new FinnHubDataLoop(companySql);
            dataLoop.Run();
            CreateWebHostBuilder(args).Build().Run();

            //CompanySqlDAO companySql = new CompanySqlDAO("Server=.\\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;");
            //FinnHubDataLoop dataLoop = new FinnHubDataLoop(companySql);
            //dataLoop.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
