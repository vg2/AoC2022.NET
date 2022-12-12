using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class DayOneChallenge : IDayChallenge
    {
        private readonly List<int> caloriesPerElf = new List<int>();

        private readonly string[] inputPath = { "DayOne", "input.txt" };
        public string[] InputPath => inputPath;

        public DayOneChallenge()
        {
            LoadCaloriesPerElf();
        }

        private void LoadCaloriesPerElf()
        {
            var lines = File.ReadAllLines(Path.Combine(InputPath));
            var index = 0;
            caloriesPerElf.Add(0);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    index++;
                    caloriesPerElf.Add(0);
                }
                else
                {
                    caloriesPerElf[index] += int.Parse(line);
                }
            }
        }

        public object ExecutePartOne()
        {
            return caloriesPerElf.Max();
        }

        public object ExecutePartTwo()
        {
            return caloriesPerElf.OrderByDescending(i => i).Take(3).Sum();
        }
    }
}
