using Bank.Domain.SeedWork;
using Bank.Domain.SeedWork;
using Bank.Domain.SeedWork;
using System.Collections.Generic;

namespace Bank.Domain
{
    public class Account : Entity, IAccount, IPaybleAccount
    {
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
            :base()
        {
            
        }

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
            this.AddDomainEvent(new DepositedAmountEvent(amount, this));
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
            this.AddDomainEvent(new WithdrawnAmountEvent(amount));
            return result;
        }      

        public bool CanCharge(Invoice invoice) =>
            IsValidOperation(invoice.Amount).Success;          

        public void ChargePayment(Invoice invoice)
        {   
            this.Balance -= invoice.Amount;
        }
    }
}