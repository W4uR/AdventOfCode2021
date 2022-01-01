using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Seven_Segment_Search
{


    class Segment
    {
        string[] segments;/*    0000
                           *   3    1
                           *   3    1
                           *   3    1
                           *    2222
                           *   6    4
                           *   6    4
                           *   6    4
                           *    5555
                           */

        bool ArrayIsFilled(string[] array)
        {
            foreach (string item in array)
            {
                if (String.IsNullOrEmpty(item))
                {
                    return false;
                }
            }
            return true;
        }
        public Segment(string input)
        {
            string[] seg = input.Split(' ');

            while (!ArrayIsFilled(segments))
            {




            }

        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            int sum = lines.Sum(x => x.Split('|')[1].Split(' ').Count(y => y.Length == 2 || y.Length == 4 || y.Length == 7 || y.Length == 3));
            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }
}
