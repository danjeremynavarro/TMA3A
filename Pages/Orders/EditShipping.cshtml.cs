using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMA3A.Data;
using TMA3A.Models;

namespace TMA3A.Pages.Orders
{
    public class EditShippingModel : PageModel
    {
        private readonly TMA3A.Data.TMA3AContext _context;

        [BindProperty]
        public InputModel Input { get; set; }
        public EditShippingModel(TMA3AContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(int? orderId)
        {
            Order? order = _context.Order.Find(orderId);
            if (order == null)
            {
                return RedirectToPage("/PartPicker/Index");
            }
            Input = new InputModel
            {
                orderId = order.Id,
                StreetAddress = order.StreetAddress,
                City = order.City,
                PostalCode = order.PostalCode,
                Country = order.Country,
            };
            return Page();
        }
        public IActionResult OnPost()
        {
            Order? order = _context.Order.Find(Input.orderId);
            if (order == null || order.Status == Order.OrderStatus.ORDERED)
            {
                return Redirect("/");
            } else
            {

            }
            order.StreetAddress = Input.StreetAddress;
            order.City = Input.City;
            order.State = Input.State;
            order.PostalCode = Input.PostalCode;
            order.Country = Input.Country;
            _context.SaveChanges();
            return RedirectToPage("/Orders/Details", new {id=order.Id});
        }
        public class InputModel
        {
            public int? orderId { get; set; }
            public string? StreetAddress { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public string? PostalCode { get; set; }
            public string? Country { get; set; }
        }
    }
}
