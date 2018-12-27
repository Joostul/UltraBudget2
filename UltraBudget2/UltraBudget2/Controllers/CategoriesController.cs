using Microsoft.AspNetCore.Mvc;
using System;
using UltraBudget2.Models;
using UltraBudget2.Repositories;

namespace UltraBudget2.Controllers
{
    public class CategoriesController : Controller
    {
        private IBudgetRepository _repository { get; set; }

        public CategoriesController(IBudgetRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.GetCategories());
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete(Category category)
        {
            var existingCategory = _repository.GetCategory(category.Id);
            return View(existingCategory);
        }

        public IActionResult Edit(string id)
        {
            var existingCategory = _repository.GetCategory(Guid.Parse(id));
            return View(existingCategory);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Category category)
        {
            category.Id = Guid.NewGuid();
            _repository.UpsertCategory(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _repository.DeleteCategory(Guid.Parse(id));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Category category)
        {
            _repository.UpsertCategory(category);
            return RedirectToAction("Index");
        }
    }
}