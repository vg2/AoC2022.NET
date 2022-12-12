using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class DayFourChallenge : IDayChallenge
    {
        private readonly string[] inputPath = { "DayFour", "input.txt" };
        public string[] InputPath => inputPath;
        public List<((int min, int max) rangeOne, (int min, int max) rangeTwo)> Pairs;

        public DayFourChallenge()
        {
            Pairs = File.ReadAllLines(Path.Combine(InputPath)).Select(line =>
            {
                var split = line.Split(',');
                var rangeSplit1 = split[0].Split('-');
                var rangeOne = (int.Parse(rangeSplit1[0]), int.Parse(rangeSplit1[1]));

                var rangeSplit2 = split[1].Split('-');
                var rangeTwo = (int.Parse(rangeSplit2[0]), int.Parse(rangeSplit2[1]));

                return (rangeOne, rangeTwo);
            }).ToList();
        }

        public object ExecutePartOne()
        {
            var count = 0;
            foreach (var pair in Pairs)
            {
                if (
                    (pair.rangeOne.min <= pair.rangeTwo.min && pair.rangeOne.max >= pair.rangeTwo.max)
                    ||
                    (pair.rangeTwo.min <= pair.rangeOne.min && pair.rangeTwo.max >= pair.rangeOne.max)
                    )
                {
                    count++;
                }
            }

            return count;
        }

        public object ExecutePartTwo()
        {
            var count = 0;
            foreach (var pair in Pairs)
            {
                if (
                    (pair.rangeOne.min <= pair.rangeTwo.min && pair.rangeOne.max >= pair.rangeTwo.min)
                    ||
                    (pair.rangeOne.max >= pair.rangeTwo.max && pair.rangeOne.min <= pair.rangeTwo.max)
                    ||
                    (pair.rangeTwo.min <= pair.rangeOne.min && pair.rangeTwo.max >= pair.rangeOne.min)
                    ||
                    (pair.rangeTwo.max >= pair.rangeOne.max && pair.rangeTwo.min <= pair.rangeOne.max)
                    )
                {
                    count++;
                }
            }

            return count;
        }
    }
}
