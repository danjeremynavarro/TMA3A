using System.ComponentModel.DataAnnotations;

namespace TMA3A.Models
{
    public class ItemCategory
    {
        [Key]
        [Display(Name = "Item Category")]
        public string Id { get; set; }
    }
}
