using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMA3A.Data;
using TMA3A.Models;

namespace TMA3A.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly TMA3A.Data.TMA3AContext _context;

        public IndexModel(TMA3A.Data.TMA3AContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        [BindProperty(SupportsGet =true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var products = from p in _context.Product
                           select p;
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(p => p.Title.Contains(SearchString));
                
            }

            Product = await products.ToListAsync();
        }
    }
}
