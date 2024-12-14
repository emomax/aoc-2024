using System.Collections.Generic;
using UnityEngine;

namespace Solvers
{
    public class Day6 : Day
    {
        public long SolveA(string[] lines)
        {
            var map = new List<List<int>>();
            const byte open = 0;
            const byte busy = 1;

            var startingPos = new Vector2Int();

            for (var y = 0; y < lines.Length; y++)
            {
                var currRow = new List<int>();

                for (var x = 0; x < lines[y].Length; x++)
                {
                    var chr = lines[y][x];
                    if (chr == '.')
                    {
                        currRow.Add(open);
                        continue;
                    }

                    if (chr == '^')
                    {
                        currRow.Add(open);
                        startingPos.x = x;
                        startingPos.y = y;

                        continue;
                    }

                    currRow.Add(busy);
                }

                map.Add(currRow);
            }

            var currDirection = new Vector2Int(0, -1);
            var currPos = new Vector2Int(startingPos.x, startingPos.y);
            var visited = new HashSet<string>();

            while (true)
            {
                visited.Add($"{currPos.x}.{currPos.y}");

                var nextSpace = currPos + currDirection;
                if (nextSpace.y >= map.Count ||
                    nextSpace.y < 0 ||
                    nextSpace.x >= map[0].Count ||
                    nextSpace.x < 0)
                {
                    // we done;
                    break;
                }

                if (map[nextSpace.y][nextSpace.x] == open)
                {
                    currPos = nextSpace;
                    continue;
                }

                currDirection = turnRight(currDirection);
            }

            return visited.Count;
        }

        private Vector2Int turnRight(Vector2Int currDirection)
        {
            if (currDirection.y == -1)
                return new Vector2Int(1, 0);
            if (currDirection.x == 1)
                return new Vector2Int(0, 1);
            if (currDirection.y == 1)
                return new Vector2Int(-1, 0);

            return new Vector2Int(0, -1);
        }

        const byte open = 0;
        const byte busy = 1;

        public long SolveB(string[] lines)
        {
            var map = new List<List<int>>();

            var startingPos = new Vector2Int();

            for (var y = 0; y < lines.Length; y++)
            {
                var currRow = new List<int>();

                for (var x = 0; x < lines[y].Length; x++)
                {
                    var chr = lines[y][x];
                    if (chr == '.')
                    {
                        currRow.Add(open);
                        continue;
                    }

                    if (chr == '^')
                    {
                        currRow.Add(open);
                        startingPos.x = x;
                        startingPos.y = y;

                        continue;
                    }

                    currRow.Add(busy);
                }

                map.Add(currRow);
            }

            var currDirection = new Vector2Int(0, -1);
            var currPos = new Vector2Int(startingPos.x, startingPos.y);
            var visited = new Dictionary<string, List<Vector2Int>>();
            var traversalPositions = new List<Vector2Int>();
            var traversalDirections = new List<Vector2Int>();

            // Populate path
            while (true)
            {
                
                var nextSpace = currPos + currDirection;
                if (nextSpace.y >= map.Count ||
                    nextSpace.y < 0 ||
                    nextSpace.x >= map[0].Count ||
                    nextSpace.x < 0)
                {
                    traversalPositions.Add(currPos);
                    traversalDirections.Add(currDirection);
                    // we done;
                    break;
                }

                if (map[nextSpace.y][nextSpace.x] == open)
                {
                    var hashedPos = $"{currPos.x}.{currPos.y}";
                    if (!visited.ContainsKey(hashedPos))
                        visited[hashedPos] = new List<Vector2Int>();

                    visited[hashedPos].Add(currDirection);
                    currPos = nextSpace;
                    
                    traversalPositions.Add(currPos);
                    traversalDirections.Add(currDirection);
                    continue;
                }

                currDirection = turnRight(currDirection);
            }

            var blockades = new HashSet<Vector2Int>();
            
            // Check for possible loops
            for (int i = 1; i < traversalPositions.Count; i++)
            {
                var currCoordinate = traversalPositions[i];
                map[currCoordinate.y][currCoordinate.x] = busy;

                if (hasLoop(map, startingPos))
                    blockades.Add(currCoordinate);
                
                map[currCoordinate.y][currCoordinate.x] = open;
            }
            
            return blockades.Count;
        }

        private bool hasLoop(List<List<int>> map, Vector2Int startingPos)
        {
            var currDirection = new Vector2Int(0, -1);
            var currPos = new Vector2Int(startingPos.x, startingPos.y);
            var visited = new Dictionary<string, List<Vector2Int>>();

            var steps = 0;
            while (steps++ < 10000) // hack, avoid infinite loop
            {
                var hashedPos = $"{currPos.x}.{currPos.y}";
                if (visited.ContainsKey(hashedPos) && visited[hashedPos].Contains(currDirection))
                    return true;

                if (!visited.ContainsKey(hashedPos))
                    visited.Add(hashedPos, new List<Vector2Int>());

                visited[hashedPos].Add(currDirection);
                    
                var nextSpace = currPos + currDirection;
                if (nextSpace.y >= map.Count ||
                    nextSpace.y < 0 ||
                    nextSpace.x >= map[0].Count ||
                    nextSpace.x < 0)
                {
                    // no loops;
                    break;
                }

                if (map[nextSpace.y][nextSpace.x] == open)
                {
                    currPos = nextSpace;
                    continue;
                }

                currDirection = turnRight(currDirection);
            }

            return false;
        }


        public int GetDay() => 6;
    }
}