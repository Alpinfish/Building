﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Building
{
    class ElevatorsChangeState : BuildingEvent
    {

        private List<Elevator> elevatorList;
        private List<Floor> floorList;
        private Statistics statistics;
        private int quantityOfFloors;
        private int quantityOfElevators;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_elevatorList"></param>
        /// <param name="_floorList"></param>
        /// <param name="_statistics"></param>
        /// <param name="_quantityOfFloors"></param>
        /// <param name="_quantityOfElevators"></param>
        public ElevatorsChangeState(List<Elevator> _elevatorList, List<Floor> _floorList,
            Statistics _statistics, int _quantityOfFloors, int _quantityOfElevators)
        {

            this.elevatorList = _elevatorList;
            this.floorList = _floorList;
            this.statistics = _statistics;
            this.quantityOfFloors = _quantityOfFloors;
            this.quantityOfElevators = _quantityOfElevators;
        }


        /// <summary>
        /// 
        /// </summary>
        public override void Happen()
        {
            int elevatorIDNumber = 1;
            foreach (Elevator elevator in elevatorList)
            {
                ExecuteNextMove(elevator);
                elevatorIDNumber++;
            }
        }


        /// <summary>
        /// Call this event to place waiting persons on this floor in the elevator.
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="personsList"></param>
        /// <param name="resultingElevatorState"></param>
        private void LoadElevator(Elevator elevator, List<Person> personsList,
            ElevatorState resultingElevatorState)
        {
            foreach (Person person in personsList)
            {
                elevator.AddPassengerToList(person);
                statistics.totalWaitingTime = statistics.totalWaitingTime + person.TimeExpiredWhileWaiting;
                Console.WriteLine("Person No. " + person.Person_Id +
                    " at floor " + person.SourceFloorNumber +
                    " entered elevator number " + elevator.Elevator_Id);
            }
            elevator.ElevatorState = resultingElevatorState;
        }

        /// <summary>
        /// Call this event to remove persons from the elevator who have selected this floor as thier destination.
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="floor"></param>
        private void UnloadElevator(Elevator elevator, Floor floor)
        {
            int currentFloorNumber = floor.FloorNumber;
            elevator.UnLoadPassengers(currentFloorNumber);

            Console.WriteLine("Passengers exit elevator number " + elevator.Elevator_Id +
                " at floor number " + currentFloorNumber);
            if (elevator.ElevatorState == ElevatorState.UnloadingGoingUp)
            {
                elevator.ElevatorState = ElevatorState.WaitingUp;

            }
            else if (elevator.ElevatorState == ElevatorState.UnloadingGoingDown)
            {
                elevator.ElevatorState = ElevatorState.WaitingDown;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="elevator"></param>
        private void MoveElevator(Elevator elevator)
        {
            int currentFloorNo = elevator.CurrentFloorNumber;
            Floor currentFloor = floorList.ElementAt(currentFloorNo);

            if (currentFloor.IsUpButtonPressed)
            {
                currentFloor.IsUpButtonPressed = false;
                if (currentFloor.GetPersonsWaitingForUpElevator().Any())
                {
                    LoadElevator(elevator, currentFloor.GetPersonsWaitingForUpElevator(), ElevatorState.MovingUp);
                    currentFloor.ClearUpList();
                }
                return;
            }
            else if (currentFloor.IsDownButtonPressed)
            {
                currentFloor.IsDownButtonPressed = false;
                if (currentFloor.GetPersonsWaitingForDownElevator().Any())
                {
                    LoadElevator(elevator, currentFloor.GetPersonsWaitingForDownElevator(), ElevatorState.MovingDown);
                    currentFloor.ClearDownList();
                }
                return;
            }

            int floorsToMove = 1;
            while (currentFloorNo + floorsToMove < quantityOfFloors || currentFloorNo - floorsToMove >= 0)
            {
                int upperFloorNo = currentFloorNo + floorsToMove;
                int lowerFloorNo = currentFloorNo - floorsToMove;
                Floor upperFloorToCheck = null;
                Floor lowerFloorToCheck = null;

                if (upperFloorNo < quantityOfFloors)
                {
                    upperFloorToCheck = floorList.ElementAt(upperFloorNo);
                }
                if (lowerFloorNo >= 0)
                {
                    lowerFloorToCheck = floorList.ElementAt(lowerFloorNo);
                }

                if (upperFloorToCheck != null && lowerFloorToCheck != null)
                {
                    if (upperFloorToCheck.IsUpButtonPressed)
                    {
                        elevator.ElevatorState = ElevatorState.MovingUp;
                        return;
                    }
                    else if (lowerFloorToCheck.IsDownButtonPressed)
                    {
                        elevator.ElevatorState = ElevatorState.MovingDown;
                    }
                    else if (upperFloorToCheck.IsDownButtonPressed)
                    {
                        elevator.ElevatorState = ElevatorState.MovingUp;
                    }
                    else if (lowerFloorToCheck.IsUpButtonPressed)
                    {
                        elevator.ElevatorState = ElevatorState.MovingDown;
                    }


                }
                else if (upperFloorToCheck != null)
                {
                    if (upperFloorToCheck.IsDownButtonPressed || upperFloorToCheck.IsUpButtonPressed)
                    {
                        elevator.ElevatorState = ElevatorState.MovingUp;
                        return;
                    }
                }
                else if (lowerFloorToCheck != null)
                {
                    if (lowerFloorToCheck.IsDownButtonPressed || lowerFloorToCheck.IsUpButtonPressed)
                    {
                        elevator.ElevatorState = ElevatorState.MovingDown;
                        return;
                    }
                }

                floorsToMove++;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elevator"></param>
        private void ExecuteNextMove(Elevator elevator)
        {
            int currentFloorNumber;
            Floor floor;

            if (elevator.ElevatorState == ElevatorState.MovingUp)
            {
                currentFloorNumber = elevator.CurrentFloorNumber + 1;
                Console.WriteLine("Elevator No. " + elevator.Elevator_Id + " moves to " + currentFloorNumber);
                elevator.CurrentFloorNumber = currentFloorNumber;
                floor = floorList.ElementAt(currentFloorNumber);

                if (elevator.UnloadAtThisFloor(currentFloorNumber))
                {
                    elevator.ElevatorState = ElevatorState.UnloadingGoingUp;
                }

                else if (floor.IsUpButtonPressed || currentFloorNumber == quantityOfFloors - 1)
                {
                    elevator.ElevatorState = ElevatorState.WaitingUp;
                }
            }
            else if (elevator.ElevatorState == ElevatorState.MovingDown)
            {
                currentFloorNumber = elevator.CurrentFloorNumber - 1;
                Console.WriteLine("Elevator No. " + elevator.Elevator_Id + " moves to " + currentFloorNumber);
                elevator.CurrentFloorNumber = currentFloorNumber;
                floor = floorList.ElementAt(currentFloorNumber);


                if (elevator.UnloadAtThisFloor(currentFloorNumber))
                {
                    elevator.ElevatorState = ElevatorState.UnloadingGoingDown;

                }
                else if (floor.IsDownButtonPressed || currentFloorNumber == 0)
                {
                    elevator.ElevatorState = ElevatorState.WaitingDown;
                }
            }

            else if (elevator.ElevatorState == ElevatorState.UnloadingGoingDown || elevator.ElevatorState == ElevatorState.UnloadingGoingUp)
            {
                currentFloorNumber = elevator.CurrentFloorNumber;
                floor = floorList.ElementAt(currentFloorNumber);
                UnloadElevator(elevator, floor);
            }

            else
            {
                MoveElevator(elevator);
            }
        }


    }
}
