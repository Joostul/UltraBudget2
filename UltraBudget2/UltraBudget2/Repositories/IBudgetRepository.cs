﻿using System;
using System.Collections.Generic;
using UltraBudget2.Models;

namespace UltraBudget2.Repositories
{
    public interface IBudgetRepository
    {
        IEnumerable<Transaction> GetTransactions();
        Transaction GetTransaction(Guid id);
        void UpsertTransaction(Transaction transaction);
        void DeleteTransaction(Guid id);

        IEnumerable<Category> GetCategories();
        Category GetCategory(Guid id);
        void UpsertCategory(Category category);
        void DeleteCategory(Guid id);
    }
}
