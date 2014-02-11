using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    class PersonArrives : BuildingEvent
    {
        public Person Person { get; set; }
        public Floor Floor { get; set; }

       /// <summary>
       /// Call this method when a person arrives at an elevator call button to ride up or down in an elevator
       /// </summary>
        override public void Happen()
        {
            Floor.AddPersonToWaitList(Person);
            Console.WriteLine("Person No. " + Person.Person_Id +
            " arrives at floor No. : " + Floor.FloorNumber);
        }
    }
}
