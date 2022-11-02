using System.ComponentModel.DataAnnotations;

namespace RaveFlixAPI.ViewModel
{
    public class CreateUserViewModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
