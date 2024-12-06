using System.Collections.Generic;
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
            var line = new List<string>() { "56 55 58 61 64 66 69 72"};

            Assert.AreEqual(1, d.SolveB(line.ToArray()));
        }
        
        [Test]
        public void Day2SpecialCase2()
        {
            var d = new Day2();
            var line = new List<string>() { "67 64 63 60 57 56 50"};

            Assert.AreEqual(1, d.SolveB(line.ToArray()));
        }
        
        [Test]
        public void Day2SpecialCase3()
        {
            var d = new Day2();
            var line = new List<string>() { "14 10 8 7 5 2"};

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
    }
}