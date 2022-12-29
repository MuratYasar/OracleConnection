using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OracleConnection.Core.Enums;

namespace OracleConnection.Core
{
    internal static class AppConfig
    {
        /// <summary>
        /// If ConnectionCode is not provided then default ConnectionA will be returned.
        /// If DBEnvironment is not provided then development environment will be returned as default.
        /// </summary>
        /// <param name="connectionCode"></param>
        /// <param name="dBEnvironment"></param>
        /// <returns></returns>
        internal static string GetConnectionString(ConnectionCode connectionCode, DBEnvironment dBEnvironment)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "oracleconnection.core.json"), optional: false, reloadOnChange: true)
                .Build();

            switch (connectionCode)
            {
                case ConnectionCode.A:

                    switch (dBEnvironment)
                    {
                        case DBEnvironment.DEVELOPMENT:
                            return configuration.GetSection($"ConnectionConfiguration:ConnectionA.Development").Value ?? String.Empty;
                        case DBEnvironment.TEST:
                            return configuration.GetSection($"ConnectionConfiguration:ConnectionA.Test").Value ?? String.Empty;
                        case DBEnvironment.PRODUCTION:
                            return configuration.GetSection($"ConnectionConfiguration:ConnectionA.Production").Value ?? String.Empty;
                        default:
                            return configuration.GetSection($"ConnectionConfiguration:ConnectionA.Development").Value ?? String.Empty;
                    }

                case ConnectionCode.B:

                    switch (dBEnvironment)
                    {
                        case DBEnvironment.DEVELOPMENT:
                            return configuration.GetSection($"ConnectionConfiguration:ConnectionB.Development").Value ?? String.Empty;
                        case DBEnvironment.TEST:
                            return configuration.GetSection($"ConnectionConfiguration:ConnectionB.Test").Value ?? String.Empty;
                        case DBEnvironment.PRODUCTION:
                            return configuration.GetSection($"ConnectionConfiguration:ConnectionB.Production").Value ?? String.Empty;
                        default:
                            return configuration.GetSection($"ConnectionConfiguration:ConnectionB.Development").Value ?? String.Empty;
                    }

                case ConnectionCode.C:

                    switch (dBEnvironment)
                    {
                        case DBEnvironment.DEVELOPMENT:
                            return configuration.GetSection($"ConnectionConfiguration:ConnectionC.Development").Value ?? String.Empty;
                        case DBEnvironment.TEST:
                            return configuration.GetSection($"ConnectionConfiguration:ConnectionC.Test").Value ?? String.Empty;
                        case DBEnvironment.PRODUCTION:
                            return configuration.GetSection($"ConnectionConfiguration:ConnectionC.Production").Value ?? String.Empty;
                        default:
                            return configuration.GetSection($"ConnectionConfiguration:ConnectionC.Development").Value ?? String.Empty;
                    }

                default:
                    return configuration.GetSection($"ConnectionConfiguration:ConnectionA.Development").Value ?? String.Empty;
            }
        }
    }
}
