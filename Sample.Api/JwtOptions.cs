namespace Sample.Api
{
    /// <summary>
    /// Represents jwt configuration
    /// </summary>
    public sealed class JwtOptions
    {
        public const string Key = "Jwt";

        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenValidityMinutes { get; set; } = 60;   
    }
}
