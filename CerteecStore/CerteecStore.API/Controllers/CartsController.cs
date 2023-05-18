using Microsoft.AspNetCore.Mvc;

namespace CerteecStore.API.Controllers
{
    public class CartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
