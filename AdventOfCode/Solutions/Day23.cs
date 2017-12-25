using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions
{
    internal class Day23 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var registry = new Dictionary<string, long>();
            for (int i = 0; i < 8; i++)
            {
                registry.Add(((char) (97 + i)).ToString(), 0);
            }

            var commands = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            return this.PerformCommands(registry, commands).ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            var b = 106500;
            var result = 0;
            for (int i = 0; i <= 1000; i++)
            {
                if (!IsPrime(b + 17 * i))
                {
                    result++;
                }
            }

            return result.ToString();
        }

        private bool IsPrime(int input)
        {
            for (int i = 2; i < Math.Sqrt(input); i++)
            {
                for (int j = 2; j < input; j++)
                {
                    if (i * j == input)
                    {
                        return false;
                    }    
                }
            }

            return true;
        }

        private bool CheckFinish(Queue<long> queue1, Queue<long> queue2, string command1, string command2)
        {
            return command1.StartsWith("rcv") && command2.StartsWith("rcv") && !queue1.Any() && !queue2.Any();
        }

        private int PerformCommands(Dictionary<string, long> registry, IList<string> commands)
        {
            int multiplications = 0;
            for (long i = 0; i < commands.Count; i++)
            {
                var command = commands[(int)i];
                switch (command.Substring(0, 3))
                {
                    case "set":
                        registry = this.SetRegistry(registry, command);
                        break;
                    case "sub":
                        registry = this.SubRegistry(registry, command);
                        break;
                    case "mul":
                        registry = this.MultiplyRegistry(registry, command);
                        multiplications++;
                        break;
                    case "jnz":
                        i += this.JumpRegistry(registry, command);
                        break;
                }
            }

            return multiplications;
        }

        private Dictionary<string, long> SetRegistry(Dictionary<string, long> registry, string command)
        {
            return this.ScalarOperationOnRegistry(registry, command, (a, b) => b);
        }

        private Dictionary<string, long> SubRegistry(Dictionary<string, long> registry, string command)
        {
            return this.ScalarOperationOnRegistry(registry, command, (a, b) => a - b);
        }

        private Dictionary<string, long> MultiplyRegistry(Dictionary<string, long> registry, string command)
        {
            return this.ScalarOperationOnRegistry(registry, command, (a, b) => a * b);
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

            return checkValue != 0 ? (jumpValue - 1) : 0;
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
