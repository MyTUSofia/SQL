using SalesExporter.Data;
using SalesExporter.Data.Migrations;
using SalesExporter.ExcelExporter;
using SalesExporter.Models.Composition;
using SalesExporter.Models.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Timers;
using System.Transactions;

namespace SalesExporter.Start
{
    public class Startup
    {
        static void Main()
        {
            Timer t = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds);
            t.AutoReset = true;
            t.Elapsed += new ElapsedEventHandler(DelayedExport);
            t.Start();

            while (true)
            {
            }
        }

        public static void DelayedExport(object sender, ElapsedEventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesExporterDbContext, Configuration>());

            // ThreadSafety via different instance of EF
            using (var db = new SalesExporterDbContext())
            {
                db.Database.CreateIfNotExists();
                PrepareExport(db, null);
            }
        }

        public static void PrepareExport(SalesExporterDbContext context, DateTime? exportDate)
        {
            var exportItems = new List<SaleExport>();

            using (TransactionScope scope = new TransactionScope())
            {
                IQueryable<Sale> filteredData = context.Sale.Where(s => s.IsExported == false);

                if (exportDate != null)
                {
                    filteredData = context.Sale.Where(s => DateTime.Compare((DateTime)s.ExportedOn, (DateTime)exportDate) >= 0);
                }

                filteredData
                    .ToList()
                    .ForEach(s =>
                    {
                        var dateNow = DateTime.UtcNow;

                        exportItems.Add(new SaleExport
                        {
                            ClientName = s.Client.Name,
                            ExportedOn = dateNow,
                            ProductsCount = s.SaleProducts.Count,
                            TotalPrice = s.TotalPrice
                        });

                        s.IsExported = true;
                        s.ExportedOn = dateNow;
                    });

                context.SaveChanges();

                try
                {
                    if (exportItems.Count > 0)
                    {
                        Exporter.ExportEntries(exportItems);
                    }
                    else
                    {
                        Console.WriteLine("There are no records which meets condition!");
                    }
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    return;
                }


                scope.Complete();
            }
        }

        public static void PopulateSomeData()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesExporterDbContext, Configuration>());

            var db = new SalesExporterDbContext();
            db.Database.CreateIfNotExists();

            var bigClient = new Client()
            {
                Name = "Some big client"
            };

            var specialProduct = new Product()
            {
                Name = "Fancy something",
                Price = 1M,
                Quantity = 1000
            };

            var soldProduct = new SaleProduct()
            {
                Product = specialProduct,
                Count = 2,
                Price = 2M
            };

            var firstSale = new Sale()
            {
                Client = bigClient,
                SaleProducts = new List<SaleProduct>() { soldProduct },
                TotalPrice = 2M
            };

            db.Client.Add(bigClient);
            db.Product.Add(specialProduct);
            db.SaleProduct.Add(soldProduct);
            db.Sale.Add(firstSale);

            Console.WriteLine("Db entities added successfully!");
            db.SaveChanges();
        }
    }
}
