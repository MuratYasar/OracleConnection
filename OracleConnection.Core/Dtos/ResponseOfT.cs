using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnection.Core.Dtos
{
    public class ResponseOfT<T>
    {
        public List<T> ReturnObject { get; set; }

        public string Error { get; set; }
    }
}
