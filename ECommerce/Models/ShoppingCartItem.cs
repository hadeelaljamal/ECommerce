using ECommerce.Data.Base;

namespace ECommerce.Models
{
    public class ShoppingCartItem : IBaseEntity
    {
        public int Id { get ; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
        //public string UserId { get; set; }
        //public virtual ApplicationUser User { get; set; }

    }
}
