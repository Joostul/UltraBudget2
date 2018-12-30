using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UltraBudget2.Repositories;

namespace UltraBudget2.Controllers
{
    public class BudgetController : Controller
    {
        private IBudgetRepository _repository { get; set; }

        public BudgetController(IBudgetRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}