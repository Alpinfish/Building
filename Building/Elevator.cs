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
        public int Elevator_Id { get; set; }
        public int CurrentFloorNumber { get; set; }
        public ElevatorState ElevatorState { get; set; }

        private List<Person> passengersList = new List<Person>();	
        private SortedSet<int> pushedButtons = new SortedSet<int>();	


        

        public List<Person> GetPassengersList()
        {
            return passengersList;
        }

        public void SetPassengersList(List<Person> _passengersList)
        {
            this.passengersList = _passengersList;
        }

        public SortedSet<int> GetPushedButtons()
        {
            return pushedButtons;
        }

        public void SetPushedButtons(SortedSet<int> _pushedButtons)
        {
            this.pushedButtons = _pushedButtons;
        }

        public void PressButton(int floorNo)
        {
            pushedButtons.Add(floorNo);
        }

        public void AddPassengerToList(Person person)
        {
            if (this.CurrentFloorNumber != person.SourceFloorNumber)
            {
                return;
            }
            else
            {
                this.passengersList.Add(person);
                this.PressButton(person.DestinationFloorNumber);
            }
        }

        public void UnLoadPassengers(int floorNo)
        {
            List<Person> personsList = new List<Person>();
            SortedSet<int> buttonsList = new SortedSet<int>();

            if (this.CurrentFloorNumber != floorNo)
            {
                return;
            }

            foreach (Person person in passengersList)
            {
                if (person.DestinationFloorNumber != floorNo)
                {
                    personsList.Add(person);
                }
            }

            this.SetPassengersList(passengersList);

            foreach (int button in pushedButtons)
            {
                if (button != floorNo)
                {
                    buttonsList.Add(button);
                }
            }
            this.SetPushedButtons(buttonsList);
        }

        public Boolean UnloadAtThisFloor(int floorNo)
        {
            if (pushedButtons.Contains(floorNo))
            {
                return true;
            }
            return false;
        }

    }
}
