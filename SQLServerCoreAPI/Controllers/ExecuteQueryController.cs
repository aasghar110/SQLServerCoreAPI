using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SQLServerCoreAPI.Controllers
{
    [Route("API/ExecuteQuery")]
    [ApiController]
    public class ExecuteQueryController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ExecuteQueryController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult ExecuteQuery([FromBody] QueryModel queryModel)
        {
            string _connectionString = _config.GetConnectionString("ConnectionString");

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(queryModel.Query, connection))
                    {
                        if (queryModel.QueryType == QueryType.Select)
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                var result = ConvertDataTableToList(reader);
                                return Ok(result);
                            }
                        }
                        else if (queryModel.QueryType == QueryType.Insert)
                        {
                            var affectedRows = command.ExecuteNonQuery();
                            return Ok($"Inserted {affectedRows} rows successfully.");
                        }
                        else
                        {
                            return BadRequest("Invalid query type.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Database operation failed: {ex.Message}");
                }
            }
        }

        private List<Dictionary<string, object>> ConvertDataTableToList(SqlDataReader reader)
        {
            var result = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                var dict = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dict[reader.GetName(i)] = reader[i];
                }
                result.Add(dict);
            }
            return result;
        }

        public enum QueryType
        {
            Select,
            Insert
        }

        public class QueryModel
        {
            public string Query { get; set; }
            public QueryType QueryType { get; set; }
        }
    }
}
