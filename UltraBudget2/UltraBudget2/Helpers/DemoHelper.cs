using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraBudget2.Models;

namespace UltraBudget2.Helpers
{
    public static class DemoHelper
    {
        public static BudgetDao GenerateDemoBudget()
        {
            var accounts = new List<Account>()
            {
                new Account()
                {
                    Id = Guid.NewGuid(),
                    Balance = 0,
                    Name = "Checking",
                    Type = AccountType.Budget
                },
                new Account()
                {
                    Id = Guid.NewGuid(),
                    Balance = 0,
                    Name = "Saving",
                    Type = AccountType.OffBudget
                }
            };

            var categories = new List<MasterCategory>()
            {
                new MasterCategory()
                {
                    Id = Guid.NewGuid(),
                    Name = "MasterCategory1",
                    SubCategories = new List<SubCategory>()
                    {
                        new SubCategory()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Subcategory11",
                            MasterCategory = "MasterCategory1",
                            Budgets = new List<Budget>()
                        },
                        new SubCategory()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Subcategory12",
                            MasterCategory = "MasterCategory1",
                            Budgets = new List<Budget>()
                        }
                    }
                },
                new MasterCategory()
                {
                    Id = Guid.NewGuid(),
                    Name = "MasterCategory2",
                    SubCategories = new List<SubCategory>()
                    {
                        new SubCategory()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Subcategory21",
                            MasterCategory = "MasterCategory2",
                            Budgets = new List<Budget>()
                        },
                        new SubCategory()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Subcategory22",
                            MasterCategory = "MasterCategory2",
                            Budgets = new List<Budget>()
                        },
                        new SubCategory()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Subcategory23",
                            MasterCategory = "MasterCategory2",
                            Budgets = new List<Budget>()
                        }
                    }
                }
            };

            var budgetDao = new BudgetDao()
            {
                Categories = categories,
                Accounts = accounts,
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Id = Guid.NewGuid(),
                        Amount = 15,
                        Account = "Checking",
                        Category = "Subcategory21",
                        DateTime = new DateTime(2018, 12, 30),
                        Payee = null
                    },
                    new Transaction()
                    {
                        Id = Guid.NewGuid(),
                        Amount = 50,
                        Account = "Saving",
                        Category = "Subcategory12",
                        DateTime = new DateTime(2018, 12, 29),
                        Payee = null
                    },
                    new Transaction()
                    {
                        Id = Guid.NewGuid(),
                        Amount = 15,
                        Account = "Checking",
                        Category = "Subcategory11",
                        DateTime = new DateTime(2019, 1, 1),
                        Payee = null
                    }
                }
            };

            return budgetDao;
        }
    }
}
