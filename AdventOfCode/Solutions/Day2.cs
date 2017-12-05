using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    using System.Runtime.Remoting.Messaging;

    internal class Day2 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            return input.Split(new[] { Environment.NewLine },StringSplitOptions.None).Select(line =>
            {
                var numbers = line.Split('\t').Select(int.Parse).ToList();
                return numbers.Max() - numbers.Min();
            }).Sum().ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            return input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(line =>
            {
                var numbers = line.Split('\t').Select(int.Parse).ToList();
                return numbers.Select(a =>
                {
                    var c = numbers.SingleOrDefault(b => a % b == 0 && a != b);
                    return c == 0 ? 0 : a / c;
                }).Max();
            }).Sum().ToString();
        }
    }
}
