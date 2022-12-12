using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class DaySevenChallenge : IDayChallenge
    {
        public string InputPath => @"DaySeven\input.txt";
        private string[] input;
        private Directory root;

        public DaySevenChallenge()
        {
            input = File.ReadAllLines(InputPath);
            root = new Directory("/", null);
            var context = root;

            foreach (var line in input)
            {
                if (IsChangeToRootCommand(line))
                {
                    context = root;
                } 
                else if (IsChangeUpCommand(line))
                {
                    context = context?.Parent;
                } 
                else if (IsChangeInCommand(line))
                {
                    var directory = new Directory(line.Split(" ")[2], context);
                    context?.Children.Add(directory);
                    context = directory;
                }
                else
                {
                    var fileCheck = IsFileLine(line);
                    if (fileCheck.isFile)
                    {
                        context?.Children.Add(new FileEntry(fileCheck.name, fileCheck.size));
                    }
                }
                
            }
        }

        public object ExecutePartOne()
        {
            var totalThresholdBreach = 0L;
            var threshold = 100000L;
            var directoriesToTraverse = new Queue<Directory>();
            directoriesToTraverse.Enqueue(root);

            var rootSize = root.GetSize();
            if (rootSize <= threshold)
            {
                totalThresholdBreach += rootSize;
            }
            while (directoriesToTraverse.TryDequeue(out var nextDir))
            {
                var currentSize = nextDir.GetSize();
                if (currentSize <= threshold)
                {
                    totalThresholdBreach += currentSize;
                }
                foreach (var child in nextDir.Children.Where(c => c is Directory))
                {
                    directoriesToTraverse.Enqueue((Directory)child);
                }
            }
            return totalThresholdBreach;
        }


        public object ExecutePartTwo()
        {
            long totalSpace = 70000000;
            long requiredFreeSpace = 30000000;
            var currentUsedSpace = root.GetSize();
            var currentFreeSpace = totalSpace - currentUsedSpace;
            var spaceRequiredToBeFreed = requiredFreeSpace - currentFreeSpace;
            var directoriesToTraverse = new Queue<Directory>();
            directoriesToTraverse.Enqueue(root);
            var eligibleSizes = new List<long>();

            while (directoriesToTraverse.TryDequeue(out var nextDir))
            {
                var currentSize = nextDir.GetSize();
                if (currentSize >= spaceRequiredToBeFreed)
                {
                    eligibleSizes.Add(currentSize);
                }
                foreach (var child in nextDir.Children.Where(c => c is Directory))
                {
                    directoriesToTraverse.Enqueue((Directory)child);
                }
            }

            return eligibleSizes.Min();
        }

        private bool IsChangeToRootCommand(string line)
        {
            return line == "$ cd /";
        }
        private bool IsChangeUpCommand(string line)
        {
            return line == "$ cd ..";
        }

        private bool IsChangeInCommand(string line)
        {
            return line.StartsWith("$ cd");
        }

        private bool IsListCommand(string line)
        {
            return line.StartsWith("$ ls");
        }

        private (bool isFile, string name, long size) IsFileLine(string line)
        {
            var splitLine = line.Split(" ");
            return (long.TryParse(splitLine[0], out var size), splitLine[1], size);
        }
    }

    internal interface IFileSystemItem
    {
        long GetSize();
        bool HasChildren();
    }

    internal class Directory : IFileSystemItem
    {
        public Directory(string path, Directory? parent)
        {
            Path = path;
            Parent = parent;
            Children = new List<IFileSystemItem>();
        }

        public Directory? Parent { get; }
        public string Path { get;  }
        public List<IFileSystemItem> Children { get; }

        public long GetSize()
        {
            return Children.Select(c => c.GetSize()).Sum();
        }

        public bool HasChildren()
        {
            return Children.Any();
        }
           
    }

    internal class FileEntry : IFileSystemItem
    {
        public FileEntry(string name, long size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; }
        public long Size { get; }

        public long GetSize()
        {
            return Size;
        }

        public bool HasChildren()
        {
            return false;
        }
    }
}
