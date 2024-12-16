using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Solvers
{
    public class Day15 : Day
    {
        public long SolveA(string[] lines)
        {
            var parsingLayout = true;
            var map = new List<List<int>>();
            var startPos = new Vector2Int(-1, -1);
            var boxes = new HashSet<Vector2Int>();
            var instructions = new List<Vector2Int>();

            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                if (line.Trim() == "")
                {
                    parsingLayout = false;
                    continue;
                }

                if (parsingLayout)
                {
                    var row = new List<int>();

                    for (var x = 0; x < line.Length; x++)
                    {
                        var chr = line[x];
                        switch (chr)
                        {
                            case '#':
                                row.Add(1);
                                continue;
                            case '.':
                                row.Add(0);
                                continue;
                            case '@':
                                row.Add(0);
                                startPos = new Vector2Int(x, y);
                                continue;
                            case 'O':
                                row.Add(0);
                                boxes.Add(new Vector2Int(x, y));
                                continue;
                            default:
                                throw new Exception($"Unknown input '{chr}'");
                        }
                    }

                    map.Add(row);

                    continue;
                }

                // parsing instructions
                foreach (var chr in line)
                {
                    switch (chr)
                    {
                        case '^':
                            instructions.Add(Vector2Int.down);
                            break;
                        case '>':
                            instructions.Add(Vector2Int.right);
                            break;
                        case 'v':
                            instructions.Add(Vector2Int.up);
                            break;
                        case '<':
                            instructions.Add(Vector2Int.left);
                            break;
                    }
                }
            }

            var currPos = startPos;
            foreach (var instruction in instructions)
            {
                // printLayout(map, boxes, currPos, instruction);

                // find next empty space;
                var currentTile = new Vector2Int(currPos.x, currPos.y);
                var shouldShuffle = false;
                while (true)
                {
                    currentTile += instruction;
                    if (map[currentTile.y][currentTile.x] == 1)
                    {
                        // Hit a wall, meaning all boxes form a rigid column
                        // no shuffling
                        break;
                    }

                    if (!boxes.Contains(currentTile))
                    {
                        // We're done, shuffle boxes
                        shouldShuffle = true;
                        break;
                    }

                    // We hit a box, continue
                }

                if (!shouldShuffle)
                    continue;

                currPos += instruction;

                if (boxes.Contains(currPos))
                {
                    boxes.Remove(currPos);
                    boxes.Add(currentTile);
                }
            }

            var sum = 0;
            foreach (var box in boxes)
                sum += box.y * 100 + box.x;

            return sum;
        }

        private void printLayout(List<List<int>> map, HashSet<Vector2Int> boxes, Vector2Int robot, Vector2Int instr)
        {
            var sb = new StringBuilder();
            sb.Append("---------\n");
            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map[0].Count; x++)
                {
                    if (robot.x == x && robot.y == y)
                    {
                        sb.Append("@");
                        continue;
                    }

                    if (boxes.Contains(new Vector2Int(x, y)))
                    {
                        sb.Append("0");
                        continue;
                    }

                    if (map[y][x] == 0) // empty tile
                    {
                        sb.Append("_.");
                        continue;
                    }

                    // wall
                    sb.Append("#");
                }

                sb.Append("\n");
            }

            sb.Append($"--- {instr} ---\n");

            Debug.Log(sb.ToString());
        }

        public long SolveB(string[] lines)
        {
            var parsingLayout = true;
            var map = new List<List<int>>();
            var startPos = new Vector2Int(-1, -1);
            var boxes = new HashSet<Vector2Int>();
            var instructions = new List<Vector2Int>();

            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                if (line.Trim() == "")
                {
                    parsingLayout = false;
                    continue;
                }

                if (parsingLayout)
                {
                    var row = new List<int>();

                    for (var x = 0; x < line.Length; x++)
                    {
                        var chr = line[x];
                        switch (chr)
                        {
                            case '#':
                                row.Add(1);
                                row.Add(1);
                                continue;
                            case '.':
                                row.Add(0);
                                row.Add(0);
                                continue;
                            case '@':
                                row.Add(0);
                                row.Add(0);
                                startPos = new Vector2Int(x * 2, y);
                                continue;
                            case 'O':
                                row.Add(0);
                                row.Add(0);
                                boxes.Add(new Vector2Int(x * 2, y));
                                continue;
                            default:
                                throw new Exception($"Unknown input '{chr}'");
                        }
                    }

                    map.Add(row);

                    continue;
                }

                // parsing instructions
                foreach (var chr in line)
                {
                    switch (chr)
                    {
                        case '^':
                            instructions.Add(Vector2Int.down);
                            break;
                        case '>':
                            instructions.Add(Vector2Int.right);
                            break;
                        case 'v':
                            instructions.Add(Vector2Int.up);
                            break;
                        case '<':
                            instructions.Add(Vector2Int.left);
                            break;
                    }
                }
            }

            var currPos = startPos;
            foreach (var instruction in instructions)
            {
                // printLayoutDoubleWidth(map, boxes, currPos, instruction);

                // find next empty space;
                var currentTile = new Vector2Int(currPos.x, currPos.y);
                var shouldShuffle = false;

                // Moving up or down.
                // see how far we can push the boxes
                currentTile += instruction;
                var isOnEvenTile = currPos.x % 2 == 0;
                var areasToCheck = new Queue<Vector2Int>();
                var visualization = new HashSet<Vector2Int>();
                var boxesToRemove = new HashSet<Vector2Int>();
                bool cantMove = false;

                areasToCheck.Enqueue(currentTile);
                while (areasToCheck.Count > 0)
                {
                    var currTile = areasToCheck.Dequeue();
                    if (map[currTile.y][currTile.x] == 1)
                    {
                        cantMove = true;   
                        break;
                    }
                    
                    if (instruction.y == 0)
                    {
                        if (boxes.Contains(currTile) || boxes.Contains(currTile + Vector2Int.left))
                        {
                            // we have a box. Let's see if we can push it further.
                            if (boxes.Contains(currTile))
                            {
                                // add visualizedPushedBox
                                visualization.Add(currTile + instruction);
                                boxesToRemove.Add(currTile);
                                // add boxesToCheck
                                // areasToCheck.Enqueue(currTile + instruction);
                                areasToCheck.Enqueue(currTile + Vector2Int.right + instruction);
                                continue;
                            }

                            if (boxes.Contains(currTile + Vector2Int.left))
                            {
                                // add visualizedPushedBox
                                visualization.Add(currTile + instruction + Vector2Int.left);
                                boxesToRemove.Add(currTile + Vector2Int.left);
                                // add boxesToCheck
                                areasToCheck.Enqueue(currTile + instruction + Vector2Int.left);
                                continue;
                            }
                        }

                        continue;
                    }

                    if (boxes.Contains(currTile) || boxes.Contains(currTile + Vector2Int.left))
                    {
                        // we have a box. Let's see if we can push it further.
                        if (boxes.Contains(currTile))
                        {
                            // add visualizedPushedBox
                            visualization.Add(currTile + instruction);
                            boxesToRemove.Add(currTile);
                            // add boxesToCheck
                            areasToCheck.Enqueue(currTile + instruction);
                            areasToCheck.Enqueue(currTile + Vector2Int.right + instruction);
                            continue;
                        }

                        if (boxes.Contains(currTile + Vector2Int.left))
                        {
                            // add visualizedPushedBox
                            visualization.Add(currTile + instruction + Vector2Int.left);
                            boxesToRemove.Add(currTile + Vector2Int.left);
                            // add boxesToCheck
                            areasToCheck.Enqueue(currTile + instruction + Vector2Int.left);
                            areasToCheck.Enqueue(currTile + instruction);
                            continue;
                        }
                    }
                }

                if (cantMove)
                    continue;

                var validPush = true;
                foreach (var visualizedBox in visualization)
                {
                    if (visualizedBox.y >= lines.Length - 1 ||
                        visualizedBox.y <= 0 ||
                        visualizedBox.x <= 1 ||
                        visualizedBox.x >= lines[0].Length * 2 - 2)
                    {
                        validPush = false;
                        break;
                    }
                }

                if (!validPush)
                    continue;

                foreach (var boxToRemove in boxesToRemove)
                    boxes.Remove(boxToRemove);

                foreach (var visualizedBox in visualization)
                    boxes.Add(visualizedBox);

                currPos += instruction;
            }

            // printLayoutDoubleWidth(map, boxes, currPos, Vector2Int.zero);

            var sum = 0;
            foreach (var box in boxes)
                sum += box.y * 100 + box.x;

            return sum;
        }


        private void printLayoutDoubleWidth(List<List<int>> map, HashSet<Vector2Int> boxes, Vector2Int robot,
            Vector2Int instr)
        {
            var sb = new StringBuilder();
            sb.Append($"----{robot}---\n");
            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map[0].Count; x++)
                {
                    if (robot.x == x && robot.y == y)
                    {
                        sb.Append("@");
                        continue;
                    }

                    if (boxes.Contains(new Vector2Int(x, y)))
                    {
                        sb.Append("00");
                        continue;
                    }

                    if (map[y][x] == 0) // empty tile
                    {
                        sb.Append("_.");
                        continue;
                    }

                    // wall
                    sb.Append("#");
                }

                sb.Append("\n");
            }

            sb.Append($"--- {instr} ---\n");

            Debug.Log(sb.ToString());
        }

        public int GetDay() => 15;
    }
}