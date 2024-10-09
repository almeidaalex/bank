using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Bank.Domain;

namespace Bank.Api.DTOs;



public record InvoiceDto(
  int Number,
  decimal Amount,
  DateTime DueDate
)
{

  public static implicit operator Invoice(InvoiceDto dto)
  {
    return new Invoice(dto.Number, dto.DueDate, dto.Amount);
  }
}
