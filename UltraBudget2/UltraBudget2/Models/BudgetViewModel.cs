using System;
using System.Collections.Generic;
using System.Linq;

namespace UltraBudget2.Models
{
    public class BudgetViewModel
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<MasterCategory> Categories { get; set; }
        public IEnumerable<Account> Accounts { get; set; }

        public IEnumerable<SubCategory> GetSubCategories()
        {
            return Categories.SelectMany(m => m.SubCategories);
        }

        public IEnumerable<Budget> GetBudgets()
        {
            var budgets = GetSubCategories().SelectMany(s => s.Budgets);

            if(budgets == null)
            {
                return new List<Budget>();
            }

            return budgets;
        }

        public IEnumerable<Budget> GetBudgetsForMonth(DateTime date)
        {
            var budgets = GetBudgets().Where(b => b.Month.Month + b.Month.Year == date.Month + date.Year);

            if(budgets == null)
            {
                return new List<Budget>();
            }

            return budgets;
        }
    }
}
