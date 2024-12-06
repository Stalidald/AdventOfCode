using System.Reflection;

namespace AdventOfCode.Solutions
{
    public abstract class Solution<T>
    {
        public abstract T Task1();
        public abstract T Task2();

        protected string ReadFile(string dayText)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Files\\{dayText}\\{dayText}.txt");
            return File.ReadAllText(path);
        }

        protected string[] ReadFileAsArray(string dayText)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Files\\{dayText}\\{dayText}.txt");
            return File.ReadAllLines(path);
        }
    }
}
