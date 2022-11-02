using System.ComponentModel.DataAnnotations;

namespace RaveFlixAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "user";
    }
}
