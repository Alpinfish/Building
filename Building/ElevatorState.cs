using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    /// <summary>
    /// An enumeration to hold the fixed states of an elevator.
    /// </summary>
    public enum ElevatorState
    {
        Stationary,
        MovingUp,
        MovingDown,
        WaitingUp,
        WaitingDown,
        UnloadingGoingUp,
        UnloadingGoingDown,
        LoadingGoingUp,
        LoadingGoingDown
    }
}
