using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UltraBudget2.Models;
using UltraBudget2.Repositories;

namespace UltraBudget2.Controllers
{
    public class HomeController : Controller
    {
        private IBudgetRepository _repository { get; set; }

        public HomeController(IBudgetRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.GetTransactions());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
