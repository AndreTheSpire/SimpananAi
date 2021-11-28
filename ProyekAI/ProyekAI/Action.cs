using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekAI
{
    class Action
    {
        public Board passive, aggresive;
        public Square passiveStart, passiveDestination;
        public Square aggresiveStart, aggresiveDestination;
        public int result;

        public Action(Board passive, Board aggresive, Square passiveStart, Square passiveDestination, Square aggresiveStart, Square aggresiveDestination, int result)
        {
            this.passive = passive;
            this.aggresive = aggresive;
            this.passiveStart = passiveStart;
            this.passiveDestination = passiveDestination;
            this.aggresiveStart = aggresiveStart;
            this.aggresiveDestination = aggresiveDestination;
            this.result = result;
        }
    }
}
