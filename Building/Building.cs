using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Building
{

    public class Building
    {
        int quantityOfFloors; //The number of floors this building has
        int quantityOfElevators; //The number of elevators this building has
        int lengthOfOperation; //How long will the building operate

        List<Floor> floorList = new List<Floor>();
        List<Elevator> elevatorList = new List<Elevator>();
        LinkedList<BuildingEvent> eventsQueue = new LinkedList<BuildingEvent>();

        private Statistics statistics = new Statistics();


        /// <summary>
        /// The singular constructor method. Use this to create a building.
        /// </summary>
        /// <param name="_quantityOfFloors"></param>
        /// <param name="_quantityOfElevators"></param>
        public Building(int _quantityOfFloors, int _quantityOfElevators, int _lengthOfOperation)
        {
            this.quantityOfFloors = _quantityOfFloors;
            this.quantityOfElevators = _quantityOfElevators;
            this.lengthOfOperation = _lengthOfOperation;
        }

        /// <summary>
        /// Create all elevators in the building, and set the to default status.
        /// </summary>
        /// <param name="numberOfElevators"></param>
        private void CreateElevators(int numberOfElevators)
        {
            for (int elevatorNumber = 1; elevatorNumber <= numberOfElevators; elevatorNumber++)
            {
                Elevator elevator = new Elevator();
                elevator.Elevator_Id = elevatorNumber;
                elevator.CurrentFloorNumber = 0;
                elevator.ElevatorState = ElevatorState.Stationary;
                elevatorList.Add(elevator);
            }
        }

        /// <summary>
        /// Create all floors in the building, and set the up and down buttons to false
        /// </summary>
        /// <param name="numberOfFloors"></param>
        private void CreateFloors(int numberOfFloors)
        {
            for (int floorNumber = 0; floorNumber < numberOfFloors; floorNumber++)
            {
                Floor _floor = new Floor();
                _floor.FloorNumber = floorNumber;
                _floor.IsUpButtonPressed = false;
                _floor.IsDownButtonPressed = false;
                floorList.Add(_floor);
            }
        }

        /// <summary>
        /// Generate one person, who arrives at a random floor, and wants to go to another random floor.
        /// </summary>
        /// <returns>A PersonArrives BuildingEvent </returns>
        private BuildingEvent GeneratePerson()
        {
            Random generator = new Random();
            double randomNumber = generator.NextDouble();

            //the probability of returning null is 0.5. This value can be anything < 1 and > 0.
            if (randomNumber > 0.5)
            {
                return null;
            }

            BuildingEvent buildingEvent = new PersonArrives();
            Person _person = new Person();
            Floor _floor = new Floor();

            //need a source and destination floor, they cant be the same.
            int sourceFloorNo = generator.Next(quantityOfFloors);
            int destinationFloorNo = generator.Next(quantityOfFloors);
            while (sourceFloorNo == destinationFloorNo)
            {
                destinationFloorNo = generator.Next(quantityOfFloors);
            }

            // maxWaitingTime can be in the range 30 to 180. This value is arbitrary.
            int minLimit = 30;
            int maxLimit = 180;
            int maxWaitingTime = generator.Next(maxLimit - minLimit + 1) + minLimit;

            _person.SourceFloorNumber = sourceFloorNo;
            _person.DestinationFloorNumber = destinationFloorNo;
            _person.MaxWaitingTime = maxWaitingTime;
            _person.TimeExpiredWhileWaiting = 0;

            _floor = floorList.ElementAt(sourceFloorNo);

            ((PersonArrives)buildingEvent).Floor = _floor;
            ((PersonArrives)buildingEvent).Person = _person;

            return buildingEvent;


        }


        /// <summary>
        /// Call this method to run the simulation with the entered values
        /// </summary>
        public void Run()
        {
            BuildingEvent buildingEvent;
            int personNumber = 1;
            Initialize();

            for (int time = 0; time < lengthOfOperation; )
            {
                BuildingEvent eventToExecute = eventsQueue.First();
                eventsQueue.RemoveFirst();

                if (!(eventToExecute is ElevatorsChangeState))
                {
                    if (eventToExecute is PersonArrives)
                    {
                        ((PersonArrives)eventToExecute).Person.Person_Id = personNumber;
                        personNumber++;
                    }

                    eventToExecute.Happen();
                    buildingEvent = GeneratePerson();
                    if (buildingEvent != null)
                    {
                        eventsQueue.AddFirst(buildingEvent);
                        statistics.numberOfPersons++;
                    }

                }
                else
                {
                    time++;
                    Console.WriteLine("");
                    Console.WriteLine("Time : " + (time));
                    eventToExecute.Happen();

                    BuildingEvent elevatorNextChange = new ElevatorsChangeState(elevatorList, floorList,
                        statistics, quantityOfFloors, quantityOfElevators);

                    eventsQueue.AddFirst(elevatorNextChange);
                    buildingEvent = GeneratePerson();

                    if (buildingEvent != null)
                    {
                        eventsQueue.AddFirst(buildingEvent);
                        statistics.numberOfPersons++;

                    }

                    BuildingEvent personsGiveUp = new PersonsGiveUpAndLeave(floorList, statistics);
                    eventsQueue.AddFirst(personsGiveUp);
                }






            }
            Console.WriteLine("");
            Console.WriteLine("Final _statistics : ");
            statistics.PrintStatistics();
            Console.ReadKey();
        }

        /// <summary>
        /// Self explanitory.
        /// </summary>
        /// <returns>A List of type Floor.</returns>
        public List<Floor> GetFloorList()
        {
            return floorList;
        }

        /// <summary>
        /// Self explanitory.
        /// </summary>
        /// <returns>A List of type Elevator.</returns>
        public List<Elevator> GetElevatorList()
        {
            return elevatorList;
        }

        /// <summary>
        /// Set the floor list for the building
        /// </summary>
        /// <param name="floorList"></param>
        public void SetFloorList(List<Floor> floorList)
        {
            this.floorList = floorList;
        }

        /// <summary>
        /// Set the elevator list for the building
        /// </summary>
        /// <param name="elevatorList"></param>
        public void SetElevatorList(List<Elevator> elevatorList)
        {
            this.elevatorList = elevatorList;
        }

        /// <summary>
        /// Get the events held in the cue.
        /// </summary>
        /// <returns>A LinkedList of type BuildingEvent</returns>
        public LinkedList<BuildingEvent> GetEventsQueue()
        {
            return eventsQueue;
        }

        /// <summary>
        /// The start values for the simulation, and the call to the construction company to build our building. 
        /// </summary>
        private void Initialize()
        {
            BuildingEvent bEvent = new ElevatorsChangeState(elevatorList, floorList, statistics, quantityOfFloors, quantityOfElevators);
            eventsQueue.AddFirst(bEvent);
            CreateFloors(quantityOfFloors);
            CreateElevators(quantityOfElevators);
        }



    }
}
