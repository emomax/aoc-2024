using System;
using System.Collections.Generic;
using UnityEngine;

namespace Solvers
{
    public class Day1 : Day
    {
        public int SolveA(string[] lines)
        {
            var left = new int[lines.Length];
            var right = new int[lines.Length];

            var caret = 0;

            foreach (var line in lines)
            {
                var tokens = line.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);

                if (!Int32.TryParse(tokens[0], out int l))
                    throw new Exception($"Couldn't parse '{tokens[0]}' to int!");
            
                if (!Int32.TryParse(tokens[1], out int r))
                    throw new Exception($"Couldn't parse '{tokens[1]}' to int!");

                left[caret] = l;
                right[caret] = r;

                caret++;
            }
        
            Array.Sort(left);
            Array.Sort(right);

            var diff = 0;

            for (var i = 0; i < caret; i++)
                diff += Mathf.Abs(left[i] - right[i]);

            return diff;
        }

        public int SolveB(string[] lines)
        {
            var left = new int[lines.Length];
            var right = new Dictionary<int, int>();

            var caret = 0;

            foreach (var line in lines)
            {
                var tokens = line.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);

                if (!Int32.TryParse(tokens[0], out int l))
                    throw new Exception($"Couldn't parse '{tokens[0]}' to int!");
            
                if (!Int32.TryParse(tokens[1], out int r))
                    throw new Exception($"Couldn't parse '{tokens[1]}' to int!");

                left[caret++] = l;

                if (right.ContainsKey(r))
                    right[r]++;
                else
                    right[r] = 1;
            }

            var diff = 0;

            for (var i = 0; i < caret; i++)
            {
                var baseVal = left[i];
                if (!right.ContainsKey(baseVal))
                    continue;
                
                diff += baseVal * right[baseVal];
            }

            return diff;
        }

        public int GetDay() => 1;
    }
}