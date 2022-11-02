using System.ComponentModel.DataAnnotations;

namespace RaveFlixAPI.ViewModel
{
    public class CreateFestivalViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Country { get; set; } = null!;
    }
}
