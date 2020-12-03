using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numberList = new List<int>();
            List<string> fileLines = new List<string>();

            // Read the file.
            string path = "in.txt";
            StreamReader fileReader = new StreamReader(path);
            string line = fileReader.ReadLine();
            while (line != null)
            {
                numberList.Add(int.Parse(line));
                line = fileReader.ReadLine();
            }
            List<int> sortedList = MergeSort(numberList);
            Console.WriteLine("The product of the two numbers that sum to 2020 is: " + (SumTwenty(sortedList).ToString()));
            Console.WriteLine("The product of the three numbers that sum to 2020 is: " + (ThreeSum(sortedList).ToString()));
            Console.ReadLine();
        }


        private static int SumTwenty(List<int> ls)
        {
            for(int i = 0; i < ls.Count; i++)
            {
                int target = 2020 - ls[i];
                if(ls.BinarySearch(target) > 0)
                {
                    return target * ls[i];
                }

            }
            throw new ArgumentNullException("No two numbers sum to 2020.");
        }

        // Binary search approach
        private static int ThreeSum(List<int> ls)
        {
            for (int i = 0; i < ls.Count; i++)
            {
                int target = 2020 - ls[i] - ls[i+1];
                // Early quit as there is no solution
                if(target < 0)
                {
                    break;
                }
                if (ls.BinarySearch(target) > 0)
                {
                    return target * ls[i] * ls[i+1];
                }

            }
            throw new ArgumentNullException("No three numbers sum to 2020. (part 2 failed)");
        }

        private static List<int> MergeSort(List<int> l)
        {
            if (l.Count <= 1)
                return l;
            // Split into lists
            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();
            int middle = l.Count / 2;
            for (int i = 0; i < middle; i++)
                leftList.Add(l[i]);
            for (int i = middle; i < l.Count; i++)
                rightList.Add(l[i]);

            // Recursion call to sort left & right
            leftList = MergeSort(leftList);
            rightList = MergeSort(rightList);

            return MergeLists(leftList, rightList);
        }

        private static List<int> MergeLists(List<int> left, List<int> right)
        {
            List<int> sortedList = new List<int>();
            int leftIndex = 0; int rightIndex = 0;
            // Merge left and right lists
            while (left.Count > leftIndex && right.Count > rightIndex)
            {
                // Compare left element and right element
                int comp = left[leftIndex].CompareTo(right[rightIndex]);
                // Comp is negative, so right is bigger. We want ascending order so we add the left element
                if (comp < 0)
                {
                    sortedList.Add(left[leftIndex]);
                    leftIndex++;
                }
                // Otherwise left is bigger than right and we add the right element
                else if (comp > 0)
                {
                    sortedList.Add(right[rightIndex]);
                    rightIndex++;
                }
                // In rare cases all 4 properties are the same and we add both left and right and up the counters.
                else
                {
                    sortedList.Add(left[leftIndex]); sortedList.Add(right[rightIndex]);
                    leftIndex++; rightIndex++;
                }
            }

            // Catch stragglers in case left or right was bigger
            while (left.Count > leftIndex)
            {
                sortedList.Add(left[leftIndex]);
                leftIndex++;
            }
            while (right.Count > rightIndex)
            {
                sortedList.Add(right[rightIndex]);
                rightIndex++;
            }
            return sortedList;
        }
    }
}
