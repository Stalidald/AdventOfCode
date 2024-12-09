namespace AdventOfCode.Solutions.Day9
{
    public class DayNineSolution : Solution<long>
    {
        List<int> FreeSpacesIndexes = new();
        List<int> NumberIndexes = new();
        public List<int> ParseLine(string input)
        {
            var numList = new List<int>();

            var fileCounter = 0;
            var numbers = input.Select(c => int.Parse(c.ToString())).ToList();
            for (int i = 0; i < numbers.Count; i++)
            {
                if (i % 2 == 0)
                {
                    NumberIndexes.Add(numList.Count);
                    numList.AddRange(Enumerable.Repeat(fileCounter++, numbers[i]));
                }
                else
                {
                    FreeSpacesIndexes.Add(numList.Count);
                    if (numbers[i] == 0) continue;
                    
                    numList.AddRange(Enumerable.Repeat(-1, numbers[i]));
                }
            }

            return numList;
        }

        private long CalculateCheckSum(List<int> numbers)
        {
            long val = 0;
            int i = 0;
            while (i < numbers.Count)
            {
                if (numbers[i] == -1)
                {
                    i++;
                    continue;
                }

                val += numbers[i] * i;
                i++;
            }

            return val;
        }

        private void RearrangeLine(List<int> numbers)
        {
            int leftSideIndex = 0;
            for (int i = numbers.Count - 1; 0 <= i && i >= leftSideIndex; i--)
            {
                if (numbers[i] != -1 && i > leftSideIndex)
                {
                    while (leftSideIndex <= i && numbers[leftSideIndex] != -1)
                    {
                        leftSideIndex++;
                    }
                    if (numbers[leftSideIndex] == -1 && leftSideIndex < i)
                    {
                        numbers[leftSideIndex] = numbers[i];
                        numbers[i] = -1;
                    }
                }
            }
        }

        public override long Task1()
        {
            var line = ReadFile("Day9");
            var numList = ParseLine(line);

            RearrangeLine(numList);
            return CalculateCheckSum(numList);
        }

        public override long Task2()
        {
            var line = ReadFile("Day9");
            var numList = ParseLine(line);

            RearrangeLine2(numList);
            return CalculateCheckSum(numList);
        }

        private void RearrangeLine2(List<int> numbers)
        {
            for (int i = NumberIndexes.Count - 1; i >= 0; i--)
            {
                var num = numbers.ElementAt(NumberIndexes[i]);
                var nums = numbers.Skip(NumberIndexes[i]).TakeWhile(_ => _ == num).ToList();
                for (int j = 0; j < FreeSpacesIndexes.Count && j < i; j++)
                {
                    var freeSpaces = numbers.Skip(FreeSpacesIndexes[j])
                                                .TakeWhile(_ => _ == -1)
                                                .ToList();

                    if (freeSpaces.Count >= nums.Count)
                    {
                        numbers.RemoveRange(FreeSpacesIndexes[j], nums.Count);
                        numbers.InsertRange(FreeSpacesIndexes[j], Enumerable.Repeat(nums[0], nums.Count));
                        numbers.RemoveRange(NumberIndexes[i], nums.Count);
                        numbers.InsertRange(NumberIndexes[i], Enumerable.Repeat(-1, nums.Count));

                        NumberIndexes[i] = FreeSpacesIndexes[j];
                        FreeSpacesIndexes[j] = FreeSpacesIndexes[j] + nums.Count;
                        break;
                    }
                }
            }
        }
    }
}
