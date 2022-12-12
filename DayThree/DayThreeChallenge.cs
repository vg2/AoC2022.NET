using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class DayThreeChallenge : IDayChallenge
    {
        public string InputPath => @"DayThree\input.txt";
        private List<(string compartmentOne, string compartmentTwo)> input;
        private List<string[]> inputTwo;
        

        public DayThreeChallenge()
        {
            input = File.ReadAllLines(InputPath).Select(line =>
            {
                // shmhggDsZCZWBDmsQTcTqrLPTbNbwQQrrN
                var length = line.Length;
                var cOne = line.Substring(0, (length / 2));
                var cTwo = line.Substring(length / 2);
                return (cOne, cTwo);
            }).ToList();

            inputTwo = File.ReadAllLines(InputPath).Chunk(3).ToList();
        }

        private int GetValueForLetter(char letter)
        {
            var upper = char.ToUpper(letter);
            var value = ((int)letter % 32);
            
            if (upper == letter)
            {
                return value + 26;
            }

            return value;
        }
     
        public object ExecutePartOne()
        {
            var commonItems = new List<char>();
            foreach (var bag in input)
            {
                var hasDuplicate = false;
                foreach (var item in bag.compartmentOne)
                {
                    if (bag.compartmentTwo.Contains(item))
                    {
                        commonItems.Add(item);
                        hasDuplicate = true;
                        break;
                    }
                }
                if (!hasDuplicate)
                {
                    Console.WriteLine(bag.compartmentOne + " - " + bag.compartmentTwo);
                }
            }

            return commonItems.Select(i => GetValueForLetter(i)).Sum();
        }

        public object ExecutePartTwo()
        {
            var badgeChars = new List<char>();
            foreach (var chunk in inputTwo)
            {
                var comparitorChunk = chunk.First();
                var comparitorBag = comparitorChunk;//.Where(i => comparitorChunk.Count(x => x == i) == 1);
                foreach (var item in comparitorBag)
                {
                    if (chunk[1].Contains(item) && chunk[2].Contains(item))
                    {
                        badgeChars.Add(item);
                        break;
                    }
                }
            }
            Console.WriteLine(string.Join(",", badgeChars));
            return badgeChars.Select(i => GetValueForLetter(i)).Sum();
        }
    }
}
