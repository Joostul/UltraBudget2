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
            var masterCategories = _repository.GetMasterCategories();
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
            var existingCategory = _repository.GetMasterCategory(category.Id);
            return View(existingCategory);
        }

        public IActionResult Edit(string id)
        {
            var existingCategory = _repository.GetMasterCategory(Guid.Parse(id));
            return View(existingCategory);
        }

        [HttpPost]
        public IActionResult CreateMasterCategory([FromForm] MasterCategory category)
        {
            category.Id = Guid.NewGuid();
            category.SubCategories = new List<SubCategory>();
            _repository.UpsertMasterCategory(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateSubCategory([FromForm] SubCategory category)
        {
            // TODO: Should be on master category id
            var masterCategory = _repository.GetMasterCategories().SingleOrDefault(m => m.Name == category.MasterCategory);
            
            category.Id = Guid.NewGuid();
            category.Budgets = new List<Budget>();
            masterCategory.SubCategories.Add(category);

            _repository.UpsertMasterCategory(masterCategory);
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
            _repository.UpsertMasterCategory(category);
            // TODO: update all transactions in this category
            return RedirectToAction("Index");
        }
    }
}