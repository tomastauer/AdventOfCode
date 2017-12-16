namespace AdventOfCode
{
    using System;
    using AdventOfCode.Solutions;

    class Program
    {
        static void Main(string[] args)
        {
            var runner = new Runner();
            Console.WriteLine(runner.RunPart2<Day16>());


            Console.ReadLine();
        }
    }
}
