namespace HugoTorrico.Models
{
    public class Detail
    {
        public int DetailId { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public decimal Subtotal { get; set; }

        public bool Active { get; set; }

        public Invoice Invoice { get; set; }

        public int InvoiceId { get; set; }

        public Product Product { get; set; }    

        public int ProductId { get; set; }


    }
}
