using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMA3A.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        //[Display(Name = "Is Assembled")]
        //public bool IsAssembled { get; set; }
        public decimal Price { get; set; }

        
        public string? ItemCategoryId { get; set; }

        [ValidateNever]
        [Display(Name = "Item Category")]
        public virtual ItemCategory? ItemCategory { get; set; }

        [Display(Name = "Image Location")]
        public string? ImgPath { get; set; } 

    }
}
