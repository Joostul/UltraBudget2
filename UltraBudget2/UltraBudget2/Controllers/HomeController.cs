using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UltraBudget2.Extensions;
using UltraBudget2.Helpers;
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
            ViewData["Message"] = "Description for UltraBudget.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact page.";

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

        public IActionResult Export()
        {
            var fileName = $"UltraBudget_{DateTime.Now.ToShortDateString()}.txt"; // TODO: set as budget name
            var budget = _repository.Export();
            byte[] file = budget.ToByteArray();
            return File(file, "text/plain", fileName);
        }

        public IActionResult Import()
        {
            return View();
        }

        public IActionResult Example()
        {
            var budget = DemoHelper.GenerateDemoBudget();
            foreach (var account in budget.Accounts)
            {
                _repository.UpsertAccount(account);
            }
            foreach (var category in budget.Categories)
            {
                _repository.UpsertCategory(category);
            }
            foreach (var transaction in budget.Transactions)
            {
                _repository.UpsertTransaction(transaction);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            BudgetDao budget = null;

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    budget = JsonConvert.DeserializeObject<BudgetDao>(reader.ReadToEnd());
                    _repository.Import(budget);
                }
            }
            catch (Exception)
            {
                //ViewBag.Message = "Not a valid budget file.";
                return View();
            }

            return View();
        }
    }
}
