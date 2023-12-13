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

        public StringController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("process")]
        public async Task<IActionResult> ProcessString([FromQuery] string inputString)
        {
            string[] blacklist = _configuration.GetSection("iisSettings:BlackList").Get<string[]>(); // Перенос черного списка из конфигуратора в массив

            if (string.IsNullOrEmpty(inputString) || string.IsNullOrWhiteSpace(inputString)) // Проверка на пустую строку и наличие пробелов || string.IsNullOrWhiteSpace(inputString)
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

            

            // Обработанная строка
            string modString = OperationsString.StringSplit(inputString);
            // повторяющиеся символы
            string repeatSymbols = OperationsString.CountPrint(modString.ToCharArray());
            // самая длинная подстрока начинающаяся и заканчивающаяся на гласные
            string substring = OperationsString.FindSubstring(modString);
            // отсортировання строка бинарным деревом
            BinaryTree binaryTree = new BinaryTree();
            string treeSortPrint = binaryTree.TreeSortPrint(inputString);
            // отсортированная строка простой сортировкой
            string quickSort = QuickSort.QuickSortPrint(inputString.ToCharArray());
            
            // Создание экземпляра класса с конфигуратором, в котором есть URL
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
    }
}
