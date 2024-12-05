using System;
using System.Collections.Generic;

namespace Solvers
{
    public class Day5 : Day
    {
        public int SolveA(string[] lines)
        {
            // Read rules
            var isAfterRules = new Dictionary<string, HashSet<string>>();
            var isBeforeRules = new Dictionary<string, HashSet<string>>();
            var isRules = true;
            var updates = new List<List<string>>();

            foreach (var line in lines)
            {
                if (line == "")
                {
                    isRules = false;
                    continue;
                }

                if (isRules)
                {
                    var tokens = line.Split("|");
                    if (!isAfterRules.ContainsKey(tokens[1]))
                        isAfterRules[tokens[1]] = new HashSet<string>();

                    if (!isBeforeRules.ContainsKey(tokens[0]))
                        isBeforeRules[tokens[0]] = new HashSet<string>();

                    isAfterRules[tokens[1]].Add(tokens[0]);
                    isBeforeRules[tokens[0]].Add(tokens[1]);
                    continue;
                }

                updates.Add(new List<string>(line.Split(",")));
            }

            var sum = 0;
            foreach (var upd in updates)
            {
                sum += GetUpdateScore(upd, isAfterRules, isBeforeRules);
            }

            return sum;
        }

        private static int GetUpdateScore(List<string> upd, Dictionary<string, HashSet<string>> isAfterRules,
            Dictionary<string, HashSet<string>> isBeforeRules)
        {
            var valid = true;
            for (int i = 0; i < upd.Count; i++)
            {
                var hasPassed = false;
                for (int j = 0; j < upd.Count; j++)
                {
                    if (i == j)
                    {
                        hasPassed = true;
                        continue;
                    }

                    if (!hasPassed)
                    {
                        if (!isAfterRules.ContainsKey(upd[j]))
                            continue;

                        if (isAfterRules[upd[j]].Contains(upd[i]))
                        {
                            valid = false;
                            break;
                        }

                        continue;
                    }

                    if (!isBeforeRules.ContainsKey(upd[j]))
                        continue;

                    if (isBeforeRules[upd[j]].Contains(upd[i]))
                    {
                        valid = false;
                        break;
                    }
                }

                if (!valid)
                    break;
            }

            if (!valid)
                return 0;

            if (!Int32.TryParse(upd[upd.Count / 2], out int num))
                throw new Exception($"Couldn't parse '{upd[upd.Count / 2]}' to int!");
            return num;
        }

        public int SolveB(string[] lines)
        {
            var isAfterRules = new Dictionary<string, HashSet<string>>();
            var isBeforeRules = new Dictionary<string, HashSet<string>>();
            var isRules = true;
            var updates = new List<List<string>>();

            // Data generation
            foreach (var line in lines)
            {
                if (line == "")
                {
                    isRules = false;
                    continue;
                }

                if (isRules)
                {
                    var tokens = line.Split("|");
                    if (!isAfterRules.ContainsKey(tokens[1]))
                        isAfterRules[tokens[1]] = new HashSet<string>();

                    if (!isBeforeRules.ContainsKey(tokens[0]))
                        isBeforeRules[tokens[0]] = new HashSet<string>();

                    isAfterRules[tokens[1]].Add(tokens[0]);
                    isBeforeRules[tokens[0]].Add(tokens[1]);
                    continue;
                }

                updates.Add(new List<string>(line.Split(",")));
            }

            // Rectify broken updates
            var sum = 0;
            foreach (var upd in updates)
            {
                if (GetUpdateScore(upd, isAfterRules, isBeforeRules) > 0)
                    continue;

                var updatedPatch = new List<string>() {upd[0]};

                for (int i = 1; i < upd.Count; i++)
                {
                    var pageToInsert = upd[i];
                    var index = 0;

                    for (; index < updatedPatch.Count; index++)
                    {
                        var currentPage = updatedPatch[index];
                        var hasRule = isAfterRules.ContainsKey(currentPage) ||
                                      isBeforeRules.ContainsKey(currentPage);

                        if (!hasRule)
                            continue;
                        
                        if (isAfterRules.ContainsKey(pageToInsert) && isAfterRules[pageToInsert].Contains(currentPage))
                            continue;
                        
                        break;
                    }

                    updatedPatch.Insert(index, pageToInsert);
                }

                sum += GetUpdateScore(updatedPatch, isAfterRules, isBeforeRules);
            }

            return sum;
        }

        public int GetDay() => 5;
    }
}