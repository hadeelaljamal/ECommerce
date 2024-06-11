using ECommerce.Data;
using ECommerce.Data.Services;
using ECommerce.Data.Static;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]

    public class ProductsController : Controller
    {
        //private readonly ECommerceDbContext _context;
        private readonly IProductServices _services;
        private readonly ICategoryServices _categoryServices;
        private readonly IWebHostEnvironment _webHost;
        private readonly IWebHostEnvironment _environment; //to upload file



        //public ProductsController(ECommerceDbContext context)
        //{
        //    _context=context;
        //}
        public ProductsController(IProductServices services, ICategoryServices categoryServices ,IWebHostEnvironment webHost, IWebHostEnvironment environment)
        {
            _services = services;
            _categoryServices = categoryServices;
            _webHost = webHost;
            _environment = environment;
        }
        /*public IActionResult Index()    //enhance this code its a Legacy code wich mean The request that is emitted will be one after the other
         / يعني الريكويست بتكون ورا بعض لما يوصلني الريسبونس بتنفذ الريكويست يلي بعده و هالشي بياخد وقت طويل لما يكون حجم الداتا عندي كبير /مش احسن شيء للبيرفورمنس
        {
            var Response = _context.Products.ToList();
            return View(Response);
        }*/

        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchTerm)   
        {
            // var Response =await _context.Products.ToListAsync();

            /*بضيف ال
              include 
            لحتى اقدر اعرض اسم ال 
            category
             */
            //var Response = await _context.Products.Include(x=>x.Category)
            //    .OrderBy(x=>x.Price)
            //    .ToListAsync();
            var Response=await _services.GetAllAsync(x=>x.Category);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                Response = Response.Where(x =>x.Name.Contains(searchTerm)).ToList(); 
            }
            return View(Response);
        }
        [AllowAnonymous]
        public async Task<IActionResult>Details(int id)
        {
            var Product = await _services.GetByIdAsync(id,x=>x.Category);
            return View(Product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = await _categoryServices.GetAllAsync();
            return View();
        }
        [HttpPost,ActionName(nameof(Create))]
        public async Task<IActionResult> CreateProduct(Product product)
        {
           if(ModelState.IsValid)
            {

                if (product.ProductPicture == null || product.ProductPicture.Length == 0)
                {
                    throw new ArgumentException("File is invalid");
                }
                string uploadFolder = Path.Combine(_environment.WebRootPath, "image");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string uniqueFileName = $"{Guid.NewGuid()} - {product.ProductPicture.FileName}";
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ProductPicture.CopyTo(fileStream);
                }
                var src = "/image/" + uniqueFileName;
                product.ImageURL = src;
                //if (product.ProductPicture != null)
                //{
                //    var pictureName = $"{Guid.NewGuid()} - {product.ProductPicture.FileName}";
                //    var src = "/image/" + pictureName;
                //    var path = Path.Combine(_webHost.WebRootPath, src);
                //    using (var fileStream = new FileStream(path, FileMode.Create))
                //    {
                //        product.ProductPicture.CopyTo(fileStream);

                //    }
                //    product.ImageURL = src;
                //}
                await _services.CreatAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View("NotFound");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Category = await _categoryServices.GetAllAsync();
            var ProductId = await _services.GetByIdAsync(id, x => x.Category);
            var product= await _services.GetByIdAsync(id) as Product;
            var productpic= product.ProductPicture;
            return View(ProductId);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {

            if (ModelState.IsValid)
            {

                if (product.ProductPicture == null || product.ProductPicture.Length == 0)
                {

                    await _services.UpdateAsync(product);
                    return RedirectToAction(nameof(Index));
                }
                else { 
                string uploadFolder = Path.Combine(_environment.WebRootPath, "image");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string uniqueFileName = $"{Guid.NewGuid()} - {product.ProductPicture.FileName}";
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.ProductPicture.CopyTo(fileStream);
                }
                var src = "/image/" + uniqueFileName;
                product.ImageURL = src;
                await _services.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
                }
            }
            return View("NotFound");
        }

        public async Task<IActionResult> Delete(int id , Product product)
        {

          await  _services.DeleteAsync(id);
          return RedirectToAction(nameof(Index));
        }

    }
}
