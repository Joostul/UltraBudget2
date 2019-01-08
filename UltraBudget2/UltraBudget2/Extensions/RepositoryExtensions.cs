using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using UltraBudget2.Models;
using UltraBudget2.Repositories;

namespace UltraBudget2.Extensions
{
    public static class RepositoryExtensions
    {
        public static List<SelectListItem> GetSubCategoriesDropdown(this IBudgetRepository repository)
        {
            var selectListCategories = new List<SelectListItem>();
            var subCategories = repository.GetMasterCategories().SelectMany(c => c.SubCategories);
            if (subCategories.Any())
            {
                foreach (var subCategory in subCategories)
                {
                    selectListCategories.Add(new SelectListItem() { Text = subCategory.Name, Value = subCategory.Name });
                }
            }

            return selectListCategories;
        }

        public static List<SelectListItem> GetMasterCategoriesDropdown(this IBudgetRepository repository)
        {
            var selectListCategories = new List<SelectListItem>();
            var masterCategories = repository.GetMasterCategories();
            if (masterCategories.Any())
            {
                foreach (var mastesrCategory in masterCategories)
                {
                    selectListCategories.Add(new SelectListItem() { Text = mastesrCategory.Name, Value = mastesrCategory.Name });
                }
            }

            return selectListCategories;
        }

        public static List<SelectListItem> GetAccountsDropdown(this IBudgetRepository repository)
        {
            var selectListAccounts = new List<SelectListItem>();
            var accounts = repository.GetAccounts();
            if (accounts.Any())
            {
                foreach (var account in accounts)
                {
                    selectListAccounts.Add(new SelectListItem() { Text = account.Name, Value = account.Name });
                }
            }

            return selectListAccounts;
        }
    }
}
