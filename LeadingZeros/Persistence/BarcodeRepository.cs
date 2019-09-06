using LeadingZeros.Helper;
using System.Data;
using System.Data.SqlClient;

namespace LeadingZeros.Persistence
{
    public class BarcodeRepository
    {
        private QueryInfo _query;
        public BarcodeRepository()
        {
            _query = new QueryInfo();
        }
        public DataTable GetBarcodeData()
        {
            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(_query.GetSourceConnectionString());
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select SeqID,UsageCode,char(9)+BarcodeNo as BarcodeNo from Garments", connection);
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            adapt.Fill(table);
            connection.Close();
            return table;
        }
    }
}
