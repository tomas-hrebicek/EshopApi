namespace Sample.Api
{
    /// <summary>
    /// Represents an jwt token.
    /// </summary>
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}