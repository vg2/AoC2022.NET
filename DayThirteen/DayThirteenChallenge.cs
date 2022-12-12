using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class DayThirteenChallenge : IDayChallenge
    {
        private readonly string[] inputPath = { "DayThirteen", "input.txt" };
        public string[] InputPath => inputPath;
        private string input;

        public DayThirteenChallenge()
        {
            input = File.ReadAllText(Path.Combine(InputPath));
        }

        public object ExecutePartOne()
        {
            return -1;
        }


        public object ExecutePartTwo()
        {
            return -1;
        }
    }
}
