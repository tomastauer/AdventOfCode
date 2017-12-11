using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day10 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var lenghts = input.Split(',').Select(int.Parse);
            var circle = Enumerable.Range(0, 256).ToArray();
            int skipSize = 0;
            int currentPosition = 0;

            foreach(var l in lenghts)
            {
                Reverse(circle, currentPosition, l);
                currentPosition = GetBoundPosition(circle, currentPosition + l + skipSize);
                skipSize++;
            }

            return (circle[0] * circle[1]).ToString();
        }

        private void Reverse(int[] circle, int start, int lenght)
        {
            int tmp;
            for(int i = 0; i < lenght/2; i++)
            {
                int boundStart = GetBoundPosition(circle, start + i);
                int boundEnd = GetBoundPosition(circle, start + lenght - i - 1);

                tmp = circle[boundEnd];
                circle[boundEnd] = circle[boundStart];
                circle[boundStart] = tmp;
            }
        }

        private int GetBoundPosition(int[] circle, int position)
        {
            return position % circle.Length;
        }

        protected override string RunInternalPart2(string input)
        {
            var lenghts = input.Select(c=>(int)c).Concat(new[] { 17, 31, 73, 47, 23 });
            var circle = Enumerable.Range(0, 256).ToArray();
            int skipSize = 0;
            int currentPosition = 0;

            for (int i = 0; i < 64; i++)
            {
                foreach (var l in lenghts)
                {
                    Reverse(circle, currentPosition, l);
                    currentPosition = GetBoundPosition(circle, currentPosition + l + skipSize);
                    skipSize++;
                }
            }

            return string.Join("", circle.Select((item, i) => (i / 16, item)).GroupBy(c => c.Item1).Select(c => c.Select(d=>d.Item2).Aggregate((acc, curr) => acc^curr).ToString("X2"))).ToLowerInvariant();
        }
    }
}
