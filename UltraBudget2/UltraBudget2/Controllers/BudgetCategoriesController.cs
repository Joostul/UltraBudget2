﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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