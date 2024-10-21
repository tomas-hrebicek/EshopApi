using Sample.Api.Security;
using Sample.Application;
using System.ComponentModel.DataAnnotations;

namespace Sample.Api.DTOs
{
    /// <summary>
    /// Represents an authentication result data.
    /// </summary>
    public record AuthenticationResultDTO
    {
        [Required]
        public Account Account { get; set; }
        [Required]
        public Token Token { get; set; }
    }
}
