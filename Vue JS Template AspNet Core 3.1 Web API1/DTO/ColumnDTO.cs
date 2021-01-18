using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkUpdateAPIExample.Models
{
    public class ColumnDTO
    {
        public string COLUMN_NAME { get; set; }
        public string IS_NULLABLE { get; set; }
        public string DATA_TYPE { get; set; }
        public int? CHARACTER_MAXIMUM_LENGTH { get; set; }
    }
}
