namespace HQ78.Recipe.Api.Models
{
    public class PaginationRequest
    {
        public string? Cursor { get; set; }

        public int Limit { get; set; }
    }
}
