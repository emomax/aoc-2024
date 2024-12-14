using System.Collections.Generic;
using UnityEngine;

namespace Solvers
{
    public class Day12 : Day
    {
        public long SolveA(string[] lines)
        {
            // Generate map
            var map = new List<List<char>>();

            foreach (var line in lines)
            {
                var row = new List<char>();

                foreach (var chr in line)
                {
                    row.Add(chr);
                }

                map.Add(row);
            }

            var visitedCoords = new HashSet<Vector2Int>();

            // Calculate pricing
            long sum = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    var startNodeVal = map[y][x];
                    var startCoords = new Vector2Int(x, y);

                    if (visitedCoords.Contains(startCoords))
                        continue;

                    var openQueue = new Queue<Vector2Int>();
                    openQueue.Enqueue(new Vector2Int(x, y));

                    // Flood fill
                    var currentArea = new List<Vector2Int>();
                    var checkedCoords = new HashSet<Vector2Int>();

                    // var linkedArea = new Queue<Node>();
                    var perimeterParts = 0;

                    while (openQueue.Count > 0)
                    {
                        var current = openQueue.Dequeue();
                        if (checkedCoords.Contains(current))
                            continue;

                        checkedCoords.Add(current);
                        visitedCoords.Add(current);
                        currentArea.Add(current);

                        var right = new Vector2Int(current.x + 1, current.y);
                        var left = new Vector2Int(current.x - 1, current.y);
                        var up = new Vector2Int(current.x, current.y - 1);
                        var down = new Vector2Int(current.x, current.y + 1);

                        var neighbours = 0;

                        if (current.x > 0 && map[left.y][left.x] == startNodeVal) // && !checkedCoords.Contains(left) )
                        {
                            openQueue.Enqueue(left);
                            neighbours++;
                        }

                        if (current.x < lines[0].Length - 1 &&
                            map[right.y][right.x] == startNodeVal) // && !checkedCoords.Contains(right))
                        {
                            openQueue.Enqueue(right);
                            neighbours++;
                        }

                        if (current.y > 0 && map[up.y][up.x] == startNodeVal) // && !checkedCoords.Contains(up))
                        {
                            openQueue.Enqueue(up);
                            neighbours++;
                        }

                        if (current.y < lines.Length - 1 &&
                            map[down.y][down.x] == startNodeVal) // && !checkedCoords.Contains(down))
                        {
                            openQueue.Enqueue(down);
                            neighbours++;
                        }

                        perimeterParts += 4 - neighbours;
                    }

                    var area = currentArea.Count;

                    // Debug.Log($"Region of '{startNodeVal}': p: {perimeterParts} * a: {area} = {perimeterParts * area}");
                    sum += perimeterParts * area;
                }
            }

            return sum;
        }

        public long SolveB(string[] lines)
        {
            // Generate map
            var map = new List<List<char>>();

            foreach (var line in lines)
            {
                var row = new List<char>();

                foreach (var chr in line)
                {
                    row.Add(chr);
                }

                map.Add(row);
            }

            var visitedCoords = new HashSet<Vector2Int>();

            // Calculate pricing
            long sum = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    var startNodeVal = map[y][x];
                    var startCoords = new Vector2Int(x, y);

                    if (visitedCoords.Contains(startCoords))
                        continue;

                    var openQueue = new Queue<Vector2Int>();
                    openQueue.Enqueue(new Vector2Int(x, y));

                    // Flood fill
                    var currentArea = new List<Vector2Int>();
                    var checkedCoords = new HashSet<Vector2Int>();

                    // Assumption: amount of corners are equal to amount of sides
                    var corners = 0;

                    while (openQueue.Count > 0)
                    {
                        var current = openQueue.Dequeue();
                        if (checkedCoords.Contains(current))
                            continue;

                        checkedCoords.Add(current);
                        visitedCoords.Add(current);
                        currentArea.Add(current);

                        var right = new Vector2Int(current.x + 1, current.y);
                        var left = new Vector2Int(current.x - 1, current.y);
                        var up = new Vector2Int(current.x, current.y - 1);
                        var down = new Vector2Int(current.x, current.y + 1);

                        var hasRightNeighbour = false;
                        var hasLeftNeighbour = false;
                        var hasDownNeighbour = false;
                        var hasUpNeighbour = false;

                        var neighbours = 0;
                        var bitValue = 0;

                        if (current.y > 0 && map[up.y][up.x] == startNodeVal) // && !checkedCoords.Contains(up))
                        {
                            openQueue.Enqueue(up);
                            neighbours++;
                            hasUpNeighbour = true;
                        }

                        if (current.x < lines[0].Length - 1 &&
                            map[right.y][right.x] == startNodeVal) // && !checkedCoords.Contains(right))
                        {
                            openQueue.Enqueue(right);
                            neighbours++;
                            hasRightNeighbour = true;
                        }

                        if (current.y < lines.Length - 1 &&
                            map[down.y][down.x] == startNodeVal) // && !checkedCoords.Contains(down))
                        {
                            openQueue.Enqueue(down);
                            neighbours++;
                            hasDownNeighbour = true;
                        }

                        if (current.x > 0 && map[left.y][left.x] == startNodeVal) // && !checkedCoords.Contains(left) )
                        {
                            openQueue.Enqueue(left);
                            neighbours++;
                            hasLeftNeighbour = true;
                        }

                        if (neighbours == 0)
                        {
                            corners += 4;
                            continue;
                        }

                        if (neighbours == 1)
                        {
                            corners += 2;
                            continue;
                        }

                        if (neighbours == 2)
                        {
                            // find outer corners
                            if (hasUpNeighbour && hasRightNeighbour)
                            {
                                corners++;
                            }

                            if (hasRightNeighbour && hasDownNeighbour)
                            {
                                corners++;
                            }

                            if (hasDownNeighbour && hasLeftNeighbour)
                            {
                                corners++;
                            }

                            if (hasLeftNeighbour && hasUpNeighbour)
                            {
                                corners++;
                            }
                        }

                        // Has multiple neighbours, find inner corners
                        if (hasUpNeighbour && hasRightNeighbour)
                        {
                            if (map[current.y - 1][current.x + 1] != startNodeVal)
                                corners++;
                        }

                        if (hasRightNeighbour && hasDownNeighbour)
                        {
                            if (map[current.y + 1][current.x + 1] != startNodeVal)
                                corners++;
                        }

                        if (hasDownNeighbour && hasLeftNeighbour)
                        {
                            if (map[current.y + 1][current.x - 1] != startNodeVal)
                                corners++;
                        }

                        if (hasLeftNeighbour && hasUpNeighbour)
                        {
                            if (map[current.y - 1][current.x - 1] != startNodeVal)
                                corners++;
                        }
                    }

                    var area = currentArea.Count;

                    // Assumption: amount of corners are equal to amount of sides
                    // Debug.Log($"Region of '{startNodeVal}': p: {corners} * a: {area} = {corners * area}");
                    sum += corners * area;
                }
            }

            return sum;
        }

        public int GetDay() => 12;
    }
}