using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    /// <summary>
    /// A program to simulate a building with elevators and people.
    /// <para>Author: Jason Astuto</para>
    /// </summary>
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            int floors;
            int elevators;
            int totalTime;
            

            Console.WriteLine("How many Floors will this building have? (2-99)");
            while (true)
            {
                try
                {
                    floors = int.Parse(Console.ReadLine());
                    if (floors >= 2 && floors <= 99)
                        break;
                    else
                    {
                        throw new Exception();
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Entry!");

                }


            }
            Console.WriteLine("How many Elevators will this building have? (1-4)");
            while (true)
            {
                try
                {
                    elevators = int.Parse(Console.ReadLine());
                    if (elevators >= 1 && elevators <= 4)
                        break;
                    else
                    {
                        throw new Exception();
                    }
                    

                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Entry!");

                }


            }
            Console.WriteLine("How many time units for simulation? ");
            while (true)
            {
                try
                {
                    totalTime = int.Parse(Console.ReadLine());
                    break;

                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Entry!");

                }


            }

            Building building = new Building(floors, elevators, totalTime);
            building.Run();
        }
    }
}
