using System.Text;
using System.Text.RegularExpressions;

namespace Pro_Tech_Web.ModifiedString.StringOper
{
    public class ModString
    {
        private string stringInput { get; set; }
        public string modifiedString { get; set; }
        public ModString(string stringInput)
        {
            this.stringInput = stringInput;
        }
        // Наибольшая подстрока
        public string FindLargestVowelSubstring(string modString)
        {
            string pattern = @"[aeiouy]";
            MatchCollection matches = Regex.Matches(modString, pattern);
            string vowels = string.Concat(matches.Select(m => m.Value));

            if (string.IsNullOrEmpty(vowels))
            {
                return string.Empty;
            }

            int maxSubstringLength = 0;
            string largestSubstring = string.Empty;

            foreach (Match match in matches)
            {
                int startIndex = match.Index;
                int endIndex = modString.LastIndexOf(match.Value);

                if (endIndex - startIndex + 1 > maxSubstringLength)
                {
                    maxSubstringLength = endIndex - startIndex + 1;
                    largestSubstring = modString.Substring(startIndex, maxSubstringLength);
                }
            }
            return largestSubstring;
        }
        // Повторяющиеся символы
        public Dictionary<char, int> CountPrint(string modString)
        {
            Dictionary<char, int> charCount = new Dictionary<char, int>();
            foreach (var item in modString)
            {
                if (charCount.ContainsKey(item))
                {
                    charCount[item]++;
                }
                else
                {
                    charCount[item] = 1;
                }
            }
            return charCount;
        }
        // Модифицированная строка
        public string StringSplit()
        {
            StringBuilder result = new StringBuilder();
            int split = stringInput.Length / 2;
            if (stringInput.Length % 2 == 0)
            {
                result.Append(stringInput.Remove(0, split).Reverse().ToArray());
                result.Append(stringInput.Remove(split).Reverse().ToArray());
                modifiedString = result.ToString();
                return result.ToString();
            }
            else
            {
                result.Append(stringInput.Reverse().ToArray()).Append(stringInput);
                modifiedString = result.ToString();
                return result.ToString();
            }
        }
    }
}
