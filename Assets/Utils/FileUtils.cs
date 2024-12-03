using System;
using System.IO;
using UnityEngine;

namespace Utils
{
    public class FileUtils
    {
        public static string[] ReadFile(string filepath) => File.ReadAllLines(filepath);
    }
}