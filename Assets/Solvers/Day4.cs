using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Solvers
{
    public class Day4 : Day
    {
        public long SolveA(string[] lines)
        {
            var verticalLines = new List<List<char>>(lines[0].Length);
            var horizontalN = new Dictionary<int, List<char>>();
            var horizontalP = new Dictionary<int, List<char>>();

            var w = lines[0].Length;
            var h = lines.Length;

            var count = 0;

            var lIndex = 0;
            var rIndex = w - 1;

            for (var i = 0; i < lines[0].Length; i++)
                verticalLines.Add(new List<char>());

            for (var i = 0; i < lines.Length; i++)
            {
                // vertical line generation
                var line = lines[i];
                for (int j = 0; j < line.Length; j++)
                    verticalLines[j].Add(line[j]);

                // diagonals generation
                for (int x = 0; x < w; x++)
                {
                    var chr = line[x];
                    var idxNeg = lIndex - i + x;
                    if (!horizontalN.ContainsKey(idxNeg))
                        horizontalN.Add(idxNeg, new List<char>());

                    horizontalN[idxNeg].Add(chr);

                    var chr2 = line[(w - 1) - x];
                    var idxPos = rIndex + i - x;
                    if (!horizontalP.ContainsKey(idxPos))
                        horizontalP.Add(idxPos, new List<char>());

                    horizontalP[idxPos].Add(chr2);
                }

                var val = getXMASCountForLine(line);
                count += val;
            }

            // Diagonals counting
            for (int i = -h; i < rIndex + h; i++)
            {
                if (horizontalN.ContainsKey(i))
                {
                    var lhn = new string(horizontalN[i].ToArray());
                    var val = getXMASCountForLine(lhn);
                    count += val;
                }
            }

            for (int i = -h; i < rIndex + h; i++)
            {
                if (horizontalP.ContainsKey(i))
                {
                    var rhn = new string(horizontalP[i].ToArray());
                    var val = getXMASCountForLine(rhn);
                    count += val;
                }
            }

            // Horizontal counting
            for (var i = 0; i < verticalLines.Count; i++)
            {
                var line = new string(verticalLines[i].ToArray());
                var val = getXMASCountForLine(line);
                count += val;
            }

            return count;
        }

        string pattern = @"(?=(XMAS|SAMX))";

        private int getXMASCountForLine(string line)
        {
            var rg = new Regex(pattern);

            MatchCollection matches = rg.Matches(line);
            return matches.Count;
        }

        public long SolveB(string[] lines)
        {
            var charMatrix = new List<List<char>>();
            var w = lines[0].Length;
            var h = lines.Length;

            foreach (var line in lines)
            {
                charMatrix.Add(new List<char>());
                foreach (var chr in line)
                    charMatrix[^1].Add(chr);
            }

            string pattern = @"MAS|SAM";
            var rg = new Regex(pattern);
            var numX = 0;
            for (int y = 1; y < h - 1; y++)
            {
                for (int x = 1; x < w - 1; x++)
                {
                    if (charMatrix[y][x] != 'A')
                        continue;
                    // Check for X permutation
                    var lineA = new char[3];
                    lineA[0] = charMatrix[y - 1][x - 1];
                    lineA[1] = charMatrix[y][x];
                    lineA[2] = charMatrix[y + 1][x + 1];

                    var lineB = new char[3];
                    lineB[0] = charMatrix[y + 1][x - 1];
                    lineB[1] = charMatrix[y][x];
                    lineB[2] = charMatrix[y - 1][x + 1];

                    var strA = new string(lineA);
                    var strB = new string(lineB);
                    
                    if (rg.IsMatch(strA) && rg.IsMatch(strB))
                        numX++;
                }
            }

            return numX;
        }

        public int GetDay() => 4;
    }
}