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

            return transactions;
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
        public IEnumerable<Category> GetCategories()
        {
            var categories = _session.Get<List<Category>>(_categoriesSessionKey);

            return categories;
        }

        public Category GetCategory(Guid id)
        {
            return GetCategories().SingleOrDefault(c => c.Id == id);
        }

        public void UpsertCategory(Category category)
        {
            var categories = GetCategories() == null ? new List<Category>() : GetCategories().ToList();

            if (!categories.Any(t => t.Id == category.Id))
            {
                categories.Add(category);
            }
            else
            {
                var existingCategory = categories.SingleOrDefault(t => t.Id == category.Id);
                var updatedCategory = new Category()
                {
                    Id = existingCategory.Id,
                    Name = string.IsNullOrWhiteSpace(category.Name) ? existingCategory.Name : category.Name
                };

                categories.Remove(existingCategory);
                categories.Add(updatedCategory);
            }

            _session.Set(_categoriesSessionKey, categories);
        }

        public void DeleteCategory(Guid id)
        {
            var existingCategory = GetCategory(id);

            if (existingCategory != null)
            {
                var categories = GetCategories().ToList();
                var existingCategoryIndex = categories.IndexOf(existingCategory) + 1;
                categories.RemoveAt(existingCategoryIndex);
                _session.Set(_categoriesSessionKey, categories);
            }
        }

        public void SetCategories(IEnumerable<Category> categories)
        {
            foreach (var category in categories)
            {
                UpsertCategory(category);
            }
        }

        // Import/export dao
        public BudgetDao Export()
        {
            return new BudgetDao()
            {
                Categories = GetCategories(),
                Transactions = GetTransactions()
            };
        }

        public void Import(BudgetDao budget)
        {
            throw new NotImplementedException();
        }
    }
}
