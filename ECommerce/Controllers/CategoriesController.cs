//using AspNetCore;
using ECommerce.Data;
using ECommerce.Data.Services;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualBasic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryServices _services; 

        
        public CategoriesController(ICategoryServices services) 
        {
            _services = services;
        }
        /*public IActionResult Index()
        {
            var Response =_context.Categories.ToList(); 
            return View(Response);
        }*/


        public async Task< IActionResult> Index()
        {
            var Response =await _services.GetAllAsync();
            return View(Response);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        
        }

        [HttpPost]
        public async Task<IActionResult>Create([Bind("Name,Description")]Category category) //use [Bind("Name,Desc")] if i dont want to add some field from form
        {
            if(ModelState.IsValid) //ModelState check on validation which i wrote it in category model by using data annotation ([required]...etc)
            {
                await _services.CreatAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _services.GetByIdAsync(id);
            if(category!= null)
            {
                return View(category);
            }
            return View("NotFound");
        }

        //????????????
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _services.GetByIdAsync(id);
            //var _result = new CategoryDTO { Id = id, Name = category.Name, Description = category.Description };
            if(category!=null)
            {
                return View(category);
            }
            return View("NotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            //if (id != category.Id)
            //{
            //    return View("NotFound");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    await _services.UpdateAsync(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return View("NotFound");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _services.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }



        //[HttpPost]
        //public async Task<IActionResult> Edit(int id,Category category)
        //{
        //    var _category = await _services.GetByIdAsync(id); 

        //    if (!ModelState.IsValid && id<=0)
        //    {
        //        return View("NotFound");
        //    }
        //    // بعمل هنا mapping 
        //    //علشان اعرفه ياخد القيم الجديدة منين قبل مبعمل update
        //    //_category.Name = category.Name;
        //    //_category.Description = category.Description;

        //    //await _services.UpdateAsync(id,category);

        //    await _services.UpdateAsync(category);
        //    return RedirectToAction(nameof(Index));

        //}


    }
} 
