using ECommerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Data.Services
{
    public interface IOrderServices
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId);
        Task<List<Order>> GetOrderAndRoleByUserIdAsync(string userId,string role);
    }
}
