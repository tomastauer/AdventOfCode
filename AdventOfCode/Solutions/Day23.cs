using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var registry = new Dictionary<string, long>();
            for (int i = 0; i < 8; i++)
            {
                registry.Add(((char)(97 + i)).ToString(), 0);
            }

            registry["a"] = 1;

            var commands = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            this.PerformCommands2(registry, commands);
            return registry["h"].ToString();
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

        private int PerformCommands2(Dictionary<string, long> registry, IList<string> commands)
        {
            int multiplications = 0;
            var history = new Dictionary<long, Dictionary<string, long>>();

            for (long i = 0; i < commands.Count; i++)
            {
                if (!history.ContainsKey(i))
                {
                    history.Add(i, new Dictionary<string, long>(registry));
                }
                else
                {
                    registry = OptimizeRegistry(history[i], registry);
                    history[i] = new Dictionary<string, long>(registry);
                }

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

        private Dictionary<string, long> OptimizeRegistry(Dictionary<string, long> historyData,
            Dictionary<string, long> currentData)
        {
            var result = new Dictionary<string, long>(currentData);
            var difference = new Dictionary<string, long>();
            foreach (var keyValuePair in historyData)
            {
                difference.Add(keyValuePair.Key, currentData[keyValuePair.Key] - historyData[keyValuePair.Key]);
            }

            foreach (var diff in difference.Where(c=>c.Value != 0))
            {
                if (historyData[diff.Key] == 0 || currentData[diff.Key] == 0)
                {
                    return result;
                }
            }
           
            long maxToZero = int.MaxValue;
            foreach (var keyValuePair in difference.Where(c => c.Value != 0))
            {
                if (historyData[keyValuePair.Key] != 0 && currentData[keyValuePair.Key] != 0 &&
                    currentData[keyValuePair.Key] % keyValuePair.Value == 0 && Math.Abs(currentData[keyValuePair.Key]) <
                    Math.Abs(historyData[keyValuePair.Key]))
                {
                    maxToZero = Math.Min(maxToZero, Math.Abs(currentData[keyValuePair.Key] / keyValuePair.Value));
                }
            }

            if (maxToZero == int.MaxValue)
            {
                return result;
            }
            foreach (var keyValuePair in difference.Where(c => c.Value != 0))
            {
                result[keyValuePair.Key] += keyValuePair.Value * (maxToZero - 1);
            }

            return result;
        }


        private (bool, long) PerformCommand(Dictionary<string, long> registry, Queue<long> ownQueue, Queue<long> otherQueue, string command)
        {
            switch (command.Substring(0, 3))
            {
                case "set":
                    this.SetRegistry(registry, command);
                    break;
                case "sub":
                    this.SubRegistry(registry, command);
                    break;
                case "mul":
                    this.MultiplyRegistry(registry, command);
                    break;
                case "mod":
                    this.ModuloRegistry(registry, command);
                    break;
                case "snd":
                    otherQueue.Enqueue(this.SendRegistry(registry, command));
                    break;
                case "rcv":
                    if (!ownQueue.Any())
                    {
                        return (false, 0);
                    }
                    long item = ownQueue.Dequeue();
                    this.RecieveRegistry(registry, command, item);
                    break;
                case "jgz":
                    return (true, 1 + this.JumpRegistry(registry, command));
                    break;
            }

            return (true, 1);
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

        private long SendRegistry(Dictionary<string, long> registry, string command)
        {
            var parts = command.Split(' ');
            if (!int.TryParse(parts[1], out var regValue))
            {
                return registry[parts[1]];
            }

            return regValue;
        }

        private Dictionary<string, long> RecieveRegistry(Dictionary<string, long> registry, string command, long item)
        {
            var parts = command.Split(' ');
            registry[parts[1]] = item;
            return registry;
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
