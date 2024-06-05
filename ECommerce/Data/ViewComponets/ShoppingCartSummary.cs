using ECommerce.Data.Cart;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.Data.ViewComponets
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _cart;
        //private readonly UserManager<ApplicationUser> _userManager;
        public ShoppingCartSummary(ShoppingCart cart)
        {
          _cart=cart;
        }
        public IViewComponentResult Invoke()
        {

            var item = _cart.GetShoppingCartTotalAmount();
            ViewBag.Total = _cart.GetShoppingCartTotal();
            return View(item);

        }
    }
}
