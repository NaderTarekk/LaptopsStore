using ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IClsProducts _clsProducts;
        UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IClsProducts clsProducts, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _clsProducts = clsProducts;
            _userManager = userManager;
        }

        LapShopProjectContext ctx = new LapShopProjectContext();
        public async Task<IActionResult> Index()
        {
            Console.WriteLine("ho");
            ViewBag.Categories = ctx.TbCategories.ToList();
            ViewBag.Prices = ctx.TbPrices.ToList();
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                ViewData["FirstName"] = user.FirstName;
            }
            return View(_clsProducts.GetAllLaptops());
        }

        public IActionResult ProductDetails(int id)
        {
            var laptop = _clsProducts.GetProductById(id);
            var productDetails = ctx.TbLaptops.Where(laps=>laps.Category == laptop.Category).ToList();
            ViewBag.RelatedProducts = productDetails;
            return View(laptop);
        }
        public IActionResult AddProductToLocalStorage(int id)
        {
            var laptop = _clsProducts.GetProductById(id);
            CartModel cart = new CartModel();
            if (HttpContext.Request.Cookies["Cart"] != null)
                cart = JsonConvert.DeserializeObject<CartModel>(HttpContext.Request.Cookies["Cart"]);

            CartItemModel getItemCart = cart.lstItems.Where(lap => lap.ProductId == id).FirstOrDefault();
            if (getItemCart != null)
            {
                getItemCart.Qty++;
                getItemCart.Total = (decimal)(getItemCart.Qty * getItemCart.Price);
            }
            else
            {
                cart.lstItems.Add(new CartItemModel
                {
                    ProductId = id,
                    ProductName = laptop.Name,
                    ProductImage = laptop.LaptopImg,
                    Price = (decimal)laptop.Price,
                    Qty = 1,
                    Total = (decimal)laptop.Price
                });
            }
            cart.Total = cart.lstItems.Sum(a => a.Total);
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(10);
            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));
            TempData["SuccessSaveInLocalStorage"] = "Product Added Successfully";
            return RedirectToAction("ProductDetails", laptop);
        }

        public IActionResult SelectCategory(string? cat)
        {
            if (cat == "All")
                return RedirectToAction("Index");
            var laptops = _clsProducts.GetAllLaptopsByCategory(cat);
            ViewBag.Categories = ctx.TbCategories.ToList();
            ViewBag.Prices = ctx.TbPrices.ToList();
            return View("Index", laptops);
        }
        public IActionResult SelectPrice(int? price)
        {
            if (price == 0)
                return RedirectToAction("Index");
            var laptops = _clsProducts.GetAllLaptopsByPrice(price);
            ViewBag.Categories = ctx.TbCategories.ToList();
            ViewBag.Prices = ctx.TbPrices.ToList();
            return View("Index", laptops);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
