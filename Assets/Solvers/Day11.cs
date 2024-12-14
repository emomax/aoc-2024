using System;
using System.Collections.Generic;

namespace Solvers
{
    public class Day11 : Day
    {
        public long SolveA(string[] lines)
        {
            var tokens = lines[0].Split(" ");
            var startList = new List<long>();
            foreach (var token in tokens)
            {
                if (!Int64.TryParse(token, out long stone))
                    throw new Exception($"Couldn't parse '{token}' to int!");
                startList.Add(stone);
            }

            var steps = 10;
            var currentList = new List<long>(startList);
            for (int i = 0; i < 25; i++)
            {
                var nextList = new List<long>();
                foreach (var stone in currentList)
                {
                    nextList.AddRange(transformStone(stone));
                }

                currentList.Clear();
                currentList.AddRange(nextList);
            }

            return currentList.Count;
        }

        List<long> transformStone(long stone)
        {
            if (stone == 0)
                return new List<long>() {1};

            var stoneStr = $"{stone}";
            if (stoneStr.Length % 2 == 0)
            {
                var part1 = stoneStr.Substring(0, stoneStr.Length / 2);
                var part2 = stoneStr.Substring(stoneStr.Length / 2);

                if (!Int64.TryParse(part1, out long parsed1))
                    throw new Exception($"Couldn't parse '{part1}' to int!");
                if (!Int64.TryParse(part2, out long parsed2))
                    throw new Exception($"Couldn't parse '{part2}' to int!");

                return new List<long>() {parsed1, parsed2};
            }

            return new List<long>() {stone * 2024};
        }

        public long SolveB(string[] lines)
        {
            var tokens = lines[0].Split(" ");
            var stoneBucket = new Dictionary<long, long>();
            foreach (var token in tokens)
            {
                if (!Int64.TryParse(token, out long stone))
                    throw new Exception($"Couldn't parse '{token}' to int!");

                if (!stoneBucket.ContainsKey(stone))
                    stoneBucket.Add(stone, 0);

                stoneBucket[stone]++;
            }

            var steps = 75;
            for (int i = 0; i < steps; i++)
            {
                var keys = new List<long>();
                foreach (var k in stoneBucket.Keys)
                    if (stoneBucket[k] != 0)
                        keys.Add(k);
                // Add new stones to a separate bucket so as not to
                // mess up current calculations
                var toAdd = new Dictionary<long, long>();
                foreach (var stone in keys)
                {
                    var stones = stoneBucket[stone];
                    foreach (var newStone in transformStone(stone))
                    {
                        if (!toAdd.ContainsKey(newStone))
                            toAdd.Add(newStone, 0);

                        toAdd[newStone] += stones;
                    }

                    stoneBucket[stone] -= stones;
                }

                // Dump all new stones back in the bucket
                foreach (var (k, v) in toAdd)
                {
                    if (!stoneBucket.ContainsKey(k))
                        stoneBucket.Add(k, 0);

                    stoneBucket[k] = v;
                }
            }

            long sum = 0;
            foreach (var (k, v) in stoneBucket)
                sum += v;

            return sum;
        }

        public int GetDay() => 11;
    }
}