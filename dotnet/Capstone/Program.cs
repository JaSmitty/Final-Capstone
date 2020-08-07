using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
        
        public static void Main(string[] args)
        {
            //FinnHubDataLoop dataLoop = new FinnHubDataLoop();
            //Thread newThread = new Thread(
            //new ThreadStart(dataLoop.Run));

            //newThread.Start();

            //CompanySqlDAO companySql = new CompanySqlDAO("Server=.\\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;");
            //FinnHubDataLoop dataLoop = new FinnHubDataLoop();
            


            CreateWebHostBuilder(args).Build().Run();

            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
