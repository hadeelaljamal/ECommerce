using ECommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    public class CategoriesController : Controller
    {
        //First I need access to the database to do the actions 
        private readonly ECommerceDbContext _context; //referance for db

        //inject for dbcontext in the controller done by contractor
        public CategoriesController(ECommerceDbContext context) //this called constructor dependency injection
        {
            _context=context;
        }
        /*public IActionResult Index()
        {
            var Response =_context.Categories.ToList(); 
            return View(Response);
        }*/


        public async Task< IActionResult> Index()
        {
            var Response =await _context.Categories.ToListAsync();
            return View(Response);
        }
    }
}
