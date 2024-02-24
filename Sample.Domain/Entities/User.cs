using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample.Domain.Entities
{
    /// <summary>
    /// Represents an user
    /// </summary>
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Username { get; set; }
        [Required]
        [MaxLength(128)]
        public string Salt { get; set; }
        [Required]
        [MaxLength(128)]
        public string Password { get; set; }
        [Required]
        public bool Active { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
