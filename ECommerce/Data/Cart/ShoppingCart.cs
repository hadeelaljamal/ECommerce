﻿using ECommerce.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Data.Cart
{
    public class ShoppingCart
    {
        private readonly ECommerceDbContext _context;
        public string ShoppingCartId { get; set; }
        public ShoppingCart(ECommerceDbContext context)
        {
            _context = context;
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            var session = service.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            var context = service.GetRequiredService<ECommerceDbContext>();
            var cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        //Get All Item in Shopping Cart
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Include(x => x.Product).ToList();
        }

        //Calculate Total Amount in Shopping Cart Item
        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Select(x => x.Product.Price * x.Amount).Sum();
            return total;
        }
        public int GetShoppingCartTotalAmount()
            => _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Select
            (x => x.Amount).Sum();
        public async Task AddItemToShoppingCart(Product product)
        {

            //var shoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(x
            //         => x.ShoppingCartId == ShoppingCartId && x.Product.Id == product.Id);

            var shoppingCartItem = await _context.ShoppingCartItems.FirstOrDefaultAsync(x => x.Product.Id == product.Id && x.ShoppingCartId == ShoppingCartId);


            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
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
