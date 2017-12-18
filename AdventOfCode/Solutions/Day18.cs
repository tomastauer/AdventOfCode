using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    using System.Runtime.Remoting.Channels;

    internal class Day18 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var registry = new Dictionary<string, long>();
            var commands = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            return this.PerformCommands(registry, commands).ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            throw new NotImplementedException();
        }

        private int PerformCommands(Dictionary<string, long> registry, IList<string> commands)
        {
            long lastSound = 0;
            for (long i = 0; i < commands.Count; i++)
            {
                var command = commands[(int)i];
                switch (command.Substring(0, 3))
                {
                    case "set":
                        registry = this.SetRegistry(registry, command);
                        break;
                    case "add":
                        registry = this.AddRegistry(registry, command);
                        break;
                    case "mul":
                        registry = this.MultiplyRegistry(registry, command);
                        break;
                    case "mod":
                        registry = this.ModuloRegistry(registry, command);
                        break;
                    case "snd":
                        lastSound = this.SoundRegistry(registry, command);
                        break;
                    case "rcv":
                        var recover = this.RecoverRegistry(registry, command, lastSound);
                        if (recover.Item1)
                        {
                            return (int)recover.Item2;
                        }
                        break;
                    case "jgz":
                        i += this.JumpRegistry(registry, command);
                        break;
                }
            }

            return 0;
        }

        private Dictionary<string, long> SetRegistry(Dictionary<string, long> registry, string command)
        {
            return this.ScalarOperationOnRegistry(registry, command, (a, b) => b);
        }

        private Dictionary<string, long> AddRegistry(Dictionary<string, long> registry, string command)
        {
            return this.ScalarOperationOnRegistry(registry, command, (a, b) => a + b);
        }

        private Dictionary<string, long> MultiplyRegistry(Dictionary<string, long> registry, string command)
        {
            return this.ScalarOperationOnRegistry(registry, command, (a, b) => a * b);
        }

        private Dictionary<string, long> ModuloRegistry(Dictionary<string, long> registry, string command)
        {
            return this.ScalarOperationOnRegistry(registry, command, (a, b) => a % b);
        }

        private long SoundRegistry(Dictionary<string, long> registry, string command)
        {
            var parts = command.Split(' ');
            if (!int.TryParse(parts[1], out var regValue))
            {
                return registry[parts[1]];
            }

            return regValue;
        }

        private long JumpRegistry(Dictionary<string, long> registry, string command)
        {
            var parts = command.Split(' ');
            if (!long.TryParse(parts[1], out var checkValue))
            {
                checkValue = registry[parts[1]];
            }

            if (!long.TryParse(parts[2], out var jumpValue))
            {
                jumpValue = registry[parts[2]];
            }

            return checkValue > 0 ? (jumpValue - 1) : 0;
        }

        private (bool, long) RecoverRegistry(Dictionary<string, long> registry, string command, long lastPlayed)
        {
            var parts = command.Split(' ');
            if (!long.TryParse(parts[1], out var checkValue))
            {
                checkValue = registry[parts[1]];
            }

            return (checkValue != 0, lastPlayed);
        }

        private Dictionary<string, long> ScalarOperationOnRegistry(Dictionary<string, long> registry, string command, Func<long, long, long> operation)
        {
            var parts = command.Split(' ');
            long origValue = registry.ContainsKey(parts[1]) ? registry[parts[1]] : 0;
            if (int.TryParse(parts[2], out var value))
            {
                registry[parts[1]] = operation(origValue, value);
            }
            else
            {
                registry[parts[1]] = operation(origValue, registry[parts[2]]);
            }
            return registry;
        }
    }
}
