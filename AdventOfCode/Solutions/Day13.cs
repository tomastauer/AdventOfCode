﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    using System.Text.RegularExpressions;

    internal class Day13 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            return this.Run(input, 0).Item1.ToString();
        }

        private (int, bool) Run(string input, int delay)
        {
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(c => Regex.Match(c, @"(?<position>\d*):\s(?<size>\d*)"))
                             .Select(m => (int.Parse(m.Groups["position"].Value), int.Parse(m.Groups["size"].Value)))
                             .ToDictionary(c => c.Item1, c => c.Item2);

            int maxDepth = lines.Keys.Max() + 1;
            var firewall = new int[maxDepth];

            foreach (var item in lines)
            {
                firewall[item.Key] = item.Value;
            }

            var catched = new HashSet<int>();

            for (int i = 0; i < maxDepth; i++)
            {
                if (firewall[i] == 0)
                {
                    continue;
                }

                if (this.GetPositionAtTime(firewall[i], i + delay) == 0)
                {
                    catched.Add(i);
                }
            }

            return (catched.Sum(c => c * firewall[c]), catched.Any());
        }

        protected override string RunInternalPart2(string input)
        {
            int delay = 0;
            while (true)
            {
                var result = this.Run(input, delay);
                
                if (!result.Item2) break;
                delay++;
            }

            return delay.ToString();
        }

        private int GetPositionAtTime(int size, int time)
        {
            int period = (size - 1) * 2;
            var result = time % period + 2 * Math.Min(0, size - 1 - time % period);
            
            return result;
        }
    }
}
