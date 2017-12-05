namespace AdventOfCode
{
    using System;

    internal class Runner
    {
        public string RunPart1<T>() where T : Solution
        {
            var solution = Activator.CreateInstance<T>();
            return solution.RunPart1();
        }

        public string RunPart2<T>() where T : Solution
        {
            var solution = Activator.CreateInstance<T>();
            return solution.RunPart2();
        }
    }
}