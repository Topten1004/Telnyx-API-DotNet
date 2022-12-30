using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using dotenv.net;
using Telnyx;

namespace asp_net_IVR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotEnv.Config();
            string TELNYX_API_KEY = System.Environment.GetEnvironmentVariable("TELNYX_API_KEY");
            TelnyxConfiguration.SetApiKey(TELNYX_API_KEY);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    string Port = Environment.GetEnvironmentVariable("PORT");
                    webBuilder.UseStartup<Startup>();
                    string[] urls = new string[] { $"http://localhost:{Port}", "https://localhost:8001" };
                    webBuilder.UseUrls(urls);
                });
    }
}
