using Microsoft.AspNetCore.Mvc;

namespace UltraBudget2.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}