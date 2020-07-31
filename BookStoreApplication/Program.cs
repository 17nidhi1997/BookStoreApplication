using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BookStoreRepositoryLayer;
using BookStoreRepositoryLayer.RepositoryImplemantation;
using BookStoreRepositoryLayer.DBConnection;


namespace BookStoreApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
           // OracleDBConnection conn = new OracleDBConnection();
           // conn.Connection();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
