using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dive
{
    class Program
    {
        static void Main(string[] args)
        {
            int x=0, y=0, aim = 0;
            string[] lines = File.ReadAllLines("input.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                byte value = byte.Parse(parts[1]);
                switch (parts[0])
                {
                    case "forward": x += value; y += aim*value; break;
                    case "up": aim -= value; break;
                    case "down": aim += value; break;
                }
            }
            Console.WriteLine($"{x*y} a válaszod...lmao");

            Console.ReadKey();
        }
    }
}
