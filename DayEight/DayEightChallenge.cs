using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class DayEightChallenge : IDayChallenge
    {
        private readonly string[] inputPath = { "DayEight", "input.txt" };
        public string[] InputPath => inputPath;
        private int[][] input;

        public DayEightChallenge()
        {
            input = File.ReadAllLines(Path.Combine(InputPath)).Select(line =>
            {
                return line.Select(character => int.Parse(character.ToString())).ToArray();
            }).ToArray();
        }

        public object ExecutePartOne()
        {
            int counter = 0;
            var totalTrees = input[0].Length * input.Length;
            for (var row = 0; row < input.Length; row++)
            {
                if (row == 0 || row == input.Length - 1)
                {
                    counter += input[0].Length;
                    continue;
                }

                var colLength = input[row].Length;

                for (var col = 0; col < colLength; col++)
                {
                    if (col == 0 || col == colLength - 1)
                    {
                        counter++;
                        continue;
                    }

                    var currentTreeHeight = input[row][col];
                    if (!input[row].Where((treeHeight, index) => (index < col && treeHeight >= currentTreeHeight)).Any() 
                        ||
                        !input[row].Where((treeHeight, index) => (index > col && treeHeight >= currentTreeHeight)).Any()
                    )
                    {
                        counter++;
                    }
                    else if (!input.Select(i => i[col]).Where((treeHeight, index) => (index < row && treeHeight >= currentTreeHeight)).Any()
                        ||
                        !input.Select(i => i[col]).Where((treeHeight, index) => (index > row && treeHeight >= currentTreeHeight)).Any()
                    )
                    {
                        counter++;
                    }

                }
            }

            return counter;
        }


        public object ExecutePartTwo()
        {
            var currentScore = 0;

            for(var row = 0; row < input.Length; row++)
            {
                for (var col = 0; col < input[row].Length; col++)
                {
                    var currentTree = input[row][col];

                    var upperRow = row;
                    var upperScore = 0;
                    while (upperRow > 0)
                    {
                        upperRow--;
                        upperScore++;
                        if (input[upperRow][col] >= currentTree)
                        {                            
                            break;
                        }
                    }

                    var lowerRow = row;
                    var lowerScore = 0;
                    while (lowerRow < input.Length-1)
                    {
                        lowerRow++;
                        lowerScore++;
                        if (input[lowerRow][col] >= currentTree)
                        {
                            break;
                        }
                    }

                    var leftCol = col;
                    var leftScore = 0;
                    while (leftCol > 0)
                    {
                        leftCol--;
                        leftScore++;
                        if (input[row][leftCol] >= currentTree)
                        {
                            break;
                        }
                    }

                    var rightCol = col;
                    var rightScore = 0;
                    while (rightCol < input[row].Length - 1)
                    {
                        rightCol++;
                        rightScore++;
                        if (input[row][rightCol] >= currentTree)
                        {
                            break;
                        }
                    }


                    var scenicScore = upperScore * leftScore * lowerScore * rightScore;
                    if (scenicScore > currentScore)
                    {
                        currentScore = scenicScore;
                    }
                }
            }
            return currentScore;
        }
    }
}
