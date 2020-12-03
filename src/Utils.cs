using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Lightning2x.AdventOfCode2020
{ 
    public static class Utils
    {
        public static List<T> TypeParser<T> (List<T> ls, string path, Func<string, T> parser)
        {
            
            List<T> parseList = new List<T>();
            List<string> fileLines = new List<string>();
            StreamReader fileReader = new StreamReader(path);
            string line = fileReader.ReadLine();
            while (line != null)
            {
                parseList.Add(parser(line));
                line = fileReader.ReadLine();
            }

            return parseList;
        }
    }
}
