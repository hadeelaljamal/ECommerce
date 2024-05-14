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
                       },
                       new Category()
                       {
                           Name="C4",
                           Description="C4"
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
                           Name="SWAROVISKI-1",Description="Metal bracelet, golden yellow colour",Price=150,ImageURL="~/image/GoldWatch.jpg",
                           ProductColor=ProductColor.gold,CategoryId=4
                       },
                       new Product()
                       {
                           Name="SWAROVISKI-2",Description="Metal bracelet, silver colour",Price=200,ImageURL="~/image/SilverWatch.jpg",
                           ProductColor=ProductColor.silver,CategoryId=5
                       },

                       new Product()
                       {
                           Name="SWAROVISKI-3",Description="Leather bracelet, golden yellow colour",Price=300,ImageURL="~/image/WhiteWatch.jpg",
                           ProductColor=ProductColor.white,CategoryId=6
                       },
                       new Product()
                       {
                           Name="SWAROVISKI-4",Description="Metal bracelet, black colour",Price=300,ImageURL="~/image/BlackWatch.jpg",
                           ProductColor=ProductColor.black,CategoryId=7
                       }

                    };
                    context.Products.AddRange(Products);
                    context.SaveChanges();
                }
            }
        }



    }
}
           