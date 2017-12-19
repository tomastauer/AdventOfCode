using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day19 : Solution
    {
        private char[] pathChar = new char[] { '|', '-', '+' };

        protected override string RunInternalPart1(string input)
        {
            var matrix = GetMatrix(input);
            return GoToNext(matrix, GetStart(matrix), (1, 0), new List<char>()).Item2;
        }

        protected override string RunInternalPart2(string input)
        {
            var matrix = GetMatrix(input);
            return GoToNext(matrix, GetStart(matrix), (1, 0), new List<char>()).Item1.ToString();
        }

        private (int, int) GetStart(List<List<char>> input)
        {
            return (0, input[0].IndexOf('|'));
        }

        private List<List<char>> GetMatrix(string input)
        {
            return input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(c => c.ToList()).ToList();
        }

        private (int, string) GoToNext(List<List<char>> matrix, (int, int) position, (int, int) direction, List<char> output)
        {
            int counter = 0;
            while(true)
            {
                var currentChar = matrix[position.Item1][position.Item2];
                if (currentChar == ' ')
                {
                    break;
                }

                if (!pathChar.Contains(currentChar))
                {
                    output.Add(currentChar);
                }

                var nextPosition = GetNextPosition(matrix, position, direction);
                position = nextPosition.Item1;
                direction = nextPosition.Item2;
                counter++;
            }
            return (counter, new string(output.ToArray()));
        }

        private ((int, int), (int, int)) GetNextPosition(List<List<char>> matrix, (int, int) position, (int, int) direction)
        {
            var currentChar = matrix[position.Item1][position.Item2];

            if (currentChar == '+')
            {
                if (direction.Item1 == 0)
                {
                    direction = (direction.Item1 + matrix[position.Item1 - 1][position.Item2] == ' ' ? 1 : -1, 0);
                }
                else
                {
                    direction = (0, direction.Item2 + matrix[position.Item1][position.Item2 - 1] == ' ' ? 1 : -1);
                }
            }
            return ((position.Item1 + direction.Item1, position.Item2 + direction.Item2), direction);
        }
    }
}
 