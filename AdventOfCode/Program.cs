using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    using System;
    using AdventOfCode.Solutions;

    class Program
    {
        static void Main(string[] args)
        {
            var runner = new Runner();
            Console.WriteLine(runner.RunPart2<Day11>());


            Console.ReadLine();
        }
    }
}
