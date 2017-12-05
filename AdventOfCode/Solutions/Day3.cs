using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day3 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var inputNumber = int.Parse(input);
            return (GetAxis(inputNumber).Select(c => Math.Abs(inputNumber - c)).Min() + GetGridSize(inputNumber) / 2).ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            var inputNumber = int.Parse(input);
            var canvas = this.GetCanvas();
            int x = 14;
            int y = 15;
            int sum = 0;

            while((sum = GetAdjacentSum(canvas, x, y)) <= inputNumber)
            {
                canvas[x, y] = sum;
                (x, y) = GetNextCoordinates(canvas, x, y);
                PrintCanvas(canvas);
            }

            return sum.ToString();
        }

        private int GetGridSize(int input)
        {
            int size = (int)Math.Ceiling(Math.Sqrt(input));
            if (size % 2 == 0)
            {
                size++;
            }
            return size;
        }

        private IEnumerable<int> GetAxis(int input)
        {
            var gridSize = GetGridSize(input);
            var major = gridSize * gridSize - gridSize / 2;
            return Enumerable.Range(0, 4).Select(i => major - (gridSize - 1) * i);
        }
        
        private int[,] GetCanvas()
        {
            var canvas = new int[31, 31];

            canvas[15, 15] = 1;
            canvas[16, 15] = 1;
            canvas[16, 16] = 2;
            canvas[15, 16] = 4;
            canvas[14, 16] = 5;

            return canvas;
        }

        private void PrintCanvas(int[,] canvas)
        {
            Enumerable.Range(0, 30).ToList().ForEach(dx =>
            {
                Enumerable.Range(0, 30).ToList().ForEach(dy => Console.Write($"{canvas[dx, dy]}\t"));
                Console.WriteLine();
            });
        }

        private int GetAdjacentSum(int[,] canvas, int x, int y)
        {
            return Enumerable.Range(-1, 3).SelectMany(dx => Enumerable.Range(-1, 3).Select(dy => canvas[x + dx, y + dy])).Sum();
        }

        private (int x, int y) GetNextCoordinates(int[,] canvas, int x, int y)
        {
            if (canvas[x - 1, y] != 0 && (canvas[x - 1, y - 1] != 0 || canvas[x - 1, y] != 0))
            {
                return (x, y + 1);
            }

            if(canvas[x-1, y] == 0 && (canvas[x - 1, y - 1] != 0 || canvas[x - 1, y - 1] != 0))
            {
                return (x - 1, y);
            }

            if (canvas[x + 1, y] == 0 && (canvas[x + 1, y + 1] != 0 || canvas[x, y + 1] != 0))
            {
                return (x + 1, y);
            }

            return (x, y - 1);

        }
    }
}
