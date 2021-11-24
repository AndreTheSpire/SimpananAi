using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekAI
{
    class Stone
    {
        public int board;
        public int x;
        public int y;
        public int side;

        public Stone(int board, int x, int y, int side)
        {
            this.board = board;
            this.x = x;
            this.y = y;
            this.side = side;
        }
    }
}
