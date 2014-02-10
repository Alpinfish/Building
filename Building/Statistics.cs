﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    class Statistics
    {
        public int numberOfPersons {get; set;}
        public int numberOfQuitters { get; set; }
        public int totalWaitingTime { get; set; }

        
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
