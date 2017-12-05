namespace AdventOfCode
{
    using System.IO;

    internal abstract class Solution
    {
        private string GetInput()
        {
            return File.ReadAllText($@"Inputs\{this.GetType().Name}.txt");
        }

        protected abstract string RunInternalPart1(string input);

        protected abstract string RunInternalPart2(string input);

        public string RunPart1()
        {
            return this.RunInternalPart1(this.GetInput());
        }

        public string RunPart2()
        {
            return this.RunInternalPart2(this.GetInput());
        }
    }
}