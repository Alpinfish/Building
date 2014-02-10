using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    public class Person
    {

        public int PersonNumber;
        public int sourceFloorNumber { get; set; } 	//person waits at this floor for elevator
        public int destinationFloorNumber {  get;  set; } 	//person wants to go to this floor
        public int maxWaitingTime { get; set; } 	//maximum time person will wait for elevator
        public int timePastInWaiting { get; set; } 	//the time person has spent in waiting. initially it is 0.

        public void incrementWaitingTime()
        {
            timePastInWaiting = timePastInWaiting + 1;
        }

        public Boolean giveUpAndLeave()
        {
            if (maxWaitingTime == timePastInWaiting)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int getPersonNo()
        {
            return PersonNumber;
        }

        public void setPersonNo(int personNo)
        {
            this.PersonNumber = personNo;
        }


    }
}
