using Microsoft.AspNetCore.Mvc;

namespace UltraBudget2.Controllers
{
    public class BudgetCategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}