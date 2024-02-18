using Microsoft.AspNetCore.Mvc;
using Pro_Tech_Web.ModifiedString.GetRandomNumb;
using Pro_Tech_Web.ModifiedString.Sorting;
using Pro_Tech_Web.ModifiedString.StringOper;
using System.Text.RegularExpressions;

namespace Pro_Tech_Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public Controller(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("process")]
        public async Task<IActionResult> ProcessString([FromQuery] string inputString)
        {
            AppSettings appSettings = _configuration.GetSection("Settings").Get<AppSettings>();
            try
            {
                if (string.IsNullOrWhiteSpace(inputString)) 
                {
                    return BadRequest("HTTP ошибка 400 Bad Request. Invalid input string.");
                }
                if (!Regex.IsMatch(inputString, "^[a-z]+$")) 
                {
                    return BadRequest("HTTP ошибка 400 Bad Request. Invalid input string.");
                }
                if (appSettings?.BlackList?.Contains(inputString) == true)
                {
                    return BadRequest($"HTTP ошибка 400 Bad Request. Строка находится в Чёрном списке.");
                }
                ModString mod = new ModString(inputString);
                RandomNumber rnd = new RandomNumber(_configuration);
                Sort sort = new Sort();
                var result = new
                {
                    InputString = inputString,
                    ModidString = mod.StringSplit(),
                    Count = mod.CountPrint(mod.modifiedString),
                    MaxSubString = mod.FindLargestVowelSubstring(mod.modifiedString),
                    TrimString = rnd.RemoveChar(inputString),
                    SortString = sort.SortString(inputString)
                };
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest($"HTTP ошибка 400 Bad Request. Ошибка: {ex.Message}");
            }
        }
    }
}
