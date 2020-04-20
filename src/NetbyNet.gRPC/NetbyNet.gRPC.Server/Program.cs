using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Colorful;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace gRPC.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FigletFont font = FigletFont.Load(@"Fonts\big.flf");
            Figlet figlet = new Figlet(font);
            Colorful.Console.WriteLine(figlet.ToAscii("gRPC Server"), ColorTranslator.FromHtml("#8AFFEF"));

            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
