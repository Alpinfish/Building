using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    public class Elevator
    {
        public int elevatorNumber { get; set; }
        public int currentFloorNo { get; set; }
        public ElevatorState elevatorState { get; set; }
        private List<Person> passengersList = new List<Person>();	
        private SortedSet<int> pushedButtons = new SortedSet<int>();	


        

        public List<Person> getPassengersList()
        {
            return passengersList;
        }

        public void setPassengersList(List<Person> passengersList)
        {
            this.passengersList = passengersList;
        }

        public SortedSet<int> getPushedButtons()
        {
            return pushedButtons;
        }

        public void setPushedButtons(SortedSet<int> pushedButtons)
        {
            this.pushedButtons = pushedButtons;
        }

        public void pressButton(int floorNo)
        {
            pushedButtons.Add(floorNo);
        }

        public void addPassengerToList(Person person)
        {
            if (this.currentFloorNo != person.sourceFloorNumber)
            {
                return;
            }
            else
            {
                this.passengersList.Add(person);
                this.pressButton(person.destinationFloorNumber);
            }
        }

        public void unLoadPassengers(int floorNo)
        {
            List<Person> personsList = new List<Person>();
            SortedSet<int> buttonsList = new SortedSet<int>();

            if (this.currentFloorNo != floorNo)
            {
                return;
            }

            foreach (Person person in passengersList)
            {
                if (person.destinationFloorNumber != floorNo)
                {
                    personsList.Add(person);
                }
            }

            this.setPassengersList(passengersList);

            foreach (int button in pushedButtons)
            {
                if (button != floorNo)
                {
                    buttonsList.Add(button);
                }
            }
            this.setPushedButtons(buttonsList);
        }

        public Boolean unloadAtThisFloor(int floorNo)
        {
            if (pushedButtons.Contains(floorNo))
            {
                return true;
            }
            return false;
        }

    }
}
