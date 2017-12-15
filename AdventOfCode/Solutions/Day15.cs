using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions
{
    using System.Text.RegularExpressions;

    internal class Day15 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var inputs = Regex.Matches(input, $@"Generator A starts with (?<gen1>\d+){Environment.NewLine}Generator B starts with (?<gen2>\d+)")
                .Cast<Match>()
                .Select(c=> (int.Parse(c.Groups["gen1"].Value), int.Parse(c.Groups["gen2"].Value)))
                .Single();

            var generator1 = this.GetGenerator1(inputs.Item1, 16807).GetEnumerator();
            var generator2 = this.GetGenerator1(inputs.Item2, 48271).GetEnumerator();

            int matches = 0;

            for (int i = 0; i < 40000000; i++)
            {
                generator1.MoveNext();
                generator2.MoveNext();

                if (this.Last16BitsMatches(generator1.Current, generator2.Current))
                {
                    matches++;
                }
            }

            generator1.Dispose();
            generator2.Dispose();

            return matches.ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            var inputs = Regex.Matches(input, $@"Generator A starts with (?<gen1>\d+){Environment.NewLine}Generator B starts with (?<gen2>\d+)")
                              .Cast<Match>()
                              .Select(c => (int.Parse(c.Groups["gen1"].Value), int.Parse(c.Groups["gen2"].Value)))
                              .Single();

            var generator1 = this.GetGenerator2(inputs.Item1, 16807, 4).GetEnumerator();
            var generator2 = this.GetGenerator2(inputs.Item2, 48271, 8).GetEnumerator();

            int matches = 0;

            for (int i = 0; i < 5000000; i++)
            {
                generator1.MoveNext();
                generator2.MoveNext();

                if (this.Last16BitsMatches(generator1.Current, generator2.Current))
                {
                    matches++;
                }
            }

            generator1.Dispose();
            generator2.Dispose();

            return matches.ToString();
        }

        private bool Last16BitsMatches(int a, int b)
        {
            return (a & 0xFFFF) == (b & 0xFFFF);
        }

        private IEnumerable<int> GetGenerator1(int @base, int factor)
        {
            var previous = @base;

            for (;;)
            {
                previous = (int)(previous * (long)factor % 2147483647L);
                yield return previous;
            }
        }

        private IEnumerable<int> GetGenerator2(int @base, int factor, int multiplier)
        {
            var previous = @base;

            for (; ; )
            {
                previous = (int)(previous * (long)factor % 2147483647L);
                if (previous % multiplier != 0) continue;
                yield return previous;
            }
        }
    }
}
