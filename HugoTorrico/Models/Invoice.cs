namespace HugoTorrico.Models
{
    public class Invoice
    {

        public int InvoiceId { get; set; }

        public DateOnly DateTime { get; set; }  

        public String InvoiceNumber { get; set; }

        public decimal Total { get; set; }

        public bool Active { get; set; }

        public Customer Customer { get; set; }

        public int CustomerId { get; set; }

    }
}
