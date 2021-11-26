using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProyekAI
{
    /// <summary>
    /// Represents a full turn and tracks relevant information through its duration
    /// </summary>
    public class Turn
    {
        public bool TurnDone = false;
        private Player CurrentPlayer { get; }
        private Board[] MainBoards { get; }
        private Board CurrentBoard { get; set;}
        private Square StartSquare { get; set; }
        private Square EndSquare { get; set; }
        public Game CurrentGame { get; }
        private Move CurrentMove { get; set; }
        private TurnType currentTurnType = TurnType.Passive;
        private bool TurnIsPassive = true;
        private bool PassiveTurnDone = false;
        /// <summary>
        /// Runs a turn, checks mid-turn for the end game condition of no legal
        /// aggressive moves remaining after passive move
        /// </summary>
        public Turn(Game currentGame)
        {
            this.CurrentPlayer = currentGame.currentPlayer;
            this.MainBoards = currentGame.mainBoards;
            this.CurrentGame = currentGame;
            ExecutePassiveTurn();
            if (EndGame.NoLegalAggressiveMove(currentGame))
            {
                Console.WriteLine(this.CurrentPlayer.LastMoveMade);
                Console.WriteLine("There are no legal aggressive moves based on that passive move.");
                Console.WriteLine(EndGame.DisplayWinMessageForOpponent(CurrentPlayer));
                currentGame.GameIsDone = true;
            }
            else
            {
                ExecuteAggressiveTurn();
            }
        }
        /// <summary>
        /// Collects input from user for their next move.  Returns
        /// false if input is invalid or illegal.  Creates and stores
        /// move as CurrentMove if legal.
        /// </summary>
        public bool GetUserInputForTurn()
        {
            Console.WriteLine("masuk ga?");
            if (!GetBoardFromUser()) { return false; }
            Console.WriteLine("masuk 1");
            if (!GetStartSquareFromUser()) { return false; }
            Console.WriteLine("masuk 2");
            if (!GetEndSquareFromUser()) { return false; }
            Console.WriteLine("masuk 3");
            Console.WriteLine("masukkk");
            this.CurrentMove = new Move(this.StartSquare, this.EndSquare, this.CurrentBoard, this.CurrentPlayer.Name, (TurnIsPassive));
            Console.WriteLine(this.StartSquare.XCoordinate);
            Console.WriteLine(this.StartSquare.YCoordinate);
            Console.WriteLine(this.CurrentBoard.BoardNumber);
            Console.WriteLine(CurrentMove.EndSquare.YCoordinate);
            return true;
        }
        /// <summary>
        /// Gets board selection from user.  If illegal, returns false. If legal, assigns as CurrentBoard.
        /// </summary>
        private bool GetBoardFromUser()
        {
            Console.WriteLine("test");
            Console.Write($"{CurrentPlayer}, select a board to make your {this.currentTurnType} move on (or type \"rules\" to see rules):  ");
            string selectedBoardInput = "";
            if (this.currentTurnType == TurnType.Passive)
            {
                selectedBoardInput = CurrentGame.cbBoard.SelectedItem.ToString();
            }
            else
            {
                selectedBoardInput = CurrentGame.comboBox1.SelectedItem.ToString();
            }
            
            if (BoardLogic.IsValidBoard(selectedBoardInput) == false) { return false; }
            this.CurrentBoard = this.MainBoards[int.Parse(selectedBoardInput) - 1];

            if (this.TurnIsPassive && BoardLogic.BoardIsHomeBoard(CurrentPlayer, this.CurrentBoard.BoardNumber) == false)
            {
                MessageBox.Show("Passive move must be made on a home board. Press enter to continue.");
                //Console.WriteLine("Passive move must be made on a home board. Press enter to continue.");
                //Console.ReadLine();
                done();
                return false;
            }
            if (!this.TurnIsPassive)
            {
                if (BoardLogic.BoardIsLegalForAggressiveMove(CurrentPlayer.LastMoveMade, CurrentBoard.BoardNumber) == false)
                {
                    MessageBox.Show("Aggressive move must be made on a board of different color than your passive move.");
                    //Console.WriteLine("Aggressive move must be made on a board of different color than your passive move.");
                    //Console.ReadLine();
                    done();
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Gets start square from user.  Returns false if illegal. Assigns to StartSquare if legal.
        /// </summary>
        private bool GetStartSquareFromUser() 
        {
            Regex numLetterCheck = new Regex(@"[a-d][1-4]");
            Console.Write($"{CurrentPlayer}, select a piece to move:  ");
            string userInput = "";
            if (this.currentTurnType == TurnType.Passive)
            {
                if (CurrentGame.tbX.Text == "0")
                {
                    userInput = "a";
                }
                else if (CurrentGame.tbX.Text == "1")
                {
                    userInput = "b";
                }
                else if (CurrentGame.tbX.Text == "2")
                {
                    userInput = "c";
                }
                else if (CurrentGame.tbX.Text == "3")
                {
                    userInput = "d";
                }
                if (CurrentGame.tbY.Text == "0")
                {
                    userInput += "1";
                }
                else if (CurrentGame.tbY.Text == "1")
                {
                    userInput += "2";
                }
                else if (CurrentGame.tbY.Text == "2")
                {
                    userInput += "3";
                }
                else if (CurrentGame.tbY.Text == "3")
                {
                    userInput += "4";
                }
            }
            else
            {
                if (CurrentGame.tbbX.Text == "0")
                {
                    userInput = "a";
                }
                else if (CurrentGame.tbbX.Text == "1")
                {
                    userInput = "b";
                }
                else if (CurrentGame.tbbX.Text == "2")
                {
                    userInput = "c";
                }
                else if (CurrentGame.tbbX.Text == "3")
                {
                    userInput = "d";
                }
                if (CurrentGame.tbbY.Text == "0")
                {
                    userInput += "1";
                }
                else if (CurrentGame.tbbY.Text == "1")
                {
                    userInput += "2";
                }
                else if (CurrentGame.tbbY.Text == "2")
                {
                    userInput += "3";
                }
                else if (CurrentGame.tbbY.Text == "3")
                {
                    userInput += "4";
                }
            }
            
            Console.WriteLine(userInput);
            if (!numLetterCheck.IsMatch(userInput))
            {
                MessageBox.Show("Not a valid square.  Press enter to continue.");
                done();
                //Console.WriteLine("Not a valid square.  Press enter to continue.");
                //Console.ReadLine();
                return false;
            }
            int startSquareInput = Conversion.ConvertLetterNumInputToBoardIndex(userInput);
            if (startSquareInput == -1)
            {
                MessageBox.Show("Not a valid square.  Press enter to continue.");
                done();
                //Console.WriteLine("Not a valid square.  Press enter to continue.");
                //Console.ReadLine();
                return false;
            }
            this.StartSquare = this.CurrentBoard.SquaresOnBoard[startSquareInput];
            if (BoardLogic.SquareHasOwnPiece(this.StartSquare, CurrentPlayer.Name) == false)
            {
                MessageBox.Show("You have no piece in that square.  Press enter to continue.");
                done();
                //Console.WriteLine("You have no piece in that square.  Press enter to continue.");
                //Console.ReadLine();
                return false;
            }
            return true;
        }
        /// <summary>
        /// Gets end square from user.  Returns false if illegal. Assigns to EndSquare if legal.
        /// </summary>
        private bool GetEndSquareFromUser()
        {
            Regex numLetterCheck = new Regex(@"[a-d][1-4]");
            Console.Write("Select square to move to:  ");
            string userInput = "";
            if (this.currentTurnType == TurnType.Passive)
            {
                if (CurrentGame.tbXX.Text == "0")
                {
                    userInput = "a";
                }
                else if (CurrentGame.tbXX.Text == "1")
                {
                    userInput = "b";
                }
                else if (CurrentGame.tbXX.Text == "2")
                {
                    userInput = "c";
                }
                else if (CurrentGame.tbXX.Text == "3")
                {
                    userInput = "d";
                }
                if (CurrentGame.tbYY.Text == "0")
                {
                    userInput += "1";
                }
                else if (CurrentGame.tbYY.Text == "1")
                {
                    userInput += "2";
                }
                else if (CurrentGame.tbYY.Text == "2")
                {
                    userInput += "3";
                }
                else if (CurrentGame.tbYY.Text == "3")
                {
                    userInput += "4";
                }
            }
            else
            {
                if (CurrentGame.tbbXX.Text == "0")
                {
                    userInput = "a";
                }
                else if (CurrentGame.tbbXX.Text == "1")
                {
                    userInput = "b";
                }
                else if (CurrentGame.tbbXX.Text == "2")
                {
                    userInput = "c";
                }
                else if (CurrentGame.tbbXX.Text == "3")
                {
                    userInput = "d";
                }
                if (CurrentGame.tbbYY.Text == "0")
                {
                    userInput += "1";
                }
                else if (CurrentGame.tbbYY.Text == "1")
                {
                    userInput += "2";
                }
                else if (CurrentGame.tbbYY.Text == "2")
                {
                    userInput += "3";
                }
                else if (CurrentGame.tbbYY.Text == "3")
                {
                    userInput += "4";
                }
            }
            
            
            userInput = userInput + CurrentGame.tbYY.Text;
            if (!numLetterCheck.IsMatch(userInput))
            {
                MessageBox.Show("Not a valid square.  Press enter to continue.");
                done();
                //Console.WriteLine("Not a valid square.  Press enter to continue.");
                //Console.ReadLine();
                return false;
            }
            int endSquareInput = Conversion.ConvertLetterNumInputToBoardIndex(userInput);
            if (endSquareInput == -1)
            {
                MessageBox.Show("Not a valid square.  Press enter to continue.");
                done();
                //Console.WriteLine("Not a valid square.  Press enter to continue.");
                //Console.ReadLine();
                return false;
            }
            this.EndSquare = this.CurrentBoard.SquaresOnBoard[endSquareInput];
            return true;
        }
        /// <summary>
        /// Collects input from user for passive turn, ensures legality, then executes move.
        /// Also updates Turn properties to reflect upcoming aggressive turn.
        /// </summary>
        private void ExecutePassiveTurn()
        {
            
                if (GetUserInputForTurn())
                {
                    CurrentGame.Refresh();
                }
                
                if (MoveLogic.MoveIsLegal(this.CurrentMove))
                {
                    ExecuteCurrentMove(this.CurrentMove);
                    CurrentPlayer.LastMoveMade = CurrentMove;
                    TurnIsPassive = false;
                    PassiveTurnDone = true;
                    this.currentTurnType = TurnType.Aggressive;
                }
                else
                {
                    Console.WriteLine(MoveLogic.PrintErrorMessage(this.CurrentMove) + "  Press enter to continue...");
                    Console.ReadLine();
                    PassiveTurnDone = false;
                }
                CurrentGame.Refresh();
            
        }
        /// <summary>
        /// Collects input from user for aggressive turn, ensures legality, then executes move.
        /// </summary>
        private void ExecuteAggressiveTurn()
        {
           
                if (GetUserInputForTurn())
                {
                    CurrentGame.Refresh();
                }
                //CurrentGame.Refresh();
                Console.WriteLine(this.CurrentPlayer.LastMoveMade);
                
                if (MoveLogic.MoveIsLegal(this.CurrentMove) && MoveLogic.MatchesPassiveMoveWhileAggressive(this.CurrentMove, this.CurrentPlayer.LastMoveMade))
                {
                    ExecuteCurrentMove(this.CurrentMove);
                    TurnDone = true;
                }
                else
                {
                    Console.WriteLine(MoveLogic.PrintErrorMessage(this.CurrentMove) + "  Press enter to continue...");
                    Console.ReadLine();
                }
                CurrentGame.Refresh();
            
        }
        /// <summary>
        /// Called after move is verified as legal.  Executes move on applicable board.
        /// </summary>
        /// <param name="currentMove"></param>
        private void ExecuteCurrentMove(Move currentMove)
        {
            currentMove.StartSquare.HasO = false;
            currentMove.StartSquare.HasX = false;

            Square transitionSquare = currentMove.EndSquare;
            if (currentMove.MoveIs2Spaces)
            {
                transitionSquare = currentMove.BoardMoveIsOn.SquaresOnBoard[currentMove.TransitionSquareIndex()];
            }
            if (currentMove.GetIndexOfSquarePastMove() != -1)
            {
                Square squarePastMove = currentMove.BoardMoveIsOn.SquaresOnBoard[currentMove.GetIndexOfSquarePastMove()];
                if (currentMove.EndSquare.HasX || transitionSquare.HasX)
                {
                    squarePastMove.HasX = true;
                }
                if (currentMove.EndSquare.HasO || transitionSquare.HasO)
                {
                    squarePastMove.HasO = true;
                }
            }
            transitionSquare.HasO = false;
            transitionSquare.HasX = false;
            if (currentMove.PlayerMakingMove == PlayerName.X)
            {
                currentMove.EndSquare.HasX = true;
                currentMove.EndSquare.HasO = false;
            }
            else
            {
                currentMove.EndSquare.HasO = true;
                currentMove.EndSquare.HasX = false;
            }
        }
        private void done()
        {
            TurnDone = true;
            PassiveTurnDone = true;
           
        }
    }
}
