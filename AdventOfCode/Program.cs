﻿using System.Collections.Generic;
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
            Console.WriteLine(runner.RunPart1<Day7>());


            Console.ReadLine();
        }
    }
}
