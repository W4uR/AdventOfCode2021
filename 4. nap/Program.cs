using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Giant_Squid
{
    static class RoundCounter
    {
        public static int Round = 0;
        public static string[] markedNumbers;
    }

    class Number
    {
        string value;
        bool beenMarked;
        public bool isMarked
        {
            get
            {
                beenMarked = beenMarked || value == RoundCounter.markedNumbers[RoundCounter.Round];
                return beenMarked;
            }

        }

        public int Value => int.Parse(value);

        public Number(string val)
        {
            value = val;
        }

    }
    class Grid
    {
        Number[,] field;
        public Grid(List<string> values)
        {
            field = new Number[5, 5];
            for (int i = 0; i < values.Count; i++)
            {
                field[i/5,i%5] = new Number(values[i]);
            }
        }

        public int Score
        {
            get
            {
                int sum = 0;
                foreach (Number number in field)
                {
                    if(!number.isMarked)
                    sum += number.Value;
                }
                return sum * int.Parse(RoundCounter.markedNumbers[RoundCounter.Round]);
            }
        }

        public bool Bingo
        {
            get{
                bool hasBingoH;//Horizontal
                bool hasBingoV;//Vertical

                for (int i = 0; i < 5; i++)//Row
                {
                    hasBingoH = true;
                    for (int j = 0; j < 5; j++)//Col
                    {
                        if (!field[i,j].isMarked)
                        {
                            hasBingoH = false;
                        }
                    }
                    if (hasBingoH) return true;
                }


                for (int i = 0; i < 5; i++)//Col
                {
                    hasBingoV = true;
                    for (int j = 0; j < 5; j++)//Row
                    {
                        if (!field[j, i].isMarked)
                        {
                            hasBingoV = false;
                        }
                    }
                    if (hasBingoV) return true;
                }

                return false;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            RoundCounter.markedNumbers = lines[0].Split(',');

            List<Grid> grids = new List<Grid>();

            List<string> GridInput = new List<string>();
            for (int i = 2; i < lines.Length; i++)
            {
                if (!String.IsNullOrEmpty(lines[i]))
                {
                    string input = Regex.Replace(lines[i], @"\s+", " ");
                    if (input[0] == ' ')
                    {
                        input = input.Substring(1);
                    }
                    GridInput.AddRange(input.Split(' '));
                }
                else
                {

                    grids.Add(new Grid(GridInput));
                    GridInput.Clear();                    
                }
            }
            //Part 1
            while (RoundCounter.Round < RoundCounter.markedNumbers.Length)
            {
                foreach (Grid grid in grids)
                {
                    if (grid.Bingo)
                    {
                        Console.WriteLine(grid.Score);
                        goto Exit;
                    }
                }
                

                RoundCounter.Round++;
            }
        Exit:         

            int lastWinerIndex = 0;
            int LowestScoreNumberIndex = 0;
            for (int i = 0; i < grids.Count; i++)
            {
                RoundCounter.Round = 0;
                while (RoundCounter.Round < RoundCounter.markedNumbers.Length)
                {
                    if (!grids[i].Bingo)
                    {
                        RoundCounter.Round++;
                        continue;
                    }
                    if (RoundCounter.Round > LowestScoreNumberIndex)
                    {
                        LowestScoreNumberIndex = RoundCounter.Round;
                        lastWinerIndex = i;
                    }
                    break;
                }
            }

            RoundCounter.Round = LowestScoreNumberIndex;
            Console.WriteLine(grids[lastWinerIndex].Score);
            Console.ReadKey();
        }
    }
}
