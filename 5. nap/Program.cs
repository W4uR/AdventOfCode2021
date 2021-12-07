using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hydrothermal_Venture
{


    class Line
    {
        int x1, x2, y1, y2;
        decimal m, b;
        public Line(string input)
        {
            string[] points = input.Split(' ');
            x1 = int.Parse(points[0].Split(',')[0]);
            y1 = int.Parse(points[0].Split(',')[1]);
            x2 = int.Parse(points[2].Split(',')[0]);
            y2 = int.Parse(points[2].Split(',')[1]);

            int rise = y2 - y1;
            int run = x2 - x1;

            m = (run==0) ? 0 : rise / run; //Oszt ez jó? idk maaaan...
            b = y2 - m * x2;
        }

        public void Print()
        {
            Console.WriteLine($"Line from {x1},{y1} to {x2},{y2}   m={m}");
        }

        public bool isDiagonal
        {
            get
            {
                return m!=0;
            }
        }

        public int MaxX { get => Math.Max(x1, x2); }
        public int MaxY { get => Math.Max(y1, y2); }

        public bool PointIsPartOf(int x, int y)
        {
            if (isDiagonal)
            {
                return y == m * x + b && IsValidX(x) && IsValidY(y);
            }
            else
            {
                return x1 == x2 && x1 == x && IsValidY(y) || y1 == y2 && y1 == y && IsValidX(x);
            }
        }

        private bool IsValidX(int x)
        {
            return x <= x2 && x >= x1 || x <= x1 && x >= x2;
        }

        private bool IsValidY(int y)
        {
            return y <= y2 && y >= y1 || y <= y1 && y >= y2;
        }

    }

    class Field
    {
        int[,] domain; //x,y
        public Field(int w, int h)
        {
            domain = new int[h, w];
        }

        public void InsertLine(Line line)
        {
           //Console.WriteLine("Inserting:");
            //line.Print();


            for (int x = 0; x < domain.GetLength(0); x++)
            {
                for (int y = 0; y < domain.GetLength(1); y++)
                {
                    if (line.PointIsPartOf(x,y))
                    {
                        domain[x, y]++;
                    }
                }
            }
        }

        public void Print()
        {
            for (int x = 0; x < domain.GetLength(0); x++)
            {
                for (int y = 0; y < domain.GetLength(1); y++)
                {
                    Console.Write($"{domain[y,x]}");//({x},{y})");
                }
                Console.WriteLine();
            }
        }
        public int DangerousPoints
        {
            get {
                int count = 0;
                foreach (int pointVal in domain)
                {
                    if (pointVal > 1) count++;
                }
                return count;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = File.ReadAllLines("input.txt");
            List<Line> lines = new List<Line>();
            Console.WriteLine("Creating lines...");
            for (int i = 0; i < inputs.Length; i++)
            {
                lines.Add(new Line(inputs[i]));
                //Console.WriteLine($"\n{inputs[i]}");
                //lines[i].Print();
            }


            Field field = new Field(lines.Max(l=>l.MaxX)+1, lines.Max(l => l.MaxY)+1);
            Console.WriteLine("Inserting lines...");
            foreach (Line line in lines)
            {
                //if (line.isDiagonal) continue;
                field.InsertLine(line);
            }
            //field.Print();
            Console.WriteLine("Almost done...");
            Console.WriteLine(field.DangerousPoints);
            Console.ReadKey();
        }
    }
}
