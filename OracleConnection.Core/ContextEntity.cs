using Microsoft.EntityFrameworkCore;
using OracleConnection.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OracleConnection.Core.Enums;

namespace OracleConnection.Core
{
    public class ContextEntity : DbContext
    {
        private readonly ConnectionCode _connectionCode;
        private readonly DBEnvironment _dBEnvironment;

        public ContextEntity(ConnectionCode connectionCode, DBEnvironment dBEnvironment)
        {
            _connectionCode = connectionCode;
            _dBEnvironment = dBEnvironment;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle(AppConfig.GetConnectionString(_connectionCode, _dBEnvironment));
        }


        public DbSet<DBLog> DBLog { get; set; }
    }
}
