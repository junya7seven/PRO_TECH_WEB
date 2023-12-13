using System.Text;
using System.Text.RegularExpressions;

namespace Tasks
{
    public class OperationsString
    {
        public static string StringSplit(string inputString)
        {
            int split = inputString.Length / 2; // Получение значение соответствующее половины длину строки
            string strResult = " ";
            if (inputString.Length % 2 == 0)
            {
                string first = new string(inputString.Substring(0, split).Reverse().ToArray()); // деление на 1 подстроку с 0 символа до половины строки + переворачивание 
                string second = new string(inputString.Substring(split).Reverse().ToArray()); // деление на 2 подстроку с половины строки до конца + переворачивание 
                strResult = string.Concat(first, second); // соединение двух подстрок
            }
            else
            {
                strResult = new string(inputString.Reverse().ToArray()); // переворачивание входящей строки
                strResult = string.Concat(strResult, inputString); // соединение перевёрнутой строки с входящей
            }
            return strResult;
        }

        public static string CountPrint(char[] inputString)
        {
            Dictionary<char, int> characterCount = new Dictionary<char, int>(); // Создаём коллекцию из пары ключей для символа и его количества в обработанной строке
            foreach (char c in inputString) // Проходимся по каждому символу в строке
            {
                if (characterCount.ContainsKey(c)) // Если символ уже есть в словаре, увеличиваем его счетчик
                {
                    characterCount[c]++;
                }
                else
                {
                    characterCount[c] = 1; // Если символа нет в словаре, добавляем его со счетчиком 1
                }
            }

            StringBuilder result = new StringBuilder();
            foreach (var pair in characterCount)
            {
                result.AppendLine($"{pair.Key} {pair.Value}");
            }

            return result.ToString();
        }

        public static string FindSubstring(string inputString)
        {
            int firstIndex = -1; // Первый индекс гласного символа
            int lastIndex = -1; // Последний индекс гласного символа
            string resultConcat = ""; // Строка результата соединения
            for (int i = 0; i < inputString.Length; i++) // Цикл поиска первого и последнего индекса гласного символа <aeiouy>
            {
                char c = inputString[i];
                if ("aeiouy".Contains(c) && firstIndex == -1)
                {
                    firstIndex = i;
                }
                if ("aeiouy".Contains(c))
                {
                    lastIndex = i;
                }
            }
            if (firstIndex != -1)
            {
                for (int j = firstIndex; j <= lastIndex; j++) // Цикл соединения строка Первый индекс гласного символа ++ Последнего гласного символа
                {
                    resultConcat += inputString[j];
                }
                return resultConcat;
            }
            else
            {
                return "Гласных нет";
            }
        }
    }
}
