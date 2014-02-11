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
        public int FloorNumber { get; set; } //the unique identifier for each instance of Floor.

        //the up and down call buttons belong to the floor!
        public Boolean IsUpButtonPressed { get; set; }
        public Boolean IsDownButtonPressed { get; set; }

        //the collection of persons waiting for either the up, or the down elevator
        private List<Person> personsWaitingForUpElevator = new List<Person>();
        private List<Person> personsWaitingForDownElevator = new List<Person>();

        /// <summary>
        /// Self explanitory.
        /// </summary>
        /// <returns></returns>
        public List<Person> GetPersonsWaitingForUpElevator()
        {
            return personsWaitingForUpElevator;
        }

        /// <summary>
        /// Self explanitory.
        /// </summary>
        /// <param name="_personsWaitingForUpElevator"></param>
        public void SetPersonsWaitingForUpElevator(List<Person> _personsWaitingForUpElevator)
        {
            this.personsWaitingForUpElevator = _personsWaitingForUpElevator;
        }

        /// <summary>
        /// Self explanitory.
        /// </summary>
        /// <returns></returns>
        public List<Person> GetPersonsWaitingForDownElevator()
        {
            return personsWaitingForDownElevator;
        }

        /// <summary>
        /// Self explanitory.
        /// </summary>
        /// <param name="_personsWaitingForDownElevator"></param>
        public void SetPersonsWaitingForDownList(List<Person> _personsWaitingForDownElevator)
        {
            this.personsWaitingForDownElevator = _personsWaitingForDownElevator;
        }

        /// <summary>
        /// Self explanitory.
        /// </summary>
        public void ClearUpList()
        {
            personsWaitingForUpElevator.Clear();
        }

        /// <summary>
        /// Self explanitory.
        /// </summary>
        public void ClearDownList()
        {
            personsWaitingForDownElevator.Clear();
        }

        /// <summary>
        /// Call this method to add a person to the wait list for an instance of Floor.
        /// </summary>
        /// <param name="person"></param>
        public void AddPersonToWaitList(Person person)
        {
            if (person.SourceFloorNumber != this.FloorNumber)
            {
                return;
            }
            else if (person.DestinationFloorNumber > person.SourceFloorNumber)
            {
                personsWaitingForUpElevator.Add(person);
                this.IsUpButtonPressed = true;
            }
            else
            {
                personsWaitingForDownElevator.Add(person);
                this.IsDownButtonPressed=true;
            }

        }

    }
}
