using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day16 : Solution
    {
        private string programNames = "abcdefghijklmnop";

        protected override string RunInternalPart1(string input)
        {
            var programs = programNames.ToList();
            var commands = input.Split(',');

            foreach (var command in commands)
            {
                programs = PerformCommands(programs, command);
            }

            return new string(programs.ToArray());
        }

        private List<char> PerformCommands(List<char> programs, string command)
        {
            switch (command[0])
            {
                case 's':
                    programs = Rotate(programs, command);
                    break;
                case 'x':
                    programs = Exchange(programs, command);
                    break;
                case 'p':
                    programs = Partner(programs, command);
                    break;
            }

            return programs;
        }

        private List<char> Rotate(List<char> programs, string command)
        {
            int rotation = int.Parse(command.Substring(1));
            return ShiftString(new string(programs.ToArray()), rotation).ToList();
        }

        private string ShiftString(string t, int length)
        {
            return t.Substring(t.Length - length, length) + t.Substring(0, t.Length - length);
        }

        private List<char> Exchange(List<char> programs, string command)
        {
            var exchanges = command.Substring(1).Split('/').Select(int.Parse).ToList();
            var temp = programs[exchanges[0]];
            programs[exchanges[0]] = programs[exchanges[1]];
            programs[exchanges[1]] = temp;
            return programs;
        }

        private List<char> Partner(List<char> programs, string command)
        {
            var exchanges = command.Substring(1).Split('/').ToList();
            int indexOf1 = programs.IndexOf(exchanges[0][0]);
            int indexOf2 = programs.IndexOf(exchanges[1][0]);

            programs[indexOf1] = exchanges[1][0];
            programs[indexOf2] = exchanges[0][0];
            return programs;
        }

        protected override string RunInternalPart2(string input)
        {
            var programs = programNames.ToList();
            var commands = input.Split(',');

            int period = 0;
            do
            {
                foreach (var command in commands)
                {
                    programs = PerformCommands(programs, command);
                }

                period++;
            } while(new string(programs.ToArray()) != programNames);

            for (int i = 0; i < 1000000000 % period; i++)
            {
                foreach (var command in commands)
                {
                    programs = PerformCommands(programs, command);
                }
            }

            return new string(programs.ToArray());
        }
    }
}
