using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMA3A.Data;
using TMA3A.Models;

namespace TMA3A.Pages.Images
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly TMA3A.Data.TMA3AContext _context;

        public DeleteModel(TMA3A.Data.TMA3AContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }
            var image = await _context.Image.FindAsync(id);

            if (image != null)
            {
                Image = image;
                _context.Image.Remove(Image);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public IActionResult OnGetAll(string id)
        {
            List<Image> AllImages = _context.Image.ToList();
            _context.Image.RemoveRange(AllImages);
            _context.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}
