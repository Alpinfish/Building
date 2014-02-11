using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    class PersonsGiveUpAndLeave : BuildingEvent
    {

        private List<Floor> floorList;
        private Statistics statistics;

        public PersonsGiveUpAndLeave(List<Floor> floorList, Statistics statistics)
        {

            this.floorList = floorList;
            this.statistics = statistics;
        }


        override public void Happen()
        {
            foreach (Floor floor in floorList)
            {
                List<Person> personsWaitingForUpList = new List<Person>();
                List<Person> personsWaitingForDownList = new List<Person>();

                foreach (Person person in floor.GetPersonsWaitingForDownList())
                {
                    person.IncrementWaitingTime();
                    if (!person.GiveUpAndLeave())
                    {
                        personsWaitingForDownList.Add(person);
                    }
                    else
                    {
                        statistics.numberOfQuitters++;
                        Console.WriteLine("Person No. " + person.PersonNumber + " at _floor No. " + floor.FloorNumber + " gives up and leaves.");
                    }
                }

                foreach (Person person in floor.GetPersonsWaitingForUpList())
                {
                    person.IncrementWaitingTime();
                    if (!person.GiveUpAndLeave())
                    {
                        personsWaitingForUpList.Add(person);
                    }
                    else
                    {
                        statistics.numberOfQuitters++;
                        Console.WriteLine("Person No. " + person.PersonNumber + " at _floor No. " + floor.FloorNumber + " gives up and leaves.");
                    }
                }

                floor.SetPersonsWaitingForUpList(personsWaitingForUpList);
                floor.SetPersonsWaitingForDownList(personsWaitingForDownList);

            }
        }
    }
}
