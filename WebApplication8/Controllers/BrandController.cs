using ClassLibrary1.BAL;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication8.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            Brand brand = new Brand();
            brand.OnGet();
            return View(brand.BrandList);
        }
    }
}
