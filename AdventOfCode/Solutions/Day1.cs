using System.Linq;

namespace AdventOfCode.Solutions
{
    internal class Day1 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            return input.Select((x, i) => (x, i))
                        .Aggregate(0, (acc, x) => acc + (x.Item1 == input[(x.Item2 + 1) % input.Length] ? int.Parse(x.Item1.ToString()) : 0))
                        .ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            return input.Select((x, i) => (x, i))
                        .Aggregate(0, (acc, x) => acc + (x.Item1 == input[(x.Item2 + input.Length/2) % input.Length] ? int.Parse(x.Item1.ToString()) : 0))
                        .ToString();
        }
    }
}
