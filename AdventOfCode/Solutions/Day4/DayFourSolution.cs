using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Day4
{
    public class DayFourSolution : Solution<long>
    {
        public override long Task1()
        {
            long result = 0;
            var lines = ReadFileAsArray("day4");

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                result += CheckHorizontally(line);

                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == 'X')
                    {
                        result += CheckDirections(i, j, lines);
                    }
                }
            }

            return result;
        }

        public override long Task2()
        {
            long result = 0;
            var lines = ReadFileAsArray("day4");

            for (int i = 1; i < lines.Length - 1; i++)
            {
                var line = lines[i];

                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == 'A' && j > 0 && j < line.Length - 1)
                    {
                        result += CheckSides(i, j, lines);
                    }
                }
            }

            return result;
        }

        private int CheckSides(int i, int j, string[] lines)
        {
            string MAS = "MAS";
            var result = 0;

            var str = lines[i - 1][j - 1].ToString() + lines[i][j].ToString() + lines[i + 1][j + 1].ToString(); // from top left to bottom right       
            var str2 = lines[i + 1][j - 1].ToString() + lines[i][j].ToString() + lines[i - 1][j + 1].ToString(); // from bottom left to top right
            var str3 = lines[i - 1][j + 1].ToString() + lines[i][j].ToString() + lines[i + 1][j - 1].ToString(); // from top right to bottom left
            var str4 = lines[i + 1][j + 1].ToString() + lines[i][j].ToString() + lines[i - 1][j - 1].ToString(); // from bottom right to top left

            if (str == MAS && str == str2) result++;
            if (str == MAS && str == str3) result++;
            if (str == MAS && str == str4) result++;
            if (str2 == MAS && str2 == str3) result++;
            if (str2 == MAS && str2 == str4) result++;
            if (str3 == MAS && str3 == str4) result++;

            return result;
        }

        private int CheckDirections(int i, int j, string[] lines)
        {
            return CheckDiagonally(i, j, lines) + CheckVertically(i, j, lines);
            //return CheckDiagonally(i, j, lines);
        }

        private int CheckDiagonally(int i, int j, string[] lines)
        {
            int result = 0;
            string xmas = "XMAS";
            if (j < lines[i].Length - 3)
            {
                // Check right
                if (i > 2)
                {
                    string str = lines[i][j].ToString() + lines[i - 1][j + 1].ToString() + lines[i - 2][j + 2].ToString() + lines[i - 3][j + 3].ToString();
                    if (str == xmas) result++;
                }
                if (i < lines.Length - 3)
                {
                    string str = lines[i][j] + lines[i + 1][j + 1].ToString() + lines[i + 2][j + 2].ToString().ToString() + lines[i + 3][j + 3].ToString();
                    if (str == xmas) result++;
                }
            }

            if (j >= 3)
            {
                // Check left
                if (i > 2)
                {
                    var str = lines[i][j].ToString() + lines[i - 1][j - 1].ToString() + lines[i - 2][j - 2].ToString() + lines[i - 3][j - 3].ToString();
                    if (str == xmas) result++;
                }
                if (i < lines.Length - 3)
                {
                    var str = lines[i][j].ToString() + lines[i + 1][j - 1].ToString() + lines[i + 2][j - 2].ToString() + lines[i + 3][j - 3].ToString();
                    if (str == xmas) result++;
                }
            }

            return result;
        }

        private int CheckHorizontally(string line)
        {
            var regex = new Regex("XMAS");
            var regex2 = new Regex("SAMX");
            var matches = regex.Matches(line);
            var matches2 = regex2.Matches(line);

            return matches.Count + matches2.Count;
        }

        private int CheckVertically(int i, int j, string[] lines)
        {
            string xmas = "XMAS";

            int result = 0;
            if (i > 2)
            {
                int counter = 0;
                bool found = true;
                while (counter < 4)
                {
                    if (lines[i - counter][j].ToString() != xmas[counter].ToString())
                    {
                        found = false;
                        break;
                    };
                    counter++;
                }

                if (found) result += 1;
            }

            if (i < lines.Length - 3)
            {
                int counter = 0;
                bool found = true;
                string str = "";
                while (counter < 4)
                {
                    var text = lines[i + counter][j].ToString();
                    var original = xmas[counter].ToString();
                    if (lines[i + counter][j].ToString() != xmas[counter].ToString())
                    {
                        found = false;
                        break;
                    };
                    counter++;
                }

                if (found) result += 1;
            }

            return result;
        }
    }
}
