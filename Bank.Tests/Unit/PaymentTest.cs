using Bank.Domain;

using FluentAssertions;

using Xunit;

namespace Bank.Tests.Unit
{
  public class PaymentTest
  {
    private readonly PaymentService _service;
    private readonly Owner _owner;
    public PaymentTest()
    {
      _service = new PaymentService();
      _owner = new Owner(1, "Alex A.");
    }

    [Fact]
    public void Should_be_able_to_pay_if_the_account_has_enough_balance()
    {
      var account = new Account(_owner, 1, 2000);
      var invoice = new Invoice(100, dueDate: new System.DateTime(2021, 03, 14), amount: 1400);
      var result = _service.Pay(account, invoice);
      result.Success.Should().BeTrue();
      account.Balance.Should().Be(600m);
    }

    [Fact]
    public void Should_not_be_allowed_to_pay_negative_number()
    {
      var account = new Account(_owner, 1, 2000);
      var invoice = new Invoice(100, dueDate: new System.DateTime(2021, 03, 14), amount: -1400);
      var result = _service.Pay(account, invoice);
      result.Failure.Should().BeTrue();
    }
  }
}