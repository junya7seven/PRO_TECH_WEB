using asdasdsad;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringController : ControllerBase
    {
        [HttpGet("process")]
        public IActionResult ProcessString([FromQuery] string inputString)
        {
            if (string.IsNullOrEmpty(inputString) || string.IsNullOrWhiteSpace(inputString)) // Проверка на пустую строку и наличие пробелов || string.IsNullOrWhiteSpace(inputString)
            {
                return BadRequest("HTTP ошибка 400 Bad Request. Invalid input string.");
            }
            if (!Regex.IsMatch(inputString, "^[a-z]+$")) // Проверка на наличие не английских символов
            {
                return BadRequest("HTTP ошибка 400 Bad Request. Invalid input string.");
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
            // получение рандомного числа + новая возврат записи в тело JSON. Для удобства создана новая запись обработанной строки + самого рандомного числа
            var randomNumb = GetRandomNumbers.Random(inputString);

            // Запись возврата тело JSON 
            var result = new
            {
                inputString,
                modString,
                repeatSymbols,
                substring,
                treeSortPrint,
                quickSort,
                randomNumb
            };
            return Ok(result);
        }
    }
}
