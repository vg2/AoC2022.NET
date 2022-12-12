using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class DaySixChallenge : IDayChallenge
    {
        private readonly string[] inputPath = { "DaySix", "input.txt" };
        public string[] InputPath => inputPath;
        private string input;

        public DaySixChallenge()
        {
            input = File.ReadAllText(Path.Combine(InputPath));
        }

        public object ExecutePartOne()
        {
            for (var i = 0; i < input.Length; i++)
            {
                if (i+3 >= input.Length) { throw new Exception("shits broken yo"); };
                
                var window = input[i..(i + 4)];

                if (IsUniqueCharacters(window))
                {
                    return i + 4;
                }
            }

            throw new Exception("Something went terribly wrong");
        }

        private bool IsUniqueCharacters(string test)
        {
            foreach (var character in test)
            {
                if (test.Count(x => x == character) != 1)
                {
                    return false;
                }
            }
            return true;
        }

        public object ExecutePartTwo()
        {
            for (var i = 0; i < input.Length; i++)
            {
                if (i + 13 >= input.Length) { throw new Exception("shits broken yo"); };

                var window = input[i..(i + 14)];

                if (IsUniqueCharacters(window))
                {
                    return i + 14;
                }
            }

            throw new Exception("Something went terribly wrong");
        }
    }
}
