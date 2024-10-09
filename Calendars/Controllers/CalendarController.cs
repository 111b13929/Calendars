using Dapper;
using Calendars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Calendars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly string _connectString = DBUtil.ConnectionString();

        [HttpGet]
        public async Task<IEnumerable<Calendar>> GetCalendars()
        {
            string sqlQuery = "SELECT * FROM MyCalendar";
            using (var connection = new SqlConnection(_connectString))
            {
                var Calendars = await connection.QueryAsync<Calendar>(sqlQuery);
                return Calendars.ToList();
            }
        }

        [HttpGet("{id}")]
        public async Task<Calendar> GetCalendar(int id)
        {
            string sqlQuery = "SELECT * FROM MyCalendar WHERE Cid = @Id";
            using (var connection = new SqlConnection(_connectString))
            {
                var Calendar = await connection.QueryFirstOrDefaultAsync<Calendar>(sqlQuery,
                new { Id = id });

                if (Calendar == null)
                {
                    return new Calendar();
                }
                return Calendar;
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddCalendar(Calendar Calendar)
        {
            string sqlQuery = "INSERT INTO MyCalendar (Cname, Cpriority, Cfinish) VALUES (@Cname, @Cpriority, @Cfinish)"; 
            using (var connection = new SqlConnection(_connectString))
            {
                await connection.ExecuteAsync(sqlQuery, Calendar);
                return Ok();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCalendar(Calendar Calendar)
        {
            string sqlQuery = "UPDATE MyCalendar SET Cname = @Cname, Cpriority = @Cpriority, Cfinish = @Cfinish , Cmemo = @Cmemo WHERE Cid = @Cid"; 
            using (var connection = new SqlConnection(_connectString))
            {
                await connection.ExecuteAsync(sqlQuery, Calendar);
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendar(int id)
        {
            string sqlQuery = "DELETE FROM MyCalendar WHERE Cid = @Id";
            using (var connection = new SqlConnection(_connectString))
            {
                await connection.ExecuteAsync(sqlQuery, new { Id = id });
                return Ok();
            }
        }

    }
}