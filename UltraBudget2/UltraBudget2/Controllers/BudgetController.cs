using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UltraBudget2.Extensions;
using UltraBudget2.Models;
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
            TempData["Categories"] = _repository.GetSubCategoriesDropdown();
            TempData["Accounts"] = _repository.GetAccountsDropdown();
            TempData["MasterCategories"] = _repository.GetCategories();
            return View();
        }
    }
}