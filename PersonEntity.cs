using System;
using System.ComponentModel.DataAnnotations;

namespace University.Infrastructure.Models
{
    public abstract class PersonEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}
