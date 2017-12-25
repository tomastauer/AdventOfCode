using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day25 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var states = new Dictionary<char, Func<bool, int, bool[], (char, int)>>
            {
                {'A', StateA},
                {'B', StateB},
                {'C', StateC},
                {'D', StateD},
                {'E', StateE},
                {'F', StateF},
            };

            var tape = new bool[1000000000];
            var currentState = 'A';
            int currentPosition = 500000000;
            for (int i = 0; i < 12919244; i++)
            {
                var result = states[currentState](tape[currentPosition], currentPosition, tape);
                currentState = result.Item1;
                currentPosition = result.Item2;
            }

            return tape.Count(c => c).ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            throw new NotImplementedException();
        }

        private (char, int) StateA(bool currentValue, int position, bool[] tape)
        {
            if (!currentValue)
            {
                tape[position] = true;
                return ('B', position + 1);
            }
            else
            {
                tape[position] = false;
                return ('C', position - 1);
            }
        }

        private (char, int) StateB(bool currentValue, int position, bool[] tape)
        {
            if (!currentValue)
            {
                tape[position] = true;
                return ('A', position - 1);
            }
            else
            {
                tape[position] = true;
                return ('D', position + 1);
            }
        }

        private (char, int) StateC(bool currentValue, int position, bool[] tape)
        {
            if (!currentValue)
            {
                tape[position] = true;
                return ('A', position + 1);
            }
            else
            {
                tape[position] = false;
                return ('E', position - 1);
            }
        }

        private (char, int) StateD(bool currentValue, int position, bool[] tape)
        {
            if (!currentValue)
            {
                tape[position] = true;
                return ('A', position + 1);
            }
            else
            {
                tape[position] = false;
                return ('B', position + 1);
            }
        }

        private (char, int) StateE(bool currentValue, int position, bool[] tape)
        {
            if (!currentValue)
            {
                tape[position] = true;
                return ('F', position - 1);
            }
            else
            {
                tape[position] = true;
                return ('C', position - 1);
            }
        }

        private (char, int) StateF(bool currentValue, int position, bool[] tape)
        {
            if (!currentValue)
            {
                tape[position] = true;
                return ('D', position + 1);
            }
            else
            {
                tape[position] = true;
                return ('A', position + 1);
            }
        }
    }
}
