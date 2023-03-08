using Microsoft.AspNetCore.Mvc;
using ClassLibrary1.BAL;
using ClassLibrary1.DAL;
using System.Collections.Generic;

namespace WebApplication8.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        Product Data = new Product();
        public ActionResult Index()
        {
            string ConnectionString = _configuration.GetConnectionString("DbString");
            Data.OnGet(ConnectionString);


            ProductDAL listItem = new ProductDAL();
            TempData["listProduct"] = listItem;
            return RedirectToAction("Index", "Home");
        }

        public Object Insert()
        {
            return View();
        }
    }
}
