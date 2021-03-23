using System;
using MediatR;

namespace Bank.Api.Commands
{
    public class CalculateIncomeCommand : IRequest
    {
        public DateTime ForDate { get; set; }
        public double InterestRate { get; set; }
    }
}
