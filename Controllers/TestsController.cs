using Microsoft.AspNetCore.Mvc;

namespace TestHiberusNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestsController : ControllerBase
    {
        public TestsController(){}

        [HttpGet("stringInversion")]
        public string GetInversion(string text)
        {
            if (String.IsNullOrEmpty(text)) return "";
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new String(charArray);
        }

        [HttpGet("characterRepeated")]
        public List<KeyValuePair<string, int>> GetRepeated(string text)
        {
            var charList = new List<KeyValuePair<string, int>>();
            if (String.IsNullOrEmpty(text)) return charList;

            char[] charArray = text.ToCharArray();
            foreach (char c in charArray)
            {
                int charIndex = charList.FindIndex(e => e.Key.Equals(c.ToString()));
                if(charIndex < 0)
                {
                    charList.Add(new KeyValuePair<string, int>(c.ToString(), 1));
                    continue;
                }
                charList[charIndex] = new KeyValuePair<string, int>(c.ToString(), charList[charIndex].Value + 1);
            }
            return charList;
        }

        [HttpGet("wordQuantity")]
        public int GetQuantity(string text)
        {
            if (String.IsNullOrEmpty(text)) return 0;
            return text
                    .Split(" ")
                    .Where(e => !e.Any(char.IsDigit) && !String.IsNullOrEmpty(e))
                    .ToArray().Length;
        }

        [HttpGet("posibleCombinations")]
        public List<int[]> GetCombinations([FromQuery]List<int>numbers, int finalValue)
        {
            List<int[]> combinations = new List<int[]>();
            List<int> combination = new List<int>();
            List<int> indexUsed = new List<int>();
            numbers = numbers.OrderBy(n => n).ToList();

            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] == finalValue)
                {
                    combination.Add(numbers[i]);
                    if (!combinations.Exists(c => c.SequenceEqual(combination))) combinations.Add(combination.ToArray());
                    combination.Clear();
                    continue;
                }
                else if (numbers[i] < finalValue)
                {
                    bool isChecked = false;
                    combination.Add(numbers[i]);
                    indexUsed.Add(i);
                    do
                    {
                        var found = numbers.Select((value, index) => new { val = value, i = index })
                                            .Where(number => number.i > indexUsed.Last() && (combination.Sum() + number.val) <= finalValue)
                                            .FirstOrDefault();

                        if (found != null)
                        {
                            if ((combination.Sum() + found.val) == finalValue)
                            {
                                combination.Add(found.val);
                                if (!combinations.Exists(c => c.SequenceEqual(combination))){
                                    combinations.Add(combination.ToArray());
                                    indexUsed.Add(found.i);
                                };
                                combination.Clear();
                                combination.Add(numbers[i]);

                            }else if ((combination.Sum() + found.val) < finalValue)
                            {
                                combination.Add(found.val);
                                indexUsed.Add(found.i);
                            }
                        }
                        else
                        {
                            isChecked = true;
                            indexUsed.Clear();
                            combination.Clear();
                        }

                    } while (!isChecked);
                }
            }
            return combinations;
        }
    }
}
