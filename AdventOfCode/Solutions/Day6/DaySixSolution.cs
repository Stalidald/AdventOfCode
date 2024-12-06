namespace AdventOfCode.Solutions.Day6
{
    public class DaySixSolution : Solution<long>
    {
        public override long Task1()
        {
            var lines = ReadFileAsArray("Day6");
            HashSet<Coordinate> visitedCoordinates = new();
            Guard guard = new() { Direction = GuardPositionEnum.UP };

            for (int i = 0; i < lines.Length; i++)
            {
                var startPosition = lines[i].IndexOf('^');
                if (startPosition != -1)
                {
                    var currentCoordinate = new Coordinate()
                    {
                        Y = i,
                        X = startPosition
                    };

                    guard.Position = currentCoordinate;
                    visitedCoordinates.Add(currentCoordinate);
                    MoveGuard(lines, guard, visitedCoordinates);
                }

                if (visitedCoordinates.Count > 0) return visitedCoordinates.Count;
            }

            return 0;
        }

        public override long Task2()
        {
            int result = 0;
            string[] map = ReadFileAsArray("Day6");
            string[] updatedMap = new string[map.Length];
            map.CopyTo(updatedMap, 0);
            Coordinate startPosition = new() { X = -1, Y = -1 };

            HashSet<Coordinate> distinctCoordinates = new();
            Guard guard = new() { Direction = GuardPositionEnum.UP };

            for (int i = 0; i < map.Length; i++)
            {
                startPosition.X = map[i].IndexOf('^');
                if (startPosition.X != -1)
                {
                    startPosition.Y = i;
                    var startCoordinate = new Coordinate()
                    {
                        Y = i,
                        X = startPosition.X
                    };

                    guard.Position = startCoordinate;
                    distinctCoordinates.Add(new Coordinate() { Y = guard.Position.Y, X = guard.Position.X });
                    MoveGuard(map, guard, distinctCoordinates);
                }

                if (distinctCoordinates.Count > 0) break;
            }

            foreach (var coordinate in distinctCoordinates)
            {
                char[] row = updatedMap[coordinate.Y].ToCharArray();
                guard.Position.X = startPosition.X; guard.Position.Y = startPosition.Y;
                guard.Direction = GuardPositionEnum.UP;
                HashSet<(Coordinate, GuardPositionEnum)> positions = new()
                {
                    (new Coordinate() { X = guard.Position.X, Y = guard.Position.Y}, GuardPositionEnum.UP)
                };

                if (row[coordinate.X] == '.')
                {
                    row[coordinate.X] = '#';
                    updatedMap[coordinate.Y] = new string(row);

                    result += MoveGuardTask2(updatedMap, guard, positions);

                    row[coordinate.X] = '.';
                    updatedMap[coordinate.Y] = new string(row);
                }
            }

            return result;
        }

        private int MoveGuardTask2(string[] lines, Guard guard, ICollection<(Coordinate, GuardPositionEnum)> positions)
        {
            int result = 0;
            switch (guard.Direction)
            {
                case GuardPositionEnum.UP:
                    while (guard.Position.Y >= 0 && lines[guard.Position.Y][guard.Position.X] != '#')
                    {
                        guard.Position.Y--;

                        (Coordinate, GuardPositionEnum) value = (new Coordinate() { X = guard.Position.X, Y = guard.Position.Y }, guard.Direction);

                        if (positions.Contains(value))
                        {
                            result++;
                            return result;
                        }

                        positions.Add(value);
                    }

                    if (guard.Position.Y < 0) return 0;

                    guard.Position.Y++;
                    guard.Direction = GuardPositionEnum.RIGHT;
                    break;
                case GuardPositionEnum.DOWN:
                    while (guard.Position.Y < lines.Length && lines[guard.Position.Y][guard.Position.X] != '#')
                    {
                        guard.Position.Y++;

                        (Coordinate, GuardPositionEnum) value = (new Coordinate() { X = guard.Position.X, Y = guard.Position.Y }, guard.Direction);

                        if (positions.Contains(value))
                        {
                            result++;
                            return result;
                        }
                        positions.Add(value);
                    }

                    if (guard.Position.Y == lines.Length) return 0;

                    guard.Position.Y--;
                    guard.Direction = GuardPositionEnum.LEFT;
                    break;
                case GuardPositionEnum.RIGHT:
                    while (guard.Position.X < lines[guard.Position.Y].Length && lines[guard.Position.Y][guard.Position.X] != '#')
                    {
                        guard.Position.X++;

                        (Coordinate, GuardPositionEnum) value = (new Coordinate() { X = guard.Position.X, Y = guard.Position.Y }, guard.Direction);

                        if (positions.Contains(value))
                        {
                            result++;
                            return result;
                        }
                        positions.Add(value);
                    }

                    if (guard.Position.X == lines[guard.Position.Y].Length) return 0;

                    guard.Position.X--;
                    guard.Direction = GuardPositionEnum.DOWN;
                    break;
                case GuardPositionEnum.LEFT:
                    while (guard.Position.X >= 0 && lines[guard.Position.Y][guard.Position.X] != '#')
                    {
                        guard.Position.X--;

                        (Coordinate, GuardPositionEnum) value = (new Coordinate() { X = guard.Position.X, Y = guard.Position.Y }, guard.Direction);

                        if (positions.Contains(value))
                        {
                            result++;
                            return result;
                        }
                        positions.Add(value);
                    }

                    if (guard.Position.X < 0) return 0;

                    guard.Position.X++;
                    guard.Direction = GuardPositionEnum.UP;
                    break;
                default:
                    break;
            }

            return MoveGuardTask2(lines, guard, positions);
        }
        private void MoveGuard(string[] lines, Guard guard, ICollection<Coordinate> visitedCoordinates)
        {
            switch (guard.Direction)
            {
                case GuardPositionEnum.UP:
                    while (guard.Position.Y >= 0 && lines[guard.Position.Y][guard.Position.X] != '#')
                    {
                        guard.Position.Y--;

                        if (guard.Position.Y >= 0 && lines[guard.Position.Y][guard.Position.X] == '.')
                            visitedCoordinates.Add(new Coordinate() { Y = guard.Position.Y, X = guard.Position.X });
                    }

                    if (guard.Position.Y < 0) return;

                    guard.Position.Y++;
                    guard.Direction = GuardPositionEnum.RIGHT;
                    break;
                case GuardPositionEnum.DOWN:
                    while (guard.Position.Y < lines.Length && lines[guard.Position.Y][guard.Position.X] != '#')
                    {
                        guard.Position.Y++;

                        if (guard.Position.Y < lines.Length && lines[guard.Position.Y][guard.Position.X] == '.')
                            visitedCoordinates.Add(new Coordinate() { Y = guard.Position.Y, X = guard.Position.X });
                    }

                    if (guard.Position.Y == lines.Length) return;

                    guard.Position.Y--;
                    guard.Direction = GuardPositionEnum.LEFT;
                    break;
                case GuardPositionEnum.RIGHT:
                    while (guard.Position.X < lines[guard.Position.Y].Length && lines[guard.Position.Y][guard.Position.X] != '#')
                    {
                        guard.Position.X++;

                        if (guard.Position.X < lines[guard.Position.Y].Length && lines[guard.Position.Y][guard.Position.X] == '.')
                            visitedCoordinates.Add(new Coordinate() { Y = guard.Position.Y, X = guard.Position.X });
                    }

                    if (guard.Position.X == lines[guard.Position.Y].Length) return;

                    guard.Position.X--;
                    guard.Direction = GuardPositionEnum.DOWN;
                    break;
                case GuardPositionEnum.LEFT:
                    while (guard.Position.X >= 0 && lines[guard.Position.Y][guard.Position.X] != '#')
                    {
                        guard.Position.X--;

                        if (guard.Position.X >= 0 && lines[guard.Position.Y][guard.Position.X] == '.')
                            visitedCoordinates.Add(new Coordinate() { Y = guard.Position.Y, X = guard.Position.X });
                    }

                    if (guard.Position.X < 0) return;

                    guard.Position.X++;
                    guard.Direction = GuardPositionEnum.UP;
                    break;
                default:
                    break;
            }

            MoveGuard(lines, guard, visitedCoordinates);
        }
    }

    public enum GuardPositionEnum
    {
        UP = 1,
        DOWN = 2,
        RIGHT = 3,
        LEFT = 4,
    }

    public class Guard
    {
        public GuardPositionEnum Direction { get; set; } = new();
        public Coordinate Position { get; set; } = new();
    }

    public class Coordinate
    {
        public int Y { get; set; }
        public int X { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(Y, X);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Coordinate otherCoordinate)
            {
                return Y == otherCoordinate.Y &&
                       X == otherCoordinate.X;
            }

            return false;
        }
    }
}
