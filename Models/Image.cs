using System.ComponentModel.DataAnnotations;

namespace TMA3A.Models
{
    public class Image
    {
        [Required]
        [Key]
        public string Location { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

    }
}
