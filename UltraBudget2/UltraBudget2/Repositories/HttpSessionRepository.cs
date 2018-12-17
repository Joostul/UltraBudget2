using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using UltraBudget2.Extensions;
using UltraBudget2.Models;

namespace UltraBudget2.Repositories
{
    public class HttpSessionRepository : IBudgetRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public HttpSessionRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

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

            _session.Set("transactions", transactions);
        }

        public void DeleteTransaction(Guid id)
        {
            var existingTransaction = GetTransaction(id);

            if(existingTransaction != null)
            {
                var transactions = GetTransactions().ToList();
                var existingTransactionIndex = transactions.IndexOf(existingTransaction) + 1;
                transactions.RemoveAt(existingTransactionIndex);
                _session.Set("transactions", transactions);
            }
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            var transactions = _session.Get<List<Transaction>>("transactions");

            return transactions;
        }

        public Transaction GetTransaction(Guid id)
        {
            return GetTransactions().SingleOrDefault(t => t.Id == id);
        }
    }
}
