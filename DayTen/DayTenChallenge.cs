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
            var cycle = 0;
            var x = 1;
            var crt = new List<string>() { string.Empty };
            var currentLine = 0;

            foreach (var line in input)
            {
                var splitLine = line.Split(' ');
                if (cycle != 0 && cycle % 40 == 0)
                {
                    crt.Add(string.Empty);
                    currentLine++;
                }

                if (splitLine[0] == "noop")
                {
                    RenderPixel(x, crt, currentLine);

                    cycle++;
                }
                else
                {
                    RenderPixel(x, crt, currentLine);
                    cycle++;

                    if (cycle != 0 && cycle % 40 == 0)
                    {
                        crt.Add(string.Empty);
                        currentLine++;
                    }

                    RenderPixel(x, crt, currentLine);
                    cycle++;
                    x += int.Parse(splitLine[1]);
                }
            }
            foreach (var line in crt)
            {
                Console.WriteLine(line);
                    };
            return -1;
        }

        private static void RenderPixel(int x, List<string> crt, int currentLine)
        {
            var lineLength = crt[currentLine].Length;
            if (lineLength == x || lineLength + 1 == x || lineLength - 1 == x)
            {
                crt[currentLine] += "#";
            }
            else
            {
                crt[currentLine] += ".";
            }
        }

    }
}
