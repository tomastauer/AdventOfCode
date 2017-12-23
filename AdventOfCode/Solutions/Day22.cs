using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day22 : Solution
    {
        private const int infinity = 200001;

        protected override string RunInternalPart1(string input)
        {
            var matrix = GetInfiniteMatrix(input.Split(new [] {Environment.NewLine}, StringSplitOptions.None).Select(c=>c.Select(d=>d == '#').ToArray()).ToArray());

            var position = GetInitialPosition(matrix);
            var direction = (-1, 0);
            int counter = 0;

            for (int i = 0; i < 10000; i++)
            {
                var pos = GetNextPosition(position, direction, matrix);
                matrix[position.Item1, position.Item2] = !matrix[position.Item1, position.Item2];
                if (matrix[position.Item1, position.Item2])
                {
                    counter++;
                }
                position = pos.Item1;
                direction = pos.Item2;
            }

            return counter.ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            var matrix = GetInfiniteMatrix(input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(c => c.Select(d => d == '#' ? State.Infected : State.Clean).ToArray()).ToArray());

            var position = GetInitialPosition(matrix);
            var direction = (-1, 0);
            int counter = 0;

            for (int i = 0; i < 10000000; i++)
            {
                var pos = GetNextPosition(position, direction, matrix);
                MarkNode(position, matrix);
                if (matrix[position] == State.Infected)
                {
                    counter++;
                }
                position = pos.Item1;
                direction = pos.Item2;

                //for (int j = 0; j < matrix.GetLength(0); j++)
                //{
                //    for (int k = 0; k < matrix.GetLength(0); k++)
                //    {
                //        Console.Write(matrix[j,k] ? "#" : ".");
                //    }
                //    Console.WriteLine();
                //}

                //Console.WriteLine();
            }

            return counter.ToString();
        }

        private enum State
        {
            Clean,
            Weakened,
            Infected,
            Flagged
        }

        private void MarkNode((int, int) position, Dictionary<(int, int), State> matrix)
        {

            if (matrix.ContainsKey(position))
            {
                matrix[position] = (State) (((int) matrix[position] + 1) % 4);
            }
            else
            {
                matrix.Add(position, State.Weakened);
            }
        }

        private (int, int) GetInitialPosition(bool[,] matrix)
        {
            return (matrix.GetLength(0) / 2, matrix.GetLength(0) / 2);
        }

        private (int, int) GetInitialPosition(Dictionary<(int, int), State> matrix)
        {
            return (infinity / 2, infinity / 2);
        }

        private ((int, int), (int, int)) GetNextPosition((int, int) currentPosition, (int, int) direction, bool[,] matrix)
        {
            if (direction.Item1 == 0)
            {
                direction = matrix[currentPosition.Item1, currentPosition.Item2]
                    ? (direction.Item2, 0)
                    : (-direction.Item2, 0);
            }
            else
            {
                direction = matrix[currentPosition.Item1, currentPosition.Item2]
                    ? (0, -direction.Item1)
                    : (0, direction.Item1);
            }

            return ((currentPosition.Item1 + direction.Item1, currentPosition.Item2 + direction.Item2), direction);
        }

        private ((int, int), (int, int)) GetNextPosition((int, int) currentPosition, (int, int) direction, Dictionary<(int, int), State> matrix)
        {
            var currentState = matrix.ContainsKey(currentPosition) ? matrix[currentPosition] : State.Clean;
            if (direction.Item1 == 0)
            {
                direction = currentState == State.Infected
                    ? (direction.Item2, 0)
                    : currentState == State.Clean ? (-direction.Item2, 0) :
                    currentState == State.Flagged ? (0, -direction.Item2) : direction;
            }
            else
            {
                direction = currentState == State.Infected
                    ? (0, -direction.Item1)
                    : currentState == State.Clean ? (0, direction.Item1) :
                    currentState == State.Flagged ? (-direction.Item1, 0) : direction;
            }

            return ((currentPosition.Item1 + direction.Item1, currentPosition.Item2 + direction.Item2), direction);
        }

        private bool[,] GetInfiniteMatrix(bool[][] input)
        {
            var result = new bool[infinity, infinity];
            var inputSize = input.GetLength(0);
            var start = infinity / 2 - inputSize/2;

            for (int i = 0; i < inputSize; i++)
            {
                for (int j = 0; j < inputSize; j++)
                {
                    result[start + i, start + j] = input[i][j];
                }
            }

            return result;
        }

        private Dictionary<(int, int), State> GetInfiniteMatrix(State[][] input)
        {
            var result = new Dictionary<(int, int), State>();
            
            var inputSize = input.GetLength(0);
            var start = infinity / 2 - inputSize / 2;

            for (int i = 0; i < inputSize; i++)
            {
                for (int j = 0; j < inputSize; j++)
                {
                    result.Add((start+i, start+j), input[i][j]);
                }
            }

            return result;
        }
    }
}
