using ECommerce.Controllers;
using ECommerce.Migrations;
using ECommerce.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce.Data.Cart
{
    public class ShoppingCart
    { 
        private readonly ECommerceDbContext _context;

        public string ShoppingCartId { get; set; }
        public string UserId { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCart(ECommerceDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
           
        }

       
       
        public static ShoppingCart GetShoppingCart(IServiceProvider service )
        {
            //var session = service.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            //var context = service.GetRequiredService<ECommerceDbContext>();
            //var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            //session.SetString("CartId", cartId);
            //return new ShoppingCart(context,httpContextAccessor) { ShoppingCartId = cartId };
            var httpContextAccessor = service.GetRequiredService<IHttpContextAccessor>();
            var session = httpContextAccessor.HttpContext.Session;
            var context = service.GetRequiredService<ECommerceDbContext>();
            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            string userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            session.SetString("CartId", cartId);
            return new ShoppingCart(context, httpContextAccessor) { ShoppingCartId = cartId , UserId = userId };
        }

        //Get All Item in Shopping Cart
        public List<Models.ShoppingCartItem> GetShoppingCartItems(string userId)
        //public List<ShoppingCartItem> GetShoppingCartItems()
        {
            ///return _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId && x.UserId == userId).Include(x => x.Product).ToList();

            return _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId && x.UserId==userId).Include(x => x.Product).ToList();


        }

        //Calculate Total Amount in Shopping Cart Item
        // public double GetShoppingCartTotal(string userId)
        public double GetShoppingCartTotal(string userId)
        {
            var total = _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId && x.UserId == userId).Select(x => x.Product.Price * x.Amount).Sum();
            return total;
        }
        public int GetShoppingCartTotalAmount(string userId)
            => _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId && x.UserId == userId).Select
            (x => x.Amount).Sum();
        public async Task AddItemToShoppingCart(Product product)
        {

            //var shoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(x
            //         => x.ShoppingCartId == ShoppingCartId && x.Product.Id == product.Id);
            string userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var shoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(x => x.Product.Id == product.Id && x.ShoppingCartId == ShoppingCartId && x.UserId==userId);


            if (shoppingCartItem == null)
            {
                shoppingCartItem = new Models.ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1,
                    UserId = userId
                };
                await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount += 1;
            }
            await _context.SaveChangesAsync();

        }

        public async Task RemoveItemFormShoppingCart(Product product)
        {
            var shoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(x
                     => x.ShoppingCartId == ShoppingCartId && x.Product.Id == product.Id);
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount -= 1;

                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
                await _context.SaveChangesAsync();
            }
           

        }
        public void ClearShoppingCart()
        {
            var items = _context.ShoppingCartItems.Where(x=> x.ShoppingCartId == ShoppingCartId).ToList();
            _context.ShoppingCartItems.RemoveRange(items);
            _context.SaveChanges();
        }
    }
}
