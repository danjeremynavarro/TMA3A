using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMA3A.Models
{
    public class OrderLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        public float Qty { get; set; }

        public float Price { get; set; }

        public Boolean IsSameItemAndPrice(Product product) {
            return product.Id == Product.Id && product.Price == Product.Price;
        }
    }
}


