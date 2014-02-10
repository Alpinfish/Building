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
                elevator.elevatorNumber = elevatorNumber;
                elevator.currentFloorNo = 0;
                elevator.elevatorState=ElevatorState.Stationary;
                elevatorList.Add(elevator);
            }
        }

        private void CreateFloors(int numberOfFloors)
        {
            for (int floorNumber = 0; floorNumber < numberOfFloors; floorNumber++)
            {
                Floor floor = new Floor();
                floor.floorNumber=floorNumber;
                floor.upButtonPressed = false;
                floor.downButtonPressed = false;
                floorList.Add(floor);
            }
        }

        private BuildingEvent generatePerson()
        {
            Random generator = new Random();
            double randomNumber = generator.NextDouble();

            //the probability of returning null is 0.5
            if (randomNumber > 0.5)
            {
                return null;
            }

            BuildingEvent buildingEvent = new PersonArrives();
            Person person = new Person();
            Floor floor = new Floor();

            //need a source and destination floor, they cant be the same.
            int sourceFloorNo = generator.Next(numberOfFloors);
            int destinationFloorNo = generator.Next(numberOfFloors);
            while (sourceFloorNo == destinationFloorNo)
            {
                destinationFloorNo = generator.Next(numberOfFloors);
            }

            // maxWaitingTime can be in the range 30 to 180
            int minLimit = 30;
            int maxLimit = 180;
            int maxWaitingTime = generator.Next(maxLimit - minLimit + 1) + minLimit;

            person.sourceFloorNumber = sourceFloorNo;
            person.destinationFloorNumber = destinationFloorNo;
            person.maxWaitingTime = maxWaitingTime;
            person.timePastInWaiting = 0;

            floor = floorList.ElementAt(sourceFloorNo);

            ((PersonArrives)buildingEvent).setFloor(floor);
            ((PersonArrives)buildingEvent).setPerson(person);

            return buildingEvent;


        }


        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            BuildingEvent buildingEvent;
            int personNumber = 1;
            initialize();

            for (int time = 0; time < timeOfOperation; )
            {
                BuildingEvent eventToExecute = eventsQueue.First();
                eventsQueue.RemoveFirst();

                if (!(eventToExecute is ElevatorsChangeState))
                {
                    if (eventToExecute is PersonArrives)
                    {
                        ((PersonArrives)eventToExecute).getPerson().setPersonNo(personNumber) ;
                        personNumber++;
                    }

                    eventToExecute.happen();
                    buildingEvent = generatePerson();
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
                    eventToExecute.happen();

                    BuildingEvent elevatorNextChange = new ElevatorsChangeState(elevatorList, floorList,
                        statistics, numberOfFloors, numberOfElevators);

                    eventsQueue.AddFirst(elevatorNextChange);
                    buildingEvent = generatePerson();

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
            Console.WriteLine("Final statistics : ");
            statistics.printStatistics();
            Console.ReadKey();
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
