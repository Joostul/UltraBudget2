using Microsoft.AspNetCore.Mvc;
using System;
using UltraBudget2.Extensions;
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
            TempData["Categories"] = _repository.GetSubCategoriesDropdown();
            TempData["Accounts"] = _repository.GetAccountsDropdown();
            return View(_repository.GetTransactions());
        }

        public IActionResult Create()
        {
            TempData["Categories"] = _repository.GetSubCategoriesDropdown();
            TempData["Accounts"] = _repository.GetAccountsDropdown();
            return View();
        }

        public IActionResult Delete(Transaction transaction)
        {
            var existingTransaction = _repository.GetTransaction(transaction.Id);
            return View(existingTransaction);
        }

        public IActionResult Edit(string id)
        {
            TempData["Categories"] = _repository.GetSubCategoriesDropdown();
            TempData["Accounts"] = _repository.GetAccountsDropdown();
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
    }
}