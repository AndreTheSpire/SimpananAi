using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekAI
{
    public class Board
    {
        public Square[] SquaresOnBoard { get; }
        public int BoardNumber { get; }
        public bool HasXs
        {
            get
            {
                bool foundX = false;
                foreach (Square square in SquaresOnBoard)
                {
                    if (square.HasX == true)
                    {
                        foundX = true;
                        break;
                    }
                }
                return foundX;
            }
        }
        public object deepCopy()
        {
            Square[] squares = new Square[16];
            for(int i=0; i<16; i++)
            {
                squares[i] = (Square)this.SquaresOnBoard[i].deepCopy();
            }
            Board board = new Board(this.BoardNumber, squares);

            return board;
        }

        public bool HasOs
        {
            get
            {
                bool foundO = false;
                foreach (Square square in SquaresOnBoard)
                {
                    if (square.HasO == true)
                    {
                        foundO = true;
                        break;
                    }
                }
                return foundO;
            }

        }
        public Board(int boardNumber, Square[] squares)
        {
            this.BoardNumber = boardNumber;
            this.SquaresOnBoard = squares;
        }
        public Board(int boardNumber)
        {
            this.BoardNumber = boardNumber;
            this.SquaresOnBoard = new Square[16];
            int squareIndex = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.SquaresOnBoard[squareIndex] = new Square(j + 1, i + 1);
                    squareIndex++;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                this.SquaresOnBoard[i].HasX = true;
                this.SquaresOnBoard[12 + i].HasO = true;
            }
        }

        public Board(Board board)
        {
            this.BoardNumber = board.BoardNumber;
            this.SquaresOnBoard = board.SquaresOnBoard;
        }

        public override string ToString()
        {
            return "Board " + this.BoardNumber;
        }

        public string GetRowAsString(int row)
        {
            string result = "|";
            for (int i = 0; i < 4; i++)
            {
                int squareIndex = ((row - 1) * 4) + i;
                if (this.SquaresOnBoard[squareIndex].HasX)
                {
                    result += "X";
                }
                else if (this.SquaresOnBoard[squareIndex].HasO)
                {
                    result += "O";
                }
                else
                {
                    result += "-";
                }
                result += "|";
            }
            return result;
        }
    }
}
