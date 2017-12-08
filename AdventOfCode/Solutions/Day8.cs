using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions
{
    using System.Text.RegularExpressions;

    internal class Day8 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var operations = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(c => new LineOperation(c)).ToList();
            var registry = operations.GroupBy(c => c.Register).ToDictionary(c => c.Key, c => 0);

            foreach (var lineOperation in operations)
            {
                if (this.CheckCondition(lineOperation.ConditionRegister, lineOperation.Operator, lineOperation.ConditionValue, registry))
                {
                    registry[lineOperation.Register] += lineOperation.ChangeOperator * lineOperation.ChangeValue;
                }
            }

            return registry.Max(c => c.Value).ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            var operations = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(c => new LineOperation(c)).ToList();
            var registry = operations.GroupBy(c => c.Register).ToDictionary(c => c.Key, c => 0);
            int highestValue = 0;

            foreach (var lineOperation in operations)
            {
                if (this.CheckCondition(lineOperation.ConditionRegister, lineOperation.Operator, lineOperation.ConditionValue, registry))
                {
                    registry[lineOperation.Register] += lineOperation.ChangeOperator * lineOperation.ChangeValue;
                    if (registry[lineOperation.Register] > highestValue)
                    {
                        highestValue = registry[lineOperation.Register];
                    }
                }
            }

            return highestValue.ToString();
        }

        private bool CheckCondition(string register, string op, int value, IDictionary<string, int> registry)
        {
            switch (op)
            {
                case ">":
                    return registry[register] > value;
                case ">=":
                    return registry[register] >= value;
                case "<":
                    return registry[register] < value;
                case "<=":
                    return registry[register] <= value;
                case "==":
                    return registry[register] == value;
                case "!=":
                    return registry[register] != value;
            }

            throw new InvalidOperationException();
        }

        private class LineOperation
        {
            private const string ParsingPattern = @"(?<register>\w*)\s(?<changeOperator>inc|dec)\s(?<changeValue>[-\d]*)\sif\s(?<condRegister>\w*)\s(?<operator>.*)\s(?<condValue>[-\d]*)";

            public LineOperation(string line)
            {
                var match = Regex.Match(line, ParsingPattern);
                this.Register = match.Groups["register"].Value;
                this.ChangeOperator = match.Groups["changeOperator"].Value == "inc" ? 1 : -1;
                this.ChangeValue = int.Parse(match.Groups["changeValue"].Value);
                this.ConditionRegister = match.Groups["condRegister"].Value;
                this.Operator = match.Groups["operator"].Value;
                this.ConditionValue = int.Parse(match.Groups["condValue"].Value);
            }

            public string Register { get; set; }

            public int ChangeOperator { get; set; }

            public int ChangeValue { get; set; }

            public string ConditionRegister { get; set; }

            public string Operator { get; set; }

            public int ConditionValue { get; set; }
        }
    }
}
