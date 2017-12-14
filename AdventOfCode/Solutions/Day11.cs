using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day11 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var directories = input.Split(',').ToList();
            return GetLength(directories).ToString();
        }

        private int GetLength(List<string> directories)
        {
            int x = 0, y = 0, z = 0;
            foreach(var directory in directories)
            {
                switch(directory)
                {
                    case "n":
                        x++;
                        z--;
                        break;
                    case "ne":
                        x++;
                        y--;
                        break;
                    case "se":
                        y--;
                        z++;
                        break;
                    case "s":
                        x--;
                        z++;
                        break;
                    case "sw":
                        x--;
                        y++;
                        break;
                    case "nw":
                        y++;
                        z--;
                        break;
                }
            }

            return (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2;
        }

        protected override string RunInternalPart2(string input)
        {
            var directories = input.Split(',').ToList();
            int furthest = 0;

            var init = new List<string>();
            foreach(var directory in directories)
            {
                init.Add(directory);
                furthest = Math.Max(furthest, GetLength(init));
            }

            return furthest.ToString();
        }
    }
}
