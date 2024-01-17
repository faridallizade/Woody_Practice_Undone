using Microsoft.AspNetCore.Mvc;

namespace Woody.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Area("Manage")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
