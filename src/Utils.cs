using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Lightning2x.AdventOfCode2020
{ 
    public static class Utils
    {
        public static bool NUnitEnabled;
        public static List<T> TypeParser<T> (string path, Func<string, T> parser)
        {
            
            List<T> parseList = new List<T>();
            StreamReader fileReader = new StreamReader(path);
            string line = fileReader.ReadLine();
            while (line != null)
            {
                parseList.Add(parser(line));
                line = fileReader.ReadLine();
            }
            fileReader.Close();
            return parseList;
        }

        public static List<T> TypeParser<T>(List<string> parseablelist, Func<string, T> parser)
        {

            List<T> parseList = new List<T>();
            for (int i = 0; i < parseablelist.Count; i++)
            {
                parseList.Add(parser(parseablelist[i]));
            }
            return parseList;
        }

        public static List<string> ReadFile(string path)
        {
            List<string> fileLines = new List<string>();
            StreamReader fileReader = new StreamReader(path);
            string line = fileReader.ReadLine();
            while (line != null)
            {
                fileLines.Add(line);
                line = fileReader.ReadLine();
            }
            fileReader.Close();
            return fileLines;
        }
    }
}
