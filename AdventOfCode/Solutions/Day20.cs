using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day20 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var particles = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(c => new Particle(c)).ToList();
            for(int i = 0; i < 5000; i++)
            {
                particles.ForEach(c => c.Evolve());
            }

            var evolved = new List<Particle>(particles);
            while(evolved.Any(c=>c.IsGettingCloser()))
            {
                evolved.ForEach(c => c.Evolve());
            }

            return evolved.IndexOf(evolved.Single(c=>c.Increment() == evolved.Min(d=>d.Increment()))).ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            var particles = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(c => new Particle(c)).ToList();
            var particlesSet = new HashSet<Particle>(particles);
            for (int i = 0; i < 5000; i++)
            {
                particles.ForEach(c => c.Evolve());
                particles = particles.GroupBy(c => c.Position).Where(c => c.Count() == 1).Select(c => c.Single()).ToList();
            }

            return particles.Count.ToString();
        }

        private class Particle
        {
            private const string pattern = @"p=<(?<px>-?\d+),(?<py>-?\d+),(?<pz>-?\d+)>, v=<(?<vx>-?\d+),(?<vy>-?\d+),(?<vz>-?\d+)>, a=<(?<ax>-?\d+),(?<ay>-?\d+),(?<az>-?\d+)>";

            public Particle(string line)
            {
                var match = Regex.Match(line, pattern);
                Position = (int.Parse(match.Groups["px"].Value), int.Parse(match.Groups["py"].Value), int.Parse(match.Groups["pz"].Value));
                Velocity = (int.Parse(match.Groups["vx"].Value), int.Parse(match.Groups["vy"].Value), int.Parse(match.Groups["vz"].Value));
                Acceleration = (int.Parse(match.Groups["ax"].Value), int.Parse(match.Groups["ay"].Value), int.Parse(match.Groups["az"].Value));

                Distances = new List<int>();
            }
            
            public (int, int, int) Position { get; set; }
            public (int, int, int) Velocity { get; set; }
            public (int, int, int) Acceleration { get; set; }

            private List<int> Distances { get; set; }

            public void Evolve()
            {
                Distances.Add(GetDistance());
                Velocity = (Velocity.Item1 + Acceleration.Item1, Velocity.Item2 + Acceleration.Item2, Velocity.Item3 + Acceleration.Item3);
                Position = (Velocity.Item1 + Position.Item1, Velocity.Item2 + Position.Item2, Velocity.Item3 + Position.Item3);
            }

            public int GetDistance()
            {
                return Math.Abs(Position.Item1) + Math.Abs(Position.Item2) + Math.Abs(Position.Item3);
            }

            public bool IsGettingCloser()
            {
                return Distances.Last() == Distances.Min() && Distances.Last() != Distances.First();
            }

            public int Increment()
            {
                return Math.Abs(Distances[Distances.Count - 1] - Distances[Distances.Count - 2]);
            }
        }
    }
}
