using System;
using Bank.Domain.SeedWork;

namespace Bank.Domain
{
    public class AccountHistory 
    {
        private AccountHistory()            
        { 
        }

        public AccountHistory(DateTime date, string description, decimal amount, EventType operation)            
        {   
            Date = date;
            Description = description;
            Amount = amount;
            Operation = operation;
        }

        public int Id { get;  }
        public DateTime Date { get; }
        public string Description { get; }
        public decimal Amount { get; }
        public EventType Operation { get; }
        public int AccountNo { get; }
    }
}
