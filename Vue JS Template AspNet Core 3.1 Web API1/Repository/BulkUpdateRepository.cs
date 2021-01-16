using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using BulkUpdateAPIExample.Repository.IRepository;
using System.Collections.Generic;
using BulkUpdateAPIExample.Models;
using Dapper;
using System.Linq;
using BulkUpdateAPIExample.DTO;
using System.Reflection;

namespace BulkUpdateAPIExample.Repository
{
    public class BulkUpdateRepository : IBulkUpdateRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _config;
        public BulkUpdateRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    
        public List<string> GetTableNames()
        {
            using (IDbConnection connection = GetConnection())
            {
                string sql = @"SELECT TABLE_NAME
                               FROM INFORMATION_SCHEMA.TABLES
                               WHERE TABLE_TYPE = 'BASE TABLE'";

                List<string> names = connection.Query<string>(sql).ToList();
                return names;
            }
        }

        public List<string> GetColumnNames(string tname)
        {
            using (IDbConnection connection = GetConnection())
            {
                string sql = @"SELECT COLUMN_NAME
                               FROM INFORMATION_SCHEMA.COLUMNS
                               WHERE TABLE_NAME = @tname";

                List<string> names = connection.Query<string>(sql, new { tname = tname}).ToList();
                return names;
            }
        }
        public void BulkUpdate(List<ValuePairs> table)
        {
            using (IDbConnection connection = GetConnection())
            {
                string sql = CreateSQLQueryStringToSearchProducts(table);
                connection.Execute(sql);
            }
        }

        public string CreateSQLQueryStringToSearchProducts(List<ValuePairs> table)
        {
            var tname = table[0].Value;
            var first = false;
            string sql = @"UPDATE " + tname;

            for (var i = 1; i < table.Count(); i++)
            {
                var name = table[i].Property;
                var value = table[i].Value;
                if (first == false)
                {
                    sql = sql + " SET " + name + " = '" + value + "'";
                    first = true;
                }
                else
                {
                    sql = sql + ", " + name + " = '" + value + "'";
                }
            }
            return sql;
        }
    }
}
