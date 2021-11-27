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
        private Player CurrentPlayer { get; set; }
        private Board[] MainBoards { get; }
        private Board CurrentBoardpassive { get; set;}

        private Board CurrentBoardaggresive { get; set; }
        private Square StartSquarepassive { get; set; }

        private Square StartSquareaggresive { get; set; }
        private Square EndSquarepassive { get; set; }
        private Square EndSquareaggresive { get; set; }
        public Game CurrentGame { get; }
        private Move CurrentMovepassive { get; set; }

        private Move CurrentMoveaggresive { get; set; }
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
            if (GetUserInputForTurn())
            {
                CurrentGame.Refresh();
            }
            CurrentPlayer.LastMoveMade = CurrentMovepassive;

            if (EndGame.NoLegalAggressiveMove(currentGame))
            {
                if(TurnDone == false)
                {
                    Console.WriteLine(this.CurrentPlayer.LastMoveMade);
                    MessageBox.Show("There are no legal aggressive moves based on that passive move.");
                    Console.WriteLine("There are no legal aggressive moves based on that passive move.");
                    Console.WriteLine(EndGame.DisplayWinMessageForOpponent(CurrentPlayer));
                    done();
                    currentGame.GameIsDone = true;
                }
                
            }
            else
            {
                ExecutePassiveAggresiveTurn();
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
            Console.WriteLine("aggresive "+ this.EndSquareaggresive.XCoordinate + this.EndSquareaggresive.YCoordinate);
            Console.WriteLine("masukkk");
            this.CurrentMovepassive = new Move(this.StartSquarepassive, this.EndSquarepassive, this.CurrentBoardpassive, this.CurrentPlayer.Name, true);
            this.CurrentMoveaggresive = new Move(this.StartSquareaggresive, this.EndSquareaggresive, this.CurrentBoardaggresive, this.CurrentPlayer.Name, false);
            //Console.WriteLine(this.StartSquare.XCoordinate);
            //Console.WriteLine(this.StartSquare.YCoordinate);
            //Console.WriteLine(this.CurrentBoard.BoardNumber);
            //Console.WriteLine(CurrentMove.EndSquare.YCoordinate);
            return true;
        }
        /// <summary>
        /// Gets board selection from user.  If illegal, returns false. If legal, assigns as CurrentBoard.
        /// </summary>
        private bool GetBoardFromUser()
        {
            Console.WriteLine("test");
            Console.Write($"{CurrentPlayer}, select a board to make your {this.currentTurnType} move on (or type \"rules\" to see rules):  ");
            string selectedBoardInputpassive = "";
            string selectedBoardInputaggresive = "";
            
                selectedBoardInputpassive = CurrentGame.cbBoard.SelectedItem.ToString();
            
            
                selectedBoardInputaggresive = CurrentGame.comboBox1.SelectedItem.ToString();
            
            
            if (BoardLogic.IsValidBoard(selectedBoardInputpassive) == false) { return false; }
            if (BoardLogic.IsValidBoard(selectedBoardInputaggresive) == false) { return false; }
            this.CurrentBoardpassive = this.MainBoards[int.Parse(selectedBoardInputpassive) - 1];
            this.CurrentBoardaggresive = this.MainBoards[int.Parse(selectedBoardInputaggresive) - 1];
            Console.WriteLine("board passive: " + selectedBoardInputpassive);
            Console.WriteLine("board agrgesive: " + selectedBoardInputaggresive);
            if (BoardLogic.BoardIsHomeBoard(CurrentPlayer, this.CurrentBoardpassive.BoardNumber) == false)
            {
                MessageBox.Show("Passive move must be made on a home board. Press enter to continue.");
                //Console.WriteLine("Passive move must be made on a home board. Press enter to continue.");
                //Console.ReadLine();
                done();
                return false;
            }
            
                if (selectedBoardInputpassive==selectedBoardInputaggresive)
                {
                    ;
                    MessageBox.Show("Aggressive move must be made on a board of different color than your passive move.");
                    //Console.WriteLine("Aggressive move must be made on a board of different color than your passive move.");
                    //Console.ReadLine();
                    done();
                    return false;
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
            string userInputpasive = "";
            string userInputaggresive = "";
            
                if (CurrentGame.tbX.Text == "0")
                {
                    userInputpasive = "a";
                }
                else if (CurrentGame.tbX.Text == "1")
                {
                    userInputpasive = "b";
                }
                else if (CurrentGame.tbX.Text == "2")
                {
                    userInputpasive = "c";
                }
                else if (CurrentGame.tbX.Text == "3")
                {
                    userInputpasive = "d";
                }
                if (CurrentGame.tbY.Text == "0")
                {
                    userInputpasive += "1";
                }
                else if (CurrentGame.tbY.Text == "1")
                {
                    userInputpasive += "2";
                }
                else if (CurrentGame.tbY.Text == "2")
                {
                    userInputpasive += "3";
                }
                else if (CurrentGame.tbY.Text == "3")
                {
                    userInputpasive += "4";
                }
            
           
                if (CurrentGame.tbbX.Text == "0")
                {
                userInputaggresive = "a";
                }
                else if (CurrentGame.tbbX.Text == "1")
                {
                userInputaggresive = "b";
                }
                else if (CurrentGame.tbbX.Text == "2")
                {
                userInputaggresive = "c";
                }
                else if (CurrentGame.tbbX.Text == "3")
                {
                userInputaggresive = "d";
                }
                if (CurrentGame.tbbY.Text == "0")
                {
                userInputaggresive += "1";
                }
                else if (CurrentGame.tbbY.Text == "1")
                {
                userInputaggresive += "2";
                }
                else if (CurrentGame.tbbY.Text == "2")
                {
                userInputaggresive += "3";
                }
                else if (CurrentGame.tbbY.Text == "3")
                {
                userInputaggresive += "4";
                }

            Console.WriteLine("start passive: " + userInputpasive);
            Console.WriteLine("start agrgesive: " + userInputaggresive);


            if (!numLetterCheck.IsMatch(userInputpasive)|| !numLetterCheck.IsMatch(userInputaggresive))
            {
                MessageBox.Show("Not a valid square.  Press enter to continue.");
                done();
                //Console.WriteLine("Not a valid square.  Press enter to continue.");
                //Console.ReadLine();
                return false;
            }
            int startSquareInputpassive = Conversion.ConvertLetterNumInputToBoardIndex(userInputpasive);
            int startSquareInputaggresive = Conversion.ConvertLetterNumInputToBoardIndex(userInputaggresive);
            if (startSquareInputpassive == -1|| startSquareInputaggresive == -1)
            {
                MessageBox.Show("Not a valid square.  Press enter to continue.");
                done();
                //Console.WriteLine("Not a valid square.  Press enter to continue.");
                //Console.ReadLine();
                return false;
            }
            this.StartSquarepassive = this.CurrentBoardpassive.SquaresOnBoard[startSquareInputpassive];
            this.StartSquareaggresive = this.CurrentBoardaggresive.SquaresOnBoard[startSquareInputaggresive];
            if (BoardLogic.SquareHasOwnPiece(this.StartSquarepassive, CurrentPlayer.Name) == false|| BoardLogic.SquareHasOwnPiece(this.StartSquareaggresive, CurrentPlayer.Name) == false)
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
            string userInputpasive = "";
            string userInputaggresive = "";

                if (CurrentGame.tbXX.Text == "0")
                {
                userInputpasive = "a";
                }
                else if (CurrentGame.tbXX.Text == "1")
                {
                userInputpasive = "b";
                }
                else if (CurrentGame.tbXX.Text == "2")
                {
                userInputpasive = "c";
                }
                else if (CurrentGame.tbXX.Text == "3")
                {
                userInputpasive = "d";
                }
                if (CurrentGame.tbYY.Text == "0")
                {
                userInputpasive += "1";
                }
                else if (CurrentGame.tbYY.Text == "1")
                {
                userInputpasive += "2";
                }
                else if (CurrentGame.tbYY.Text == "2")
                {
                userInputpasive += "3";
                }
                else if (CurrentGame.tbYY.Text == "3")
                {
                userInputpasive += "4";
                }
            
                if (CurrentGame.tbbXX.Text == "0")
                {
                userInputaggresive = "a";
                }
                else if (CurrentGame.tbbXX.Text == "1")
                {
                userInputaggresive = "b";
                }
                else if (CurrentGame.tbbXX.Text == "2")
                {
                userInputaggresive = "c";
                }
                else if (CurrentGame.tbbXX.Text == "3")
                {
                userInputaggresive = "d";
                }
                if (CurrentGame.tbbYY.Text == "0")
                {
                userInputaggresive += "1";
                }
                else if (CurrentGame.tbbYY.Text == "1")
                {
                userInputaggresive += "2";
                }
                else if (CurrentGame.tbbYY.Text == "2")
                {
                userInputaggresive += "3";
                }
                else if (CurrentGame.tbbYY.Text == "3")
                {
                userInputaggresive += "4";
                }

            Console.WriteLine("end passive: " + userInputpasive);
            Console.WriteLine("end agrgesive: " + userInputaggresive);

            if (!numLetterCheck.IsMatch(userInputpasive) || !numLetterCheck.IsMatch(userInputaggresive))
            {
                MessageBox.Show("Not a valid square.  Press enter to continue.");
                done();
                //Console.WriteLine("Not a valid square.  Press enter to continue.");
                //Console.ReadLine();
                return false;
            }
            int endSquareInputpassive = Conversion.ConvertLetterNumInputToBoardIndex(userInputpasive);
            int endSquareInputaggresive = Conversion.ConvertLetterNumInputToBoardIndex(userInputaggresive);
            if (endSquareInputpassive == -1 || endSquareInputaggresive == -1)
            {
                MessageBox.Show("Not a valid square.  Press enter to continue.");
                done();
                //Console.WriteLine("Not a valid square.  Press enter to continue.");
                //Console.ReadLine();
                return false;
            }
            this.EndSquarepassive = this.CurrentBoardpassive.SquaresOnBoard[endSquareInputpassive];
            this.EndSquareaggresive = this.CurrentBoardaggresive.SquaresOnBoard[endSquareInputaggresive];
            return true;
        }
        /// <summary>
        /// Collects input from user for passive turn, ensures legality, then executes move.
        /// Also updates Turn properties to reflect upcoming aggressive turn.
        /// </summary>
        /// 
        private void ExecutePassiveAggresiveTurn()
        {
            if (TurnDone == false)
            {

            
                if (MoveLogic.MoveIsLegal(this.CurrentMovepassive) && MoveLogic.MoveIsLegal(this.CurrentMoveaggresive) && MoveLogic.MatchesPassiveMoveWhileAggressive(this.CurrentMovepassive, this.CurrentMoveaggresive))
                {
                    Console.WriteLine("ini aggresive start" + this.CurrentMoveaggresive.StartSquare.HasO);
                    Console.WriteLine("ini passive start" + this.CurrentMovepassive.StartSquare.HasO);
                    if (PassiveTurnDone == false)
                    {
                        ExecuteCurrentMove(this.CurrentMovepassive);
                        //CurrentPlayer.LastMoveMade = CurrentMovepassive;
                        TurnIsPassive = false;
                        PassiveTurnDone = true;
                        this.currentTurnType = TurnType.Aggressive;
                    }

                    Console.WriteLine("ini aggresive start" + this.CurrentMoveaggresive.StartSquare.HasO);
                    Console.WriteLine("ini passive start" + this.CurrentMovepassive.StartSquare.HasO);

                    ExecuteCurrentMove(this.CurrentMoveaggresive);
                    Console.WriteLine("ini aggresive end" + this.CurrentMoveaggresive.EndSquare);
                    Console.WriteLine("ini passive end" + this.CurrentMovepassive.EndSquare);
                    if (CurrentPlayer == CurrentGame.PlayerO)
                    {
                        CurrentPlayer = CurrentGame.PlayerX;
                        CurrentGame.currentPlayer = CurrentGame.PlayerX;
                        CurrentGame.turnplayer.Text = "Turn :Player X";
                    }
                    else
                    {
                        CurrentPlayer = CurrentGame.PlayerO;
                        CurrentGame.currentPlayer = CurrentGame.PlayerO;
                        CurrentGame.turnplayer.Text = "Turn :Player O";
                    }
                    TurnDone = true;
                }
                else
                {
                    if (this.CurrentMovepassive != null ||this.CurrentMoveaggresive!=null)
                    {
                        MessageBox.Show(MoveLogic.PrintErrorMessage(this.CurrentMovepassive));
                        MessageBox.Show(MoveLogic.PrintErrorMessage(this.CurrentMovepassive));
                    }
                    //Console.WriteLine(MoveLogic.PrintErrorMessage(this.CurrentMove) + "  Press enter to continue...");
                    //    Console.ReadLine();
                    PassiveTurnDone = false;
                    done();
                }
                CurrentGame.Refresh();
            }
        }
        //private void ExecutePassiveTurn()
        //{
        //    if (TurnDone == false)
        //    {

        //        if (GetUserInputForTurn())
        //        {
        //            CurrentGame.Refresh();
        //        }

        //        if (MoveLogic.MoveIsLegal(this.CurrentMovepassive)&& MoveLogic.MoveIsLegal(this.CurrentMoveaggresive) && MoveLogic.MatchesPassiveMoveWhileAggressive(this.CurrentMovepassive, this.CurrentMoveaggresive))
        //        {
        //            if (PassiveTurnDone == false)
        //            {
        //                ExecuteCurrentMove(this.CurrentMove);
        //                CurrentPlayer.LastMoveMade = CurrentMove;
        //                TurnIsPassive = false;
        //                PassiveTurnDone = true;
        //                this.currentTurnType = TurnType.Aggressive;
        //            }
        //        }
        //        else
        //        {
        //            if (this.CurrentMovepassive != null||)
        //            {
        //                MessageBox.Show(MoveLogic.PrintErrorMessage(this.CurrentMove));
        //            }
        //            //Console.WriteLine(MoveLogic.PrintErrorMessage(this.CurrentMove) + "  Press enter to continue...");
        //            //    Console.ReadLine();
        //            PassiveTurnDone = false;
        //            done();
        //        }
        //        CurrentGame.Refresh();
        //    }
            
        //}
        ///// <summary>
        ///// Collects input from user for aggressive turn, ensures legality, then executes move.
        ///// </summary>
        //private void ExecuteAggressiveTurn()
        //{
        //    if (TurnDone == false)
        //    {
        //        if (GetUserInputForTurn())
        //        {
        //            CurrentGame.Refresh();
        //        }
        //        //CurrentGame.Refresh();
        //        Console.WriteLine(this.CurrentPlayer.LastMoveMade);

        //        if (MoveLogic.MoveIsLegal(this.CurrentMove) && MoveLogic.MatchesPassiveMoveWhileAggressive(this.CurrentMove, this.CurrentPlayer.LastMoveMade))
        //        {
                    
        //                ExecuteCurrentMove(this.CurrentMove);
        //                if (CurrentPlayer == CurrentGame.PlayerO)
        //                {
        //                    CurrentPlayer = CurrentGame.PlayerX;
        //                    CurrentGame.currentPlayer = CurrentGame.PlayerX;
        //                    CurrentGame.turnplayer.Text = "Turn :Player X";
        //                }
        //                else
        //                {
        //                    CurrentPlayer = CurrentGame.PlayerO;
        //                CurrentGame.currentPlayer = CurrentGame.PlayerO;
        //                CurrentGame.turnplayer.Text = "Turn :Player O";
        //                }
        //                TurnDone = true;
                    

        //        }
        //        else
        //        {
        //            MessageBox.Show(MoveLogic.PrintErrorMessage(this.CurrentMove));
        //            done();
        //            //Console.WriteLine(MoveLogic.PrintErrorMessage(this.CurrentMove) + "  Press enter to continue...");
        //            //    Console.ReadLine();
        //        }
        //        CurrentGame.Refresh();
        //    }
                
            
        //}
        /// <summary>
        /// Called after move is verified as legal.  Executes move on applicable board.
        /// </summary>
        /// <param name="currentMove"></param>
        private void ExecuteCurrentMove(Move currentMove)
        {
            Console.WriteLine("batas==================");
            Console.WriteLine(currentMove.StartSquare.HasO);
            Console.WriteLine(currentMove.EndSquare.HasO);
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
            //if (CurrentPlayer == CurrentGame.PlayerO)
            //{
            //    CurrentPlayer = CurrentGame.PlayerX;
            //    CurrentGame.turnplayer.Text="Turn :Player X";
            //}
            //else
            //{
            //    CurrentPlayer = CurrentGame.PlayerO;
            //    CurrentGame.turnplayer.Text = "Turn :Player O";
            //}

        }
    }
}
