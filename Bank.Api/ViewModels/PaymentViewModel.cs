using Bank.Api.DTOs;

namespace Bank.Api.ViewModels
{
    public struct PaymentViewModel
    {
        public int AccountNo { get; set; }
        public InvoiceDto Invoice { get; set; }
    }
}
