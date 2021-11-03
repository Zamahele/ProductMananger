using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Product
{
    public class ExcelData
    {
        public List<Product> ProductExcelData(string fileName)
        {
            var products = new List<Product>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductCode = reader.GetValue(0).ToString(),
                            Name = reader.GetValue(1).ToString(),
                            Description = reader.GetValue(2).ToString(),
                            Price = Convert.ToDecimal(reader.GetValue(4))
                        });
                    }
                }
            }
            return products;
        }


    }
}