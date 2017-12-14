using AdventOfCode.Reusable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day14 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var knotHash = new KnotHash();

            return Enumerable.Range(0, 128).Select(i => GetBinary(knotHash.GetHash($"{input}-{i}"))).SelectMany(c => c).Count(c => c == '1').ToString();
        }

        private string GetBinary(string input)
        {
            return String.Join(String.Empty,
              input.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            );
        }

        protected override string RunInternalPart2(string input)
        {
            return PerformLabelIteration(GetDisk(input)).ToString();
        }
        
        private bool Label(int[,] disk, int i, int j, int label)
        {
            if(disk[i, j] == 1)
            {
                disk[i, j] = label;
                Label(disk, i - 1, j, label);
                Label(disk, i + 1, j, label);
                Label(disk, i, j - 1, label);
                Label(disk, i, j + 1, label);
                return true;
            }

            return false;
        }

        private int PerformLabelIteration(int[,] disk)
        {
            int label = 2;
            for (int i = 1; i < 129; i++)
            {
                for (int j = 1; j < 129; j++)
                {
                    if(Label(disk, i, j, label))
                    {
                        label++;
                    }

                }
            }

            return label - 2;
        }

        private int[,] GetDisk(string input)
        {
            var knotHash = new KnotHash();

            var disk = new int[130, 130];
            int i = 1;
            foreach (var line in Enumerable.Range(0, 128).Select(q => GetBinary(knotHash.GetHash($"{input}-{q}"))))
            {
                int j = 1;

                foreach (var c in line)
                {
                    disk[i, j] = int.Parse(c.ToString());
                    j++;
                }

                i++;
            }

            return disk;
        }
    }
}
