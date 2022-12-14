using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMA3A.Data;
using TMA3A.Models;

namespace TMA3A.Core
{
    public class InitData 
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {

            var context = new TMA3AContext(serviceProvider.GetRequiredService<DbContextOptions<TMA3AContext>>());
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.ItemCategory.Any())
            {
                context.ItemCategory.AddRange(
                        new ItemCategory { Id = "Motherboard" },
                        new ItemCategory { Id = "CPU" },
                        new ItemCategory { Id = "RAM" },
                        new ItemCategory { Id = "Graphic Card" },
                        new ItemCategory { Id = "OS" },
                        new ItemCategory { Id = "Sound Card" },
                        new ItemCategory { Id = "Miscellaneous" }
                    );
            }
            context.SaveChanges();
            string[] roles = new string[] {"Admin","User"};
            var rolesToAdd = new List<IdentityRole>();
            foreach (var role in roles)
            {
                var rQuery = from r in context.Roles
                                where r.Name.Equals(role)
                                select r;
                var rResult = rQuery.FirstOrDefault();
                if (rResult == null)
                {
                    var rAdd = new IdentityRole(role);
                    var result = await _roleManager.CreateAsync(rAdd);
                }
            }

            context.SaveChanges();
        }
    }
}
