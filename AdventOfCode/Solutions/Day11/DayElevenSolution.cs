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
            if (i == 75) return input;
            

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
            var newStones = new List<long>();

            var map = new Dictionary<long, List<long>>();
            newStones.AddRange(CheckStone2(stones, 0, map));


            return newStones.Count;
        }

        private List<long> CheckStone2(List<long> input, int i, Dictionary<long, List<long>> map)
        {
            if (i == 75) return input;
            var stones = new List<long>();

            foreach (var stone in input)
            {

                if (map.TryGetValue(stone, out var stonesInMap))
                {
                    stones.AddRange(stonesInMap);
                    continue;
                }
                map.Add(stone, []);

                var stoneStr = stone.ToString();
                if (stone == 0)
                {
                    map[stone].Add(1);
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
                        map[stone].Add(long.Parse(leftSide));
                    }
                    else
                    {
                        stones.Add(0);
                        map[stone].Add(0);
                    }

                    if (!string.IsNullOrEmpty(rightSide))
                    {
                        stones.Add(long.Parse(rightSide));
                        map[stone].Add(long.Parse(rightSide));
                    }
                    else
                    {
                        stones.Add(0);
                        map[stone].Add(0);
                    }
                }
                else
                {
                    stones.Add(stone * 2024);
                    map[stone].Add(stone * 2024);
                }
            }

            return CheckStone2(stones, ++i, map);
        }

    }
}
