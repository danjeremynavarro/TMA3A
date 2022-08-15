using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMA3A.Data;
using TMA3A.Models;
using Microsoft.EntityFrameworkCore;

namespace TMA3A.Pages.PartPicker
{
    public class StoreModel : PageModel
    {
        private readonly TMA3AContext _context;

        private void SetSpotlight()
        {
            var outData = new List<Order>();
            int count = Orders.Count() > 4 ? 4 : Orders.Count();
            var randomOrders = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int tries = Orders.Count();

                while (tries-- > 0)
                {
                    int randomId = GetRandomOrderId();
                    if (!randomOrders.Contains(randomId))
                    {
                        randomOrders.Add(randomId);
                        break;
                    }
                }
            }

            foreach (var randomId in randomOrders)
            {
                var o = Orders.ElementAt(randomId);
                outData.Add(o);
            }
            Spotlight = outData;
        }
        private IEnumerable<Order> Orders { get; set; }

        [BindProperty]
        public IEnumerable<Order> Spotlight { get; set; }
  
        public StoreModel(TMA3AContext context)
        {
            _context = context;
            Orders = _context.Order.Include(b => b.OrderLines)
                .ThenInclude(c => c.Product);
            SetSpotlight();
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        private int GetRandomOrderId()
        {
            if (Orders.Any())
            {
                int randomId = new Random().Next(Orders.Count());
                return randomId;
            }
            return 0;
        }

    }
}
