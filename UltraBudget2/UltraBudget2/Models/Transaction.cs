using System;
using System.ComponentModel.DataAnnotations;

namespace UltraBudget2.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateTime { get; set; }
        [DataType(DataType.Text)]
        [Required]
        public string Category { get; set; }
        [DataType(DataType.Text)]
        public string Payee { get; set; }
        [DataType(DataType.Text)]
        [Required]
        public string Account { get; set; }
    }
}
