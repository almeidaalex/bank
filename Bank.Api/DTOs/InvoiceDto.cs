using System;
using Bank.Domain;

namespace Bank.Api.DTOs
{
    public class InvoiceDto
    {
        public int Number { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DueDate { get; set; }
        
        public static implicit operator Invoice(InvoiceDto dto)
        {
            return new Invoice(dto.Number, dto.DueDate.GetValueOrDefault(), dto.Amount);
        }
    }
}
