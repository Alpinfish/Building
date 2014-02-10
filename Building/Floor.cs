using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    public class Floor
    {
        public int floorNumber { get; set; }
        public Boolean upButtonPressed { get; set; }
        public Boolean downButtonPressed { get; set; }
        private List<Person> personsWaitingForUpList = new List<Person>();
        private List<Person> personsWaitingForDownList = new List<Person>();


        public List<Person> getPersonsWaitingForUpList()
        {
            return personsWaitingForUpList;
        }

        public void setPersonsWaitingForUpList(List<Person> personsWaitingForUpList)
        {
            this.personsWaitingForUpList = personsWaitingForUpList;
        }

        public List<Person> getPersonsWaitingForDownList()
        {
            return personsWaitingForDownList;
        }

        public void setPersonsWaitingForDownList(List<Person> personsWaitingForDownList)
        {
            this.personsWaitingForDownList = personsWaitingForDownList;
        }

        public void clearUpList()
        {
            personsWaitingForUpList.Clear();
        }
        public void clearDownList()
        {
            personsWaitingForDownList.Clear();
        }

        public void addPersonToList(Person person)
        {
            if (person.sourceFloorNumber != this.floorNumber)
            {
                return;
            }
            else if (person.destinationFloorNumber > person.sourceFloorNumber)
            {
                personsWaitingForUpList.Add(person);
                this.upButtonPressed = true;
            }
            else
            {
                personsWaitingForDownList.Add(person);
                this.downButtonPressed=true;
            }

        }

    }
}
