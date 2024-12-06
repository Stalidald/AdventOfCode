namespace AdventOfCode.Solutions.Day5
{
    public class Node
    {
        public List<int> LeftSide = new();
        public List<int> RightSide = new List<int>();
    }

    public class DayFiveSolution : Solution<long>
    {
        public override long Task1()
        {
            long result = 0;
            var lines = ReadFileAsArray("Day5");
            int i = 0;
            var rules = CollectRules(lines, ref i);

            i++;
            while (i < lines.Length) 
            {
                var numbers = lines[i].Split(',').Select(int.Parse).ToList();
                if (IsLineValid(numbers, rules))
                {
                    //Console.WriteLine(lines[i]);
                    result += numbers[numbers.Count() / 2];
                }
                i++;
            }

            return result;
        }

        private bool IsLineValid(List<int> numbers, Dictionary<int, Node> rules)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                if (!IsRightSideValid(numbers, i, rules) || !IsLeftSideValid(numbers, i, rules)) return false;
            }

            return true;
        }

        private bool IsLeftSideValid(List<int> numbers, int index, Dictionary<int, Node> rules)
        {
            var rulesForNumber = rules.GetValueOrDefault(numbers[index]);
            var leftSide = numbers.Take(index).ToList();

            return leftSide.All(rulesForNumber.LeftSide.Contains);
        }

        private bool IsRightSideValid(List<int> numbers, int index, Dictionary<int, Node> rules)
        {
            var rulesForNumber = rules.GetValueOrDefault(numbers[index]);
            var rightSide = numbers.Skip(index + 1).ToList();

            return rightSide.All(rulesForNumber.RightSide.Contains);
        }

        private Dictionary<int, Node> CollectRules(string[] lines, ref int i)
        {
            var rules = new Dictionary<int, Node>();

            var line = lines[i];
            while (line != string.Empty)
            {
                var data = line.Split('|');
                int leftValue = int.Parse(data[0]);
                int rightValue = int.Parse(data[1]);

                if (rules.TryGetValue(leftValue, out Node node1))
                {
                    node1.RightSide.Add(rightValue);
                }
                else
                {
                    node1 = new Node();
                    node1.RightSide.Add(rightValue);

                    rules.Add(leftValue, node1);
                }

                if (rules.TryGetValue(rightValue, out Node node2))
                {
                    node2.LeftSide.Add(leftValue);
                }
                else
                {
                    node2 = new Node();
                    node2.LeftSide.Add(leftValue);

                    rules.Add(rightValue, node2);
                }

                line = lines[++i];
            }

            return rules;
        }

        public override long Task2()
        {
            long result = 0;
            var lines = ReadFileAsArray("Day5");

            int i = 0;
            var rules = CollectRules(lines, ref i);
           
            i++;
            while (i < lines.Length)
            {
                var numbers = lines[i].Split(',').Select(int.Parse).ToList();
                if (!IsLineValid(numbers, rules))
                {
                    //Console.WriteLine(lines[i]);
                    ReorderNumbers(numbers, rules);
                    result += numbers[numbers.Count() / 2];
                }
                i++;
            }

            return result;
        }

        private void ReorderNumbers(List<int> numbers, Dictionary<int, Node> rules)
        {
            bool rulesSatisfied;
            do
            {
                rulesSatisfied = true;
                for (int i = 0; i < numbers.Count; i++)
                {
                    var number = numbers[i];
                    if (rules.TryGetValue(number, out var rule))
                    {
                        foreach (var left in rule.LeftSide)
                        {
                            if (numbers.IndexOf(left) >= i && numbers.Contains(left))
                            {
                                numbers.Remove(left);
                                numbers.Insert(i, left);
                                rulesSatisfied = false;
                            }
                        }

                        foreach (var right in rule.RightSide)
                        {
                            if (numbers.IndexOf(right) <= i && numbers.Contains(right))
                            {
                                numbers.Remove(right);

                                if (i + 1 > numbers.Count())
                                {
                                    numbers.Add(right);
                                }
                                else
                                {
                                    numbers.Insert(i + 1, right);
                                }
                                rulesSatisfied = false;
                            }
                        }
                    }
                }
            } while (!rulesSatisfied);
        }


    }
}
