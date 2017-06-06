using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SalesExporter.ExcelExporter
{
    public static class Exporter
    {
        public static void ExportEntries<T>(IEnumerable<T> entries)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            PropertyInfo[] properties = typeof(T).GetProperties();
            string[] fields = new string[properties.Length];

            for (int i = 0; i < properties.Count(); i++)
            {
                fields[i] = properties[i].Name.Replace("_", " ");
            }

            // Populate the first row
            IRow row = sheet1.CreateRow(0);
            for (int i = 0; i < fields.Length; i++)
            {
                row.CreateCell(i).SetCellValue(fields[i]);
            }
            entries.ElementAt(0);
            // Populate data from entries
            for (int i = 0; i < entries.Count(); i++)
            {
                IRow row2 = sheet1.CreateRow((i + 1));

                for (int j = 0; j < fields.Length; j++)
                {
                    row2.CreateCell(j).SetCellValue(properties[j].GetValue(entries.ElementAt(i), null).ToString());
                }
            }

            FileStream sw = File.Create("test.xlsx");

            workbook.Write(sw);

            sw.Close();
        }
    }
}
