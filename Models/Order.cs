using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMA3A.Models
{
    public class Order
    {
        public enum OrderStatus
        {
            CART,
            ORDERED
        }

        [Display(Name = "Order #")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Customer")]
        public string? UserId { get; set; }
        public virtual User? User { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.CART;
        [ValidateNever]
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        [Display(Name = "Street")]
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }
        public string? Country { get; set; }    
    }
}
