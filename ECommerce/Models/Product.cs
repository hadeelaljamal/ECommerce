using ECommerce.Data.Base;
using ECommerce.Enums.Data;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Product : IBaseEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(10, ErrorMessage = "This {0} Is Spasefic Between {2},{1}", MinimumLength = 5)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description Is Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price Is Required")]
        public double Price { get; set; }

        public string ImageURL { get; set; }

        /*Since colors are something fixed and known that do not change,
         * we will store them in enum. We can add it to the same class,
         * but we will create a file called data, in which I will put all the data for the project.*/
        public ProductColor ProductColor { get; set; }



        //The Relationship between Category and product is one to many
        //Nanigational Proparity
        public int CategoryId { get; set; } //Foreign  Key
        [ForeignKey(nameof(CategoryId))] //[ForeignKey("CategoryId")] another way to write ForeignKey 
        public Category Category { get; set; }


        public ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }

        [NotMapped]
        
        public IFormFile ProductPicture { get; set; }

    }
}
