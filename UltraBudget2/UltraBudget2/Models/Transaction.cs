﻿using System;
using System.ComponentModel.DataAnnotations;

namespace UltraBudget2.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
        [DataType(DataType.Text)]
        public string Category { get; set; }
        [DataType(DataType.Text)]
        public string Payee { get; set; }
    }
}