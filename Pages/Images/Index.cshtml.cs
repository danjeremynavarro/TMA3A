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
    public class IndexModel : PageModel
    {
        private readonly TMA3A.Data.TMA3AContext _context;

        public string Location;

        public IndexModel(TMA3A.Data.TMA3AContext context, IConfiguration configuration)
        {
            _context = context;
            Location = configuration.GetSection("ImageSlidesConfig")
                        .GetSection("Location").Value;
        }

        public IList<Image> Image { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //Fetch all files in the Folder (Directory).
            Image = _context.Image.ToList();
        }
    }
}
