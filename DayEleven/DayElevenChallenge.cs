using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class DayElevenChallenge : IDayChallenge
    {
        private readonly string[] inputPath = { "DayEleven", "input.txt" };
        public string[] InputPath => inputPath;
        private string[] input;

        List<(Queue<long> items, Func<long, long> operation, int testDivisor, int trueTarget, int falseTarget, int itemsInspected)> monkeys;
        List<Monkey> monkeyz = new List<Monkey>();

        class Monkey
        {
            public Queue<long> Items { get; }
            public Func<long, long> Operation { get; }
            public int TestDivisor { get; }
            public int ItemsInspected { get; private set; }
            public int TrueTargetIndex { get; }
            public int FalseTargetIndex { get; }


            public Monkey TrueTarget { get; private set; }
            public Monkey FalseTarget { get; private set; }

            public Monkey(Queue<long> items, Func<long, long> operation, int testDivisor, int trueTarget, int falseTarget)
            {
                Items = items;
                Operation = operation;
                TestDivisor = testDivisor;
                TrueTargetIndex = trueTarget;
                FalseTargetIndex = falseTarget;
            }

            public void InspectItem()
            {
                ItemsInspected++;
            }

            public void SetTargets(Monkey trueTarget, Monkey falseTarget)
            {
                TrueTarget = trueTarget;
                FalseTarget = falseTarget;
            }
        }

        public DayElevenChallenge()
        {
            input = File.ReadAllLines(Path.Combine(InputPath));
            monkeys = new();

            (Queue<long> items, Func<long, long> operation, int testDivisor, int trueTarget, int falseTarget, int itemsInspected) currentMonkey = new();
            foreach (var line in input)
            {
                if (line.Trim().StartsWith("Monkey"))
                {
                    currentMonkey = (new Queue<long>(), null, -1, -1, -1, 0);
                }
                else if (line.Trim().StartsWith("Starting items:"))
                {
                    var splitText = line.Trim().Split("Starting items: ");
                    var items = splitText[1].Split(", ").Select(i => int.Parse(i));
                    foreach (var item in items)
                    {
                        currentMonkey.items.Enqueue(item);
                    }
                }

                else if (line.Trim().StartsWith("Operation:"))
                {
                    var splitText = line.Trim().Split("Operation: ");
                    var splitOp = splitText[1].Split(" = ");
                    var splitSymbols = splitOp[1].Split(" ");

                    Func<long, long> operation = (x) =>
                    {
                        var arg1 = splitSymbols[0] == "old" ? x : int.Parse(splitSymbols[0]);
                        var arg2 = splitSymbols[2] == "old" ? x : int.Parse(splitSymbols[2]); 

                        if (splitSymbols[1] == "+")
                        {
                            return arg1 + arg2;
                        }
                        else
                        {
                            return arg1 * arg2;
                        }                        
                    };
                    currentMonkey.operation = operation;
                }
                else if (line.Trim().StartsWith("Test:"))
                {
                    var splitTest = line.Trim().Split("Test: divisible by ");
                    var divisor = int.Parse(splitTest[1]);
                    currentMonkey.testDivisor = divisor;
                }
                else if (line.Trim().StartsWith("If true:"))
                {
                    var splitTest = line.Trim().Split("If true: throw to monkey ");
                    var trueMonkey = int.Parse(splitTest[1]);
                    currentMonkey.trueTarget = trueMonkey;
                }
                else if (line.Trim().StartsWith("If false:"))
                {
                    var splitTest = line.Trim().Split("If false: throw to monkey ");
                    var falseMonkey = int.Parse(splitTest[1]);
                    currentMonkey.falseTarget = falseMonkey;
                    monkeys.Add(currentMonkey);
                    monkeyz.Add(new Monkey(currentMonkey.items, currentMonkey.operation, currentMonkey.testDivisor, currentMonkey.trueTarget, currentMonkey.falseTarget));
                }
            }

            foreach(var monkey in monkeyz)
            {
                var trueMonkey = monkeyz[monkey.TrueTargetIndex];
                var falseMonkey = monkeyz[monkey.FalseTargetIndex];

                monkey.SetTargets(trueMonkey, falseMonkey);
            }
        }


        public object ExecutePartOne()
        {
            var roundLimit = 20;
            var currentRound = 0;
            while (currentRound < roundLimit)
            {
                currentRound++;
                foreach (var monkey in monkeyz)
                {
                    while (monkey.Items.TryDequeue(out var item))
                    {
                        monkey.InspectItem();

                        var newValue = monkey.Operation(item);
                        var testValue = (long)Math.Floor(newValue / 3.0);
                        if (testValue % monkey.TestDivisor == 0)
                        {
                            monkey.TrueTarget.Items.Enqueue(testValue);
                        }
                        else
                        {
                            monkey.FalseTarget.Items.Enqueue(testValue);
                        }

                    }

                }
            }
            return monkeyz.Select(m => m.ItemsInspected).OrderByDescending(i => i).Take(2).Aggregate((a, b) =>  a * b);
        }


        public object ExecutePartTwo()
        {
            var roundLimit = 10000;
            var currentRound = 0;
            var factor = monkeyz.Select(m => m.TestDivisor).Aggregate((a, b) => a * b);

            while (currentRound < roundLimit)
            {
                currentRound++;
                foreach (var monkey in monkeyz)
                {
                    while (monkey.Items.TryDequeue(out var item))
                    {
                        monkey.InspectItem();

                        var newValue = monkey.Operation(item);
                        var testValue = newValue % factor;
                        if (testValue % monkey.TestDivisor == 0)
                        {
                            monkey.TrueTarget.Items.Enqueue(testValue);
                        }
                        else
                        {
                            monkey.FalseTarget.Items.Enqueue(testValue);
                        }

                    }

                }
            }
            return monkeyz.Select(m => m.ItemsInspected).OrderByDescending(i => i).Take(2).Aggregate((a, b) => a * b);
        }
    }
}
