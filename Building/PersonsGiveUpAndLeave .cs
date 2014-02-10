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
        Statistics statistics;

        public PersonsGiveUpAndLeave(List<Floor> floorList, Statistics statistics)
        {

            this.floorList = floorList;
            this.statistics = statistics;
        }


        override public void happen()
        {
            foreach (Floor floor in floorList)
            {
                List<Person> personsWaitingForUpList = new List<Person>();
                List<Person> personsWaitingForDownList = new List<Person>();

                foreach (Person person in floor.getPersonsWaitingForDownList())
                {
                    person.incrementWaitingTime();
                    if (!person.giveUpAndLeave())
                    {
                        personsWaitingForDownList.Add(person);
                    }
                    else
                    {
                        statistics.numberOfQuitters++;
                        Console.WriteLine("Person No. " + person.PersonNumber + " at floor No. " + floor.floorNumber + " gives up and leaves.");
                    }
                }

                foreach (Person person in floor.getPersonsWaitingForUpList())
                {
                    person.incrementWaitingTime();
                    if (!person.giveUpAndLeave())
                    {
                        personsWaitingForUpList.Add(person);
                    }
                    else
                    {
                        statistics.numberOfQuitters++;
                        Console.WriteLine("Person No. " + person.PersonNumber + " at floor No. " + floor.floorNumber + " gives up and leaves.");
                    }
                }

                floor.setPersonsWaitingForUpList(personsWaitingForUpList);
                floor.setPersonsWaitingForDownList(personsWaitingForDownList);

            }
        }
    }
}
