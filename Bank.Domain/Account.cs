using Bank.Domain.Contracts;
using Bank.Domain.Events;
using System.Collections.Generic;

namespace Bank.Domain
{
    public class Account : IAccount, IEntity
    {
        private readonly HashSet<IDomainEvent> _events;

        public Account(int accountNo, decimal initialBalance)
            :this(accountNo)
        {
            this.Balance = initialBalance;
        }

        public Account(int accountNo)
            :this()
        {            
            No = accountNo;
        }
        protected Account()
        {
            _events = new HashSet<IDomainEvent>();
        }

        public IReadOnlyCollection<IDomainEvent> Events => _events;

        public Owner Owner { get; }

        public int No { get; }

        public decimal Balance { get; private set; } = 0;


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
            this.AddDomainEvent(new DepositedAmount(amount, this));
            return Result.Ok();
        }

        public void Monetize(decimal rate)
        {
            this.Balance *= rate/100 + 1;
        }

        public Result Withdraw(decimal amount)
        {
            var result = IsValidOperation(amount);
            if (result.Success)
            {
                this.Balance -= amount;
                return Result.Ok();
            }
            this.AddDomainEvent(new WithdrawnAmount(amount));
            return result;
        }

        public void AddDomainEvent(IDomainEvent domainEvent) =>        
            _events.Add(domainEvent);

        public void ClearEvents() =>
            _events.Clear();            
        
    }
}