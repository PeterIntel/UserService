using System;
using System.ComponentModel.DataAnnotations;

namespace User.Service.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
