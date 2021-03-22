using System;
using Bank.Domain;

namespace Bank.Api.DTOs
{
    public class AccountStatementDto
    {
        public AccountStatementDto(DateTime date, string operation, decimal amount, string description)
        {
            Date = date;
            Operation = operation;
            Amount = amount;
            Description = description;
        }

        public DateTime Date { get; }
        public string Operation { get; }
        public decimal Amount { get; }
        public string Description { get; }

        public static implicit operator AccountStatementDto(AccountHistory entity)
        {
            return new(entity.Date, entity.Operation.ToString(), entity.Amount, entity.Description);
        }
    }
}
