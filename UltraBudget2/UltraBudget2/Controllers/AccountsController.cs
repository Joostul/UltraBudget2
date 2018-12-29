using Microsoft.AspNetCore.Mvc;
using System;
using UltraBudget2.Models;
using UltraBudget2.Repositories;

namespace UltraBudget2.Controllers
{
    public class AccountsController : Controller
    {
        private IBudgetRepository _repository { get; set; }

        public AccountsController(IBudgetRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.GetAccounts());
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete(Account account)
        {
            var existingAccount = _repository.GetAccount(account.Id);
            return View(existingAccount);
        }

        public IActionResult Edit(string id)
        {
            var existingAccount = _repository.GetAccount(Guid.Parse(id));
            return View(existingAccount);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Account account)
        {
            account.Id = Guid.NewGuid();
            // TODO: check if Account with same name already exists
            _repository.UpsertAccount(account);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _repository.DeleteAccount(Guid.Parse(id));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Account account)
        {
            _repository.UpsertAccount(account);
            return RedirectToAction("Index");
        }
    }
}