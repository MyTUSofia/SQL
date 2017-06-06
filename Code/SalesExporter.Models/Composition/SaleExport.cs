using System;

namespace SalesExporter.Models.Composition
{
    public class SaleExport
    {
        public string ClientName { get; set; }

        public DateTime ExportedOn { get; set; }

        public decimal TotalPrice { get; set; }

        public int ProductsCount { get; set; }
    }
}
