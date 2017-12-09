using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions
{
    internal class Day9 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            int totalScore = 0;
            int immediateScore = 0;

            foreach (var c in RemoveGarbage(input))
            {
                switch (c)
                {
                    case '{':
                        immediateScore++;
                        totalScore += immediateScore;
                        break;
                    case '}':
                        immediateScore--;
                        break;
                }
            }

            return totalScore.ToString();
        }
    

        protected override string RunInternalPart2(string input)
        {
            var skipped = Regex.Replace(input, "!.", "");
            var garbageRegex = new Regex("<[^>]*>");
            return (skipped.Length - garbageRegex.Replace(skipped, "").Length - garbageRegex.Matches(skipped).Count * 2).ToString();
        }

        private string RemoveGarbage(string input)
        {
            return Regex.Replace(Regex.Replace(input, "!.", ""), "<[^>]*>", "");
        }
    }
}
