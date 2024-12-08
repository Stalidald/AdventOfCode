namespace AdventOfCode.Solutions.Day8
{
    public class DayEightSolution : Solution<long>
    {
        public List<(char Character, HashSet<(int I, int J)> Coordinates)> AntiNodes = [];
        public List<(char Character, HashSet<(int I, int J)> Coordinates)> Antennas = [];

        private bool CheckAntiNode(string[] input, HashSet<(int, int)> coordinates, int i, int j)
        {
            return i >= 0 && i < input.Length && j >= 0 && j < input[i].Length
                && !coordinates.Contains((i, j)) && !AntiNodes.Any(a => a.Coordinates.Contains((i, j)));
        }

        private bool CheckAntiNode2(string[] input, int i, int j)
        {
            return i >= 0 && i < input.Length && j >= 0 && j < input[i].Length
                && input[i][j] == '.';
        }

        private bool CheckLoop(string[] input, int i, int j)
        {
            return i >= 0 && i < input.Length && j >= 0 && j < input[i].Length;
        }

        public override long Task1()
        {
            var input = ReadFileAsArray("Day8");

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '.') continue;

                    #region Find / initalize char antennas and antinodes
                    var charAntennas = Antennas.FirstOrDefault(c => c.Character == input[i][j]);
                    if (charAntennas == default)
                    {
                        var list = new HashSet<(int, int)> { (i, j) };
                        Antennas.Add((input[i][j], list));
                        continue;
                    }

                    var charAntinodes = AntiNodes.FirstOrDefault(c => c.Character == input[i][j]);
                    if (charAntinodes == default)
                    {
                        AntiNodes.Add((input[i][j], []));
                        charAntinodes = AntiNodes[^1];
                    }
                    #endregion

                    foreach (var coordinate in charAntennas.Coordinates)
                    {
                        var iDistance = i - coordinate.I;
                        var jDistance = j - coordinate.J;

                        int tempI = 0;
                        int tempJ = 0;
                        if (jDistance < 0)
                        {
                            jDistance = Math.Abs(jDistance);

                            // Bottom Left 
                            tempI = i + iDistance;
                            tempJ = j - jDistance;
                            if (CheckAntiNode(input, charAntinodes.Coordinates, tempI, tempJ))
                            {
                                charAntinodes.Coordinates.Add((tempI, tempJ));
                            }

                            // Top right
                            tempI = coordinate.I - iDistance;
                            tempJ = coordinate.J + jDistance;
                            if (CheckAntiNode(input, charAntinodes.Coordinates, tempI, tempJ))
                            {
                                charAntinodes.Coordinates.Add((tempI, tempJ));
                            }
                        }
                        else
                        {
                            // Bottom Right
                            tempI = i + iDistance;
                            tempJ = j + jDistance;
                            if (CheckAntiNode(input, charAntinodes.Coordinates, tempI, tempJ))
                            {
                                charAntinodes.Coordinates.Add((tempI, tempJ));
                            }

                            // Top left
                            tempI = coordinate.I - iDistance;
                            tempJ = coordinate.J - jDistance;
                            if (CheckAntiNode(input, charAntinodes.Coordinates, tempI, tempJ))
                            {
                                charAntinodes.Coordinates.Add((tempI, tempJ));
                            }
                        }
                    }

                    charAntennas.Coordinates.Add((i, j));
                }
            }

            return AntiNodes.Sum(a => a.Coordinates.Count);
        }

        public override long Task2()
        {
            var input = ReadFileAsArray("Day8");

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '.') continue;

                    #region Find / initalize char antennas and antinodes
                    var charAntennas = Antennas.FirstOrDefault(c => c.Character == input[i][j]);
                    if (charAntennas == default)
                    {
                        var list = new HashSet<(int, int)> { (i, j) };
                        Antennas.Add((input[i][j], list));
                        continue;
                    }

                    var charAntinodes = AntiNodes.FirstOrDefault(c => c.Character == input[i][j]);
                    if (charAntinodes == default)
                    {
                        AntiNodes.Add((input[i][j], []));
                        charAntinodes = AntiNodes[^1];
                    }
                    else
                    {
                        charAntinodes.Coordinates.Add((i, j));
                    }
                    #endregion

                    foreach (var coordinate in charAntennas.Coordinates)
                    {
                        charAntinodes.Coordinates.Add((coordinate.I, coordinate.J));
                        var iDistance = i - coordinate.I;
                        var jDistance = j - coordinate.J;

                        int tempI = 0;
                        int tempJ = 0;
                        if (jDistance < 0)
                        {
                            jDistance = Math.Abs(jDistance);

                            // Bottom Left 
                            tempI = i + iDistance;
                            tempJ = j - jDistance;
                            while (CheckLoop(input, tempJ, tempJ))
                            {
                                if (CheckAntiNode2(input, tempI, tempJ))
                                {
                                    charAntinodes.Coordinates.Add((tempI, tempJ));
                                }
                                tempI += iDistance;
                                tempJ -= jDistance;
                            }

                            // Top right
                            tempI = coordinate.I - iDistance;
                            tempJ = coordinate.J + jDistance;
                            while (CheckLoop(input, tempJ, tempJ))
                            {
                                if (CheckAntiNode2(input, tempI, tempJ))
                                {
                                    charAntinodes.Coordinates.Add((tempI, tempJ));
                                }
                                tempI -= iDistance;
                                tempJ += jDistance;
                            }
                        }
                        else
                        {
                            // Bottom Right
                            tempI = i + iDistance;
                            tempJ = j + jDistance;
                            while (CheckLoop(input, tempJ, tempJ))
                            {
                                if (CheckAntiNode2(input, tempI, tempJ))
                                {
                                    charAntinodes.Coordinates.Add((tempI, tempJ));
                                }
                                tempI += iDistance;
                                tempJ += jDistance;
                            }

                            // Top left
                            tempI = coordinate.I - iDistance;
                            tempJ = coordinate.J - jDistance;
                            while (CheckLoop(input, tempJ, tempJ))
                            {
                                if (CheckAntiNode2(input, tempI, tempJ))
                                {
                                    charAntinodes.Coordinates.Add((tempI, tempJ));
                                }
                                tempI -= iDistance;
                                tempJ -= jDistance;
                            }
                        }
                    }

                    charAntennas.Coordinates.Add((i, j));
                }
            }

            return AntiNodes.SelectMany(_ => _.Coordinates).Distinct().Count();
        }
    }

}
