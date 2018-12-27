using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public IActionResult Export()
        {
            var fileName = $"UltraBudget_{DateTime.Now.ToShortDateString()}.txt"; // TODO: set as budget name
            var budget = new BudgetDao
            {
                Categories = _repository.GetCategories(),
                Transactions = _repository.GetTransactions()
            };
            byte[] file = ObjectToByteArray(budget);
            return File(file, "text/plain", fileName);
        }

        private byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            TextWriter textWriter = new StringWriter();
            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(textWriter, obj);

            var encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(textWriter.ToString());

            return bytes;

        }
    }
}
