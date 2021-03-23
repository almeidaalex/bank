using System;
using Bank.Domain.SeedWork;

namespace Bank.Domain
{
    public class AccountOperation 
    {
        private AccountOperation()            
        { 
        }

        public AccountOperation(DateTime date, string description, decimal amount, EventType operation)            
        {   
            Date = date;
            Description = description;
            Amount = amount;
            Operation = operation;
        }

        public int Id { get;  }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public EventType Operation { get; private set; }
        public int AccountNo { get; private set; }
    }
}
