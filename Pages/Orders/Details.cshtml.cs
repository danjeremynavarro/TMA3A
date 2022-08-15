using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMA3A.Data;
using TMA3A.Models;

namespace TMA3A.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly TMA3A.Data.TMA3AContext _context;

        public DetailsModel(TMA3A.Data.TMA3AContext context)
        {
            _context = context;
        }

        public decimal SubTotal 
        { get
            {
                if (Order == null)
                {
                    return 0M;
                }
                
                decimal outData = 0M;
                foreach(var orderLine in Order.OrderLines)
                {
                    outData += (decimal)((float)orderLine.Product.Price * orderLine.Qty);
                }
                return outData;
            } 
        }

        public Order Order { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(b => b.OrderLines)
                .ThenInclude(c=>c.Product)
                .Include(b=>b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            else
            { 
                Order = order;
            }
            return Page();
        }

        public IActionResult OnPostSubmit(int? id)
        {
            _context.Order.Include(b => b.OrderLines)
                .ThenInclude(c => c.Product);
            Order? order = _context.Order.Include(b => b.OrderLines)
                .ThenInclude(c => c.Product).FirstOrDefault(d=>d.Id == id);
            if (order == null) { RedirectToPage("Orders/Index"); }
            order.Status = Order.OrderStatus.ORDERED;
            Order = order;
            _context.SaveChanges();
            return Page();
        }
    }
}
