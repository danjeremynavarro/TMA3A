using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMA3A.Data;
using TMA3A.Models;

namespace TMA3A.Pages.Images
{
    public class EditModel : PageModel
    {
        private readonly TMA3A.Data.TMA3AContext _context;

        public string Location;

        public EditModel(TMA3A.Data.TMA3AContext context, IConfiguration configuration)
        {
            _context = context;
            Location = configuration.GetSection("ImageSlidesConfig")
            .GetSection("Location").Value;
        }

        [BindProperty]
        public Image Image { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image =  await _context.Image.FirstOrDefaultAsync(m => m.Location == id);
            if (image == null)
            {
                return NotFound();
            }
            Image = image;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(Image.Location))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ImageExists(string id)
        {
          return (_context.Image?.Any(e => e.Location == id)).GetValueOrDefault();
        }
    }
}
