using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class DayTwoChallenge : IDayChallenge
    {
        public string InputPath => @"DayTwo\input.txt";
        private List<(Guess opponent, Guess me, Outcome outcome)> inputOne;
        private List<(Guess opponent, Guess me, Outcome outcome)> inputTwo;

        private enum Guess
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        private enum Outcome
        {
            Loss = 0,
            Draw = 3,
            Win = 6

        }

        private Dictionary<char, Guess> OpponentGuessMap = new Dictionary<char, Guess>
        {
            {'A', Guess.Rock},
            {'B', Guess.Paper},
            {'C', Guess.Scissors},
        };

        private Dictionary<char, Guess> MyGuessMap = new Dictionary<char, Guess>
        {
            {'X', Guess.Rock},
            {'Y', Guess.Paper},
            {'Z', Guess.Scissors},
        };

        private Dictionary<char, Outcome> OutcomeMap = new Dictionary<char, Outcome>
        {
            {'X', Outcome.Loss},
            {'Y', Outcome.Draw},
            {'Z', Outcome.Win},
        };

        public DayTwoChallenge()
        {
            inputOne = File.ReadAllLines(InputPath).Select(line =>
            {
                var split = line.Split(' ');
                var opponentGuess = OpponentGuessMap[split[0][0]];
                var myGuess = MyGuessMap[split[1][0]];
                return (opponentGuess, myGuess, GetOutcome(opponentGuess, myGuess));
            }).ToList();

            inputTwo = File.ReadAllLines(InputPath).Select(line =>
            {
                var split = line.Split(' ');
                var opponentGuess = OpponentGuessMap[split[0][0]];
                var outcome = OutcomeMap[split[1][0]];
                return (opponentGuess, GetRequiredGuess(opponentGuess, outcome), outcome);
            }).ToList();
        }

        private Outcome GetOutcome(Guess opponent, Guess me)
        {
            if (opponent == me)
            {
                return Outcome.Draw;
            }
            if (opponent == Guess.Rock && me == Guess.Paper
                || opponent == Guess.Scissors && me == Guess.Rock
                || opponent == Guess.Paper && me == Guess.Scissors)
            {
                return Outcome.Win;
            }

            return Outcome.Loss;
        }

        private Guess GetRequiredGuess(Guess opponent, Outcome outcome)
        {
            if (outcome == Outcome.Draw)
            {
                return opponent;
            }

            if (
                ((outcome == Outcome.Win && opponent == Guess.Paper) || (outcome == Outcome.Loss && opponent == Guess.Rock))
                )
            {
                return Guess.Scissors;
            }

            if (
                ((outcome == Outcome.Win && opponent == Guess.Scissors) || (outcome == Outcome.Loss && opponent == Guess.Paper))
                )
            {
                return Guess.Rock;
            }

            if (
                ((outcome == Outcome.Win && opponent == Guess.Rock) || (outcome == Outcome.Loss && opponent == Guess.Scissors))
                )
            {
                return Guess.Paper;
            }

            throw new NotImplementedException();
        }

     
        public object ExecutePartOne()
        {
            return inputOne.Select(i => (int)i.me + (int)i.outcome).Sum();
        }

        public object ExecutePartTwo()
        {
            return inputTwo.Select(i => (int)i.me + (int)i.outcome).Sum();
        }
    }
}
