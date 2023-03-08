using ClassLibrary1.BAL;
using ClassLibrary1.DAL;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication8.Controllers
{
	public class SliderController : Controller
	{
        private readonly IConfiguration _configuration;

        public SliderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        Slider slider = new Slider();
		public ActionResult Index()
		{
            string ConnectionString = _configuration.GetConnectionString("DbString");
            slider.OnGet(ConnectionString);
			return View();
		}
	}
}
