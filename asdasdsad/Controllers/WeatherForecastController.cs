using asdasdsad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Tasks;

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SemaphoreSlim _semaphore;

        public StringController(IConfiguration configuration)
        {
            _configuration = configuration;
            var parallelLimit = _configuration.GetValue<int>("iisSettings:ParallelLimit");
            _semaphore = new SemaphoreSlim(parallelLimit, parallelLimit);
        }

        [HttpGet("process")]
        public async Task<IActionResult> ProcessString([FromQuery] string inputString)
        {
            if (!_semaphore.Wait(0))
            {
                // Если лимит параллельных запросов превышен, возвращаем ошибку 503
                return StatusCode(503, "Service Unavailable. The server is currently busy.");
            }
            try
            {
                string[] blacklist = _configuration.GetSection("iisSettings:BlackList").Get<string[]>(); // Перенос черного списка из конфигуратора в массив

                if (string.IsNullOrWhiteSpace(inputString)) // Проверка на пустую строку и наличие пробелов
                {
                    return BadRequest("HTTP ошибка 400 Bad Request. Invalid input string.");
                }
                if (!Regex.IsMatch(inputString, "^[a-z]+$")) // Проверка на наличие не английских символов
                {
                    return BadRequest("HTTP ошибка 400 Bad Request. Invalid input string.");
                }
                if (blacklist.Contains(inputString)) // Проверка на наличие слов в черном списке
                {
                    return BadRequest("HTTP ошибка 400 Bad Request. Слово находится в чёрном списке.");
                }

                OperationsString oper = new OperationsString();
                string modString = await oper.StringSplit(inputString);
                string repeatSymbols = await oper.CountPrint(modString.ToString());
                string substring = await oper.FindSubstring(modString);
                BinaryTree binaryTree = new BinaryTree();
                string treeSortPrint = binaryTree.TreeSortPrint(inputString);
                string quickSort = QuickSort.QuickSortPrint(inputString.ToCharArray());

                RandomNumberService randomNumberService = new RandomNumberService(_configuration);
                var modifiedString = await randomNumberService.GetModifiedStringAsync(inputString);

                // Запись возврата тело JSON 
                var result = new
                {
                    inputString,
                    modString,
                    repeatSymbols,
                    substring,
                    treeSortPrint,
                    quickSort,
                    modifiedString
                };

                return Ok(result);
            }
            finally
            {
                // Возвращаем разрешение после обработки запроса
                _semaphore.Release();
            }

        }
    }
}
