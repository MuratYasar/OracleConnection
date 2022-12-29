using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnection.Core.Log
{
    public interface ILoggerManager
    {
        void LogTrace(string message,   [CallerFilePath] string filePath = "",  [CallerMemberName] string memberName = "",  [CallerLineNumber] int lineNumber = 0);
        void LogDebug(string message,   [CallerFilePath] string filePath = "",  [CallerMemberName] string memberName = "",  [CallerLineNumber] int lineNumber = 0);
        void LogInfo(string message,    [CallerFilePath] string filePath = "",  [CallerMemberName] string memberName = "",  [CallerLineNumber] int lineNumber = 0);
        void LogSuccess(string message, [CallerFilePath] string filePath = "",  [CallerMemberName] string memberName = "",  [CallerLineNumber] int lineNumber = 0);
        void LogWarn(string message,    [CallerFilePath] string filePath = "",  [CallerMemberName] string memberName = "",  [CallerLineNumber] int lineNumber = 0);
        void LogError(string message,   [CallerFilePath] string filePath = "",  [CallerMemberName] string memberName = "",  [CallerLineNumber] int lineNumber = 0);
        void LogFatal(string message,   [CallerFilePath] string filePath = "",  [CallerMemberName] string memberName = "",  [CallerLineNumber] int lineNumber = 0);
    }
}
