using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lanternfish
{

    /*class Fish
    {
        byte timer;
        public Fish(byte initalTimer)
        {
            timer = initalTimer;
        }
        public Fish()
        {
            timer = 8;
        }
        public bool Reproduce()
        {
            if (timer-- > 0) return false;
            timer = 6;
            return true;
        }
    }
    */
    class Program
    {
        static void Main(string[] args)
        {
            string[] initalStates = File.ReadAllLines("input.txt").First().Split(',');
            /*
            List<Fish> fish = new List<Fish>();
            foreach (string state in initalStates)
            {
                fish.Add(new Fish(byte.Parse(state)));
            }
            
            List<Fish> newGen = new List<Fish>();
            for (int i = 0; i < 256; i++)
            {
                
                foreach (Fish f in fish)
                {
                    if (f.Reproduce()) newGen.Add(new Fish());
                }
                if ((i & 0b_111) == 7)
                {
                    Console.Clear();
                    Console.WriteLine($"Day:{i}\t{fish.Count}");
                }
                fish.AddRange(newGen);
                newGen.Clear();
            }
            Console.Clear();
            Console.WriteLine(fish.Count);
            */
            long[] states = new long[9]; //[0;8]
            long[] prevGen = new long[9]; //[0;8]

            for (int i = 0; i < initalStates.Length; i++)
            {
                prevGen[int.Parse(initalStates[i])]++;
            }

            for (int i = 0; i < states.Length; i++)
            {
                Console.WriteLine(i + "\t" + states[i]);
            }


            for (int i = 0; i < 256; i++)
            {

                states[0] = prevGen[1];
                states[1] = prevGen[2];
                states[2] = prevGen[3];
                states[3] = prevGen[4];
                states[4] = prevGen[5];
                states[5] = prevGen[6];
                states[6] = prevGen[7] + prevGen[0];
                states[7] = prevGen[8];
                states[8] = prevGen[0];
                //prevGen = states;
                Array.Copy(states, prevGen, 9);
            }
            long sum = 0;
            for (int i = 0; i < states.Length; i++)
            {
                Console.WriteLine(states[i]);
                sum += states[i];
            }
            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }
}
