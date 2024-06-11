using ECommerce.Data.Cart;
using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace ECommerce.Data.ViewComponets
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _cart;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //private readonly UserManager<ApplicationUser> _userManager;
        public ShoppingCartSummary(ShoppingCart cart , IHttpContextAccessor httpContextAccessor)
        {
          _cart=cart;
          _httpContextAccessor = httpContextAccessor;
        }
        public IViewComponentResult Invoke()
        
        {
            string userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var item = _cart.GetShoppingCartTotalAmount(userId);
            ViewBag.Total = _cart.GetShoppingCartTotal(userId);
            return View(item);

        }
    }
}
