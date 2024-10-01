
namespace bookapi_minimal.Contracts
{
  
    public record CreateBookRequest
    { 
    
        public string Title { get; init; }
        public string Author { get; init; }
        public string Description { get; init; }
        public string Category { get; init; }
        public string Language { get; init; }
        public int TotalPages { get; init; }
    }
}