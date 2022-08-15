using Microsoft.EntityFrameworkCore;
using TMA3A.Data;
using TMA3A.Models;

namespace TMA3A.Core
{
    /**
     * Manages the images table and the automated creation of new entries.
     */
    public class ReadImages
    {
        /**
         * Adds a file to image database. File must be in wwwroot/imgSlides.
         * Will not overwrite if file exist;
         */
        private static void AddFile(string filePath, TMA3AContext context)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if(context.Image.Find(fileInfo.Name) != null)
            {
                return;
            }

            context.Add(
                    new Image
                    {
                        Location = fileInfo.Name
                    }
                );;
        }

        public static void Initialize(
            Object? environment,     
            TMA3AContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            var e = environment as IWebHostEnvironment;
            string[] files = Directory.GetFiles(Path.Combine(e.WebRootPath, "imgSlides"));
            foreach(string file in files)
            {
                AddFile(file, context);
            }
            context.SaveChanges();
        }
    }
}
