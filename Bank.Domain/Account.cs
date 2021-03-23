using System;
using System.Collections.Generic;
using Bank.Domain.Contracts;
using Bank.Domain.Events;
using Bank.Domain.SeedWork;

namespace Bank.Domain
{
    public sealed class Account : Entity, IAccount, IPaybleAccount, IYieldAccount
    {
        private readonly List<AccountOperation> _operations;

        public Account(Owner owner, int accountNo, decimal initialBalance)
            :this(accountNo)
        {
            this.Balance = initialBalance;
            this.Owner = owner;
            this.OwnerId = owner.Id;
        }

        public Account(int accountNo)
            :this()
        {            
            No = accountNo;            
        }
        private Account()
            :base()
        {
            _operations = new List<AccountOperation>();
        }

        public int OwnerId { get; }

        public Owner Owner { get; private set; }

        public int No { get; }

        public decimal Balance { get; internal set; } = 0;

        public Result IsValidOperation(decimal amount)
        {
            return Result.Combine(HasBalance(amount), ValidValue(amount));
        }

        private Result HasBalance(decimal amount)
        {
           return this.Balance >= amount 
                ? Result.Ok() 
                : Result.Fail("Não há saldo suficiente para saque");
        }

        private static Result ValidValue(decimal amount)
        {
            return amount > 0 
                ? Result.Ok() 
                : Result.Fail("O valor da operação deve ser maior que zero");
        }

        public Result Deposit(decimal amount)
        {
            if (amount <= 0)
                return Result.Fail($"Não é permitido depósito igual ou menor que zero, valor informado '{amount:n2}'");
            this.Balance += amount;
            this.AddDomainEvent(new DepositedAmountEvent(amount, this));
            return Result.Ok();
        }

        public Result Withdraw(decimal amount)
        {
            var result = IsValidOperation(amount);
            if (result.Success)
            {
                this.Balance -= amount;
                this.AddDomainEvent(new WithdrawnAmountEvent(this, amount));
                return Result.Ok();
            }            
            return result;
        }      

        public bool CanCharge(Invoice invoice) =>
            IsValidOperation(invoice.Amount).Success;          

        public void ChargePayment(Invoice invoice)
        {   
            this.Balance -= invoice.Amount;
            this.AddDomainEvent(new ChargedPaymentEvent(this, invoice));
        }

        public void AddOperation(AccountOperation accountOperation) =>        
            _operations.Add(accountOperation);
        
        public IReadOnlyCollection<AccountOperation> Operations => _operations;

        public DateTime? LastYieldedDate { get; private set; }

        public void SetBalance(decimal balance, DateTime currentDate)
        {
            this.Balance = balance;
            this.LastYieldedDate = currentDate;
        }
    }
}