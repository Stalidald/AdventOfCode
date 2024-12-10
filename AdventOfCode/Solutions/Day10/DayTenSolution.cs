namespace AdventOfCode.Solutions.Day10
{
    public class DayTenSolution : Solution<long>
    {
        private List<HashSet<(int, int)>> Coordinates = new();
        private List<(int, int)> Coordinates2 = new();
        public override long Task1()
        {
            var input = ReadFileAsArray("Day10");
            long result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[i][j] == '0')
                    {
                        Coordinates.Add(new());
                        TravelInput(0, i + 1, j, input, result);
                        TravelInput(0, i - 1, j, input, result);
                        TravelInput(0, i, j + 1, input, result);
                        TravelInput(0, i, j - 1, input, result);
                    }
                }
            }

            var lengths = Coordinates.Select(hs => hs.Count).Sum();
            return lengths;
        }

        private void TravelInput(int currentNumber, int i, int j, string[] input, long result)
        {
            if (i < 0 || j < 0 || i >= input.Length || j >= input[i].Length) return;

            if (int.TryParse(input[i][j].ToString(), out int num))
            {
                if (num - currentNumber == 1)
                {
                    if (num == 9)
                    {
                        Coordinates[Coordinates.Count - 1].Add((i, j));
                    };

                    TravelInput(num, i + 1, j, input, result); // Bottom
                    TravelInput(num, i - 1, j, input, result); // Up
                    TravelInput(num, i, j + 1, input, result); // Right
                    TravelInput(num, i, j - 1, input, result); // Left
                }
            }

            return;
        }

        public override long Task2()
        {
            var input = ReadFileAsArray("Day10");
            long result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[i][j] == '0')
                    {
                        TravelInput2(0, i + 1, j, input, result);
                        TravelInput2(0, i - 1, j, input, result);
                        TravelInput2(0, i, j + 1, input, result);
                        TravelInput2(0, i, j - 1, input, result);
                    }
                }
            }

            return Coordinates2.Count();
        }

        private void TravelInput2(int currentNumber, int i, int j, string[] input, long result)
        {
            if (i < 0 || j < 0 || i >= input.Length || j >= input[i].Length) return;

            if (int.TryParse(input[i][j].ToString(), out int num))
            {
                if (num - currentNumber == 1)
                {
                    if (num == 9)
                    {
                        Coordinates2.Add((i, j));
                    };

                    TravelInput2(num, i + 1, j, input, result); // Bottom
                    TravelInput2(num, i - 1, j, input, result); // Up
                    TravelInput2(num, i, j + 1, input, result); // Right
                    TravelInput2(num, i, j - 1, input, result); // Left
                }
            }

            return;
        }
    }
}
