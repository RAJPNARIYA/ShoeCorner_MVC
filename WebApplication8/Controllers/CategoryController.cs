using ClassLibrary1.BAL;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication8.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IConfiguration _configuration;

        public CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            Category category = new Category();
            string ConnectionString = _configuration.GetConnectionString("DbString");
            category.OnGet(ConnectionString);
            return View(category.CategoryList);
        }
    }
}
