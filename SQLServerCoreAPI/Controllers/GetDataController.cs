using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace SQLServerCoreAPI.Controllers
{
    [Route("API/GetData")]
    [ApiController]
    public class GetDataController : ControllerBase
    {
        private readonly IConfiguration _config;

        public GetDataController(IConfiguration config)
        {
            _config = config;
        }


        [HttpPost]
        public IActionResult ExecuteStoredProcedure([FromBody] JsonDocument request)
        {
            try
            {
                string _connectionString = _config.GetConnectionString("ConnectionString");

                if (request.RootElement.ValueKind != JsonValueKind.Object)
                    return BadRequest("Invalid JSON request.");

                var spName = request.RootElement.GetProperty("SPName").GetString();
                var paramNames = request.RootElement.GetProperty("Parameters").EnumerateArray();
                var paramValues = request.RootElement.GetProperty("Values").EnumerateArray();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(spName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        while (paramNames.MoveNext() && paramValues.MoveNext())
                        {
                            command.Parameters.AddWithValue(paramNames.Current.GetString(), paramValues.Current.ToString());
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);

                            var result = new List<Dictionary<string, object>>();
                            foreach (DataRow row in dataTable.Rows)
                            {
                                var dict = new Dictionary<string, object>();
                                foreach (DataColumn col in dataTable.Columns)
                                {
                                    dict[col.ColumnName] = row[col];
                                }
                                result.Add(dict);
                            }

                            return Ok(result);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
