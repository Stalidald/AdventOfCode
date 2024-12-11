using System.Collections.Concurrent;

namespace AdventOfCode.Solutions.Day11
{
    /*
     * 0 -> 1
     * even number of digits (e.g. 10 => 2 pieces) -> two stones  1 0,  1000 -> 10 0 (no leading zero)ű
     * None of the above -> stone number * 2024
     */

    public class DayElevenSolution : Solution<long>
    {
        private long step = 0;
        private List<long> ParseInput(string input) => input.Split(' ').Select(long.Parse).ToList();

        private List<long> CheckStone(List<long> input, int i)
        {
            if (i == 25) return input;


            var stones = new List<long>();

            foreach (var stone in input)
            {
                var stoneStr = stone.ToString();
                if (stone == 0)
                {
                    stones.Add(1);
                }
                else if (stoneStr.Length % 2 == 0)
                {
                    var leftSide = stoneStr.Substring(0, stoneStr.Length / 2);
                    var rightSide = stoneStr.Substring(stoneStr.Length / 2);

                    leftSide = leftSide.TrimStart('0');
                    rightSide = rightSide.TrimStart('0');

                    if (!string.IsNullOrEmpty(leftSide))
                    {
                        stones.Add(int.Parse(leftSide));
                    }
                    else
                    {
                        stones.Add(0);
                    }

                    if (!string.IsNullOrEmpty(rightSide))
                    {
                        stones.Add(int.Parse(rightSide));
                    }
                    else
                    {
                        stones.Add(0);
                    }
                }
                else
                {
                    stones.Add(stone * 2024);
                }
            }

            return CheckStone(stones, ++i);
        }

        public override long Task1()
        {
            var stones = ParseInput(ReadFile("Day11"));
            var newStones = new List<long>();

            newStones.AddRange(CheckStone(stones, 0));

            return newStones.Count;
        }

        public override long Task2()
        {
            var stones = ParseInput(ReadFile("Day11"));

            var map = new ConcurrentDictionary<long, long>();

            step = stones.Count;
            foreach (var stone in stones)
            {
                var res = CalculateStepCount(stone, 0);
            }

            return step;
        }

        private long CalculateStepCount(long stone, int iteration)
        {

            var valStr = stone.ToString();

            long partRes = 1;
            if (iteration == 75) return 0;

            if (stone == 0)
            {
                partRes = CalculateStepCount(1, iteration + 1);
                return partRes;
            }

            if (valStr.Length % 2 == 0)
            {
                int mid = valStr.Length / 2;
                long leftPart = long.Parse(valStr.Substring(0, mid));
                long rightPart = long.Parse(valStr.Substring(mid));

                step ++;
                partRes = CalculateStepCount(leftPart, iteration + 1) + CalculateStepCount(rightPart, iteration + 1);
                return partRes;
            }

            partRes = CalculateStepCount(2024 * stone, iteration + 1);
            return partRes;
        }


        private List<long> CheckStone2(List<long> input, int i, ConcurrentDictionary<long, long> map)
        {
            if (i == 25) return input;
            var stones = new List<long>();

            for (int j = 0; j < input.Count; j++)
            {
                var stone = input[j];
                if (map.TryGetValue(stone, out var stoneInMap))
                {
                    stones.Add(stoneInMap);
                    continue;
                }
                map.TryAdd(stone, 1);

                var stoneStr = stone.ToString();
                if (stone == 0)
                {
                    stones.Add(1);
                }
                else if (stoneStr.Length % 2 == 0)
                {
                    var leftSide = stoneStr.Substring(0, stoneStr.Length / 2);
                    var rightSide = stoneStr.Substring(stoneStr.Length / 2);

                    leftSide = leftSide.TrimStart('0');
                    rightSide = rightSide.TrimStart('0');

                    if (!string.IsNullOrEmpty(leftSide))
                    {
                        stones.Add(long.Parse(leftSide));
                        map[stone] = long.Parse(leftSide);
                    }
                    else
                    {
                        stones.Add(0);
                        map[stone] = 0;
                    }

                    if (!string.IsNullOrEmpty(rightSide))
                    {
                        stones.Add(long.Parse(rightSide));
                        map[stone] = long.Parse(rightSide);
                    }
                    else
                    {
                        stones.Add(0);
                        map[stone] = 0;
                    }
                }
                else
                {
                    var num = stone * 2024;
                    stones.Add(num);
                    map[stone] = num;
                }
            }

            return CheckStone2(stones, ++i, map);
        }

    }
}
