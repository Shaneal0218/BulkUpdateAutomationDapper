using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkUpdateAPIExample.DTO;
using BulkUpdateAPIExample.Models;

namespace BulkUpdateAPIExample.Repository.IRepository
{
    public interface IBulkUpdateRepository
    {
        List<string> GetTableNames();
        public List<ColumnDTO> GetColumnNames(string tname);
        public void BulkUpdate(List<ValuePairs> table);
        public void RowUpdate(List<ValuePairs> table);
        public List<object> GetData(string tname);
    }
}
