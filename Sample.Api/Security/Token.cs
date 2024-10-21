namespace Sample.Api.Security
{
    /// <summary>
    /// Represents a token.
    /// </summary>
    public class Token
    {
        public string Data { get; set; }
        public DateTime Expiration { get; set; }
    }
}