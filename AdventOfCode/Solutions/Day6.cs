using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day6 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var banks = input.Split('\t').Select(int.Parse).ToList();
            var history = new List<string>();

            return this.RunInternal(banks, history);
        }
        
        protected override string RunInternalPart2(string input)
        {
            var banks = input.Split('\t').Select(int.Parse).ToList();
            var history = new List<string>();

            this.RunInternal(banks, history);

            var lastHistory = history.Last();
            history.Clear();
            history.Add(lastHistory);

            return this.RunInternal(banks, history);
        }

        private string RunInternal(IList<int> banks, ICollection<string> history)
        {
            while (!this.CheckBankHistory(banks, history))
            {
                this.StoreBankHistory(banks, history);
                int bankMax = banks.Max();
                int maxBankIndex = banks.IndexOf(bankMax);

                banks[maxBankIndex] = 0;

                for (int i = 0; i < bankMax; i++)
                {
                    banks[++maxBankIndex % banks.Count]++;
                }
            }

            return history.Count.ToString();
        }

        private void StoreBankHistory(IEnumerable<int> banks, ICollection<string> history)
        {
            history.Add(this.SerializeBanks(banks));
        }

        private bool CheckBankHistory(IEnumerable<int> banks, ICollection<string> history)
        {
            return history.Contains(this.SerializeBanks(banks));
        }

        private string SerializeBanks(IEnumerable<int> banks)
        {
            return string.Join(",", banks);
        }
    }
}
