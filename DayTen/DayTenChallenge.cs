using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class DayTenChallenge : IDayChallenge
    {
        public string InputPath => @"DayTen\input.txt";
        private string input;

        public DayTenChallenge()
        {
            input = File.ReadAllText(InputPath);
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
