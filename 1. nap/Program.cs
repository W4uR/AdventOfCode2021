using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sonar_Sweep
{
    class Program
    {
        static StreamReader sr = new StreamReader("input2.txt");
        static int sor = 0;
        static int[] sums = new int[4];
        static void Main(string[] args)
        {

            Console.WriteLine($"{SonarSweepPT2()} alkalommal növekedett a mélység..lmao");
            //Console.WriteLine(-1%4);
            sr.Close();
            Console.ReadKey();
        }

        static int SonarSweep()
        {
            int counter = 0;
            StreamReader sr = new StreamReader("input.txt");
            int elozo = int.Parse(sr.ReadLine());
            int most;
            while (!sr.EndOfStream)
            {
                most = int.Parse(sr.ReadLine());
                if (most > elozo)
                {
                    counter++;
                }
                elozo = most;
            }

            return counter;
        }

        static int DoStuff(int aktsor,int index)
        {
            if (sor % 4 != (index+3)%4)
            {
                if (sor % 4 == index)//Első mérés - reset
                {
                    sums[index] = 0;
                }

                sums[index] += aktsor;
                if (sor % 4 == (index+2)%4 && sums[index] > sums[(index+3)%4])// Utolsó mérés
                {
                    return 1;
                }
            }
            return 0;
        }

        /*
         * A-CD     # Első mérés A, utolsó mérés C
         * AB-D
         * ABC-
         * -BCD     # Első mérés D, utolsó mérés B
         * A-CD
         * AB-D
         * ABC-
         */


        static int DoStuff2(int aktsor, byte index)
        {
            byte SorMod4 = Mod4(sor); // === sor % 4

            if (SorMod4 != Mod4(index+3))
            {
                if (SorMod4 == index)//Első mérés - reset
                {
                    sums[index] = 0;
                }

                sums[index] += aktsor;
                if (SorMod4 == Mod4(index + 2) && sums[index] > sums[Mod4(index + 3)])// Utolsó mérés
                {
                    return 1;
                }
            }
            return 0;
        }

        static byte Mod4(int num)
        {
            return (byte)(num & 0b_0011);
        }
        // x % 4 === x & 0b11
        static int SonarSweepPT2()
        {
            int counter = 0;
            while (!sr.EndOfStream)
            {
                int aktsor = int.Parse(sr.ReadLine());
                sor++;
                for (byte i = 0; i < sums.Length; i++)
                {
                    counter += DoStuff2(aktsor,i);
                }

                /*
                //-----A-----
                if (sor % 4 != 3)
                {
                    if (sor % 4 == 0)
                    {
                        sums[0] = 0;
                    }
                    sums[0] += aktsor;
                    if ((sor - 2) % 4 == 0 && sums[0] > sums[3])
                    {
                        counter++;
                    }
                    if (sr.EndOfStream)
                    {
                        break;
                    }
                }

                //-----B-----
                if (sor % 4 != 0)
                {
                    if ((sor-1) % 4 == 0)
                    {
                        sums[1] = 0;
                    }
                    sums[1] += aktsor;
                    if ((sor + 1) % 4 == 0 && sums[1] > sums[0])
                    {
                        counter++;
                    }
                    if (sr.EndOfStream)
                    {
                        break;
                    }
                }

                //-----C-----
                if (sor % 4 != 1)
                {
                    if ((sor - 2) % 4 == 0)
                    {
                        sums[2] = 0;
                    }
                    sums[2] += aktsor;
                    if (sor % 4 == 0 && sums[2] > sums[1])
                    {
                        counter++;
                    }
                    if (sr.EndOfStream)
                    {
                        break;
                    }
                }

                //-----D-----
                if (sor % 4 != 2)
                {
                    if ((sor + 1) % 4 == 0)
                    {
                        sums[3] = 0;
                    }
                    sums[3] += aktsor;
                    if ((sor-1) % 4 == 0 && sums[3] > sums[2])
                    {
                        counter++;
                    }
                    if (sr.EndOfStream)
                    {
                        break;
                    }
                }
                */
                //Console.WriteLine($"{sor}.\t{aktsor}\t{counter}");
            }

            return counter-3;
        }

    }
}
