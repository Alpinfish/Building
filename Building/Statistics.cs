using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    class Statistics
    {
        private int numberOfPersons {get; set;}
        private int numberOfQuitters { get; set; }
        private int totalWaitingTime { get; set; }

        
        public Statistics()
        {
            numberOfPersons = 0;
            numberOfQuitters = 0;
            totalWaitingTime = 0;
        }


        public void printStatistics()
        {
            Console.WriteLine("Total Persons Entered : " + numberOfPersons);
            Console.WriteLine("Total Number of Persons who gave up : " + numberOfQuitters);
            Console.WriteLine("Total Waiting Time : " + totalWaitingTime);
        }

    }
}
