using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesExporter.Models.Db
{
    public class Product
    {
        private ICollection<SaleProduct> saleProducts;

        public Product()
        {
            this.saleProducts = new HashSet<SaleProduct>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<SaleProduct> SaleProduct
        {
            get { return this.saleProducts; }
            set { this.saleProducts = value; }
        }
    }
}
