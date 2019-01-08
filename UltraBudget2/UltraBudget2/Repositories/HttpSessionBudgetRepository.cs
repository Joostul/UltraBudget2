using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using UltraBudget2.Extensions;
using UltraBudget2.Models;

namespace UltraBudget2.Repositories
{
    public class HttpSessionBudgetRepository : IBudgetRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private const string _transactionsSessionKey = "transactions";
        private const string _categoriesSessionKey = "categories";
        private const string _accountsSessionKey = "Accounts";

        public HttpSessionBudgetRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Transactions
        public void UpsertTransaction(Transaction transaction)
        {
            var transactions = GetTransactions() == null ? new List<Transaction>() : GetTransactions().ToList();

            if (!transactions.Any(t => t.Id == transaction.Id))
            {
                transactions.Add(transaction);
            }
            else
            {
                var existingTransaction = transactions.SingleOrDefault(t => t.Id == transaction.Id);
                var updatedTransction = new Transaction()
                {
                    Id = existingTransaction.Id,
                    Amount = transaction.Amount == 0.0m ? existingTransaction.Amount : transaction.Amount,
                    Category = string.IsNullOrWhiteSpace(transaction.Category) ? existingTransaction.Category : transaction.Category,
                    DateTime = transaction.DateTime == DateTime.MaxValue ? existingTransaction.DateTime : transaction.DateTime,
                    Payee = string.IsNullOrWhiteSpace(transaction.Payee) ? existingTransaction.Payee : transaction.Payee
                };

                transactions.Remove(existingTransaction);
                transactions.Add(updatedTransction);
            }

            _session.Set(_transactionsSessionKey, transactions);
        }

        public void DeleteTransaction(Guid id)
        {
            var existingTransaction = GetTransaction(id);

            if(existingTransaction != null)
            {
                var transactions = GetTransactions().ToList();
                var existingTransactionIndex = transactions.IndexOf(existingTransaction) + 1;
                transactions.RemoveAt(existingTransactionIndex);
                _session.Set(_transactionsSessionKey, transactions);
            }
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            var transactions = _session.Get<List<Transaction>>(_transactionsSessionKey);

            if(transactions == null)
            {
                return new List<Transaction>();
            }

            return transactions.OrderByDescending(t => t.DateTime);
        }

        public Transaction GetTransaction(Guid id)
        {
            return GetTransactions().SingleOrDefault(t => t.Id == id);
        }

        public void SetTransactions(IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                UpsertTransaction(transaction);
            }
        }

        // Categories
        public IEnumerable<MasterCategory> GetMasterCategories()
        {
            var categories = _session.Get<List<MasterCategory>>(_categoriesSessionKey);

            if(categories == null)
            {
                return new List<MasterCategory>();
            }

            return categories;
        }

        public MasterCategory GetMasterCategory(Guid id)
        {
            return GetMasterCategories().SingleOrDefault(c => c.Id == id);
        }

        public void UpsertMasterCategory(MasterCategory category)
        {
            var categories = GetMasterCategories() == null ? new List<MasterCategory>() : GetMasterCategories().ToList();

            if (!categories.Any(t => t.Id == category.Id))
            {
                categories.Add(category);
            }
            else
            {
                var existingCategory = categories.SingleOrDefault(t => t.Id == category.Id);

                var updatedCategory = new MasterCategory()
                {
                    Id = existingCategory.Id,
                    Name = string.IsNullOrWhiteSpace(category.Name) ? existingCategory.Name : category.Name,
                    SubCategories = category.SubCategories
                };

                categories.Remove(existingCategory);
                categories.Add(updatedCategory);
            }

            _session.Set(_categoriesSessionKey, categories);
        }

        public void DeleteCategory(Guid id)
        {
            var existingCategory = GetMasterCategory(id);

            if (existingCategory != null)
            {
                var categories = GetMasterCategories().ToList();
                var existingCategoryIndex = categories.IndexOf(existingCategory) + 1;
                categories.RemoveAt(existingCategoryIndex);
                _session.Set(_categoriesSessionKey, categories);
            }
        }

        public void SetMasterCategories(IEnumerable<MasterCategory> categories)
        {
            foreach (var category in categories)
            {
                UpsertMasterCategory(category);
            }
        }

        public IEnumerable<SubCategory> GetSubCategoriesForMaster(Guid id)
        {
            return GetMasterCategory(id).SubCategories;
        }

        public SubCategory GetSubCategory(Guid id)
        {
            return GetSubCategories().SingleOrDefault(s => s.Id == id);
        }

        public void UpsertSubcategory(Guid mastercategoryId, SubCategory subCategory)
        {
            var mastercategory = GetMasterCategory(mastercategoryId);

            if(mastercategory != null)
            {
                if (!mastercategory.SubCategories.Any(t => t.Name == subCategory.Name))
                {
                    mastercategory.SubCategories.Add(subCategory);
                    UpsertMasterCategory(mastercategory);
                }
                else
                {
                    var existingSubcategory = mastercategory.SubCategories.SingleOrDefault(s => s.Name == subCategory.Name);
                    var newBudgets = subCategory.Budgets.Except(existingSubcategory.Budgets);
                    var updatedBudgets = existingSubcategory.Budgets;
                    updatedBudgets.AddRange(newBudgets);

                    var updatedSubcategory = new SubCategory()
                    {
                        Id = subCategory.Id,
                        Name = subCategory.Name,
                        Budgets = updatedBudgets,
                        MasterCategory = existingSubcategory.MasterCategory
                    };

                    mastercategory.SubCategories.Remove(existingSubcategory);
                    mastercategory.SubCategories.Add(updatedSubcategory);
                    UpsertMasterCategory(mastercategory);
                }
            }
        }

        public IEnumerable<SubCategory> GetSubCategories()
        {
            return GetMasterCategories().SelectMany(m => m.SubCategories);
        }

        // Import/export dao
        public BudgetDao Export()
        {
            return new BudgetDao()
            {
                Categories = GetMasterCategories(),
                Transactions = GetTransactions(),
                Accounts = GetAccounts()
            };
        }

        public void Import(BudgetDao budget)
        {
            throw new NotImplementedException();
        }

        // Accounts
        public IEnumerable<Account> GetAccounts()
        {
            var accounts = _session.Get<List<Account>>(_accountsSessionKey);

            if(accounts == null)
            {
                return new List<Account>();
            }

            return accounts;
        }

        public Account GetAccount(Guid id)
        {
            return GetAccounts().SingleOrDefault(c => c.Id == id);
        }

        public void UpsertAccount(Account account)
        {
            var accounts = GetAccounts() == null ? new List<Account>() : GetAccounts().ToList();

            if (!accounts.Any(t => t.Id == account.Id))
            {
                accounts.Add(account);
            }
            else
            {
                var existingAccount = accounts.SingleOrDefault(t => t.Id == account.Id);
                var updatedAccount = new Account()
                {
                    Id = existingAccount.Id,
                    Name = string.IsNullOrWhiteSpace(account.Name) ? existingAccount.Name : account.Name
                };

                accounts.Remove(existingAccount);
                accounts.Add(updatedAccount);
            }

            _session.Set(_accountsSessionKey, accounts);
        }

        public void DeleteAccount(Guid id)
        {
            var existingAccount = GetAccount(id);

            if (existingAccount != null)
            {
                var accounts = GetAccounts().ToList();
                var existingAccountIndex = accounts.IndexOf(existingAccount) + 1;
                accounts.RemoveAt(existingAccountIndex);
                _session.Set(_accountsSessionKey, accounts);
            }
        }

        // Budgets
        public void UpsertBudget(Guid mastercategoryId, Guid subcategoryId, Budget budget)
        {
            var subcategory = GetSubCategory(subcategoryId);
            if(!subcategory.Budgets.Any(b => b.Month == budget.Month))
            {
                subcategory.Budgets.Add(budget);
                UpsertSubcategory(mastercategoryId, subcategory);
            }
            else
            {
                var existingBudget = subcategory.Budgets.SingleOrDefault(b => b.Month == budget.Month);
                var updatedBudget = new Budget()
                {
                    Balance = budget.Balance,
                    Month = budget.Month
                };
                subcategory.Budgets.Remove(existingBudget);
                subcategory.Budgets.Add(updatedBudget);
                UpsertSubcategory(mastercategoryId, subcategory);
            }
        }
    }
}
