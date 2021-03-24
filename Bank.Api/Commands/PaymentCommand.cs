using System.ComponentModel.DataAnnotations;
using Bank.Api.DTOs;
using Bank.Domain;

namespace Bank.Api.Commands
{
    public sealed class PaymentCommand : AccountCommand<Account>
    {   
        [Display(Name = "Boleto")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public InvoiceDto Invoice { get; set;  }

    }
}
