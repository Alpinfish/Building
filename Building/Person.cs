using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    public class Person
    {

        public int Person_Id { get; set; }                      //unique identifier for each person
        public int SourceFloorNumber { get; set; }          	//where each person starts the journey
        public int DestinationFloorNumber {  get;  set; }       //where each person ends the journey	
        public int MaxWaitingTime { get; set; } 	            //The maximum patience a person has
        public int TimeExpiredWhileWaiting { get; set; }        //How long has it been? 	

        /// <summary>
        /// Call this method to move the time along its linear path.
        /// </summary>
        public void IncrementWaitingTime()
        {
            TimeExpiredWhileWaiting++;
        }

        /// <summary>
        /// Is a person ready to give up and leave.
        /// </summary>
        /// <returns></returns>
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
