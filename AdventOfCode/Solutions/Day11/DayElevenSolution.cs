using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode.Solutions.Day11
{
    /*
     * 0 -> 1
     * even number of digits (e.g. 10 => 2 pieces) -> two stones  1 0,  1000 -> 10 0 (no leading zero)ű
     * None of the above -> stone number * 2024
     */

    public class DayElevenSolution : Solution<long>
    {
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
                    rightSide =  rightSide.TrimStart('0');

                    if (!string.IsNullOrEmpty(leftSide))
                    {
                        stones.Add(int.Parse(leftSide));
                    } else
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

            return stones.Sum(x => CalculateStepCount(x, 0, map));
        }

        private long CalculateStepCount(long stone, int iteration, ConcurrentDictionary<long, long> map)
        {          
            return map.GetOrAdd(stone, c =>
            {
                var valStr = c.ToString();

                if (iteration == 75) return 1;

                if (stone == 0) return CalculateStepCount(1, ++iteration, map);

                if (valStr.Length % 2 == 0)
                {
                    int mid = valStr.Length / 2;
                    long leftPart = long.Parse(valStr.Substring(0, mid));
                    long rightPart = long.Parse(valStr.Substring(mid));

                    return CalculateStepCount(leftPart, ++iteration, map) + CalculateStepCount(rightPart, ++iteration, map);
                }

                return CalculateStepCount(2024 * c, ++iteration, map);
            });     
        }


        private List<long> CheckStone2(List<long> input, int i, ConcurrentDictionary<long, long> map)
        {
            if (i == 25) return input;
            var stones = new List<long>();

            for (int j  = 0; j < input.Count; j++)
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
