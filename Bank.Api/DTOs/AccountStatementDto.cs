using System;
using Bank.Domain;

namespace Bank.Api.DTOs
{
    public class AccountStatementDto
    {
        public AccountStatementDto(DateTime date, string operation, decimal amount, string description, int accountNo)
        {
            Date = date;
            Operation = operation;
            Amount = amount;
            Description = description;
            AccountNo = accountNo;
        }

        public int AccountNo { get; }
        public DateTime Date { get; }
        public string Operation { get; }
        public decimal Amount { get; }
        public string Description { get; }

        public static implicit operator AccountStatementDto(AccountOperation entity)
        {
            return new(entity.Date, entity.Operation.ToString(), entity.Amount, entity.Description, entity.AccountNo);
        }
    }
}
