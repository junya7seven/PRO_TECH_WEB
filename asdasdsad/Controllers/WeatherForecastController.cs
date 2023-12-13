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
            string[] blacklist = _configuration.GetSection("iisSettings:BlackList").Get<string[]>(); // ������� ������� ������ �� ������������� � ������

            if (string.IsNullOrEmpty(inputString) || string.IsNullOrWhiteSpace(inputString)) // �������� �� ������ ������ � ������� �������� || string.IsNullOrWhiteSpace(inputString)
            {
                return BadRequest("HTTP ������ 400 Bad Request. Invalid input string.");
            }
            if (!Regex.IsMatch(inputString, "^[a-z]+$")) // �������� �� ������� �� ���������� ��������
            {
                return BadRequest("HTTP ������ 400 Bad Request. Invalid input string.");
            }
            if (blacklist.Contains(inputString)) // �������� �� ������� ���� � ������ ������
            {
                return BadRequest("HTTP ������ 400 Bad Request. ����� ��������� � ������ ������.");
            }

            

            // ������������ ������
            string modString = OperationsString.StringSplit(inputString);
            // ������������� �������
            string repeatSymbols = OperationsString.CountPrint(modString.ToCharArray());
            // ����� ������� ��������� ������������ � ��������������� �� �������
            string substring = OperationsString.FindSubstring(modString);
            // �������������� ������ �������� �������
            BinaryTree binaryTree = new BinaryTree();
            string treeSortPrint = binaryTree.TreeSortPrint(inputString);
            // ��������������� ������ ������� �����������
            string quickSort = QuickSort.QuickSortPrint(inputString.ToCharArray());
            
            // �������� ���������� ������ � ��������������, � ������� ���� URL
            RandomNumberService randomNumberService = new RandomNumberService(_configuration);
            var modifiedString = await randomNumberService.GetModifiedStringAsync(inputString);
            // ������ �������� ���� JSON 
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
