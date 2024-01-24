using System;
using System.ComponentModel.DataAnnotations;

using Bank.Domain;

namespace Bank.Api.DTOs
{
  public class InvoiceDto
  {
    const string REQUIRED_MSG = "O campo {0} é obrigatório";

    [Display(Name = "Número")]
    [Required(ErrorMessage = REQUIRED_MSG)]
    public int? Number { get; set; }

    [Display(Name = "Valor")]
    [Required(ErrorMessage = REQUIRED_MSG)]
    public decimal? Amount { get; set; }

    [Display(Name = "Data de Vencimento")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = REQUIRED_MSG)]
    public DateTime? DueDate { get; set; }

    public static implicit operator Invoice(InvoiceDto dto)
    {
      return new Invoice(dto.Number.GetValueOrDefault(), dto.DueDate.GetValueOrDefault(), dto.Amount.GetValueOrDefault());
    }
  }
}