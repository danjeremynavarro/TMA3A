using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMA3A.Data;
using TMA3A.Models;

namespace TMA3A.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly TMA3A.Data.TMA3AContext _context;
        private readonly UserManager<User> _userManager;

        public User? CurrentUser { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SearchString { get; set; } = null;

        public IndexModel(TMA3A.Data.TMA3AContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var user = _userManager.GetUserId(User);
            CurrentUser = _context.User.Find(user);

            IList<Order> allOrder = await _context.Order
                .Include(o => o.User)
                .Include(o => o.OrderLines)
                .ThenInclude(b => b.Product).ToListAsync();
            if (_context.Order != null && CurrentUser != null)
            {
                var oq = from o in allOrder
                         where o.UserId == CurrentUser.Id
                         select o;
                Order = oq.ToList();
            } else if(SearchString is not null)
            {
                var oq = from o in allOrder
                         where o.Id == SearchString && o.UserId is null
                         select o;
                Order = oq.ToList();
                if (Order.Count == 0)
                {
                    ViewData["DoesNotExist"] = $"Order# {SearchString} does not exist or the order is for a registered user. Ensure you are logged in with the correct account.";
                    return Page();
                }
            }
            return Page();
        }
    }
}
