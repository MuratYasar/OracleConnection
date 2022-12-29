using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnection.Core.Dtos
{
    public class Response
    {
        public DataTable ReturnObject { get; set; }

        public string Error { get; set; }
    }
}
