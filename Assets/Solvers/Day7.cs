using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Solvers
{
    public class Day7 : Day
    {
        public long SolveA(string[] lines)
        {
            var maxFactors = 0;
            long sum = 0;
            foreach (var line in lines)
            {
                var outerTokens = line.Split(":");

                if (!Int64.TryParse(outerTokens[0], out long resultant))
                    throw new Exception($"Couldn't parse '{outerTokens[0]}' to int!");

                var factors = new List<long>();
                var partsTokens = outerTokens[1].Trim().Split(" ");
                foreach (var token in partsTokens)
                {
                    if (!Int64.TryParse(token, out long factor))
                        throw new Exception($"Couldn't parse '{token}' to int!");
                    factors.Add(factor);
                }

                if (canBeSum(resultant, 0, factors))
                    sum += resultant;
            }

            return sum;
        }

        public bool canBeSum(long resultant, long sum, List<long> factors)
        {
            if (factors.Count == 0)
                return resultant == sum;

            if (canBeSum(resultant, sum + factors[0], factors.Skip(1).ToList()))
                return true;

            if (canBeSum(resultant, sum * factors[0], factors.Skip(1).ToList()))
                return true;

            return false;
        }

        public long SolveB(string[] lines)
        {
            var maxFactors = 0;
            long sum = 0;
            foreach (var line in lines)
            {
                var outerTokens = line.Split(":");

                if (!Int64.TryParse(outerTokens[0], out long resultant))
                    throw new Exception($"Couldn't parse '{outerTokens[0]}' to int!");

                var factors = new List<long>();
                var partsTokens = outerTokens[1].Trim().Split(" ");
                foreach (var token in partsTokens)
                {
                    if (!Int64.TryParse(token, out long factor))
                        throw new Exception($"Couldn't parse '{token}' to int!");
                    factors.Add(factor);
                }

                if (canBeSum2(resultant,0, factors))
                    sum += resultant;
            }

            return sum;
        }

        public bool canBeSum2(long resultant, long sum, List<long> factors)
        {
            if (factors.Count == 0)
                return resultant == sum;

            if (canBeSum2(resultant, sum + factors[0], factors.Skip(1).ToList()))
                return true;

            if (canBeSum2(resultant, sum * factors[0], factors.Skip(1).ToList()))
                return true;

            var concated = $"{sum}{factors[0]}";
            if (!Int64.TryParse(concated, out long concLong))
                throw new Exception($"Couldn't parse '{concated}' to int!");
            if (canBeSum2(resultant, concLong, factors.Skip(1).ToList()))
                return true;

            return false;
        }

        private long evaluate(string val)
        {
            if (val[0] == '+' || val[0] == '*')
                val = val.Substring(1);
            
            var dt = new DataTable();
            var v = dt.Compute(val, "");
            return (Int32) v;
        }

        public int GetDay() => 7;
    }
}