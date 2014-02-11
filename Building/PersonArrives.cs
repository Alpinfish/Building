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

       
        override public void Happen()
        {
            Floor.AddPersonToWaitList(Person);
            Console.WriteLine("Person No. " + Person.PersonNumber +
            " arrives at _floor No. : " + Floor.FloorNumber);
        }
    }
}
