using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day17 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            int shift = int.Parse(input);
            int currentIndex = 0;
            var result = new List<int>
            {
                0
            };

            for (int counter = 1; counter <= 2017; counter++)
            {
                int nextPosition = ((currentIndex + shift) % result.Count) + 1;
                result.Insert(nextPosition, counter);
                currentIndex = nextPosition;
            }

            return result[currentIndex+1].ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            int shift = int.Parse(input);
            int currentIndex = 0;
            var result = new List<int>
            {
                0
            };
            int lastCounter = 0;
            for (int counter = 1; counter <= 50000000; counter++)
            {
                int nextPosition = ((currentIndex + shift) % counter) + 1;
                if (nextPosition == 1)
                {
                    lastCounter = counter;
                }
                currentIndex = nextPosition;
            }

            return lastCounter.ToString();
        }
    }
}
