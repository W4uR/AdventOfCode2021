using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace The_Treachery_of_Whales
{
    //   A medián az az x szám, melytől a sokaság elemeinek abszolút eltérés összege a legkisebb:
    class Program
    {
        static void Main(string[] args)
        {
            string[] s_position = /*{"16","1","2","0","4","2","7","1","2","14"};*/File.ReadAllLines("input.txt").First().Split(',');
            int[] i_position = new int[s_position.Length];
            for (int i = 0; i < s_position.Length; i++)
            {
                i_position[i] = int.Parse(s_position[i]);
            }
            Array.Sort(i_position);
            int med = i_position.Length % 2 == 1? i_position[i_position.Length / 2] 
                                                : (i_position[i_position.Length / 2] + i_position[i_position.Length / 2 - 1]) / 2;

            Console.WriteLine($"Median is: {med}");



            int sumFuel = 0;
            for (int i = 0; i < i_position.Length; i++)
            {
                sumFuel += FuelBurned(Math.Abs(med - i_position[i]));
            }
            Console.WriteLine(sumFuel);


            sumFuel = 0;
            int min = int.MaxValue;
            for (int i = i_position.Min(); i <= i_position.Max(); i++)
            {
                for (int j = 0; j < i_position.Length; j++)
                {
                   sumFuel += FuelBurned(Math.Abs(i - i_position[j]));
                }
                if (sumFuel< min)
                {
                    min = sumFuel;
                }
                sumFuel = 0;
            }
            Console.WriteLine(min);


            Console.ReadKey();
        }

        static int FuelBurned(int steps)
        {
            return (steps * steps + steps) / 2;
        }
    }
}
