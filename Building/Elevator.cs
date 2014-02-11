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
        public int Elevator_Id { get; set; } //The unique identifier
        public int CurrentFloorNumber { get; set; } //The current floor the elevator is at
        public ElevatorState ElevatorState { get; set; } //The state of the elevator

        private List<Person> passengersList = new List<Person>(); //The list of passengers on the elevator
        private SortedSet<int> pushedButtons = new SortedSet<int>(); //The pused floor destination buttons 	


        
        /// <summary>
        /// Self explanitory.
        /// </summary>
        /// <returns>A list of type passenger.</returns>
        public List<Person> GetPassengersList()
        {
            return passengersList;
        }

        /// <summary>
        /// Self explanitory.
        /// </summary>
        /// <param name="_passengersList"></param>
        public void SetPassengersList(List<Person> _passengersList)
        {
            this.passengersList = _passengersList;
        }

        /// <summary>
        /// Self explanitory.
        /// </summary>
        /// <returns>A sorted set of the pushed destination buttons</returns>
        public SortedSet<int> GetPushedButtons()
        {
            return pushedButtons;
        }

        /// <summary>
        /// Self explanitory.
        /// </summary>
        /// <param name="_pushedButtons"></param>
        public void SetPushedButtons(SortedSet<int> _pushedButtons)
        {
            this.pushedButtons = _pushedButtons;
        }

        /// <summary>
        /// Use this method to select a destination button once the passenger is on board the elevator
        /// </summary>
        /// <param name="floorNo"></param>
        public void PressButton(int floorNo)
        {
            pushedButtons.Add(floorNo);
        }

        /// <summary>
        /// Use this method to add a passenger to an instance of an elevator
        /// </summary>
        /// <param name="person"></param>
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

        /// <summary>
        /// Use this method to remove a passenger from an instance of an elevator. 
        /// Also removes the passengers destination floor from the collection of pushed buttons on the elevator.
        /// </summary>
        /// <param name="floorNo"></param>
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

        /// <summary>
        /// Is there a passenger on the list who wants to exit at this floor
        /// </summary>
        /// <param name="floorNo"></param>
        /// <returns></returns>
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
