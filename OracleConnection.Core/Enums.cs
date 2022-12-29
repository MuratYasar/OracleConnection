using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnection.Core
{
    public class Enums
    {
        public enum ConnectionCode : int
        {
            A = 1,
            B = 2,
            C = 3
        }

        public enum DBEnvironment : int
        {
            DEVELOPMENT = 1,
            TEST = 2,
            PRODUCTION = 3
        }

        public enum LOGLEVEL : int
        {
            TRACE = 1,
            DEBUG = 2,
            INFO = 3,
            SUCCESS = 4,
            WARN = 5,
            ERROR = 6,
            FATAL = 7
        }
    }
}
