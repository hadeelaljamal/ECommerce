using ECommerce.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Category : IBaseEntity
    {
        public Category()
        {
            Products=new HashSet<Product>();
        }
        //Entity framework smart enough to Know CategoryId is the primary key because it's ending with Id
        // data annotation([Key]) benefit is add more restricted to the field
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Is Required")]
        [StringLength(10,ErrorMessage ="This {0} Is Spasefic Between {2},{1}",MinimumLength =5)]
        [Display(Name ="Category Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description Is Required")]
        public string Description { get; set; }

        //Navigational Property
        public ICollection<Product> Products { get; set; }

    }
}
