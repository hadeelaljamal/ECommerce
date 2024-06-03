using ECommerce.Data.Cart;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.Data.ViewComponets
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _cart;
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
