using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Building
{

    public class Building
    {
        int numberOfFloors;
        int numberOfElevators;
        int timeOfOperation; //number of time units for which simulation will run

        List<Floor> floorList = new List<Floor>();
        List<Elevator> elevatorList = new List<Elevator>();
        LinkedList<BuildingEvent> eventsQueue = new LinkedList<BuildingEvent>();

        private Statistics statistics = new Statistics();


        /// <summary>
        /// The singular constructor method. Use this to create a building.
        /// </summary>
        /// <param name="numberOfFloors"></param>
        /// <param name="numberOfElevators"></param>
        public Building(int numberOfFloors, int numberOfElevators, int timeOfOperation)
        {

            this.numberOfFloors = numberOfFloors;
            this.numberOfElevators = numberOfElevators;
            this.timeOfOperation = timeOfOperation;
        }

        private void CreateElevators(int numberOfElevators)
        {
            for (int elevatorNumber = 1; elevatorNumber <= numberOfElevators; elevatorNumber++)
            {
                Elevator elevator = new Elevator();

                elevatorList.Add(elevator);
            }
        }

        private void CreateFloors(int numberOfFloors)
        {
            for (int floorNumber = 0; floorNumber < numberOfFloors; floorNumber++)
            {
                Floor floor = new Floor();

                floorList.Add(floor);
            }
        }

        public void Run()
        {
            initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A List of type Floor.</returns>
        public List<Floor> getFloorList()
        {
            return floorList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A List of type Elevator.</returns>
        public List<Elevator> getElevatorList()
        {
            return elevatorList;
        }

        /// <summary>
        /// Set the floor list for the building
        /// </summary>
        /// <param name="floorList"></param>
        public void setFloorList(List<Floor> floorList)
        {
            this.floorList = floorList;
        }

        /// <summary>
        /// Set the elevator list for the building
        /// </summary>
        /// <param name="elevatorList"></param>
        public void setElevatorList(List<Elevator> elevatorList)
        {
            this.elevatorList = elevatorList;
        }

        /// <summary>
        /// Get the events held in the cue.
        /// </summary>
        /// <returns>A LinkedList of type BuildingEvent</returns>
        public LinkedList<BuildingEvent> getEventsQueue()
        {
            return eventsQueue;
        }

        private void initialize()
        {
            BuildingEvent bEvent = new ElevatorsChangeState(elevatorList, floorList, statistics, numberOfFloors, numberOfElevators);
            eventsQueue.AddFirst(bEvent);
            CreateFloors(numberOfFloors);
            CreateElevators(numberOfElevators);
        }



    }
}
