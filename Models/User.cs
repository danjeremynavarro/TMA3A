using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TMA3A.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }      
        public string? PostalCode { get; set; }

        public string? Country { get; set; } 

        [ValidateNever]
        public ICollection<Order> Orders { get; set; }

    }
}

