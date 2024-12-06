using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day3
{
    public class DayThreeSolution : Solution<long>
    {
        private string ReadFile()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\Day3\Day3.txt");
            return File.ReadAllText(path);
        }

        public override long Task1()
        {
            long result = 0;
            var fileContent = ReadFile();
            var mulRegex = new Regex(@"mul\((?<num1>\d{1,3})\,(?<num2>\d{1,3})\)");

            var matches = mulRegex.Matches(fileContent);

            foreach (Match match in matches)
            {
                result += long.Parse(match.Groups["num1"].Value) * long.Parse(match.Groups["num2"].Value);         
            }

            return result;
        }

        public override long Task2()
        {
            long result = 0;
            var fileContent = ReadFile();
            var mulRegex = new Regex(@"mul\((?<num1>\d{1,3})\,(?<num2>\d{1,3})\)|(?<instruction>do\(\)|don't\(\))");

            var shouldSkip = false;
            var matches = mulRegex.Matches(fileContent);
            foreach (Match match in matches)
            {
                var instruction = match.Groups["instruction"].Value;
                if (instruction == "don't()")
                {
                    shouldSkip = true;
                } else if (instruction == "do()")
                {
                    shouldSkip = false;
                } else if (!shouldSkip)
                {
                    result += long.Parse(match.Groups["num1"].Value) * long.Parse(match.Groups["num2"].Value);

                }
            }

            return result;
        }
    }
}
