using Microsoft.AspNetCore.Mvc;

namespace UltraBudget2.Controllers
{
    public class GoalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}