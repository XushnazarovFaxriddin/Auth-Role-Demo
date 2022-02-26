using AuthRoleDemo.Entites;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuthRoleDemo.Models
{
    public class UserValid
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,MinLength(3), MaxLength(30)]
        public string FirstName { get; set; }

        [Required,MinLength(3), MaxLength(30)]
        public string LastName { get; set; }

        [Required,MinLength(4),MaxLength(16)]
        public string Username { get; set; }

        [DefaultValue(Role.User)]
        public Role Role { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public Role Role { get; set; }


        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
