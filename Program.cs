using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Galleass {
    public class Program {
        public static void Main (string[] args) {
            CreateWebHostBuilder (args).Build ().Run ();
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .ConfigureLogging ((context, logging) => {
                var env = context.HostingEnvironment;
                var config = context.Configuration.GetSection ("Logging");
                // ...
                logging.AddConfiguration (config);
                logging.AddConsole ();
                // ...
                logging.AddFilter ("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            })
            .UseStartup<Startup> ();
    }
}