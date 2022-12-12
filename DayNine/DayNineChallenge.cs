using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class DayNineChallenge : IDayChallenge
    {
        public string InputPath => @"DayNine\input.txt";
        private (char direction, int amount)[] input;
        List<(int x, int y)> tailTraveledPositions;
        List<(int x, int y, bool isTail)> tailTraveledPositions2;

        public DayNineChallenge()
        {
            input = File.ReadAllLines(InputPath).Select(line =>
            {
                var splitLine = line.Split(' ');
                return (splitLine[0][0], int.Parse(splitLine[1]));
            }).ToArray();
        }

        public object ExecutePartOne()
        {
            (int x, int y) headPos = (0, 0);
            (int x, int y) tailPos = (0, 0);

            tailTraveledPositions = new List<(int x, int y)> { tailPos };

            foreach (var headMove in input)
            {
                switch (headMove.direction)
                {
                    case 'U':
                        var (newHeadUp, newTailUp) = MoveUp(headPos, tailPos, headMove.amount);
                        headPos = newHeadUp;
                        tailPos = newTailUp;
                        break;
                    case 'D':
                        var (newHeadDown, newTailDown) = MoveDown(headPos, tailPos, headMove.amount);
                        headPos = newHeadDown;
                        tailPos = newTailDown;
                        break;
                    case 'L':
                        var (newHeadLeft, newTailLeft) = MoveLeft(headPos, tailPos, headMove.amount);
                        headPos = newHeadLeft;
                        tailPos = newTailLeft;
                        break;
                    case 'R':
                        var (newHeadRight, newTailRight) = MoveRight(headPos, tailPos, headMove.amount);
                        headPos = newHeadRight;
                        tailPos = newTailRight;
                        break;
                   default: break;
                }
            }

            return tailTraveledPositions.Count;
        }

        private void AddToTraveled(List<(int x, int y)> traveledPositions, (int x, int y) tail)
        {
            if (!traveledPositions.Contains(tail))
            {
                traveledPositions.Add(tail);
            }
        }

        private void AddToTraveled(List<(int x, int y, bool isTail)> traveledPositions, (int x, int y, bool isTail) tail)
        {
            if (!traveledPositions.Contains(tail))
            {
                traveledPositions.Add(tail);
            }
        }

        private bool IsAdjacent((int x, int y) headPos, (int x, int y) tailPos)
        {
            return (headPos == tailPos)
                || (headPos.x == tailPos.x && Math.Abs(headPos.y - tailPos.y) == 1)
                || (headPos.y == tailPos.y && Math.Abs(headPos.x - tailPos.x) == 1)
                || (Math.Abs(headPos.y - tailPos.y) == 1 && Math.Abs(headPos.x - tailPos.x) == 1);
        }

        private bool IsAdjacent((int x, int y, bool isTail) headPos, (int x, int y, bool isTail) tailPos)
        {
            return (headPos.x == tailPos.x && headPos.y == tailPos.y)
                || (headPos.x == tailPos.x && Math.Abs(headPos.y - tailPos.y) == 1)
                || (headPos.y == tailPos.y && Math.Abs(headPos.x - tailPos.x) == 1)
                || (Math.Abs(headPos.y - tailPos.y) == 1 && Math.Abs(headPos.x - tailPos.x) == 1);
        }

        private (int newTailX, int newTailY) MoveTail((int x, int y) head, (int x, int y) tail)
        {
            (int newX, int newY) newTail = (-1, -1);
            if (head.x == tail.x && head.y != tail.y)
            {
                newTail.newX = tail.x;
                if (head.y > tail.y)
                {
                    newTail.newY = tail.y + 1;
                }
                else
                {
                    newTail.newY = tail.y - 1;
                }
            }

            if (head.y == tail.y && head.x != tail.x)
            {
                newTail.newY = tail.y;
                if (head.x > tail.x)
                {
                    newTail.newX = tail.x + 1;
                }
                else
                {
                    newTail.newX = tail.x - 1;
                }
            }

            if (head.y > tail.y && head.x > tail.x)
            {
                newTail.newX = tail.x + 1;
                newTail.newY = tail.y + 1;
            }

            if (head.y > tail.y && head.x < tail.x)
            {
                newTail.newX = tail.x - 1;
                newTail.newY = tail.y + 1;
            }

            if (head.y < tail.y && head.x > tail.x)
            {
                newTail.newX = tail.x + 1;
                newTail.newY = tail.y - 1;
            }

            if (head.y < tail.y && head.x < tail.x)
            {
                newTail.newX = tail.x - 1;
                newTail.newY = tail.y - 1;
            }

            AddToTraveled(tailTraveledPositions, newTail);
            return newTail;
        }

        private (int newTailX, int newTailY, bool isTail) MoveTail((int x, int y, bool isTail) head, (int x, int y, bool isTail) tail)
        {
            (int newX, int newY, bool isTail) newTail = (-1, -1, tail.isTail);
            if (head.x == tail.x && head.y != tail.y)
            {
                newTail.newX = tail.x;
                if (head.y > tail.y)
                {
                    newTail.newY = tail.y + 1;
                }
                else
                {
                    newTail.newY = tail.y - 1;
                }
            }

            if (head.y == tail.y && head.x != tail.x)
            {
                newTail.newY = tail.y;
                if (head.x > tail.x)
                {
                    newTail.newX = tail.x + 1;
                }
                else
                {
                    newTail.newX = tail.x - 1;
                }
            }

            if (head.y > tail.y && head.x > tail.x)
            {
                newTail.newX = tail.x + 1;
                newTail.newY = tail.y + 1;
            }

            if (head.y > tail.y && head.x < tail.x)
            {
                newTail.newX = tail.x - 1;
                newTail.newY = tail.y + 1;
            }

            if (head.y < tail.y && head.x > tail.x)
            {
                newTail.newX = tail.x + 1;
                newTail.newY = tail.y - 1;
            }

            if (head.y < tail.y && head.x < tail.x)
            {
                newTail.newX = tail.x - 1;
                newTail.newY = tail.y - 1;
            }

            if (tail.isTail)
            {
                AddToTraveled(tailTraveledPositions2, newTail);
            }

            return newTail;
        }

        private ((int newHeadx, int newHeady), (int newTailx, int newTaily)) MoveUp((int x, int y) head, (int x, int y) tail, int amount)
        {
            var currentHead = head;
            var currentTail = tail;

            for (var i = 0; i < amount; i++)
            {
                currentHead.y++;

                if (!IsAdjacent(currentHead, currentTail))
                {
                    currentTail = MoveTail(currentHead, currentTail);
                }
            }

            return (currentHead, currentTail);
        }

        private ((int headx, int heady), (int tailx, int taily)) MoveDown((int x, int y) head, (int x, int y) tail, int amount)
        {
            var currentHead = head;
            var currentTail = tail;

            for (var i = 0; i < amount; i++)
            {
                currentHead.y--;

                if (!IsAdjacent(currentHead, currentTail))
                {
                    currentTail = MoveTail(currentHead, currentTail);
                }
            }

            return (currentHead, currentTail);
        }

        private ((int headx, int heady), (int tailx, int taily)) MoveLeft((int x, int y) head, (int x, int y) tail, int amount)
        {
            var currentHead = head;
            var currentTail = tail;

            for (var i = 0; i < amount; i++)
            {
                currentHead.x--;

                if (!IsAdjacent(currentHead, currentTail))
                {
                    currentTail = MoveTail(currentHead, currentTail);
                }
            }

            return (currentHead, currentTail);
        }

        private ((int headx, int heady), (int tailx, int taily)) MoveRight((int x, int y) head, (int x, int y) tail, int amount)
        {
            var currentHead = head;
            var currentTail = tail;

            for (var i = 0; i < amount; i++)
            {
                currentHead.x++;

                if (!IsAdjacent(currentHead, currentTail))
                {
                    currentTail = MoveTail(currentHead, currentTail);
                }
            }

            return (currentHead, currentTail);
        }

        private (int newX, int newY, bool isTail)[] MoveKnotsUp((int x, int y, bool isTail)[] knots, int amount)
        {

            for (var i = 0; i < amount; i++)
            {
                knots[0].y++;

                for (var knotPos = 1; knotPos < knots.Length; knotPos++)
                {
                    if (!IsAdjacent(knots[knotPos-1], knots[knotPos]))
                    {
                        knots[knotPos] = MoveTail(knots[knotPos - 1], knots[knotPos]);
                    }
                }

            }

            return knots;
        }

        private (int newX, int newY, bool isTail)[] MoveKnotsDown((int x, int y, bool isTail)[] knots, int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                knots[0].y--;

                for (var knotPos = 1; knotPos < knots.Length; knotPos++)
                {
                    if (!IsAdjacent(knots[knotPos - 1], knots[knotPos]))
                    {
                        knots[knotPos] = MoveTail(knots[knotPos - 1], knots[knotPos]);
                    }
                }

            }

            return knots;
        }

        private (int newX, int newY, bool isTail)[] MoveKnotsLeft((int x, int y, bool isTail)[] knots, int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                knots[0].x--;

                for (var knotPos = 1; knotPos < knots.Length; knotPos++)
                {
                    if (!IsAdjacent(knots[knotPos - 1], knots[knotPos]))
                    {
                        knots[knotPos] = MoveTail(knots[knotPos - 1], knots[knotPos]);
                    }
                }

            }

            return knots;
        }

        private (int newX, int newY, bool isTail)[] MoveKnotsRight((int x, int y, bool isTail)[] knots, int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                knots[0].x++;

                for (var knotPos = 1; knotPos < knots.Length; knotPos++)
                {
                    if (!IsAdjacent(knots[knotPos - 1], knots[knotPos]))
                    {
                        knots[knotPos] = MoveTail(knots[knotPos - 1], knots[knotPos]);
                    }
                }

            }

            return knots;
        }


        public object ExecutePartTwo()
        {
            (int x, int y, bool isTail)[] knots = Enumerable.Range(0, 10).Select((_, i) => (0, 0, i == 9)).ToArray();
            var head = knots.First();

            tailTraveledPositions2 = new List<(int x, int y, bool isTail)> { knots.Last() };
            var currentKnots = knots;

            foreach (var headMove in input)
            {
                switch (headMove.direction)
                {
                    case 'U':
                        currentKnots = MoveKnotsUp(currentKnots, headMove.amount);
                        break;
                    case 'D':
                        currentKnots = MoveKnotsDown(currentKnots, headMove.amount);
                        break;
                    case 'L':
                        currentKnots = MoveKnotsLeft(currentKnots, headMove.amount);
                        break;
                    case 'R':
                        currentKnots = MoveKnotsRight(currentKnots, headMove.amount);
                        break;
                    default: break;
                }
            }

            return tailTraveledPositions2.Count;
        }
    }
}
