using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Smoke_Basin
{
    
    class Spot
    {
        public static int NextBasinID = 1;

        public void Flood_Fill(int ourID, in Spot[,] field)
        {
            if (!PotentialBasin) return;
            SetBasin(ourID);
            //Do the same for neighbours
            Flood_Fill(field);
        }
        public void Flood_Fill(in Spot[,] field)
        {

            //Do the same for neighbours
            if (y != 0)
                field[y - 1, x].Flood_Fill(Basin_ID, field);

            if (x + 1 != field.GetLength(1))
                field[y, x + 1].Flood_Fill(Basin_ID, field);

            if (y + 1 != field.GetLength(0))
                field[y + 1, x].Flood_Fill(Basin_ID, field);

            if (x != 0)
                field[y, x - 1].Flood_Fill(Basin_ID, field);
        }


        int x, y, height;
        int Basin_ID = -1; // -1 for not being part of a Basin
        bool isLowSpot;
        public Spot(char h,int x , int y)
        {
            this.x = x;
            this.y = y;
            height = int.Parse(h.ToString());
        }

        public int GetHeight => height;
        public bool SetIsLow(List<int> neighbours)
        {
            isLowSpot = height < neighbours.Min();
            if (isLowSpot)
            {
                SetBasin(NextBasinID++);
            }
            return isLowSpot;
        }

        public bool IsLow => isLowSpot;

        public bool PotentialBasin
        {
            get => Basin_ID < 0 && height < 9;
        }
        public void SetBasin(int id)
        {
            if (Basin_ID < 0) Basin_ID = id;
        }
        public bool PartOfBasin => Basin_ID > 0;
        public int BasinID => Basin_ID;
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            int max_x = lines[0].Length;
            int max_y = lines.Length;

            Spot[,] field = new Spot[max_y, max_x];
            for (int x = 0; x < max_x; x++)
            {
                for (int y = 0; y < max_y; y++)
                {
                    field[y, x] = new Spot(lines[y][x], x, y);
                }
            }
            #region Part I
            int answer_I = 0;
            for (int y = 0; y < max_y; y++)
            {
                for (int x = 0; x < max_x; x++)
                {
                    if (SetLowPoint(field, x, y, max_x, max_y)) answer_I += 1 + field[y, x].GetHeight;
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Part I: {0}",answer_I);
            #endregion

            #region Part II

            int answer_II = 1;

            for (int x = 0; x < max_x; x++)
            {
                for (int y = 0; y < max_y; y++)
                {
                    if (field[y, x].IsLow)
                    {
                        field[y, x].Flood_Fill(field);
                    }
                }
            }

            int[] sizes = new int[Spot.NextBasinID-1];//index = id-1

            for (int x = 0; x < max_x; x++)
            {
                for (int y = 0; y < max_y; y++)
                {
                    if (field[y,x].PartOfBasin)
                    {
                        sizes[field[y, x].BasinID - 1]++;
                    }
                }
            }



            for (int i = 0; i < 3; i++)
            {
                int maxIndex = 0;
                for (int j = 0; j < sizes.Length; j++)
                {
                    if (sizes[j] > sizes[maxIndex])
                    {
                        maxIndex = j;
                    }
                }
                answer_II *= sizes[maxIndex];
                sizes[maxIndex] = 0;
            }

            /*
            for (int y = 0; y < max_y; y++)
            {
                for (int x = 0; x < max_x; x++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (field[y, x].PartOfBasin)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(field[y,x].GetHeight);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            */

            Console.WriteLine("Part II: {0}", answer_II); //Fél órán át Debugoltam, mert azt hittem beragadt valahol... pedig csak ez a sor hiányzott :)
            #endregion
            Console.ReadKey();
        }

            static bool SetLowPoint(in Spot[,] field, int x, int y,in int max_x, in int max_y)
            {
                List<int> neighbours = new List<int>(); 

                if (y != 0)
                neighbours.Add(field[y - 1, x].GetHeight);

                if (x + 1 != max_x)
                neighbours.Add(field[y  , x+1].GetHeight);

                if (y + 1 != max_y)
                neighbours.Add(field[y + 1, x].GetHeight);

                if (x != 0)
                neighbours.Add(field[y  , x-1].GetHeight);

                return field[y, x].SetIsLow(neighbours);
            }      
    }
}
