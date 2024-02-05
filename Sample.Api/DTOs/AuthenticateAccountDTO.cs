using System.ComponentModel.DataAnnotations;

namespace Sample.Api.DTOs
{
    /// <summary>
    /// Represents a data for authenticate account
    /// </summary>
    public record AuthenticateAccountDTO
    {
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Username { get; set; }
        
        /// <summary>
        /// Password hash
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Password { get; set; }
    }
}
