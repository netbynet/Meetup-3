using Colorful;
using Greet.V2;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Drawing;
using System.Threading;

namespace gRPC.Client
{
    internal class Program
    {
        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            FigletFont font = FigletFont.Load(@"Fonts\big.flf");
            Figlet figlet = new Figlet(font);
            Colorful.Console.WriteLine(figlet.ToAscii("gRPC Client"), ColorTranslator.FromHtml("#ADFF2F"));

            //Colorful.Console.WriteLine("Test", ColorTranslator.FromHtml("#ADFF2F"));

            var channel = GrpcChannel.ForAddress(@"https://localhost:5001");

            var client = new Greeter.GreeterClient(channel);

            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(10));

            using (var call = client.SayHello2(new HelloRequest { Name = "Mukuch" }, cancellationToken: cts.Token))
            {
                await foreach (var msg in call.ResponseStream.ReadAllAsync())
                {
                    Colorful.Console.WriteLine(msg, ColorTranslator.FromHtml("#ADFF2F"));
                }
            }
        }
    }
}