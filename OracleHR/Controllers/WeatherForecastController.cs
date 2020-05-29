using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using OracleHR.Model;

namespace OracleHR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private IConfiguration _config;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            string oradb = _config.GetValue<string>("ORACLE");
            try
            {
                using (var conn = new OracleConnection(oradb))
                {
                    conn.Open();
                    var recs = conn.Query<Employee>("SELECT * from EMPLOYEES");
                    return recs;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return null;
        }
    }
}
