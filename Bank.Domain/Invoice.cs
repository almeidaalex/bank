using System;

namespace Bank.Domain
{
    public struct Invoice
    {
        public Invoice(int number, DateTime dueDate, decimal amount)
        {
            Number = number;
            DueDate = dueDate;
            Amount = amount;
        }

        public int Number { get; }
        public DateTime DueDate { get; }
        public decimal Amount { get; }
    }
}
