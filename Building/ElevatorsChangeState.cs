using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    class ElevatorsChangeState : BuildingEvent
    {

        private List<Elevator> elevatorList;
        private List<Floor> floorList;
        private Statistics statistics;
        private int numberOfFloors;
        private int numberOfElevators;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="elevatorList"></param>
        /// <param name="floorList"></param>
        /// <param name="statistics"></param>
        /// <param name="numberOfFloors"></param>
        /// <param name="numberOfElevators"></param>
        public ElevatorsChangeState(List<Elevator> elevatorList, List<Floor> floorList,
            Statistics statistics, int numberOfFloors, int numberOfElevators)
        {

            this.elevatorList = elevatorList;
            this.floorList = floorList;
            this.statistics = statistics;
            this.numberOfFloors = numberOfFloors;
            this.numberOfElevators = numberOfElevators;
        }


        /// <summary>
        /// 
        /// </summary>
        public override void happen()
        {
            int elevatorNumber = 1;
            foreach (Elevator elevator in elevatorList)
            {
                executeNextMove(elevator);
                elevatorNumber++;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="elevator"></param>
        private void executeNextMove(Elevator elevator)
        {
            int currentFloorNumber;
            Floor floor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="personsList"></param>
        /// <param name="resultingElevatorState"></param>
        private void passengersEnterElevator(Elevator elevator, List<Person> personsList,
            ElevatorState resultingElevatorState)
        {
            foreach (Person person in personsList)
            {
                elevator.addPassengerToList(person);
                statistics.totalWaitingTime = statistics.totalWaitingTime + person.timePastInWaiting;
                Console.WriteLine("Person No. " + person.PersonNumber +
                    " at floor " + person.sourceFloorNumber +
                    " entered elevator number " + elevator.elevatorNumber);
            }
            elevator.elevatorState = resultingElevatorState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="floor"></param>
        private void unloadElevator(Elevator elevator, Floor floor)
        {
            int currentFloorNumber = floor.floorNumber;
            elevator.unLoadPassengers(currentFloorNumber);

            Console.WriteLine("Passengers exit elevator number " + elevator.elevatorNumber +
                " at floor number " + currentFloorNumber);
            if (elevator.elevatorState == ElevatorState.UnloadingGoingUp)
            {
                elevator.elevatorState = ElevatorState.WaitingUp;

            }
            else if (elevator.elevatorState == ElevatorState.UnloadingGoingDown)
            {
                elevator.elevatorState = ElevatorState.WaitingDown;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="floor"></param>
        private void loadOrMoveElevator(Elevator elevator, Floor floor)
        {
            int currentFloorNo = floor.floorNumber;

            //ToDo:comment this
            //elevator moving up
            if (elevator.elevatorState == ElevatorState.WaitingUp || elevator.currentFloorNo == 0)
            {
                floor.upButtonPressed = false;
                if (floor.getPersonsWaitingForUpList().Any())
                {
                    passengersEnterElevator(elevator, floor.getPersonsWaitingForUpList(), ElevatorState.MovingUp);
                    floor.clearUpList();

                }
                else if (!floor.getPersonsWaitingForDownList().Any() && currentFloorNo != 0
                    && !elevator.getPassengersList().Any())
                {
                    floor.downButtonPressed = false;
                    if (floor.getPersonsWaitingForDownList().Any())
                    {
                        passengersEnterElevator(elevator, floor.getPersonsWaitingForDownList(), ElevatorState.MovingDown);
                        floor.clearDownList();

                    }
                }
                else if (elevator.getPassengersList().Any() && currentFloorNo != numberOfFloors - 1)
                {
                    elevator.currentFloorNo = currentFloorNo + 1;
                    Console.WriteLine("Elevator No. " + elevator.elevatorNumber + " moves to " + (currentFloorNo + 1));
                    if (elevator.unloadAtThisFloor(currentFloorNo + 1))
                    {
                        elevator.elevatorState = ElevatorState.UnloadingGoingUp;

                    }
                    else if (currentFloorNo + 1 == numberOfFloors - 1)
                    {
                        elevator.elevatorState = ElevatorState.WaitingUp;
                    }
                    else
                    {
                        elevator.elevatorState = ElevatorState.MovingUp;
                    }

                }


            }

            //ToDo: comment this
            //elevator moving down
            else if (elevator.elevatorState == ElevatorState.WaitingDown || elevator.currentFloorNo == numberOfFloors - 1)
            {

                floor.downButtonPressed = false;

                if (floor.getPersonsWaitingForDownList().Any())
                {
                    passengersEnterElevator(elevator, floor.getPersonsWaitingForDownList(), ElevatorState.MovingDown);
                    floor.clearDownList();
                }
                else if (floor.getPersonsWaitingForUpList().Any() && currentFloorNo != numberOfFloors - 1
                    && !elevator.getPassengersList().Any())
                {
                    floor.upButtonPressed = false;
                    if (floor.getPersonsWaitingForUpList().Any())
                    {
                        passengersEnterElevator(elevator, floor.getPersonsWaitingForUpList(), ElevatorState.MovingUp);
                        floor.clearUpList();
                    }
                }
                else if (elevator.getPassengersList().Any() && currentFloorNo != 0)
                {
                    elevator.currentFloorNo = currentFloorNo - 1;
                    Console.WriteLine("Elevator No. " + elevator.elevatorNumber + " moves to " + (currentFloorNo - 1));
                    if (elevator.unloadAtThisFloor(currentFloorNo - 1))
                    {
                        elevator.elevatorState = ElevatorState.UnloadingGoingDown;
                    }
                    else if (currentFloorNo - 1 == 0)
                    {
                        elevator.elevatorState = ElevatorState.WaitingDown;
                    }
                    else
                    {
                        elevator.elevatorState = ElevatorState.MovingUp;
                    }
                }
            }

            if (!elevator.getPushedButtons().Any())
            {
                elevator.elevatorState = ElevatorState.Stationary;
            }
        }


    }
}
