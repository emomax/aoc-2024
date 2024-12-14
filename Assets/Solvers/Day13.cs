using System;
using System.Text.RegularExpressions;

namespace Solvers
{
    public class Day13 : Day
    {
        public long SolveA(string[] lines)
        {
            string aPattern = @"Button\ A\:\ X\+(\d+)\,\ Y\+(\d+)";
            string bPattern = @"Button\ B\:\ X\+(\d+)\,\ Y\+(\d+)";
            string prizePattern = @"Prize\:\ X\=(\d+)\,\ Y\=(\d+)";

            var aRegex = new Regex(aPattern);
            var bRegex = new Regex(bPattern);
            var prizeRegex = new Regex(prizePattern);

            var usedCoins = 0;

            for (int i = 0; i < lines.Length;)
            {
                var aTokens = aRegex.Match(lines[i]);
                var bTokens = bRegex.Match(lines[i + 1]);
                var prizeTokens = prizeRegex.Match(lines[i + 2]);

                i += 4;

                if (!Int32.TryParse(aTokens.Groups[1].Value, out int x1))
                    throw new Exception($"Couldn't parse '{aTokens.Groups[1]}' to int!");
                if (!Int32.TryParse(aTokens.Groups[2].Value, out int y1))
                    throw new Exception($"Couldn't parse '{bTokens.Groups[2]}' to int!");

                if (!Int32.TryParse(bTokens.Groups[1].Value, out int x2))
                    throw new Exception($"Couldn't parse '{bTokens.Groups[1]}' to int!");
                if (!Int32.TryParse(bTokens.Groups[2].Value, out int y2))
                    throw new Exception($"Couldn't parse '{bTokens.Groups[2]}' to int!");

                if (!Int64.TryParse(prizeTokens.Groups[1].Value, out long x3))
                    throw new Exception($"Couldn't parse '{prizeTokens.Groups[1]}' to int!");
                if (!Int64.TryParse(prizeTokens.Groups[2].Value, out long y3))
                    throw new Exception($"Couldn't parse '{prizeTokens.Groups[2]}' to int!");

                var a = 0;
                var b = 0;
                var done = false;
                for (int j = 0; j < 100; j++)
                {
                    for (int k = 0; k < 100; k++)
                    {
                        if (x1 * j + x2 * k == x3 &&
                            y1 * j + y2 * k == y3)
                        {
                            a = j;
                            b = k;
                            done = true;
                            break;
                        }
                    }

                    if (done)
                        break;
                }

                if (!done)
                    continue;

                usedCoins += a * 3 + b;
            }

            return usedCoins;
        }


        public long SolveB(string[] lines)
        {
            string aPattern = @"Button\ A\:\ X\+(\d+)\,\ Y\+(\d+)";
            string bPattern = @"Button\ B\:\ X\+(\d+)\,\ Y\+(\d+)";
            string prizePattern = @"Prize\:\ X\=(\d+)\,\ Y\=(\d+)";

            var aRegex = new Regex(aPattern);
            var bRegex = new Regex(bPattern);
            var prizeRegex = new Regex(prizePattern);

            long usedCoins = 0;

            for (int i = 0; i < lines.Length;)
            {
                var aTokens = aRegex.Match(lines[i]);
                var bTokens = bRegex.Match(lines[i + 1]);
                var prizeTokens = prizeRegex.Match(lines[i + 2]);

                i += 4;

                if (!decimal.TryParse(aTokens.Groups[1].Value, out decimal x1))
                    throw new Exception($"Couldn't parse '{aTokens.Groups[1]}' to long!");
                if (!decimal.TryParse(aTokens.Groups[2].Value, out decimal y1))
                    throw new Exception($"Couldn't parse '{bTokens.Groups[2]}' to long!");

                if (!decimal.TryParse(bTokens.Groups[1].Value, out decimal x2))
                    throw new Exception($"Couldn't parse '{bTokens.Groups[1]}' to decimal!");
                if (!decimal.TryParse(bTokens.Groups[2].Value, out decimal y2))
                    throw new Exception($"Couldn't parse '{bTokens.Groups[2]}' to decimal!");

                if (!decimal.TryParse(prizeTokens.Groups[1].Value, out decimal x3))
                    throw new Exception($"Couldn't parse '{prizeTokens.Groups[1]}' to int!");
                if (!decimal.TryParse(prizeTokens.Groups[2].Value, out decimal y3))
                    throw new Exception($"Couldn't parse '{prizeTokens.Groups[2]}' to int!");

                x3 += 10000000000000;
                y3 += 10000000000000;

                var intersection = FindIntersection(x1, y1, x2, y2, x3, y3);
                if (intersection.x < 0)
                    continue;

                usedCoins += (long) (intersection.x * 3 + intersection.y);
            }

            return usedCoins;
        }

        public class Point
        {
            public decimal x;
            public decimal y;

            public Point(decimal x, decimal y)
            {
                this.x = x;
                this.y = y;
            }
        };

        private Point FindIntersection(decimal x1, decimal y1,
            decimal x2, decimal y2,
            decimal x3, decimal y3)
        {
            decimal bp = ((x1 * y3) - (y1 * x3)) / ((x1 * y2) - (y1 * x2));
            decimal ap = (x3 - (x2 * bp)) / x1;

            if (ap % 1 == 0 && bp % 1 == 0)
                return new Point(ap, bp);

            return new Point(-1, -1);
        }

        public int GetDay() => 13;
    }
}