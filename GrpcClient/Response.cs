namespace GrpcClient
{
    public class Response
    {
        public string? ErrorMessage { get; set; }
        public string? Result { get; set; }
        public bool IsSuccess { get; set; }
    }
}