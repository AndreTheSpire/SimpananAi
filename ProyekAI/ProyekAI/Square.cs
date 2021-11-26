using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekAI
{
    public class Square
    {
        public Square(int xCoordinate, int yCoordinate)
        {
            this.XCoordinate = xCoordinate;
            this.YCoordinate = yCoordinate;
        }
        public int XCoordinate { get; }
        public int YCoordinate { get; }

        public bool HasX { get; set; }
        public bool HasO { get; set; }

        public override string ToString()
        {
            string result = "";
            switch (this.YCoordinate)
            {
                case 1:
                    result += "A";
                    break;
                case 2:
                    result += "B";
                    break;
                case 3:
                    result += "C";
                    break;
                case 4:
                    result += "D";
                    break;
            }
            result += this.XCoordinate;
            return result;
        }
    }
}
