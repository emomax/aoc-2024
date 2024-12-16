using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Solvers
{
    public class Day16 : Day
    {
        private bool reverseX;
        private bool reverseY;

        public Day16(bool reverseX = false, bool reverseY = false)
        {
            this.reverseX = reverseX;
            this.reverseY = reverseY;
        }

        private class Node
        {
            public Node(Vector2Int coord, Vector2Int direction, int score, Node prev, int h)
            {
                this.coord = coord;
                this.score = score;
                this.direction = direction;
                this.prev = prev;
                this.h = h;
            }

            public readonly Vector2Int coord;
            public readonly Vector2Int direction;
            public int score;
            public int h;
            public Node prev;

            protected bool Equals(Node other)
            {
                return coord.Equals(other.coord) && direction.Equals(other.direction);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Node) obj);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(coord, direction);
            }
        }

        public class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : IComparable
        {
            public int Compare(TKey x, TKey y)
            {
                int result = x.CompareTo(y);

                if (result == 0)
                    return 1; // Handle equality as being greater. Note: this will break Remove(key) or

                return result;
            }
        }

        public int calcH(Vector2Int coord, Vector2Int goal) =>
            Mathf.Abs(goal.x - coord.x) + Mathf.Abs(goal.y - coord.y);

        public long SolveA(string[] lines)
        {
            var map = new List<List<bool>>();
            var start = new Vector2Int(-1, -1);

            var end = new Vector2Int(-1, -1);
            for (int y = 0; y < lines.Length; y++)
            {
                var row = new List<bool>();
                for (int x = 0; x < lines[0].Length; x++)
                {
                    var xIdx = reverseX ? lines[0].Length - 1 - x : x;
                    var yIdx = reverseY ? lines.Length - 1 - y : y;
                    var chr = lines[yIdx][xIdx];

                    switch (chr)
                    {
                        case '#':
                            row.Add(false);
                            continue;
                        case '.':
                            row.Add(true);
                            continue;
                        case 'S':
                            row.Add(true);
                            start = new Vector2Int(x, y);
                            continue;
                        case 'E':
                            row.Add(true);
                            end = new Vector2Int(x, y);
                            continue;
                    }
                }

                map.Add(row);
            }

            // solve
            var closed = new Dictionary<Node, Node>();
            var open = new SortedList<double, Node>(new DuplicateKeyComparer<double>());
            var startNode = new Node(start, Vector2Int.right, 0, null, calcH(start, end));
            open.Add(startNode.h, startNode);

            Node goalNode = null;
            while (open.Count > 0)
            {
                var curr = open.Values[0];
                open.RemoveAt(0);
                if (curr.coord == end)
                {
                    goalNode = curr;
                    break;
                }

                if (!closed.ContainsKey(curr))
                    closed.Add(curr, curr);

                if (closed[curr].h + closed[curr].score < curr.h + curr.score)
                {
                    // exists node with same direction that has better score
                    continue;
                }
                closed[curr] = curr;

                var (lCoord, lDirection) = turnLeft(curr.coord, curr.direction);
                var (rCoord, rDirection) = turnRight(curr.coord, curr.direction);
                var fCoord = curr.coord + curr.direction;

                if (map[fCoord.y][fCoord.x])
                {
                    var fw = new Node(fCoord, curr.direction, curr.score + 1, curr, calcH(fCoord, end));
                    open.Add(fw.h + fw.score, fw);
                }

                if (map[lCoord.y][lCoord.x])
                {
                    var ln = new Node(lCoord, lDirection, curr.score + 1001, curr, calcH(lCoord, end));
                    open.Add(ln.h + ln.score, ln);
                }

                if (map[rCoord.y][rCoord.x])
                {
                    var rn = new Node(rCoord, rDirection, curr.score + 1001, curr, calcH(rCoord, end));
                    open.Add(rn.h + rn.score, rn);
                }
            }

            visPath(goalNode, map);

            return goalNode.score;
        }

        void visPath(Node goalNode, List<List<bool>> map)
        {
            var steps = new Dictionary<Vector2Int, string>();

            bool first = true;

            var currentVis = goalNode;
            while (currentVis.prev != null)
            {
                if (first)
                {
                    if (!steps.ContainsKey(currentVis.coord))
                        steps[currentVis.coord] = "";
                    steps[currentVis.coord] = "E";
                    first = false;
                }
                else
                {
                    if (currentVis.direction.x == 0)
                    {
                        if (currentVis.direction.y > 0)
                        {
                            if (!steps.ContainsKey(currentVis.coord))
                                steps[currentVis.coord] = "";
                            steps[currentVis.coord] = "v";
                        }
                        else
                        {
                            if (!steps.ContainsKey(currentVis.coord))
                                steps[currentVis.coord] = "";
                            steps[currentVis.coord] = "^";
                        }
                    }
                    else
                    {
                        if (currentVis.direction.x > 0)
                        {
                            if (!steps.ContainsKey(currentVis.coord))
                                steps[currentVis.coord] = "";
                            steps[currentVis.coord] = ">";
                        }
                        else
                        {
                            if (!steps.ContainsKey(currentVis.coord))
                                steps[currentVis.coord] = "";
                            steps[currentVis.coord] = "<";
                        }
                    }
                }

                currentVis = currentVis.prev;
            }

            steps[currentVis.coord] = "S";

            var sb = new StringBuilder();
            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map[0].Count; x++)
                {
                    if (steps.ContainsKey(new Vector2Int(x, y)))
                    {
                        sb.Append(steps[new Vector2Int(x, y)]);
                        continue;
                    }

                    sb.Append(map[y][x] ? "._" : "#");
                }

                sb.Append("\n");
            }

            Debug.Log(sb.ToString());
        }

        private (Vector2Int newPos, Vector2Int newDir) turnLeft(Vector2Int pos, Vector2Int currDirection)
        {
            if (currDirection.x == 0)
            {
                if (currDirection.y < 0)
                    return (pos + Vector2Int.left, Vector2Int.left);
                return (pos + Vector2Int.right, Vector2Int.right);
            }

            if (currDirection.x > 0)
                return (pos + Vector2Int.down, Vector2Int.down);

            return (pos + Vector2Int.up, Vector2Int.up);
        }

        private (Vector2Int newPos, Vector2Int newDir) turnRight(Vector2Int pos, Vector2Int currDirection)
        {
            if (currDirection.x == 0)
            {
                if (currDirection.y > 0)
                    return (pos + Vector2Int.left, Vector2Int.left);

                return (pos + Vector2Int.right, Vector2Int.right);
            }

            if (currDirection.x > 0)
                return (pos + Vector2Int.up, Vector2Int.up);

            return (pos + Vector2Int.down, Vector2Int.down);
        }

        public long SolveB(string[] lines)
        {
            var map = new List<List<bool>>();
            var start = new Vector2Int(-1, -1);

            var end = new Vector2Int(-1, -1);
            for (int y = 0; y < lines.Length; y++)
            {
                var row = new List<bool>();
                for (int x = 0; x < lines[0].Length; x++)
                {
                    var xIdx = reverseX ? lines[0].Length - 1 - x : x;
                    var yIdx = reverseY ? lines.Length - 1 - y : y;
                    var chr = lines[yIdx][xIdx];

                    switch (chr)
                    {
                        case '#':
                            row.Add(false);
                            continue;
                        case '.':
                            row.Add(true);
                            continue;
                        case 'S':
                            row.Add(true);
                            start = new Vector2Int(x, y);
                            continue;
                        case 'E':
                            row.Add(true);
                            end = new Vector2Int(x, y);
                            continue;
                    }
                }

                map.Add(row);
            }

            // solve
            var closed2 = new Dictionary<Node, Node>();
            var open = new SortedList<double, Node>(new DuplicateKeyComparer<double>());
            var startNode = new Node(start, Vector2Int.right, 0, null, calcH(start, end));
            open.Add(startNode.h, startNode);

            var goalNodes = new List<Node>();
            var bestScore = -1;
            while (open.Count > 0)
            {
                var curr = open.Values[0];
                open.RemoveAt(0);
                if (curr.coord == end)
                {
                    if (bestScore == -1)
                        bestScore = curr.score;
                    
                    if (curr.score == bestScore)
                        goalNodes.Add(curr);
                    continue;
                }

                if (!closed2.ContainsKey(curr))
                    closed2.Add(curr, curr);

                if (closed2[curr].h + closed2[curr].score < curr.h + curr.score)
                {
                    // exists node with same direction that has better score
                    continue;
                }
                closed2[curr] = curr;

                var (lCoord, lDirection) = turnLeft(curr.coord, curr.direction);
                var (rCoord, rDirection) = turnRight(curr.coord, curr.direction);
                var fCoord = curr.coord + curr.direction;

                if (map[fCoord.y][fCoord.x])
                {
                    var fw = new Node(fCoord, curr.direction, curr.score + 1, curr, calcH(fCoord, end));
                    open.Add(fw.h + fw.score, fw);
                }

                if (map[lCoord.y][lCoord.x])
                {
                    var ln = new Node(lCoord, lDirection, curr.score + 1001, curr, calcH(lCoord, end));
                    open.Add(ln.h + ln.score, ln);
                }

                if (map[rCoord.y][rCoord.x])
                {
                    var rn = new Node(rCoord, rDirection, curr.score + 1001, curr, calcH(rCoord, end));
                    open.Add(rn.h + rn.score, rn);
                }
            }

            var allPlaces = new HashSet<Vector2Int>();
            foreach (var node in goalNodes)
            {
                var currNode = node;
                while (currNode != null)
                {
                    if (!allPlaces.Contains(currNode.coord))
                        allPlaces.Add(currNode.coord);

                    currNode = currNode.prev;
                }
            }

            return allPlaces.Count;
        }

        public int GetDay() => 16;
    }
}