using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class DayFiveChallenge : IDayChallenge
    {
        private readonly string[] inputPath = { "DayFive", "input.txt" };
        public string[] InputPath => inputPath;
        private Stack<char>[] stacks;
        private List<(int amount, int from, int to)> moveInstructions;

        public DayFiveChallenge()
        {
            var lines = File.ReadAllLines(Path.Combine(InputPath));

            var stackCountLine = lines.First(l => l.Trim().StartsWith('1'));
            var stackCount = int.Parse(stackCountLine.Trim().Last().ToString());
            var maxStackSize = stackCount * stackCount;

            stacks = new Stack<char>[stackCount];
            for(var i = 0; i < stackCount; i++)
            {
                stacks[i] = new Stack<char>();
            }

            var stackCountLineIndex = lines.ToList().IndexOf(stackCountLine);
            // 1 5 9 13
            // x -> (x * 4) + 1
            for (var i = 0; i < stackCountLineIndex; i++)
            {
                var stackLine = lines[i];
                for (var col = 0; col < stackCount; col++)
                {
                    var characterLocation = (col * 4) + 1;
                    var character = stackLine[characterLocation];
                    if ((int)character != 32) {
                        stacks[col].Push(character);
                    }
                }
            }
            
            for (var i = 0; i < stackCount; i++)
            {
                var reversedStack = stacks[i].ToList();
                stacks[i] = new Stack<char>(reversedStack);
            }

            var splitLineIndex = lines.ToList().IndexOf(string.Empty);

            moveInstructions = new List<(int amount, int from, int to)>();
            for (var moveInstructionLineIndex = splitLineIndex + 1; moveInstructionLineIndex < lines.Length; moveInstructionLineIndex++)
            {
                var moveInstructionLine = lines[moveInstructionLineIndex];
                var amountSplit = moveInstructionLine.Split(" from ");
                var amount = int.Parse(amountSplit[0].Split(" ").Last());

                var moveSplit = amountSplit[1].Split(" to ");
                var from = int.Parse(moveSplit[0]);
                var to = int.Parse(moveSplit[1]);

                moveInstructions.Add((amount, from, to));
            }
        }

        public object ExecutePartOne()
        {
            foreach (var instruction in moveInstructions)
            {
                for (var i = 0; i < instruction.amount; i++)
                {
                    if (stacks[instruction.from - 1].TryPop(out var moved))
                    {
                        stacks[instruction.to - 1].Push(moved);
                    }
                }
            }

            return string.Join(string.Empty, stacks.Where(s => s.Any()).Select(s => s.Peek()));
        }

        public object ExecutePartTwo()
        {
            foreach (var instruction in moveInstructions)
            {
                var tempStack = new Stack<char>();
                for (var i = 0; i < instruction.amount; i++)
                {
                    if (stacks[instruction.from - 1].TryPop(out var moved))
                    {
                        tempStack.Push(moved);
                    }
                }
                while(tempStack.TryPop(out var popped))
                {
                    stacks[instruction.to - 1].Push(popped);
                }
            }

            return string.Join(string.Empty, stacks.Where(s => s.Any()).Select(s => s.Peek()));
        }
    }
}
