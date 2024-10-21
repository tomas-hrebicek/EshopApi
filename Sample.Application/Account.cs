using Sample.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sample.Application
{
    public class Account
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Username { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}