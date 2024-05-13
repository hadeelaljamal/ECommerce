using ECommerce.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Data
{
    public class AppDbInitializer
    {

        /*I write this static function to Call it
         * by class name Without generate object from this class */
        public static void Seed(IApplicationBuilder builder) //void becase I don't want it to return any data,just to check if i had any data
        {
            using (var applicationservies = builder.ApplicationServices.CreateScope())
            {
                //reference for database file
                var context = applicationservies.ServiceProvider.GetService<ECommerceDbContext>();


                //first I well check if the database created
                context.Database.EnsureCreated();

                //Category
                if (!context.Categories.Any()) //Check the table is empty(no data on it)
                {
                    var categorias = new List<Category>() //make list of category
                    {
                       new Category()
                       {
                           Name="C1",
                           Description="C1"
                       },
                       new Category()
                       {
                           Name="C2",
                           Description="C2"
                       },
                       new Category()
                       {
                           Name="C3",
                           Description="C3"
                       }
                    };
                    context.Categories.AddRange(categorias);
                    context.SaveChanges();
                }


                //Product
                if (!context.Products.Any()) //Check the table is empty(no data on it)
                {
                    var Products = new List<Product>() //make list of category
                    {
                       new Product()
                       {
                           Name="P1",Description="D1",Price=150,ImageURL="https...",
                           ProductColor=ProductColor.Red,CategoryId=1
                       },
                       new Product()
                       {
                           Name="P2",Description="D2",Price=200,ImageURL="https...",
                           ProductColor=ProductColor.Green,CategoryId=2
                       },

                       new Product()
                       {
                           Name="P3",Description="D3",Price=300,ImageURL="https...",
                           ProductColor=ProductColor.Yellow,CategoryId=3
                       }
                    };
                    context.Products.AddRange(Products);
                    context.SaveChanges();
                }
            }
        }



    }
}
           