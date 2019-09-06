using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadingZeros.Helper
{
   public class QueryInfo
    {
        public string SourceInstanceName => ConfigurationManager.AppSettings["SourceInstanceName"];
        public string SourceDatabaseName => ConfigurationManager.AppSettings["SourceDatabaseName"];
        public string SourceUsername => "";
        
        public string SourcePassword => "";
        public string GetSourceConnectionString()
        {
            var connStr = $"Server={SourceInstanceName};Database={SourceDatabaseName};User Id={SourceUsername};Password={SourcePassword};";
            return connStr;
        }
    }
}
