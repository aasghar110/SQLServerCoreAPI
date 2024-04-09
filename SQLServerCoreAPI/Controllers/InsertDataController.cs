using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SQLServerCoreAPI.Controllers
{
    [Route("API/InsertData")]
    [ApiController]
    public class InsertDataController : ControllerBase
    {
        private readonly IConfiguration _config;

        public InsertDataController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult InsertData([FromBody] StoredProcedureRequest request)
        {
            try
            {
                string _connectionString = _config.GetConnectionString("ConnectionString");

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(request.SPName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (request.Parameters != null && request.Values != null)
                        {
                            for (int i = 0; i < request.Parameters.Count; i++)
                            {
                                if (request.Parameters[i] != null && request.Values[i] != null)
                                {
                                    command.Parameters.AddWithValue(request.Parameters[i], request.Values[i]);
                                }
                            }
                        }

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Return success message in JSON format
                            return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Stored procedure executed successfully." });
                        }
                        else
                        {
                            // No rows affected, return error
                            return StatusCode(StatusCodes.Status400BadRequest, new { StatusCode = StatusCodes.Status400BadRequest, Message = "Stored Procedure Executed Perfectly but No Rows Affected" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Return error message in JSON format
                return StatusCode(StatusCodes.Status500InternalServerError, new { StatusCode = StatusCodes.Status500InternalServerError, Message = $"Error executing stored procedure: {ex.Message}" });
            }
        }

    }

    public class StoredProcedureRequest
    {
        public string SPName { get; set; } = "";
        public List<string>? Parameters { get; set; }
        public List<string>? Values { get; set; }
    }
}
