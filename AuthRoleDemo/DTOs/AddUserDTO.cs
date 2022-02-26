using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthRoleDemo.DTOs
{
    public class AddUserDTO
    {
        [Required, MinLength(3), MaxLength(30)]
        public string FirstName { get; set; }

        [Required, MinLength(3), MaxLength(30)]
        public string LastName { get; set; }

        [Required, MinLength(4), MaxLength(16)]
        public string Username { get; set; }

        [Required, MinLength(4), MaxLength(16)]
        public string Password { get; set; }
    }
}
