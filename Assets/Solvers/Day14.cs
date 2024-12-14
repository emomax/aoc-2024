using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Solvers
{
    public class Day14 : Day
    {
        private readonly int w;
        private readonly int h;

        public Day14(int w, int h)
        {
            this.w = w;
            this.h = h;
        }

        public long SolveA(string[] lines)
        {
            var positions = new List<Vector2Int>(lines.Length);
            var velocities = new List<Vector2Int>(lines.Length);
            string pattern = @"p\=(\d+)\,(\d+)\ v\=(-?\d+)\,(-?\d+)";
            var regex = new Regex(pattern);

            foreach (var line in lines)
            {
                var matches = regex.Match(line);

                if (!Int32.TryParse(matches.Groups[1].Value, out int p1))
                    throw new Exception($"Couldn't parse '{matches.Groups[1]}' to long!");
                if (!Int32.TryParse(matches.Groups[2].Value, out int p2))
                    throw new Exception($"Couldn't parse '{matches.Groups[2]}' to long!");

                if (!Int32.TryParse(matches.Groups[3].Value, out int v1))
                    throw new Exception($"Couldn't parse '{matches.Groups[1]}' to Int32!");
                if (!Int32.TryParse(matches.Groups[4].Value, out int v2))
                    throw new Exception($"Couldn't parse '{matches.Groups[2]}' to Int32!");

                positions.Add(new Vector2Int(p1, p2));
                velocities.Add(new Vector2Int(v1, v2));
            }

            for (var j = 0; j < 100; j++)
            {
                for (var i = 0; i < positions.Count; i++)
                {
                    var pos = positions[i];
                    var vel = velocities[i];

                    pos += vel;
                    if (pos.y < 0)
                        pos.y += h;
                    if (pos.y > h - 1)
                        pos.y -= h;
                    if (pos.x < 0)
                        pos.x += w;
                    if (pos.x > w - 1)
                        pos.x -= w;

                    positions[i] = pos;
                }
            }

            Debug.Log("After");
            print(positions, true);

            var botsInUpperLeftQuadrant = 0;
            var botsInLowerLeftQuadrant = 0;
            var botsInUpperRightQuadrant = 0;
            var botsInLowerRightQuadrant = 0;

            for (int i = 0; i < positions.Count; i++)
            {
                if (positions[i].x == w / 2)
                    continue;
                if (positions[i].y == h / 2)
                    continue;

                if (positions[i].x < w / 2)
                {
                    if (positions[i].y < h / 2)
                    {
                        botsInUpperLeftQuadrant++;
                        continue;
                    }

                    botsInLowerLeftQuadrant++;
                    continue;
                }

                if (positions[i].y < h / 2)
                {
                    botsInUpperRightQuadrant++;
                    continue;
                }

                botsInLowerRightQuadrant++;
            }

            return botsInLowerLeftQuadrant * botsInLowerRightQuadrant * botsInUpperLeftQuadrant *
                   botsInUpperRightQuadrant;
        }

        private bool possibleChristmasTree(List<Vector2Int> positions)
        {
            var positionLookup = new Dictionary<Vector2Int, int>();
            foreach (var i in positions)
            {
                if (!positionLookup.ContainsKey(i))
                    positionLookup.Add(i, 0);

                positionLookup[i]++;
            }
            var printedTree = new List<string>();

            for (int y = 0; y < h; y++)
            {
                var sb = new StringBuilder();
                for (int x = 0; x < w; x++)
                {
                    var currPos = new Vector2Int(x, y);
                    if (positionLookup.ContainsKey(currPos))
                    {
                        sb.Append("*");
                        continue;
                    }

                    sb.Append(" ");
                }

                printedTree.Add(sb.ToString());
            }

            var pattern1 = @"\*\*\*"; 
            var pattern2 = @"\*\*\*\*\*";
            var pattern3 = @"\*\*\*\*\*\*\*";

            var regexes = new List<Regex>()
            {
                new(pattern1),
                new(pattern2),
                new(pattern3)
            };

            var currRegex = 0;

            for (var i = 0; i < printedTree.Count; i++)
            {
                if (regexes[currRegex].IsMatch(printedTree[i]))
                {
                    currRegex++;

                    if (currRegex >= regexes.Count)
                        return true;
                    continue;
                }

                currRegex = 0;
            }

            return false;
        }

        private void print(List<Vector2Int> positions, bool omitMiddle = false, int num = -1)
        {
            var poses = new Dictionary<Vector2Int, int>();

            foreach (var i in positions)
            {
                if (!poses.ContainsKey(i))
                    poses.Add(i, 0);

                poses[i]++;
            }

            var sb = new StringBuilder();
            if (num != -1)
                sb.Append($" --  {num} --\n");
            for (int y = 0; y < h; y++)
            {
                if (omitMiddle && y == h / 2)
                {
                    sb.Append("\n");
                    continue;
                }

                for (int x = 0; x < w; x++)
                {
                    if (omitMiddle && x == w / 2)
                    {
                        sb.Append("   ");
                        continue;
                    }

                    var currPos = new Vector2Int(x, y);
                    if (poses.ContainsKey(currPos))
                    {
                        sb.Append("# ");
                        continue;
                    }

                    sb.Append("- ");
                }

                sb.Append("\n");
            }

            sb.Append("----------");

            Debug.Log(sb.ToString());
        }

        public long SolveB(string[] lines)
        {
            var positions = new List<Vector2Int>(lines.Length);
            var velocities = new List<Vector2Int>(lines.Length);
            string pattern = @"p\=(\d+)\,(\d+)\ v\=(-?\d+)\,(-?\d+)";
            var regex = new Regex(pattern);

            foreach (var line in lines)
            {
                var matches = regex.Match(line);

                if (!Int32.TryParse(matches.Groups[1].Value, out int p1))
                    throw new Exception($"Couldn't parse '{matches.Groups[1]}' to long!");
                if (!Int32.TryParse(matches.Groups[2].Value, out int p2))
                    throw new Exception($"Couldn't parse '{matches.Groups[2]}' to long!");

                if (!Int32.TryParse(matches.Groups[3].Value, out int v1))
                    throw new Exception($"Couldn't parse '{matches.Groups[1]}' to Int32!");
                if (!Int32.TryParse(matches.Groups[4].Value, out int v2))
                    throw new Exception($"Couldn't parse '{matches.Groups[2]}' to Int32!");

                positions.Add(new Vector2Int(p1, p2));
                velocities.Add(new Vector2Int(v1, v2));
            }

            for (var j = 0; j < 100000; j++)
            {
                for (var i = 0; i < positions.Count; i++)
                {
                    var pos = positions[i];
                    var vel = velocities[i];

                    pos += vel;
                    if (pos.y < 0)
                        pos.y += h;
                    if (pos.y > h - 1)
                        pos.y -= h;
                    if (pos.x < 0)
                        pos.x += w;
                    if (pos.x > w - 1)
                        pos.x -= w;

                    positions[i] = pos;
                }

                if (possibleChristmasTree(positions))
                {
                    print(positions, false, j);
                    return j + 1;
                }
            }

            return -1;
        }

        public int GetDay() => 14;
    }
}