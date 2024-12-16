using System.Collections.Generic;
using System.Threading.Tasks;
using Solvers;
using UnityEngine;
using Utils;

namespace cmd
{
    public class AdventOfCodeRunner : MonoBehaviour
    {
        public async void Start()
        {
            var days = new List<Day>
            {
                // new Day1(),
                // new Day2(),
                // new Day3(),
                // new Day4(),
                // new Day5(),
                // // new Day6(), // slow
                // // new Day7(), // slow
                // new Day8(),
                // // new Day9(), // slow
                // // new Day10(), // slow
                // new Day11(),
                // new Day12(),
                // new Day13(),
                // new Day14(101, 103),
                // new Day15(),
                new Day16(),
            };
            
            Debug.Log($"Running {days.Count} days..");
            foreach (var day in days)
            {
                var taskAStartTime = Time.realtimeSinceStartupAsDouble;
                Debug.Log($"Running day {day.GetDay()}");

                var lines = FileUtils.ReadFile($"Assets/PuzzleInput/Day{day.GetDay()}.txt");
                long res = 0;
                await Task.Run(() => { res = day.SolveA(lines); });
                Debug.Log($"a: {res} ({Time.realtimeSinceStartupAsDouble - taskAStartTime:0.000}s)");

                var taskBStartTime = Time.realtimeSinceStartupAsDouble;
                await Task.Run(() => { res = day.SolveB(lines); });
                Debug.Log($"b: {res} ({(Time.realtimeSinceStartupAsDouble - taskBStartTime):0.000}s)");

                Debug.Log("==========");
            }
        }
    }
}