using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProyekAI
{
    /// <summary>
    /// Methods to check that board rules are enforced.
    /// </summary>
    public static class BoardLogic
    {
        // Checks if a board is a homeboard of the player.
        public static bool BoardIsHomeBoard(Player player, int boardNum)
        {
            foreach (int num in player.HomeBoards)
            {
                if (num == boardNum)
                {
                    return true;
                }
            }
            return false;
        }

        // Checks if board is of a different color
        // than the previous passive move's board
        public static bool BoardIsLegalForAggressiveMove(Move passiveMove, int boardNum)
        {
            if (passiveMove == null)
            {
                return false;
            }
            if (!(passiveMove.BoardMoveIsOn.BoardNumber % 2 != boardNum % 2))
            {
                return false;
            }
            return true;
        }

        // Checks that user input is a board, or a
        // request to see rules.
        public static bool IsValidBoard(string selectedBoardInput)
        {
            switch (selectedBoardInput)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                    break;
                case "rules":
                    Console.Clear();
                    
                   
                    return false;
                default:
                    MessageBox.Show("Not a valid board. Press enter to continue.");
                    //Console.WriteLine("Not a valid board. Press enter to continue.");
                    //MessageBox.Show("Not a valid board");
                    //Console.ReadLine();
                    return false;
            }
            return true;
        }

        // Checks if square contains player's own piece.
        public static bool SquareHasOwnPiece(Square square, PlayerName player)
        {
            if (square.HasO && player == PlayerName.O)
            {
                return true;
            }
            if (square.HasX && player == PlayerName.X)
            {
                return true;
            }
            return false;
        }
    }
}
