using SalesExporter.Models;
using System.Data.Entity;

namespace SalesExporter.Data
{
    public class SalesExporterDbContext : DbContext
    {
        public SalesExporterDbContext()
            : base("SalesExporterDatabase")
        {

        }

        public virtual IDbSet<Sale> Sale { get; set; }

        public virtual IDbSet<Client> Client { get; set; }

        public virtual IDbSet<Product> Product { get; set; }

        public virtual IDbSet<SaleProduct> SaleProduct { get; set; }
    }
}
