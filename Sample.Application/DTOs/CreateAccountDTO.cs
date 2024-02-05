using System.ComponentModel.DataAnnotations;

namespace Sample.Application.DTOs
{
    /// <summary>
    /// Represents a data for create account
    /// </summary>
    public record CreateAccountDTO
    {
        /// <summary>
        /// Unique username
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
