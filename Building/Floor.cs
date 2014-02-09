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
        private int floorNo { get; set; }
        private Boolean upButtonPressed { get; set; }
        private Boolean downButtonPressed { get; set; }
        private List<Person> personsWaitingForUpList = new List<Person>();
        private List<Person> personsWaitingForDownList = new List<Person>();

    }
}
