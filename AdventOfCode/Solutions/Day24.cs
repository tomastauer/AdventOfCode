using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day24 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var bridges = input.Split(new[] {Environment.NewLine}, StringSplitOptions.None).Select(c=> new Bridge(c)).ToList();
            var starters = bridges.Where(c => c.Ends.Item1 == 0 || c.Ends.Item2 == 0);

            var result = new List<List<Bridge>>();

            foreach (var starter in starters)
            {
                var bridgesInstance = new List<Bridge>(bridges);
                RemoveBridge(starter, bridgesInstance);
                
                DeepSearch(starter, starter.GetOtherEnd(0), bridgesInstance, new List<Bridge>(), result);
            }

            return result.Select(c => c.Sum(d => d.Ends.Item1 + d.Ends.Item2)).Max().ToString();
        }

        private void DeepSearch(Bridge instance, int end, List<Bridge> bridges, List<Bridge> path, List<List<Bridge>> paths)
        {
            path.Add(instance);
            var connectableBridges = bridges.Where(c => c.IsConnectable(end)).ToList();
            foreach (var next in connectableBridges)
            {
                var pathCopy = path.ToList();
                var copy = new List<Bridge>(bridges);
                RemoveBridge(next, copy);

                DeepSearch(next, next.GetOtherEnd(end), copy, pathCopy, paths);
            }

            if (!connectableBridges.Any())
            {
                paths.Add(path);
            }
        }

        private void DeepSearch2(Bridge instance, int end, List<Bridge> bridges, List<Bridge> path, List<List<Bridge>> paths)
        {
            path.Add(instance);
            var connectableBridges = bridges.Where(c => c.IsConnectable(end)).ToList();
            foreach (var next in connectableBridges)
            {
                var pathCopy = path.ToList();
                var copy = new List<Bridge>(bridges);
                RemoveBridge(next, copy);

                DeepSearch2(next, next.GetOtherEnd(end), copy, pathCopy, paths);
            }

            if (!connectableBridges.Any())
            {
                if (!paths.Any() || path.Count >= paths.Max(c => c.Count))
                {
                    paths.Add(path);
                }
            }
        }


        private void RemoveBridge(Bridge bridge, List<Bridge> bridges)
        {
            var listBridge = bridges.FirstOrDefault(c => c.GetHashCode() == bridge.GetHashCode());
            bridges.Remove(listBridge);
        }

        protected override string RunInternalPart2(string input)
        {
            var bridges = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(c => new Bridge(c)).ToList();
            var starters = bridges.Where(c => c.Ends.Item1 == 0 || c.Ends.Item2 == 0);

            var result = new List<List<Bridge>>();

            foreach (var starter in starters)
            {
                var bridgesInstance = new List<Bridge>(bridges);
                RemoveBridge(starter, bridgesInstance);

                DeepSearch2(starter, starter.GetOtherEnd(0), bridgesInstance, new List<Bridge>(), result);
            }

            return result.Where(c=>c.Count == result.Max(d=>d.Count)).Select(c => c.Sum(d => d.Ends.Item1 + d.Ends.Item2)).Max().ToString();
        }
        
        private class Bridge
        {
            private readonly string _line;

            public Bridge(string line)
            {
                _line = line;

                var parts = line.Split('/').Select(int.Parse).ToList();
                Ends = (parts[0], parts[1]);
            }

            public override int GetHashCode()
            {
                return _line.GetHashCode();
            }

            public (int, int) Ends { get; }

            public bool IsConnectable(int end)
            {
                return Ends.Item1 == end || Ends.Item2 == end;
            }

            public int GetOtherEnd(int end)
            {
                return Ends.Item1 == end ? Ends.Item2 : Ends.Item1;
            }
        }
    }
}
