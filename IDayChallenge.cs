using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal interface IDayChallenge
    {
        string[] InputPath { get; }
        object ExecutePartOne();

        object ExecutePartTwo();
    }
}
