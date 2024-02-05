using System.ComponentModel.DataAnnotations;

namespace Sample.Api.DTOs
{
    /// <summary>
    /// Represents an authentication result data.
    /// </summary>
    public record AuthenticationResultDTO
    {
        /// <summary>
        /// Token
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// Token validity
        /// </summary>
        [Required]
        public DateTime Expiration { get; set; }
    }
}
