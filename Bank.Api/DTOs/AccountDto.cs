using System.Collections.Generic;
using System.Linq;

using Bank.Domain;

namespace Bank.Api.DTOs;

public record AccountDto(int AccountNo, string Title, decimal Balance, IEnumerable<AccountStatementDto> Statements)
{
  public static implicit operator AccountDto(Account entity)
  {
    var stats = entity.Operations.Select(operation => (AccountStatementDto)operation);
    return new(entity.No, entity.Owner?.Title, entity.Balance, stats);
  }
}
