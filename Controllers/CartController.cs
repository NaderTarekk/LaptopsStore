using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ecommerce.Controllers
{
    public class CartController : Controller
    {
        CartModel cart = new CartModel();
        UserManager<ApplicationUser> _userManager;
        LapShopProjectContext _ctx;
        SignInManager<ApplicationUser> _signIn;
        public CartController(UserManager<ApplicationUser> userManager, LapShopProjectContext ctx, SignInManager<ApplicationUser> signIn)
        {
            _userManager = userManager;
            _ctx = ctx;
            _signIn = signIn;
        }
        public async Task<IActionResult> Index()
        {
            if (_signIn.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                ViewData["FirstName"] = user.FirstName;
            }
            if (HttpContext.Request.Cookies["Cart"] != null)
            {
                cart = JsonConvert.DeserializeObject<CartModel>(HttpContext.Request.Cookies["Cart"]);
            }
            return View(cart);
        }
        public async Task<IActionResult> CancelOrder(int id)
        {
            var cart = _ctx.TbCarts.Where(c => c.Id == id).FirstOrDefault();
            _ctx.TbCarts.Remove(cart);
            _ctx.SaveChanges();
            return RedirectToAction("SuccessOrder");
        }
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);
            cart = JsonConvert.DeserializeObject<CartModel>(HttpContext.Request.Cookies["Cart"]);
            cart.UserId = user.Id;
            cart.UserLocation = user.Location;
            cart.Date = DateTime.Now;
            cart.Total += 9;
            _ctx.TbCarts.Add(cart);
            try
            {
                await _ctx.SaveChangesAsync();
                TempData["CheckoutStatus"] = "Cart Added Successully ✅";
                TempData["color"] = "success";
                Response.Cookies.Delete("Cart");
            }
            catch
            {
                TempData["CheckoutStatus"] = "error sending products";
                TempData["color"] = "danger";
            }
            return RedirectToAction("SuccessOrder");
        }
        public async Task<IActionResult> SuccessOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserDetails = user;
            ViewData["FirstName"] = user.FirstName;
            var getUserOrder = _ctx.TbCarts.Where(u => u.UserId == user.Id).Include(c => c.lstItems).Where(c => c.Id == c.Id).ToList();
            return View(getUserOrder);
        }
        public IActionResult IncreaseQuantity(int id)
        {
            cart = JsonConvert.DeserializeObject<CartModel>(HttpContext.Request.Cookies["Cart"]);
            var product = cart.lstItems.Where(lap => lap.ProductId == id).FirstOrDefault();
            product.Qty++;
            product.Total = (decimal)(product.Qty * product.Price);
            cart.Total = cart.lstItems.Sum(a => a.Total);
            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Index");
        }
        public IActionResult DecreaseQuantity(int id)
        {
            cart = JsonConvert.DeserializeObject<CartModel>(HttpContext.Request.Cookies["Cart"]);
            var product = cart.lstItems.Where(lap => lap.ProductId == id).FirstOrDefault();
            product.Qty--;
            product.Total = (decimal)(product.Qty * product.Price);
            cart.Total = cart.lstItems.Sum(a => a.Total);
            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Index");
        }
        public IActionResult ClearAllProducts()
        {
            Response.Cookies.Delete("Cart");
            return RedirectToAction("Index");
        }
        public IActionResult RemoveProduct(int id)
        {
            cart = JsonConvert.DeserializeObject<CartModel>(HttpContext.Request.Cookies["Cart"]);
            var product = cart.lstItems.Where(lap => lap.ProductId == id).FirstOrDefault();
            cart.lstItems.Remove(product);
            cart.Total = cart.lstItems.Sum(a => a.Total);
            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Index");
        }

        public IActionResult AccessDenide()
        {
            return View();
        }
    }
}
