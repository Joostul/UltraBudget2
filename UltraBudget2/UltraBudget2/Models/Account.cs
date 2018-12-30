using System;
using System.ComponentModel.DataAnnotations;

namespace UltraBudget2.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public AccountType Type { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
    }
}
