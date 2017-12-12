using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    using System.Text.RegularExpressions;

    internal class Day12 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var reachablePrograms = new HashSet<int>();
            var programQueue = new Queue<int>();
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            
            programQueue.Enqueue(0);

            while (programQueue.Any())
            {
                int programToReach = programQueue.Dequeue();
                var parsed = this.ParseLine(lines[programToReach]);
                reachablePrograms.Add(parsed.Item1);

                foreach (var n in parsed.Item2.Where(c => !reachablePrograms.Contains(c)))
                {
                    programQueue.Enqueue(n);
                }
            }

            return reachablePrograms.Count.ToString();
        }

        private (int, int[]) ParseLine(string line)
        {
            var numbers = Regex.Matches(line, @"(\d+)").Cast<Match>().Select(c=> int.Parse(c.Value)).ToList();
            return(numbers[0], numbers.Skip(1).ToArray());
        }

        protected override string RunInternalPart2(string input)
        {
            var processedPrograms = new HashSet<int>();
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(this.ParseLine).ToList();
            int groups = 0;

            while (processedPrograms.Count != lines.Count)
            {
                var groupRoot = lines.First(c => !processedPrograms.Contains(c.Item1));
                var reachablePrograms = new HashSet<int>();
                var programQueue = new Queue<int>();

                programQueue.Enqueue(groupRoot.Item1);

                while (programQueue.Any())
                {
                    int programToReach = programQueue.Dequeue();
                    reachablePrograms.Add(lines[programToReach].Item1);
                    processedPrograms.Add(lines[programToReach].Item1);
                    foreach (var n in lines[programToReach].Item2.Where(c => !reachablePrograms.Contains(c)))
                    {
                        programQueue.Enqueue(n);
                    }
                }

                groups++;
            }

            return groups.ToString();
        }
    }
}
