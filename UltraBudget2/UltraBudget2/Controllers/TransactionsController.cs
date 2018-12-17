using Microsoft.AspNetCore.Mvc;
using System;
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
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Transaction transaction)
        {
            transaction.Id = Guid.NewGuid();
            _repository.UpsertTransaction(transaction);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            var existingTransaction = _repository.GetTransaction(Guid.Parse(id));
            return View(existingTransaction);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Transaction transaction)
        {
            _repository.UpsertTransaction(transaction);
            return RedirectToAction("Index");
        }
    }
}