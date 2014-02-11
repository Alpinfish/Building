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
        public int FloorNumber { get; set; }
        public Boolean IsUpButtonPressed { get; set; }
        public Boolean IsDownButtonPressed { get; set; }
        private List<Person> personsWaitingForUpList = new List<Person>();
        private List<Person> personsWaitingForDownList = new List<Person>();


        public List<Person> GetPersonsWaitingForUpList()
        {
            return personsWaitingForUpList;
        }

        public void SetPersonsWaitingForUpList(List<Person> personsWaitingForUpList)
        {
            this.personsWaitingForUpList = personsWaitingForUpList;
        }

        public List<Person> GetPersonsWaitingForDownList()
        {
            return personsWaitingForDownList;
        }

        public void SetPersonsWaitingForDownList(List<Person> personsWaitingForDownList)
        {
            this.personsWaitingForDownList = personsWaitingForDownList;
        }

        public void ClearUpList()
        {
            personsWaitingForUpList.Clear();
        }
        public void ClearDownList()
        {
            personsWaitingForDownList.Clear();
        }

        public void AddPersonToWaitList(Person person)
        {
            if (person.SourceFloorNumber != this.FloorNumber)
            {
                return;
            }
            else if (person.DestinationFloorNumber > person.SourceFloorNumber)
            {
                personsWaitingForUpList.Add(person);
                this.IsUpButtonPressed = true;
            }
            else
            {
                personsWaitingForDownList.Add(person);
                this.IsDownButtonPressed=true;
            }

        }

    }
}
