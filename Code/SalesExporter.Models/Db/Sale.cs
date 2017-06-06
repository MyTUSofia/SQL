using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesExporter.Models.Db
{
    public class Sale
    {
        private ICollection<SaleProduct> products;

        public Sale()
        {
            this.products = new HashSet<SaleProduct>();
        }

        [Key]
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<SaleProduct> SaleProducts
        {
            get { return this.products; }
            set { this.products = value; }
        }

        public DateTime? ExportedOn { get; set; }

        public bool IsExported { get; set; }
    }
}
