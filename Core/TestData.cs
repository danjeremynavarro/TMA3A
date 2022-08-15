using Microsoft.EntityFrameworkCore;
using TMA3A.Models;
using TMA3A.Data;
using System.Collections;

namespace TMA3A.Core
{
    public class TestData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TMA3AContext(serviceProvider.GetRequiredService<DbContextOptions<TMA3AContext>>()))
            {
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

                if (!context.ItemCategory.Any())
                {
                    context.Product.AddRange(
                            new Product
                            {
                                Title = "RazorMX2 Motherboard",
                                Description = "2 PCIE Slots",
                                Price = 10,
                                ItemCategory = context.ItemCategory.Find("Motherboard"),
                                ImgPath = "/img/motherboard.png" 
                            },
                            new Product
                            {
                                Title = "RazorMX3 Motherboard",
                                Description = "3 PCIE Slots",
                                Price = 50,
                                ItemCategory = context.ItemCategory.Find("Motherboard"),
                                ImgPath = "/img/motherboard.png"
                            },
                            new Product
                            {
                                Title = "RazorMX4 Motherboard",
                                Description = "4 PCIE Slots",
                                Price = 60,
                                ItemCategory = context.ItemCategory.Find("Motherboard"),
                                ImgPath = "/img/motherboard.png"
                            },
                            new Product
                            {
                                Title = "RazorCPX1 CPU",
                                Description = "Intel 10th Gen 2xCore 3.8Ghz",
                                Price = 410,
                                ItemCategory = context.ItemCategory.Find("CPU"),
                                ImgPath = "/img/cpu.png"
                            },
                            new Product
                            {
                                Title = "RazorCPX2 CPU",
                                Description = "Intel 10th Gen 4xCore 3.8Ghz",
                                Price = 568.12M,
                                ItemCategory = context.ItemCategory.Find("CPU"),
                                ImgPath = "/img/cpu.png"
                            },
                            new Product
                            {
                                Title = "RazorRPX1 Ram",
                                Description = "8GB Ram DDR3",
                                Price = 82.2M,
                                ItemCategory = context.ItemCategory.Find("RAM"),
                                ImgPath = "/img/ram.png"
                            },
                            new Product
                            {
                                Title = "RazorRPX2 Ram",
                                Description = "16GB Ram DDR3",
                                Price = 568.52M,
                                ItemCategory = context.ItemCategory.Find("RAM"),
                                ImgPath = "/img/ram.png"
                            },
                            new Product
                            {
                                Title = "RazorRPX3 Ram",
                                Description = "8GB Ram DDR4",
                                Price = 575.82M,
                                ItemCategory = context.ItemCategory.Find("RAM"),
                                ImgPath = "/img/ram.png"
                            },
                            new Product
                            {
                                Title = "RazorRPX4 Ram",
                                Description = "16GB Ram DDR4",
                                Price = 600.12M,
                                ItemCategory = context.ItemCategory.Find("RAM"),
                                ImgPath = "/img/ram.png"
                            },
                            new Product
                            {
                                Title = "GeForce RTX 2060",
                                Description = "8GB VRAM",
                                Price = 1204.62M,
                                ItemCategory = context.ItemCategory.Find("Graphic Card"),
                                ImgPath = "/img/graphic.png"
                            },
                            new Product
                            {
                                Title = "GeForce RTX 3060",
                                Description = "12GB VRAM",
                                Price = 1001.11M,
                                ItemCategory = context.ItemCategory.Find("Graphic Card"),
                                ImgPath = "/img/graphic.png"
                            },
                            new Product
                            {
                                Title = "Razor Headset",
                                Description = "Headset",
                                Price = 60.78M,
                                ItemCategory = context.ItemCategory.Find("Miscellaneous"),
                                ImgPath = "/img/misc.png"
                            },
                            new Product
                            {
                                Title = "Razor Keyboard",
                                Description = "Keyboard",
                                Price = 60.78M,
                                ItemCategory = context.ItemCategory.Find("Miscellaneous"),
                                ImgPath = "/img/misc.png"
                            }
                            ,
                            new Product
                            {
                                Title = "Razor Mouse",
                                Description = "Mouse",
                                Price = 60.78M,
                                ItemCategory = context.ItemCategory.Find("Miscellaneous"),
                                ImgPath = "/img/misc.png"
                            },
                            new Product
                            {
                                Title = "Windows 10",
                                Description = "Windows 10 Operating System",
                                Price = 80.11M,
                                ItemCategory = context.ItemCategory.Find("OS"),
                                ImgPath = "/img/os.png"
                            },
                            new Product
                            {
                                Title = "Windows 7",
                                Description = "Windows  Operating System",
                                Price = 40.11M,
                                ItemCategory = context.ItemCategory.Find("OS"),
                                ImgPath = "/img/os.png"
                            },
                            new Product
                            {
                                Title = "SoundX 10",
                                Description = "SoundX 10 Sound Card",
                                Price = 40.11M,
                                ItemCategory = context.ItemCategory.Find("Sound Card"),
                                ImgPath = "/img/sound.png"
                            },
                            new Product
                            {
                                Title = "SoundX 11",
                                Description = "SoundX 11 Sound Card",
                                Price = 50.11M,
                                ItemCategory = context.ItemCategory.Find("Sound Card"),
                                ImgPath = "/img/sound.png"
                            }
                        );
                }

                context.SaveChanges();
            }
        }
    }
}
