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

        enum ElevatorState
        {
            stationary,
            movingUp,
            movingDown,
            waitingUp,	//Elevator has stopped at a floor while in process of moving up.
            waitingDown,	//Elevator has stopped at a floor while in process of moving down.
            unloadingGoingUp,	//Elevator is unloading passengers at a floor while moving up.
            unloadingGoingDown, //Elevator is unloading passengers at a floor while moving down.
            loadingGoingUp,      //Elevator is loading passengers at a floor while moving up.
            loadingGoingDown //Elevator is loading passengers at a floor while moving down.
        }

        public ElevatorsChangeState(List<Elevator> elevatorList, List<Floor> floorList,
            Statistics statistics, int numberOfFloors, int numberOfElevators)
        {

            this.elevatorList = elevatorList;
            this.floorList = floorList;
            this.statistics = statistics;
            this.numberOfFloors = numberOfFloors;
            this.numberOfElevators = numberOfElevators;
        }

        
        public override void happen()
        {
            int elevatorNumber = 1;
            foreach (Elevator elevator in elevatorList)
            {
                executeNextMove(elevator);
                elevatorNumber++;
            }
        }


        private void executeNextMove(Elevator elevator)
        {
            int currentFloorNumber;
            Floor floor;
        }
    }
}
