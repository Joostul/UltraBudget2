using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
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
            // TODO: Maybe can be in constructor?
            TempData["Categories"] = GetCategoriesDropdown();
            TempData["Accounts"] = GetAccountsDropdown();
            return View(_repository.GetTransactions());
        }

        public IActionResult Create()
        {
            TempData["Categories"] = GetCategoriesDropdown();
            TempData["Accounts"] = GetAccountsDropdown();
            return View();
        }

        public IActionResult Delete(Transaction transaction)
        {
            var existingTransaction = _repository.GetTransaction(transaction.Id);
            return View(existingTransaction);
        }

        public IActionResult Edit(string id)
        {
            TempData["Categories"] = GetCategoriesDropdown();
            TempData["Accounts"] = GetAccountsDropdown();
            var existingTransaction = _repository.GetTransaction(Guid.Parse(id));
            return View(existingTransaction);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Id = Guid.NewGuid();
                _repository.UpsertTransaction(transaction);
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid input for transaction.";
            }
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
            if (ModelState.IsValid)
            {
                _repository.UpsertTransaction(transaction);
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid input for transaction.";
            }
            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetCategoriesDropdown()
        {
            var selectListCategories = new List<SelectListItem>();
            var categories = _repository.GetCategories().Where(c => c.Type == CategoryType.Sub);
            if (categories.Any())
            {
                foreach (var category in categories)
                {
                    selectListCategories.Add(new SelectListItem() { Text = category.Name, Value = category.Name });
                }
            }

            return selectListCategories;
        }

        private List<SelectListItem> GetAccountsDropdown()
        {
            var selectListCategories = new List<SelectListItem>();
            var accounts = _repository.GetAccounts();
            if (accounts.Any())
            {
                foreach (var account in accounts)
                {
                    selectListCategories.Add(new SelectListItem() { Text = account.Name, Value = account.Name });
                }
            }

            return selectListCategories;
        }
    }
}