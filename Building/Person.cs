using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    public class Person
    {

        public int PersonNumber { get; set; }
        public int SourceFloorNumber { get; set; } 	
        public int DestinationFloorNumber {  get;  set; } 	
        public int MaxWaitingTime { get; set; } 	
        public int TimeExpiredWhileWaiting { get; set; } 	

        public void IncrementWaitingTime()
        {
            TimeExpiredWhileWaiting++;
        }

        public Boolean GiveUpAndLeave()
        {
            if (MaxWaitingTime == TimeExpiredWhileWaiting)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
