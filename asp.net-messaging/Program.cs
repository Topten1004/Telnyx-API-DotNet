using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using dotenv.net;


namespace asp.net_messaging
{
    public class Program
    {
        public static void Main(string[] args)
        {
          DotEnv.Config();
          CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    string Port = Environment.GetEnvironmentVariable("TELNYX_APP_PORT");
                    webBuilder.UseStartup<Startup>();
                    string[] urls = new string[] {$"http://localhost:{Port}", "https://localhost:8001"};
                    webBuilder.UseUrls(urls);
                });
    }
}
