using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMA3A.Data;
namespace TMA3A.Pages.Images
{
    public class ShowModel : PageModel
    {
        private readonly TMA3A.Data.TMA3AContext _context;
        public string ImageFolderPath=String.Empty;
        public record ImageRecord
        {
            public string FullPath { get; init; }
            public string Name { get; init; }
            public string Description { get; init; }
        };

        public ShowModel(TMA3AContext context, IConfiguration configuration)
        {
            _context = context;
            ImageFolderPath = configuration.GetSection("ImageSlidesConfig")
                       .GetSection("Location").Value;
        }

        public IList<ImageRecord> Images { get; set; } = new List<ImageRecord>();

        public void OnGet()
        {
            var images = _context.Image.ToList();
            foreach (var image in images)
            {
                string fullPath = $"{ImageFolderPath}/{image.Location}";
                Images.Add(new ImageRecord
                {
                    FullPath = fullPath,
                    Name = image.Name,
                    Description = image.Description
                });
                
            }
        }
    }
}
