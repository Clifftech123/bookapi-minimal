

namespace bookapi_minimal.Contracts
{
    
        public record ErrorResponse
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

    }

}