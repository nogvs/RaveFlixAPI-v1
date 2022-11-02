using System.ComponentModel.DataAnnotations;

namespace RaveFlixAPI.ViewModel
{
    public class CreateGenreViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
