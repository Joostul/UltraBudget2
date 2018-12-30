using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UltraBudget2.Extensions;
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
            var masterCategories = _repository.GetCategories();
            return View(masterCategories);
        }

        public IActionResult CreateMasterCategory()
        {
            return View();
        }

        public IActionResult CreateSubCategory()
        {
            var categoriesDropdown = _repository.GetMasterCategoriesDropdown();
            TempData["Categories"] = categoriesDropdown;
            return View();
        }

        public IActionResult Delete(MasterCategory category)
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
        public IActionResult CreateMasterCategory([FromForm] MasterCategory category)
        {
            category.Id = Guid.NewGuid();
            category.SubCategories = new List<SubCategory>();
            _repository.UpsertCategory(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateSubCategory([FromForm] SubCategory category)
        {
            var masterCategory = _repository.GetCategories().SingleOrDefault(m => m.Name == category.MasterCategory);
            
            category.Id = Guid.NewGuid();
            masterCategory.SubCategories.Add(category);

            _repository.UpsertCategory(masterCategory);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _repository.DeleteCategory(Guid.Parse(id));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit([FromForm] MasterCategory category)
        {
            _repository.UpsertCategory(category);
            // TODO: update all transactions in this category
            return RedirectToAction("Index");
        }
    }
}