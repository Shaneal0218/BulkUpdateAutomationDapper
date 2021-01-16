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
        public List<string> GetColumnNames(string tname);
        public void BulkUpdate(List<ValuePairs> table);
    }
}
