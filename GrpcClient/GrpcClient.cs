using Grpc.Net.Client;
using GrpcConverterClient;

namespace GrpcClient
{
    public class Client
    {
        private readonly string _uri;

        public Client(string uri)
        {
            _uri = uri;
        }

        public async Task<Response> ConvertAsync(string number)
        {
            using var channel = GrpcChannel.ForAddress(_uri);
            var client = new Converter.ConverterClient(channel);
            var reply = await client.ConvertAsync(new CurrencyRequest { Number = number });

            return new Response
            {
                IsSuccess = reply.IsSuccess,
                ErrorMessage = reply.ErrorMessage,
                Result = reply.Result
            };
        }
    }
}
