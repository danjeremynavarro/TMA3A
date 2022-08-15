using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMA3A.Data;
using TMA3A.Models;

namespace TMA3A.Pages.Images
{
    public class DetailsModel : PageModel
    {
        private readonly TMA3A.Data.TMA3AContext _context;

        public DetailsModel(TMA3A.Data.TMA3AContext context)
        {
            _context = context;
        }

      public Image Image { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image.FirstOrDefaultAsync(m => m.Location == id);
            if (image == null)
            {
                return NotFound();
            }
            else 
            {
                Image = image;
            }
            return Page();
        }
    }
}
