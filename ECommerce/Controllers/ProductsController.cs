using ECommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ECommerceDbContext _context;

        public ProductsController(ECommerceDbContext context)
        {
            _context=context;
        }
        /*public IActionResult Index()    //enhance this code its a Legacy code wich mean The request that is emitted will be one after the other
         / يعني الريكويست بتكون ورا بعض لما يوصلني الريسبونس بتنفذ الريكويست يلي بعده و هالشي بياخد وقت طويل لما يكون حجم الداتا عندي كبير /مش احسن شيء للبيرفورمنس
        {
            var Response = _context.Products.ToList();
            return View(Response);
        }*/


        public async Task<IActionResult> Index()   
        {
            var Response =await _context.Products.ToListAsync();
            return View(Response);
        }
    }
}
