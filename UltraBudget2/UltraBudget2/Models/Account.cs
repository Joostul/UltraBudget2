using System;

namespace UltraBudget2.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public int MyProperty { get; set; }
    }
}
