using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    public abstract class BuildingEvent
    {
        /// <summary>
        /// An abstract class for various events during the simulation.
        /// </summary>
        public abstract void Happen();
    }
}
