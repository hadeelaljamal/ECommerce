using ECommerce.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class ShoppingCartItem : IBaseEntity
    {
        public int Id { get ; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
