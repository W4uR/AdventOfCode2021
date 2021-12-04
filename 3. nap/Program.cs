using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Binary_Diagnostic
{
    class Program
    {
        static string gamma = "";
        static string epsilon = "";
        static int oneLineLenght;
        static int inputLength;
        static void Main(string[] args)
        {
            string[] lines = /*{
                "00100",
                "11110",
                "10110",
                "10111",
                "10101",
                "01111",
                "00111",
                "11100",
                "10000",
                "11001",
                "00010",
                "01010"
            };*/File.ReadAllLines("input.txt");
            oneLineLenght = lines[0].Length;
            inputLength = lines.Length;
            FirstPartSolution(lines);
            SecondPartSolution(lines);



            Console.ReadKey();
        }

        static void FirstPartSolution(string[] lines)
        {
            
            int[] bits = new int[oneLineLenght];

            for (int i = 0; i < inputLength; i++)
            {
                for (int j = 0; j < oneLineLenght; j++)
                {
                    bits[j] += (lines[i][j] - 48);
                }
            }

            
            int half = inputLength / 2;
            for (int i = 0; i < oneLineLenght; i++)
            {
                if (bits[i] > half)
                {
                    gamma += '1';
                    epsilon += '0';
                }
                else
                {
                    gamma += '0';
                    epsilon += '1';
                }
            }
            Console.WriteLine(Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2));
        }


        static int Get0s(ref List<string> lines, int charIndex)
        {
            int n0 = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                n0 += lines[i][charIndex] == '0' ? 1 : 0;
            }
            return n0;
        }

        static char MostCommonValue(ref List<string> lines, int charIndex)
        {
            int n0 = Get0s(ref lines, charIndex);
            int n1 = lines.Count - n0;

            if (n0 > n1)
            {
                return '0';
            }else if (n1 > n0)
            {
                return '1';
            }
            else
            {
                return '-';
            }
        }

        static char LeastCommonValue(ref List<string> lines, int charIndex)
        {
            int n0 = Get0s(ref lines, charIndex);
            int n1 = lines.Count - n0;

            if (n0 > n1)
            {
                return '1';
            }
            else if (n1 > n0)
            {
                return '0';
            }
            else
            {
                return '-';
            }
        }


        static bool OXBit(ref List<string> lines, int charIndex, int index)
        {
            char MCV = MostCommonValue(ref lines, charIndex);
            char bit2Test = lines[index][charIndex];

            //Console.WriteLine($"Round:  {charIndex}\t{MCV}\t{bit2Test}\t{lines[index]}\tRemove?----{!(bit2Test == MCV || MCV == '-' && bit2Test == '1')}");
          
            return (bit2Test == MCV || MCV == '-' && bit2Test == '1');

        }

        static bool COBit(ref List<string> lines, int charIndex, int index)
        {
            char LCV = LeastCommonValue(ref lines, charIndex);
            char bit2Test = lines[index][charIndex];

           // Console.WriteLine($"Round:  {charIndex}\t{LCV}\t{bit2Test}\t{lines[index]}\tRemove?----{!(bit2Test == LCV || LCV == '-' && bit2Test == '0')}");

            return (bit2Test == LCV || LCV == '-' && bit2Test == '0');
        }


        static void SecondPartSolution(string[] lines)
        {
            string oxRating="";
            string coRating="";

            List<string> potOXRating = lines.ToList();
            List<string> potCORating = lines.ToList();
            List<string> tmp = new List<string>();


            for (int j = 0; j < oneLineLenght; j++)
            {
                for (int i = 0; i < potOXRating.Count;i++)
                {
                    if (OXBit(ref potOXRating, j, i))
                    {
                        tmp.Add(potOXRating[i]);
                    }
                    
                }

                potOXRating.Clear();
                for (int i = 0; i < tmp.Count; i++)
                {
                    potOXRating.Add(tmp[i]);
                }
                tmp.Clear();
                if (potOXRating.Count == 1) { oxRating = potOXRating[0]; break; }
            }
            Console.WriteLine("\n\n\n");
            tmp.Clear();
            for (int j = 0; j < oneLineLenght; j++)
            {
                for (int i = 0; i < potCORating.Count; i++)
                {
                    if (COBit(ref potCORating, j, i))
                    {
                        tmp.Add(potCORating[i]);
                    }

                }
                potCORating.Clear();
                for (int i = 0; i < tmp.Count; i++)
                {
                    potCORating.Add(tmp[i]);
                }
                tmp.Clear();
                if (potCORating.Count == 1) { coRating = potCORating[0]; ; break; }
            }
            
            /*
            Console.WriteLine($"potOXRating:\t{oxRating}");
            Console.WriteLine($"potCORating:\t{coRating}");
            */
            Console.WriteLine(Convert.ToInt32(oxRating, 2) * Convert.ToInt32(coRating, 2));

        }
    }
}
