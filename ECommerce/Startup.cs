using ECommerce.Data;
using ECommerce.Data.Cart;
using ECommerce.Data.Services;
using ECommerce.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure DbContext
            services.AddDbContext<ECommerceDbContext>(options=>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefultConnection"));
            });
            services.AddControllersWithViews();
            services.AddScoped<ICategoryServices, CategoryServices>();//this work if any one use ICategoryServices the scop create CategoryServices object 
            services.AddScoped<IProductServices, ProductServices>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(x => ShoppingCart.GetShoppingCart(x));
            services.AddSession();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<ShoppingCart>();
            //Identity
            services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<ECommerceDbContext>();
            services.AddMemoryCache();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
            services.AddAuthorization();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            ///
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Products}/{action=Index}/{id?}");
                /*first page display when run it is a Product             */
            });
            

            AppDbInitializer.Seed(app);
            AppDbInitializer.SedingUsersAndRolesAsync(app).Wait();
        }
    }
}
