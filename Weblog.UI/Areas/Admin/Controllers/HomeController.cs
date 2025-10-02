using Microsoft.AspNetCore.Mvc;

namespace Weblog.UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
