using ECommerce.Enums.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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
    }
}
