using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleConnection.Core.Models
{
    [Table(name: "DBLog", Schema = "Test")]
    public class DBLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public Int64 Id { get; set; }

        [Column("DATE")]
        public DateTime LogTime { get; set; } = DateTime.Now;

        [Column("METHODNAME")]
        public string? MethodName { get; set; }

        [Column("PARAMETER")]
        public string? Parameter { get; set; }

        /// <summary>
        /// TRACE, DEBUG, INFO, SUCCESS, WARN, ERROR, FATAL
        /// </summary>
        [Column("LEVEL")]
        public string? Level { get; set; }

        [Column("MESSAGE")]
        public string? Message { get; set; }
    }
}
