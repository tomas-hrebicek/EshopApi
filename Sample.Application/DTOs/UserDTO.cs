using System.ComponentModel.DataAnnotations;

namespace Sample.Application.DTOs
{
    /// <summary>
    /// Represents an user
    /// </summary>
    public record UserDTO
    {
        /// <summary>
        /// User unique identificator
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Unique username
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Username { get; set; }

        /// <summary>
        /// True, if user is active
        /// </summary>
        [Required]
        public bool Active { get; set; }
    }
}
