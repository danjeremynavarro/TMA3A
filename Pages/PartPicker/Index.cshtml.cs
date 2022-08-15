using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TMA3A.Data;
using TMA3A.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TMA3A.Pages.PartPicker
{
    public class FormDataJson
    {
        public string SelectedMotherboard { get; set; } = default!;
        public string SelectedCPU { get; set; } = default!;
        public string SelectedRAM { get; set; } = default!;
        public string SelectedGraphicCard { get; set; } = default!;
        public string[] AdditionalComponent { get; set; } = default!;
    }

    public class IndexModel : PageModel
    {
        private readonly TMA3A.Data.TMA3AContext _context;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public Order? Order { get; set; } = null;
        public IndexModel(TMA3A.Data.TMA3AContext context, UserManager<User> userManager)
        {
            _context = context;
            Products = _context.Product.ToList();
            _userManager = userManager;
        }
        public List<Product> Products { get; set; }
        public List<Selection> Motherboard { get; set; } = default!;
        [BindProperty]
        public int SelectedMotherboard { get; set; } = default!;

        public List<Selection> CPU { get; set; } = default!;
        [BindProperty]
        public int SelectedCPU{ get; set; } = default!;

        public List<Selection> RAM { get; set; } = default!;
        [BindProperty]
        public int SelectedRAM { get; set; } = default!;

        public List<Selection> GraphicCard { get; set; } = default!;
        [BindProperty]
        public int SelectedGraphicCard { get; set; } = default!;

        public List<Selection> OS { get; set; } = default!;
        [BindProperty]
        public int SelectedOS { get; set; } = default!;

        public List<Selection> SoundCard { get; set; } = default!;
        [BindProperty]
        public int SelectedSoundCard { get; set; } = default!;

        public List<Selection> OtherComponents { get; set; } = default!;

        [BindProperty]
        public SelectionProduct[] AdditionalItems { get; set; } = default!;

        public class SelectionProduct
        {
            public Product Product { get; set; }
            public Boolean Selected { get; set; }       
        }

        public class Selection : SelectListItem
        {
            public Product Product { get; set; }
        }

        /**
         *  Adds all selection and reset all fields in the page
         */
        private void ResetFields()
        {
            var motherboard = from m in Products
                              where m.ItemCategoryId == "Motherboard"
                              select new Selection
                              {
                                  Value = m.Id.ToString(),
                                  Text = m.Title,
                                  Product = m
                              };
            Motherboard = motherboard.ToList();

            var cpu = from m in Products
                      where m.ItemCategoryId == "CPU"
                      select new Selection
                      {
                          Value = m.Id.ToString(),
                          Text = m.Title,
                          Product = m
                      };
            CPU = cpu.ToList();

            var ram = from m in Products
                      where m.ItemCategoryId == "RAM"
                      select new Selection
                      {
                          Value = m.Id.ToString(),
                          Text = m.Title,
                          Product = m
                      };
            RAM = ram.ToList();

            var graphicCard = from m in Products
                              where m.ItemCategoryId == "Graphic Card"
                              select new Selection
                              {
                                  Value = m.Id.ToString(),
                                  Text = m.Title,
                                  Product = m
                              };
            GraphicCard = graphicCard.ToList();

            var oS = from m in Products
                              where m.ItemCategoryId == "OS"
                              select new Selection
                              {
                                  Value = m.Id.ToString(),
                                  Text = m.Title,
                                  Product = m
                              };
            OS = oS.ToList();

            var soundCard = from m in Products
                     where m.ItemCategoryId == "Sound Card"
                     select new Selection
                     {
                         Value = m.Id.ToString(),
                         Text = m.Title,
                         Product = m
                     };
            SoundCard = soundCard.ToList();

            var products = from m in Products
                           where m.ItemCategoryId == "Miscellaneous"
                           select new SelectionProduct
                           {
                               Product = m,
                               Selected = false
                           };
            AdditionalItems = products.ToArray<SelectionProduct>();
        }

        /**
         * returns product id of selected miscellaneous items
         * */
        private List<int> GetSelectedAdditionalItems()
        {
            if (AdditionalItems.Length == 0)
            {
                return new List<int>();
            }
            var list = new List<int>();
            foreach (var product in AdditionalItems)
            {
                if (product.Selected == true)
                {
                    list.Add(product.Product.Id);
                }
            }
            return list;
        }

        /**
         *  Create or update Order
         */
        private int? CreateOrUpdateOrder()
        {
            List<int> productsToOrder = GetSelectedAdditionalItems();
            productsToOrder.Add(SelectedMotherboard);
            productsToOrder.Add(SelectedCPU);
            productsToOrder.Add(SelectedRAM);
            productsToOrder.Add(SelectedGraphicCard);
            productsToOrder.Add(SelectedOS);
            productsToOrder.Add(SelectedSoundCard);
            List<Product> itemToOrder = new List<Product>();
            foreach(var selectedProduct in productsToOrder)
            {
                var productInfo = from p in _context.Product
                                  where p.Id == selectedProduct
                                  select p;
                Product? product = productInfo.FirstOrDefault();
                if (product != null)
                {
                    itemToOrder.Add(product);
                } else { continue; }
            }

            Order? orderToAttach = _context.Order.Find(Order.Id); ;
            if (orderToAttach == null)
            {
                orderToAttach = new Order();
                _context.Order.Add(orderToAttach); 
            }
          
            List<OrderLine> orderLines = new List<OrderLine>();
            foreach(var p in itemToOrder)
            {
                var sameProducts = from sp in orderLines
                                   where sp.IsSameItemAndPrice(p)
                                   select sp;
                if (sameProducts.Any())
                {
                    sameProducts.First().Qty += 1;
                    continue;
                } else
                {
                    OrderLine line = new OrderLine
                    {
                        Order = orderToAttach,
                        Product = p,
                        Price = (float)p.Price,
                        Qty = 1
                    };
                    _context.OrderLine.Add(line);
                    orderLines.Add(line);   
                }
            }

            var user = _userManager.GetUserId(User);
            if (user != null)
            {
                User? u = _context.User.Find(user);
                orderToAttach.User = u;
            }
            _context.SaveChanges();

            return orderToAttach.Id;
        }

        private void setFieldsForOrderId()
        {
            ResetFields();
            if (Order == null) { return; }
            foreach(OrderLine orderLine in Order.OrderLines)
            {
                switch (orderLine.Product.ItemCategoryId)
                {
                    case "Motherboard":
                        SelectedMotherboard = orderLine.ProductId;
                        break;
                    case "CPU":
                        SelectedCPU = orderLine.ProductId;
                        break;
                    case "RAM":
                        SelectedRAM = orderLine.ProductId;
                        break;
                    case "Graphic Card":
                        SelectedGraphicCard = orderLine.ProductId;
                        break;
                    case "OS":
                        SelectedOS = orderLine.ProductId;
                        break;
                    case "Sound Card":
                        SelectedGraphicCard = orderLine.ProductId;
                        break;
                    case "Miscellaneous":
                        foreach(SelectionProduct items in AdditionalItems)
                        {
                            if (items.Product.Id == orderLine.Product.Id)
                            {
                                items.Selected = true;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return;
        }

        public IActionResult OnGet(int? orderId)
        {          
            if (orderId == null)
            {
                ResetFields();
            } else
            {
                var order = _context.Order.Include(b => b.OrderLines)
                    .ThenInclude(c => c.Product)
                    .FirstOrDefault(b => b.Id == orderId);
                
                if (order != null)
                {
                    Order = order;
                    setFieldsForOrderId();
                }
            }            
            return Page();
        }

        public IActionResult OnPost(int? orderId)
        {
            if (orderId != null)
            {
                
                Order = _context.Order.Include(p => p.OrderLines).FirstOrDefault(o=>o.Id==orderId);
                _context.OrderLine.RemoveRange(Order.OrderLines); // Removes all Orderlines and creates new ones
            }
            int? newOrderId = CreateOrUpdateOrder();
            if (newOrderId != null) { return RedirectToPage("/Orders/EditShipping", new {orderid=newOrderId}); }
            ResetFields();
            return Page();
        }
    }
}
