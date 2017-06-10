using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SalesExporter.Models.Composition;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SalesExporter.ExcelExporter
{
    public static class Exporter
    {
        // Not very beautifiul but time is money :(
        public static void ExportEntries(IEnumerable<SaleExport> entries)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            PropertyInfo[] properties = typeof(SaleExport).GetProperties();
            string[] fields = new string[(properties.Length - 1)];

            for (int i = 0; i < properties.Count(); i++)
            {
                var name = properties[i].Name.Replace("_", " ");
                if (name != "Products")
                {
                    fields[i] = name;
                }
            }

            // Populate the first row
            IRow row = sheet1.CreateRow(0);
            for (int i = 0; i < fields.Length; i++)
            {
                row.CreateCell(i).SetCellValue(fields[i]);
            }

            var currentRow = 1;
            foreach (var entry in entries)
            {
                IRow excelRow = sheet1.CreateRow(currentRow);

                for (int j = 0; j < fields.Length; j++)
                {
                    excelRow.CreateCell(j).SetCellValue(properties[j].GetValue(entry, null).ToString());
                }
                currentRow++;

                foreach (var product in entry.Products)
                {
                    IRow productRow = sheet1.CreateRow(currentRow);

                    productRow.CreateCell(0).SetCellValue("*");
                    productRow.CreateCell(1).SetCellValue(product.Name);
                    productRow.CreateCell(2).SetCellValue(product.Count);
                    productRow.CreateCell(3).SetCellValue(product.Price.ToString());
                    currentRow++;
                }

            }

            FileStream sw = File.Create("test.xlsx");

            workbook.Write(sw);

            sw.Close();
        }
    }
}
