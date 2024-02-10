using System.Text;
using System.Text.RegularExpressions;

namespace Tasks
{
    public class OperationsString
    {
        public async Task<string> StringSplit(string inputString)
        {
            int split = inputString.Length / 2; 
            string strResult = " ";
            if (inputString.Length % 2 == 0)
            {
                string first = new string(inputString.Substring(0, split).Reverse().ToArray()); 
                string second = new string(inputString.Substring(split).Reverse().ToArray()); 
                strResult = string.Concat(first, second); 
            }
            else
            {
                strResult = new string(inputString.Reverse().ToArray()); 
                strResult = string.Concat(strResult, inputString); 
            }
            return strResult;
        }

        public Task<string> CountPrint(string inputString)
        {
            return Task.Run(() =>
            {
                Dictionary<char, int> characterCount = new Dictionary<char, int>();
                foreach (char c in inputString)
                {
                    if (characterCount.ContainsKey(c))
                    {
                        characterCount[c]++;
                    }
                    else
                    {
                        characterCount[c] = 1;
                    }
                }

                StringBuilder result = new StringBuilder();
                foreach (var pair in characterCount)
                {
                    result.AppendLine($"{pair.Key} {pair.Value}");
                }

                return result.ToString();
            });

        }

        public async Task<string> FindSubstring(string inputString)
        {
            int firstIndex = -1; 
            int lastIndex = -1; 
            string resultConcat = ""; 
            for (int i = 0; i < inputString.Length; i++) 
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
                for (int j = firstIndex; j <= lastIndex; j++) 
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
