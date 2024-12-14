using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Solvers
{
    public class Day8 : Day

    {
        public long SolveA(string[] lines)
        {
            string pattern = @"[a-zA-Z0-9]";
            var rg = new Regex(pattern);

            var antennas = new Dictionary<string, List<Vector2Int>>();
            var col = 0;
            
            foreach (var line in lines)
            {
                MatchCollection matches = rg.Matches(line);

                foreach (Match match in matches)
                {
                    if (!antennas.ContainsKey(match.Value))
                        antennas[match.Value] = new List<Vector2Int>();
                         
                    antennas[match.Value].Add(new Vector2Int(match.Index, col));
                }

                col++;
            }

            var antiNodes = CalculateAntiNodes(antennas, lines[0].Length, lines.Length);
            return antiNodes.Count;
        }

        private HashSet<Vector2Int> CalculateAntiNodes(Dictionary<string, List<Vector2Int>> antennas, int w, int h)
        {
            var antinodes = new HashSet<Vector2Int>();
            foreach (var (antennaID, positions) in antennas)
            {
                foreach (var position in positions)
                {
                    foreach (var position2 in positions)
                    {
                        if (position2 == position)
                            continue;

                        var distanceVec = position2 - position;
                        
                        var an1 = position - distanceVec;
                        var an2 = position2 + distanceVec;

                        if (an1.x >= 0 && an1.x < w && an1.y >= 0 && an1.y < h)
                            antinodes.Add(an1);
                        if (an2.x >= 0 && an2.x < w && an2.y >= 0 && an2.y < h)
                            antinodes.Add(an2);
                    }
                }
            }

            return antinodes;
        }

        public long SolveB(string[] lines)
        {
            string pattern = @"[a-zA-Z0-9]";
            var rg = new Regex(pattern);

            var antennas = new Dictionary<string, List<Vector2Int>>();
            var col = 0;
            
            foreach (var line in lines)
            {
                MatchCollection matches = rg.Matches(line);

                foreach (Match match in matches)
                {
                    if (!antennas.ContainsKey(match.Value))
                        antennas[match.Value] = new List<Vector2Int>();
                         
                    antennas[match.Value].Add(new Vector2Int(match.Index, col));
                }

                col++;
            }

            var antiNodes = CalculateAntiNodes2(antennas, lines[0].Length, lines.Length);
            return antiNodes.Count;
        }
        
        private HashSet<Vector2Int> CalculateAntiNodes2(Dictionary<string, List<Vector2Int>> antennas, int w, int h)
        {
            var antinodes = new HashSet<Vector2Int>();
            foreach (var (antennaID, positions) in antennas)
            {
                foreach (var position in positions)
                {
                    foreach (var position2 in positions)
                    {
                        if (position2 == position)
                            continue;

                        var distanceVec = position2 - position;

                        // Per antenna
                        antinodes.Add(position);
                        antinodes.Add(position2);

                        // Direction 1
                        var currPos = position - distanceVec;
                        while (currPos.x >= 0 && currPos.x < w && currPos.y >= 0 && currPos.y < h)
                        {
                            antinodes.Add(currPos);
                            currPos -= distanceVec;
                        }

                        currPos = position2 + distanceVec;
                        while (currPos.x >= 0 && currPos.x < w && currPos.y >= 0 && currPos.y < h)
                        {
                            antinodes.Add(currPos);
                            currPos += distanceVec;
                        }
                    }
                }
            }

            return antinodes;
        }

        public int GetDay() => 8;
    }
}