using System.Collections.Generic;

namespace Solvers
{
    public class Day9 : Day
    {
        public long SolveA(string[] lines)
        {
            var disk = new List<int>();

            var i = 0;
            var isFile = true;
            foreach (var chr in lines[0])
            {
                int val = chr - '0';

                if (isFile)
                {
                    for (int j = 0; j < val; j++)
                        disk.Add(i);

                    isFile = false;
                    i++;
                    continue;
                }

                for (int j = 0; j < val; j++)
                    disk.Add(-1);

                isFile = true;
            }

            List<int> defragmented = defragment(disk);
            long checkSum = 0;

            i = 0;
            foreach (var id in defragmented)
            {
                if (id == -1)
                    break;

                checkSum += id * i++;
            }

            return checkSum;
        }

        private List<int> defragment(List<int> disk)
        {
            var caret = 0;
            var copy = new List<int>(disk);
            for (int i = copy.Count - 1; i > 0; i--)
            {
                if (copy[i] == -1)
                    continue;

                while (copy[caret] != -1)
                    caret++;

                if (caret >= i)
                    break;

                copy[caret] = copy[i];
                copy[i] = -1;
            }

            return copy;
        }

        struct Slot
        {
            public List<int> layout;
            public int originalValue;

            public Slot(int val, int size)
            {
                layout = new();
                for (var i = 0; i < size; i++)
                    layout.Add(val);

                originalValue = val;
            }

            public bool hasSpaceFor(int queriedSize)
            {
                var freeSpace = 0;
                foreach (var i in layout)
                    freeSpace += i == -1 ? 1 : 0;

                return freeSpace >= queriedSize;
            }

            public void slotIn(List<int> otherLayout)
            {
                for (var i = 0; i < layout.Count; i++)
                {
                    if (layout[i] != -1)
                        continue;

                    for (var j = 0; j < otherLayout.Count; j++)
                    {
                        layout[i + j] = otherLayout[j];
                    }

                    break;
                }
            }

            public void free()
            {
                for (var i = 0; i < layout.Count; i++)
                    layout[i] = -1;

                originalValue = -1;
            }
        }

        public long SolveB(string[] lines)
        {
            var disk = new List<Slot>();

            // Generate disk layout
            var i = 0;
            var isFile = true;
            foreach (var chr in lines[0])
            {
                int val = chr - '0';

                if (isFile)
                {
                    disk.Add(new Slot(i, val));

                    isFile = false;
                    i++;
                    continue;
                }

                disk.Add(new Slot(-1, val));

                isFile = true;
            }

            List<Slot> defragmented = defragmentByFile(disk);
            long checkSum = 0;
            var caret = 0;

            // Calculate checksum
            foreach (var slot in defragmented)
            {
                foreach (var part in slot.layout)
                {
                    if (part == -1)
                    {
                        caret++;
                        continue;
                    }

                    checkSum += part * caret++;
                }
            }

            return checkSum;
        }

        private List<Slot> defragmentByFile(List<Slot> disk)
        {
            var copy = new List<Slot>(disk);

            for (int i = copy.Count - 1; i > 0; i--)
            {
                var currentSlot = copy[i];
                
                for (int j = 0; j < copy.Count; j++)
                {
                    if (j > i)
                        break;
                    
                    if (copy[j].originalValue == -1 && copy[j].hasSpaceFor(currentSlot.layout.Count))
                    {
                        copy[j].slotIn(currentSlot.layout);
                        copy[i].free();
                        break;
                    }
                    
                }
            }

            return copy;
        }

        public int GetDay() => 9;
    }
}