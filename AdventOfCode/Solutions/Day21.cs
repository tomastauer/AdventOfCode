using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day21 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var transformations = input.Split(new[] {Environment.NewLine}, StringSplitOptions.None)
                .Select(c => new Transformation(c)).ToList();

            var fragments = GetFragments(Pixelize(".#./..#/###"));

            for (int i = 0; i < 5; i++)
            {
                fragments = GetFragments(Pixelize(EvolveFragments(fragments, transformations)));
            }

            return Depixelize(Pixelize(fragments)).Count(c => c == '#').ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            var transformations = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Select(c => new Transformation(c)).ToList();

            var fragments = GetFragments(Pixelize(".#./..#/###"));

            for (int i = 0; i < 18; i++)
            {
                fragments = GetFragments(Pixelize(EvolveFragments(fragments, transformations)));
            }

            return Depixelize(Pixelize(fragments)).Count(c => c == '#').ToString();
        }

        private char[][] Pixelize(string pattern)
        {
            return pattern.Split('/').Select(c => c.ToArray()).ToArray();
        }

        private char[][] Pixelize(Fragment[,] fragments)
        {
            var fragmentsSize = fragments.GetLength(0);
            var fragmentSize = fragments[0, 0].Size;
            var outputSize = fragments[0, 0].Size * fragmentsSize;
            var result = new char[outputSize][];
            for (int i = 0; i < outputSize; i++)
            {
                result[i] = new char[outputSize];
            }

            for (int i = 0; i < fragmentsSize; i++)
            {
                for (int j = 0; j < fragmentsSize; j++)
                {
                    for (int x = 0; x < fragmentSize; x++)
                    {
                        for (int y = 0; y < fragmentSize; y++)
                        {
                            result[i*fragmentSize + x][j*fragmentSize + y] = fragments[i,j].Pixels[x][y];
                        }
                    }
                }
            }

            return result;
        }

        private string Depixelize(char[][] pixels)
        {
            return string.Join("/", pixels.Select(c => new string(c)).ToArray());
        }

        private Fragment[,] EvolveFragments(Fragment[,] fragments, List<Transformation> transformations)
        {
            int size = fragments.GetLength(0);

            var result = new Fragment[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i,j] = new Fragment(transformations.Single(c => c.MatchInput(Depixelize(fragments[i, j].Pixels))).GetOutput());
                }
            }

            return result;
        }
        
        private Fragment[,] GetFragments(char[][] pixels)
        {
            int denominator = pixels.Length % 2 == 0 ? 2 : 3;
            var resultSize = pixels.Length/denominator;
            var result = new Fragment[resultSize,resultSize];

            for (int i = 0; i < resultSize; i++)
            {
                for (int j = 0; j < resultSize; j++)
                {
                    var part = new char[denominator][];

                    for (int x = 0; x < denominator; x++)
                    {
                        part[x] = new char[denominator];

                        for (int y = 0; y < denominator; y++)
                        {
                            part[x][y] = pixels[i * denominator + x][j * denominator + y];
                        }
                    }

                    result[i, j] = new Fragment(part);
                }
            }
            return result;
        }

        private class Fragment
        {
            public char[][] Pixels { get; }

            public int Size => Pixels.Length;

            public Fragment(char[][] pixels)
            {
                Pixels = pixels;
            }
        }

        private class Transformation
        {
            public Transformation(string transformation)
            {
                var parts = transformation.Split(new[] {" => "}, StringSplitOptions.None);
                variations = GetAllVariations(parts[0]);
                output = parts[1];
            }

            private readonly List<string> variations;
            private readonly string output;

            public bool MatchInput(string pattern)
            {
                return variations.Contains(pattern);
            }

            public char[][] GetOutput()
            {
                return Pixelize(output);
            }

            private List<string> GetAllVariations(string pattern)
            {
                var result = new List<string>();
                var pixels = Pixelize(pattern);

                for (int i = 0; i < 4; i++)
                {
                    pixels = Rotate(pixels);
                    result.Add(Depixelize(pixels));
                    result.Add(Depixelize(Flip(pixels)));
                }

                return result;
            }

            private char[][] Pixelize(string pattern)
            {
                return pattern.Split('/').Select(c => c.ToArray()).ToArray();
            }

            private string Depixelize(char[][] pixels)
            {
                return string.Join("/", pixels.Select(c => new string(c)).ToArray());
            }

            private char[][] Rotate(char[][] pixels)
            {
                var result = new char[pixels.Length][];

                for (int i = 0; i < pixels.Length; i++)
                {
                    result[i] = new char[pixels.Length];

                    for (int j = 0; j < pixels.Length; j++)
                    {
                        result[i][j] = pixels[j][pixels.Length - i - 1];
                    }
                }

                return result;
            }

            private char[][] Flip(char[][] pixels)
            {
                var result = new char[pixels.Length][];

                for (int i = 0; i < pixels.Length; i++)
                {
                    result[i] = new char[pixels.Length];

                    for (int j = 0; j < pixels.Length; j++)
                    {
                        result[i][j] = pixels[pixels.Length - i - 1][j];
                    }
                }

                return result;
            }
        }
    }
}
