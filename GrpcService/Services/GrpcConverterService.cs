using Grpc.Core;

namespace GrpcService.Services
{
    public class GrpcConverterService : Converter.ConverterBase
    {
        public override async Task<CurrencyReply> Convert(CurrencyRequest request, ServerCallContext context)
        {
            var currency = request.Number.Replace(" ", string.Empty);
            var validation = CurrencyHelper.Validate(currency);

            if (validation)
            {
                var result = CurrencyHelper.Convert(currency);
                return await Task.FromResult(new CurrencyReply() { IsSuccess = true, Result = result });
            }
            return await Task.FromResult(new CurrencyReply()
            {
                IsSuccess = false,
                ErrorMessage = $"{currency} should be in format 111111111,11"
            });
        }
    }
}