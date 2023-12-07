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
            if (string.IsNullOrEmpty(inputString) || string.IsNullOrWhiteSpace(inputString)) // �������� �� ������ ������ � ������� �������� || string.IsNullOrWhiteSpace(inputString)
            {
                return BadRequest("HTTP ������ 400 Bad Request. Invalid input string.");
            }
            if (!Regex.IsMatch(inputString, "^[a-z]+$")) // �������� �� ������� �� ���������� ��������
            {
                return BadRequest("HTTP ������ 400 Bad Request. Invalid input string.");
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
            // ��������� ���������� ����� + ����� ������� ������ � ���� JSON. ��� �������� ������� ����� ������ ������������ ������ + ������ ���������� �����
            var randomNumb = GetRandomNumbers.Random(inputString);

            // ������ �������� ���� JSON 
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
