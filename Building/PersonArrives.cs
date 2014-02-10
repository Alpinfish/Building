using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    class PersonArrives : BuildingEvent
    {
        private Person person;
        private Floor floor;

        public Person getPerson()
        {
            return person;
        }

        public Floor getFloor()
        {
            return floor;
        }

        public void setPerson(Person person)
        {
            this.person = person;
        }

        public void setFloor(Floor floor)
        {
            this.floor = floor;
        }
        override public void happen()
        {
            floor.addPersonToList(person);
            Console.WriteLine("Person No. " + person.PersonNumber +
            " arrives at floor No. : " + floor.floorNumber);
        }
    }
}
