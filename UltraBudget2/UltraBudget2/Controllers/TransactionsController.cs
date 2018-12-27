using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using UltraBudget2.Models;
using UltraBudget2.Repositories;

namespace UltraBudget2.Controllers
{
    public class TransactionsController : Controller
    {
        private IBudgetRepository _repository { get; set; }

        public TransactionsController(IBudgetRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.GetTransactions());
        }

        public IActionResult Create()
        {
            ViewData["Categories"] = GetCategoriesDropDown();

            return View();
        }

        public IActionResult Delete(Transaction transaction)
        {
            var existingTransaction = _repository.GetTransaction(transaction.Id);
            return View(existingTransaction);
        }

        public IActionResult Edit(string id)
        {
            ViewData["Categories"] = GetCategoriesDropDown();
            var existingTransaction = _repository.GetTransaction(Guid.Parse(id));
            return View(existingTransaction);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Transaction transaction)
        {
            transaction.Id = Guid.NewGuid();
            _repository.UpsertTransaction(transaction);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _repository.DeleteTransaction(Guid.Parse(id));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Transaction transaction)
        {
            _repository.UpsertTransaction(transaction);
            return RedirectToAction("Index");
        }      
        
        private List<SelectListItem> GetCategoriesDropDown()
        {
            var selectListCategories = new List<SelectListItem>();
            var categories = _repository.GetCategories();
            if (categories != null)
            {
                foreach (var category in categories)
                {
                    selectListCategories.Add(new SelectListItem() { Text = category.Name, Value = category.Name });
                }
            }

            return selectListCategories;
        }
    }
}