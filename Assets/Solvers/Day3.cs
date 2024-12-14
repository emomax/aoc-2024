using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Solvers
{
    public class Day3 : Day
    {
        public long SolveA(string[] lines)
        {
            string pattern = @"mul\((-?\d{1,3}),(-?\d{1,3})\)";
            var rg = new Regex(pattern);

            long sum = 0;
            foreach (var line in lines)
            {
                MatchCollection matches = rg.Matches(line);

                foreach (Match match in matches)
                {
                    if (!Int64.TryParse(match.Groups[1].Value, out long a))
                        throw new Exception($"Couldn't parse '{match.Groups[1]}' to int!");
                    if (!Int64.TryParse(match.Groups[2].Value, out long b))
                        throw new Exception($"Couldn't parse '{match.Groups[2]}' to int!");

                    sum += a * b;
                }
            }

            return (int) sum;
        }

        public long SolveB(string[] lines)
        {
            string pattern = @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don\'t\(\)";
            var rg = new Regex(pattern);

            long sum = 0;
            var isDoing = true;
            
            foreach (var line in lines)
            {
                MatchCollection matches = rg.Matches(line);

                foreach (Match match in matches)
                {
                    var matchVal = match.Groups[0].Value;
                    switch (matchVal)
                    {
                        case "do()":
                            isDoing = true;
                            continue;
                        case "don't()":
                            isDoing = false;
                            continue;
                    }

                    if (!isDoing)
                        continue;

                    if (!Int64.TryParse(match.Groups[1].Value, out long a))
                        throw new Exception($"Couldn't parse '{match.Groups[1]}' to int!");
                    if (!Int64.TryParse(match.Groups[2].Value, out long b))
                        throw new Exception($"Couldn't parse '{match.Groups[2]}' to int!");

                    sum += a * b;
                }
            }

            return (int) sum;
        }

        public int GetDay() => 3;
    }
}