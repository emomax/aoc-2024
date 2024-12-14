using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Solvers
{
    public class Day10 : Day
    {
        private struct Node
        {
            public Vector2Int coord;
            public Vector2Int cameFrom;
            public int travelled;

            public Node(Vector2Int coord, int travelled) : this(coord, travelled, new Vector2Int(-1, -1))
            {
            }

            public Node(Vector2Int coord, int travelled, Vector2Int cameFrom)
            {
                this.coord = coord;
                this.travelled = travelled;
                this.cameFrom = new Vector2Int(-1, -1);
            }
        }

        public long SolveA(string[] lines)
        {
            var map = new List<List<int>>();
            var startNodes = new List<Vector2Int>();

            // generate map
            for (var y = 0; y < lines.Length; y++)
            {
                var row = new List<int>();
                var line = lines[y];
                for (var x = 0; x < line.Length; x++)
                {
                    var val = line[x] - '0';
                    row.Add(val);

                    if (val == 0)
                        startNodes.Add(new Vector2Int(x, y));
                }

                map.Add(row);
            }

            var goalNodes = new List<Node>();
            foreach (var nodePos in startNodes)
            {
                var depth = 0;

                var checkedNeighbours = new HashSet<Vector2Int>();
                var uncheckedNodes = new Queue<Node>();
                var startNode = new Node(nodePos, 0);
                uncheckedNodes.Enqueue(startNode);

                while (uncheckedNodes.Count != 0)
                {
                    var current = uncheckedNodes.Dequeue();
                    if (checkedNeighbours.Contains(current.coord))
                        continue;

                    checkedNeighbours.Add(current.coord);
                    if (current.travelled == 9)
                    {
                        goalNodes.Add(current);
                        continue;
                    }

                    var leftNeighbour = current.coord + Vector2Int.left;
                    var rightNeighbour = current.coord + Vector2Int.right;
                    var belowNeighbour = current.coord + Vector2Int.up;
                    var aboveNeighbour = current.coord + Vector2Int.down;

                    // get valid neighbours
                    if (leftNeighbour.x >= 0 && map[leftNeighbour.y][leftNeighbour.x] == current.travelled + 1)
                        uncheckedNodes.Enqueue(new Node(leftNeighbour, current.travelled + 1, current.coord));

                    if (rightNeighbour.x < map[0].Count &&
                        map[rightNeighbour.y][rightNeighbour.x] == current.travelled + 1)
                        uncheckedNodes.Enqueue(new Node(rightNeighbour, current.travelled + 1, current.coord));

                    if (belowNeighbour.y < map.Count &&
                        map[belowNeighbour.y][belowNeighbour.x] == current.travelled + 1)
                        uncheckedNodes.Enqueue(new Node(belowNeighbour, current.travelled + 1, current.coord));

                    if (aboveNeighbour.y >= 0 && map[aboveNeighbour.y][aboveNeighbour.x] == current.travelled + 1)
                        uncheckedNodes.Enqueue(new Node(aboveNeighbour, current.travelled + 1, current.coord));
                }
            }

            return goalNodes.Count;
        }

        public long SolveB(string[] lines)
        {
            var map = new List<List<int>>();
            var startNodes = new List<Vector2Int>();

            // generate map
            for (var y = 0; y < lines.Length; y++)
            {
                var row = new List<int>();
                var line = lines[y];
                for (var x = 0; x < line.Length; x++)
                {
                    var val = line[x] - '0';
                    row.Add(val);

                    if (val == 0)
                        startNodes.Add(new Vector2Int(x, y));
                }

                map.Add(row);
            }

            var goalNodes = new List<Node>();
            foreach (var nodePos in startNodes)
            {
                var depth = 0;

                // var checkedNeighbours = new HashSet<Vector2Int>();
                var uncheckedNodes = new Queue<Node>();
                var startNode = new Node(nodePos, 0);
                uncheckedNodes.Enqueue(startNode);

                while (uncheckedNodes.Count != 0)
                {
                    var current = uncheckedNodes.Dequeue();
                    // if (checkedNeighbours.Contains(current.coord))
                    //     continue;

                    // checkedNeighbours.Add(current.coord);
                    if (current.travelled == 9)
                    {
                        goalNodes.Add(current);
                        continue;
                    }

                    var leftNeighbour = current.coord + Vector2Int.left;
                    var rightNeighbour = current.coord + Vector2Int.right;
                    var belowNeighbour = current.coord + Vector2Int.up;
                    var aboveNeighbour = current.coord + Vector2Int.down;

                    // get valid neighbours
                    if (leftNeighbour.x >= 0 && map[leftNeighbour.y][leftNeighbour.x] == current.travelled + 1)
                        uncheckedNodes.Enqueue(new Node(leftNeighbour, current.travelled + 1, current.coord));

                    if (rightNeighbour.x < map[0].Count &&
                        map[rightNeighbour.y][rightNeighbour.x] == current.travelled + 1)
                        uncheckedNodes.Enqueue(new Node(rightNeighbour, current.travelled + 1, current.coord));

                    if (belowNeighbour.y < map.Count &&
                        map[belowNeighbour.y][belowNeighbour.x] == current.travelled + 1)
                        uncheckedNodes.Enqueue(new Node(belowNeighbour, current.travelled + 1, current.coord));

                    if (aboveNeighbour.y >= 0 && map[aboveNeighbour.y][aboveNeighbour.x] == current.travelled + 1)
                        uncheckedNodes.Enqueue(new Node(aboveNeighbour, current.travelled + 1, current.coord));
                }
            }

            return goalNodes.Count;
        }

        public int GetDay() => 10;
    }
}