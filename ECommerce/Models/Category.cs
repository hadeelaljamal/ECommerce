using System.Collections.Generic;

namespace ECommerce.Models
{
    public class Category
    {
        public Category()
        {
            Products=new HashSet<Product>();
        }
        //Entity framework smart enough to Know CategoryId is the primary key because it's ending with Id
        // data annotation([Key]) benefit is add more restricted to the field
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigational Property
        public ICollection<Product> Products { get; set; }

    }
}
