namespace AdventOfCode.Solutions.Day7
{
    public class DaySevenSolution : Solution<long>
    {
        #region Task 1
        public override long Task1()
        {
            long result = 0;
            var input = Parse(ReadFileAsArray("Day7"));

            foreach (var inp in input)
            {
                if (CheckListRecursive(inp.Value, inp.Key, 0))
                {
                    result += inp.Key;
                }
            }

            return result;
        }

        private bool CheckListRecursive(List<long> numbers, long searchValue, long currentResult)
        {
            if (numbers.Count == 0)
            {
                return searchValue == currentResult;
            }

            long firstNum = numbers[0];
            List<long> remainingNums = numbers.Skip(1).ToList();

            return CheckListRecursive(remainingNums, searchValue, currentResult * firstNum)
                || CheckListRecursive(remainingNums, searchValue, currentResult + firstNum);
        }
        #endregion

        #region Task 2
        public override long Task2()
        {
            long result = 0;
            var input = Parse(ReadFileAsArray("Day7"));

            foreach (var inp in input)
            {
                if (CheckListRecursive2(inp.Value, inp.Key, 0))
                {
                    result += inp.Key;
                }
            }

            return result;
        }

        private bool CheckListRecursive2(List<long> numbers, long searchValue, long currentResult)
        {
            if (numbers.Count == 0)
            {
                return searchValue == currentResult;
            }

            long firstNum = numbers[0];
            List<long> remainingNums = numbers.Skip(1).ToList();

            return CheckListRecursive2(remainingNums, searchValue, currentResult * firstNum)
                || CheckListRecursive2(remainingNums, searchValue, currentResult + firstNum)
                || CheckListRecursive2(remainingNums, searchValue, long.Parse($"{currentResult}{firstNum}"));
        }
        #endregion

        #region Helpers
        private Dictionary<long, List<long>> Parse(string[] lines)
        {
            var dic = new Dictionary<long, List<long>>();

            foreach (var line in lines)
            {
                var data = line.Split(':');
                var list = data[1].Trim().Split(' ').Select(long.Parse).ToList();

                dic.Add(long.Parse(data[0]), list);
            }

            return dic;
        }
        #endregion
    }
}
