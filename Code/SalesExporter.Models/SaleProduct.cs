using System.ComponentModel.DataAnnotations;

namespace SalesExporter.Models
{
    public class SaleProduct
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int SaleId { get; set; }

        public virtual Sale Sale { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }
    }
}
