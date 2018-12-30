using System;
using System.ComponentModel.DataAnnotations;

namespace UltraBudget2.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Category { get; set; }

        [DataType(DataType.Text)]
        public string Payee { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Account { get; set; }
    }
}
