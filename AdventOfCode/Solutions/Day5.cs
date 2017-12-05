using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day5 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var values = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int index = 0;
            int counter = 0;
            while(index < values.Length)
            {
                int val = values[index];
                values[index]++;
                index += val;
                counter++;
            }

            return counter.ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            var values = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int index = 0;
            int counter = 0;
            while (index < values.Length)
            {
                int val = values[index];
                values[index] += val >= 3 ? -1 : 1;
                index += val;
                counter++;
            }

            return counter.ToString();
        }
    }
}
