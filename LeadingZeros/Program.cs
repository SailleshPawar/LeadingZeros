using LeadingZeros.Helper;
using LeadingZeros.Models;
using LeadingZeros.Persistence;
using System.Collections.Generic;
using System.Data;

namespace LeadingZeros
{

    public class EntityOperationResult
    {
        public string SeqID { get; set; }
        public string UsageCode { get; set; }

        public string BarcodeNo { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BarcodeRepository repository = new BarcodeRepository();
            var records = repository.GetBarcodeData();
                var results = new List<EntityOperationResult>();
                foreach (DataRow row in records.Rows)
                {
                    var result = new EntityOperationResult
                    {
                        BarcodeNo = row["BarcodeNo"].ToString(),
                        SeqID = row["SeqID"].ToString(),
                        UsageCode= row["UsageCode"].ToString()
                    };
                    results.Add(result);
                }
            var csvData= Export_To_CSV.ToCsv(",", results);
            FileOperation fileOperation = new FileOperation();
            fileOperation.SaveFile(csvData, @"D:\TipOFTheDay", "xyz");
        }
    }
}
