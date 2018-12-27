using Microsoft.AspNetCore.Mvc;

namespace UltraBudget2.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}