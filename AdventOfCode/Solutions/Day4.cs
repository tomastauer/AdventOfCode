using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day4 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            return input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(line => line.Split(' ').GroupBy(c => c).Max(c => c.Count())).Count(c => c == 1).ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            return input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(line => line.Split(' ').Select(c => String.Concat(c.Select(d=>d).OrderBy(d=>d))).GroupBy(c => c).Max(c => c.Count())).Count(c => c == 1).ToString();
        }
    }
}
