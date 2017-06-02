using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesExporter.Models
{
    public class Client
    {
        private ICollection<Sale> sales;

        public Client()
        {
            this.sales = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Sale> Sales
        {
            get { return this.sales; }
            set { this.sales = value; }
        }
    }
}
