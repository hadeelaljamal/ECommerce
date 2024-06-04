using ECommerce.Models;
using ECommerce.Enums.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ECommerce.Data.Static;

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
                           Name="SWAROVISKI-1",Description="Metal bracelet, golden yellow colour",Price=150,ImageURL="/image/GoldWatch.jpg",
                           ProductColor=ProductColor.gold,CategoryId=1
                       },
                       new Product()
                       {
                           Name="SWAROVISKI-2",Description="Metal bracelet, silver colour",Price=200,ImageURL="/image/SilverWatch.jpg",
                           ProductColor=ProductColor.silver,CategoryId=2
                       },

                       new Product()
                       {
                           Name="SWAROVISKI-3",Description="Leather bracelet, golden yellow colour",Price=300,ImageURL="/image/WhiteWatch.jpg",
                           ProductColor=ProductColor.white,CategoryId=3
                       },
                       new Product()
                       {
                           Name="SWAROVISKI-4",Description="Metal bracelet, black colour",Price=300,ImageURL="/image/BlackWatch.jpg",
                           ProductColor=ProductColor.black,CategoryId=4
                       }

                    };
                    context.Products.AddRange(Products);
                    context.SaveChanges();
                }
            }
        }

        public static async Task SedingUsersAndRolesAsync(IApplicationBuilder builder)
        {
            using(var applicationservices = builder.ApplicationServices.CreateScope())
            {
                #region Role
                var roleManager = applicationservices.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if(! await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }
                #endregion

                #region User
                var userManager = 
                    applicationservices.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                if (await userManager.FindByEmailAsync("admin@admin.com") == null)
                {
                   var newAdminUser =  new ApplicationUser()
                    {
                        Email = "admin@admin.com",
                        EmailConfirmed = true,
                        FullName = "Admin User",
                        UserName = "Admin"

                    };
                    await userManager.CreateAsync(newAdminUser,"@Dmin123");

                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);

                }

                if (await userManager.FindByEmailAsync("user@user.com") == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        Email = "user@user.com",
                        EmailConfirmed = true,
                        FullName = "User",
                        UserName = "User"

                    };
                    await userManager.CreateAsync(newUser, "@User123");

                    await userManager.AddToRoleAsync(newUser, UserRoles.User);

                }



                #endregion
            }
            }
        }

    }

           