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
        private readonly string[] inputPath = { "DayTen", "input.txt" };
        public string[] InputPath => inputPath;
        private string[] input;

        public DayTenChallenge()
        {
            input = File.ReadAllLines(Path.Combine(InputPath));
        }

        public object ExecutePartOne()
        {
            var cycle = 0;
            var trackedCycleValues = new List<int>();
            var x = 1;
            foreach(var line in input)
            {
                var splitLine = line.Split(' ');
                
                if (splitLine[0] == "noop")
                {
                    cycle++;
                    CheckAndAddCycle(cycle, trackedCycleValues, x);
                }
                else {
                    cycle++;
                    CheckAndAddCycle(cycle, trackedCycleValues, x);
                    cycle++;                    
                    CheckAndAddCycle(cycle, trackedCycleValues, x);
                    x += int.Parse(splitLine[1]);
                }
            }
            Console.WriteLine(string.Join(",",trackedCycleValues));
            return trackedCycleValues.Sum();
        }

        private void CheckAndAddCycle(int cycle, List<int> trackedCycleValues, int currentRegisterValue)
        {
            if (cycle == 20 || ((cycle - 20) % 40 == 0))
            {
                trackedCycleValues.Add(currentRegisterValue * cycle);
            }
        }

        public object ExecutePartTwo()
        {
            return -1;
        }
    }
}
