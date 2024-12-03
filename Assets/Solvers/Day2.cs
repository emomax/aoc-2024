using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Solvers
{
    public class Day2 : Day
    {
        public int SolveA(string[] lines)
        {
            var safeLevels = 0;

            foreach (var line in lines)
            {
                var tokens = line.Split(' ');
                var levels = new int[tokens.Length];

                var i = 0;
                for (i = 0; i < tokens.Length; i++)
                {
                    if (!Int32.TryParse(tokens[i], out int num))
                        throw new Exception($"Couldn't parse '{tokens[0]}' to int!");

                    levels[i] = num;
                }

                if (IsSafeLevel(levels, i))
                    safeLevels++;
            }

            return safeLevels;
        }

        private bool IsSafeLevel(int[] levels, int max)
        {
            var isAscending = levels[0] < levels[max - 1];

            if (isAscending)
            {
                for (int i = 1; i < max; i++)
                {
                    var diff = levels[i] - levels[i - 1];
                    if (diff == 0) // no increase
                        return false;

                    if (diff < 0)
                        return false;

                    if (diff > 3)
                        return false;
                }

                return true;
            }

            // Descending
            for (int i = 1; i < max; i++)
            {
                var diff = levels[i] - levels[i - 1];
                if (diff == 0) // no decrease
                    return false;

                if (diff > 0)
                    return false;

                if (diff < -3)
                    return false;
            }

            return true;
        }

        public int SolveB(string[] lines)
        {
            var safeLevels = 0;

            foreach (var line in lines)
            {
                var tokens = line.Split(' ');
                var levels = new int[tokens.Length];
                var i = 0;

                foreach (var token in tokens)
                {
                    if (!Int32.TryParse(tokens[i], out int num))
                        throw new Exception($"Couldn't parse '{tokens[0]}' to int!");

                    levels[i++] = num;
                }

                var levelIsSafe = IsSafeLevelWithModule(levels, tokens.Length);
                if (levelIsSafe)
                    safeLevels++;
            }

            return safeLevels;
        }

        private bool IsSafeLevelWithModule(int[] levels, int max)
        {
            var hasEncounteredError = false;
            var hasEncounteredErrorThisFrame = false;
            
            var isAscending = true;

            // Compare start to average to see if it's 
            // ascending or descending
            var sum = 0.0;
            for (int i = 0; i < max; i++)
                sum += levels[0] > levels[i] ? 1 : -1;

            if (sum > 0)
                isAscending = false;

            // Since start at second element, ensure first element is not an offender
            var allButFirst = new List<int>();
            for (var i = 1; i < max; i++)
                allButFirst.Add(levels[i]);
            
            if (IsSafeLevel(allButFirst.ToArray(), max - 1))
                return true;
                
            if (isAscending)
            {
                for (int i = 1; i < max; i++)
                {
                    var diff = levels[i] - (hasEncounteredErrorThisFrame ? levels[i - 2] : levels[i - 1]);
                    hasEncounteredErrorThisFrame = false;
                    if (diff == 0) // no increase {
                    {
                        if (hasEncounteredError)
                            return false;

                        hasEncounteredError = true;
                        hasEncounteredErrorThisFrame = true;
                        continue;
                    }

                    if (diff < 0)
                    {
                        if (hasEncounteredError)
                            return false;

                        hasEncounteredError = true;
                        hasEncounteredErrorThisFrame = true;
                        continue;
                    }

                    if (diff > 3)
                    {
                        if (hasEncounteredError)
                            return false;

                        hasEncounteredError = true;
                        hasEncounteredErrorThisFrame = true;
                        continue;
                    }
                }

                return true;
            }

            // Descending
            for (int i = 1; i < max; i++)
            {
                var diff = levels[i] - (hasEncounteredErrorThisFrame ? levels[i - 2] : levels[i - 1]);
                hasEncounteredErrorThisFrame = false;
                if (diff == 0) // no decrease
                {
                    if (hasEncounteredError)
                        return false;

                    hasEncounteredErrorThisFrame = true;
                    hasEncounteredError = true;
                    continue;
                }

                if (diff > 0)
                {
                    if (hasEncounteredError)
                        return false;

                    hasEncounteredErrorThisFrame = true;
                    hasEncounteredError = true;
                    continue;
                }

                if (diff < -3)
                {
                    if (hasEncounteredError)
                        return false;

                    hasEncounteredErrorThisFrame = true;
                    hasEncounteredError = true;
                    continue;
                }
            }

            return true;
        }

        public int GetDay() => 2;
    }
}