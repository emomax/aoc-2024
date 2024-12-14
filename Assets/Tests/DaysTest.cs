using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Solvers;
using Utils;

namespace Tests
{
    public class DaysTest
    {
        [Test]
        public void Day1a()
        {
            string path = "Assets/Tests/TestData/Day1.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day1();

            Assert.AreEqual(11, d.SolveA(lines));
        }

        [Test]
        public void Day1b()
        {
            string path = "Assets/Tests/TestData/Day1.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day1();

            Assert.AreEqual(31, d.SolveB(lines));
        }

        // ------------Day 2----------------// 
        [Test]
        public void Day2a()
        {
            string path = "Assets/Tests/TestData/Day2.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day2();

            Assert.AreEqual(2, d.SolveA(lines));
        }

        [Test]
        public void Day2b()
        {
            string path = "Assets/Tests/TestData/Day2.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day2();

            Assert.AreEqual(4, d.SolveB(lines));
        }

        [Test]
        public void Day2SpecialCase()
        {
            var d = new Day2();
            var line = new List<string>() {"56 55 58 61 64 66 69 72"};

            Assert.AreEqual(1, d.SolveB(line.ToArray()));
        }

        [Test]
        public void Day2SpecialCase2()
        {
            var d = new Day2();
            var line = new List<string>() {"67 64 63 60 57 56 50"};

            Assert.AreEqual(1, d.SolveB(line.ToArray()));
        }

        [Test]
        public void Day2SpecialCase3()
        {
            var d = new Day2();
            var line = new List<string>() {"14 10 8 7 5 2"};

            Assert.AreEqual(1, d.SolveB(line.ToArray()));
        }

        [Test]
        public void Day3a()
        {
            string path = "Assets/Tests/TestData/Day3.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day3();

            Assert.AreEqual(161, d.SolveA(lines));
        }

        [Test]
        public void Day3b()
        {
            string path = "Assets/Tests/TestData/Day3b.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day3();

            Assert.AreEqual(48, d.SolveB(lines));
        }

        [Test]
        public void Day4a()
        {
            string path = "Assets/Tests/TestData/Day4.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day4();

            Assert.AreEqual(18, d.SolveA(lines));
        }

        [Test]
        public void Day4aMatrixInversion()
        {
            var d = new Day4();
            var lines = new List<string>()
            {
                "XMASM",
                "MASMS",
                "AMASA",
                "SMASM",
                "XMASX",
            };

            Assert.AreEqual(4, d.SolveA(lines.ToArray()));
        }

        [Test]
        public void Day4b()
        {
            string path = "Assets/Tests/TestData/Day4.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day4();

            Assert.AreEqual(9, d.SolveB(lines));
        }

        [Test]
        public void Day5a()
        {
            string path = "Assets/Tests/TestData/Day5.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day5();

            Assert.AreEqual(143, d.SolveA(lines));
        }

        [Test]
        public void Day5b()
        {
            string path = "Assets/Tests/TestData/Day5.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day5();

            Assert.AreEqual(123, d.SolveB(lines));
        }

        [Test]
        public void Day6a()
        {
            string path = "Assets/Tests/TestData/Day6.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day6();

            Assert.AreEqual(41, d.SolveA(lines));
        }

        [Test]
        public void Day6b()
        {
            string path = "Assets/Tests/TestData/Day6.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day6();

            Assert.AreEqual(6, d.SolveB(lines));
        }

        [Test]
        public void Day7a()
        {
            string path = "Assets/Tests/TestData/Day7.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day7();

            Assert.AreEqual(3749, d.SolveA(lines));
        }

        [Test]
        public void Day7TestRecursiveSumming()
        {
            var d = new Day7();

            var input = "****+";
            var resultant = 190;
            var factors = new List<long>() {10, 19, 1};
            Assert.IsTrue(d.canBeSum(resultant, 0, factors));
        }

        [Test]
        public void Day7b()
        {
            string path = "Assets/Tests/TestData/Day7.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day7();

            Assert.AreEqual(11387, d.SolveB(lines));
        }
        
        [Test]
        public void Day8a()
        {
            string path = "Assets/Tests/TestData/Day8.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day8();

            Assert.AreEqual(14, d.SolveA(lines));
        }

        [Test]
        public void Day8b()
        {
            string path = "Assets/Tests/TestData/Day8.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day8();

            Assert.AreEqual(34, d.SolveB(lines));
        }
        
        [Test]
        public void Day9a()
        {
            string path = "Assets/Tests/TestData/Day9.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day9();

            Assert.AreEqual(1928, d.SolveA(lines));
        }

        [Test]
        public void Day9b()
        {
            string path = "Assets/Tests/TestData/Day9.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day9();

            Assert.AreEqual(2858, d.SolveB(lines));
        }
        
        [Test]
        public void Day9bEdgeCase1()
        {
            var d = new Day9();
            Assert.AreEqual(132, d.SolveB(new string[] {"12345"}));
        }
        
        [Test]
        public void Day10a()
        {
            string path = "Assets/Tests/TestData/Day10.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day10();

            Assert.AreEqual(36, d.SolveA(lines));
        }

        [Test]
        public void Day10b()
        {
            string path = "Assets/Tests/TestData/Day10.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day10();

            Assert.AreEqual(81, d.SolveB(lines));
        }
        
        [Test]
        public void Day11a()
        {
            string path = "Assets/Tests/TestData/Day11.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day11();

            Assert.AreEqual(55312, d.SolveA(lines));
        }

        [Test]
        public void Day11b()
        {
            string path = "Assets/Tests/TestData/Day11.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day11();
        
            Assert.AreEqual(65601038650482, d.SolveB(lines));
        }
        
        [Test]
        public void Day12a()
        {
            string path = "Assets/Tests/TestData/Day12.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day12();

            Assert.AreEqual(1930, d.SolveA(lines));
        }

        [Test]
        public void Day12b()
        {
            string path = "Assets/Tests/TestData/Day12.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day12();
        
            Assert.AreEqual(1206, d.SolveB(lines));
        }
        
        [Test]
        public void Day13a()
        {
            string path = "Assets/Tests/TestData/Day13.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day13();

            Assert.AreEqual(480, d.SolveA(lines));
        }

        [Test]
        public void Day13b()
        {
            string path = "Assets/Tests/TestData/Day13.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day13();
        
            Assert.AreEqual(875318608908, d.SolveB(lines));
        }
        
        [Test]
        public void Day14a()
        {
            string path = "Assets/Tests/TestData/Day14.txt";
            var lines = FileUtils.ReadFile(path);
            var d = new Day14(11, 7);

            Assert.AreEqual(12, d.SolveA(lines));
        }
    }
}