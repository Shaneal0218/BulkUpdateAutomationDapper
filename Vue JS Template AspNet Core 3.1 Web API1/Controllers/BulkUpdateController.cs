using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BulkUpdateAPIExample.DTO;
using BulkUpdateAPIExample.Models;
using BulkUpdateAPIExample.Repository.IRepository;

namespace BulkUpdateAPIExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BulkUpdateController : ControllerBase
    {
        private readonly IBulkUpdateRepository _repo;

        public BulkUpdateController(IBulkUpdateRepository repo)
        {
            _repo = repo;
        }

        [Route("getTableNames")]
        [HttpGet]
        public IActionResult GetTableNames()
        {
            List<string> names =_repo.GetTableNames();
            return Ok(names);
        }

        [Route("getColumnNames/{tname}")]
        [HttpGet]
        public IActionResult GetColumnNames(string tname)
        {
            List<ColumnDTO> columns = _repo.GetColumnNames(tname);
            return Ok(columns);
        }
        [Route("bulkUpdate")]
        [HttpPost]
        public IActionResult BulkUpdate(List<ValuePairs> values)
        {
            
            _repo.BulkUpdate(values);
            return Ok(StatusCode(200));
        }
        [Route("rowUpdate")]
        [HttpPost]
        public IActionResult RowUpdate(List<ValuePairs> values)
        {

            _repo.RowUpdate(values);
            return Ok(StatusCode(200));
        }
        [Route("getData/{tname}")]
        [HttpGet]
        public IActionResult GetData(string tname)
        {

            List<object> data = _repo.GetData(tname);
            return Ok(data);
        }
    }
}
