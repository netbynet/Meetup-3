using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Greet
{
    public class GreeterServiceV1 : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterServiceV1> _logger;
        public GreeterServiceV1(ILogger<GreeterServiceV1> logger)
        {
            _logger = logger;
        }

        public override async Task SayHello(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            _logger.LogInformation($"Sending hello to {request.Name}");

            for (int i = 0; i < 100; i++)
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    break;
                }
                var message = $"Hello {Guid.NewGuid()}";
                _logger.LogInformation($"Sending {message}");
                await responseStream.WriteAsync(new HelloReply { Message = message });
                await Task.Delay(500);
            }
        }
    }
}
