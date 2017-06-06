using SalesExporter.Data;
using SalesExporter.Data.Migrations;
using SalesExporter.ExcelExporter;
using SalesExporter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SalesExporter.Start
{
    public class Startup
    {
        static void Main()
        {
            var list = new List<Product>() { new Product() { Name = "123" }, new Product() { Name = "789" }, new Product() { Name = "456" } };
            Exporter.ExportEntries(list);

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesExporterDbContext, Configuration>());

            //var db = new SalesExporterDbContext();
            //db.Database.CreateIfNotExists();

            //var bigClient = new Client()
            //{
            //    Name = "Some big client"
            //};

            //var specialProduct = new Product()
            //{
            //    Name = "Fancy something",
            //    Price = 1M,
            //    Quantity = 1000
            //};

            //var soldProduct = new SaleProduct()
            //{
            //    Product = specialProduct,
            //    Count = 2,
            //    Price = 2M
            //};

            //var firstSale = new Sale()
            //{
            //    Client = bigClient,
            //    SaleProducts = new List<SaleProduct>() { soldProduct },
            //    TotalPrice = 2M
            //};

            //db.Client.Add(bigClient);
            //db.Product.Add(specialProduct);
            //db.SaleProduct.Add(soldProduct);
            //db.Sale.Add(firstSale);

            //Console.WriteLine("Db entities added successfully!");
            //db.SaveChanges();
        }
    }
}
