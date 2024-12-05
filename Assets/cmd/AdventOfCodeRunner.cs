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
                new Day1(),
                new Day2(),
                new Day3(),
                new Day4(),
                new Day5(),
            };
            
            Debug.Log($"Running {days.Count} days..");
            foreach (var day in days)
            {
                Debug.Log($"Running day {day.GetDay()}");
                
                var lines = FileUtils.ReadFile($"Assets/PuzzleInput/Day{day.GetDay()}.txt");
                await Task.Run(() =>
                {
                    Debug.Log($"a: {day.SolveA(lines)}");
                });
                
                await Task.Run(() =>
                {
                    Debug.Log($"b: {day.SolveB(lines)}");
                });
                
                Debug.Log("==========");
            }
        }
    }
}