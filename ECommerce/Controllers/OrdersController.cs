using ECommerce.Data.Cart;
using ECommerce.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductServices _services;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderServices _orderServices;
        public OrdersController(IProductServices services, ShoppingCart shoppingCart, IOrderServices orderServices)
        {
            _services = services;
            _shoppingCart = shoppingCart;
            _orderServices = orderServices;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string roleId = User.FindFirstValue(ClaimTypes.Role);
            var order = await _orderServices.GetOrderAndRoleByUserIdAsync(userId, roleId);
            return View(order);
        }
        public IActionResult ShoppingCart()
        {
            //
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            //var item = _shoppingCart.GetShoppingCartItems(userId);
            var item = _shoppingCart.GetShoppingCartItems();
            //ViewBag.Total = _shoppingCart.GetShoppingCartTotal(userId);
            ViewBag.Total = _shoppingCart.GetShoppingCartTotal();
            return View(item);
            
        }
        public async Task<IActionResult> AddToCart(int id)
        {
            var item = await _services.GetByIdAsync(id);
            if (item != null)
            {
                await _shoppingCart.AddItemToShoppingCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var item = await _services.GetByIdAsync(id);
            if (item != null)
            {
                await _shoppingCart.RemoveItemFormShoppingCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));

        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _orderServices.StoreOrderAsync(items, userId);
            _shoppingCart.ClearShoppingCart();
            return View("CompleteOrder");
        }

        public async Task<IActionResult> CancelOrder(int orderId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _orderServices.CancelOrder(userId, orderId);
            return RedirectToAction(nameof(Index));
        }
    }
}
