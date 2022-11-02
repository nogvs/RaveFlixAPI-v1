using System.ComponentModel.DataAnnotations;

namespace RaveFlixAPI.Models
{
    public class Festival
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;

    }
}
