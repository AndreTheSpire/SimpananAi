using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekAI
{
    class Evaluator
    {
        public Board[] boards;
        public Board[] duplicatedBoard;
        public List<Action> actions;
        public Action bestAction;
        private Square StartSquarepassive { get; set; }

        private Square StartSquareaggresive { get; set; }
        private Square EndSquarepassive { get; set; }
        private Square EndSquareaggresive { get; set; }
        public int index;
        public int[,] setMoves = new int[,] {
            {0,1}, {0,-1}, {1,0}, {-1,0},
            {0,2}, {0,-2}, {2,0}, {-2,0},
            {1,1}, {-1,-1}, {-1,1}, {1,-1},
            {2,2}, {-2,-2}, {-2,2}, {2,-2}
        };
        public Evaluator(Board[] duplicatedBoard)
        {

            //foreach (Square s in duplicatedBoard[1].SquaresOnBoard)
            //{
            //    Console.WriteLine("posisi X " + s.XCoordinate + "================");
            //    Console.WriteLine("posisi Y " + s.YCoordinate + "================");
            //    Console.WriteLine("ada player X?  " + s.HasX + "================");
            //}
            this.duplicatedBoard = duplicatedBoard;
            boards = new Board[4];
            Array.Copy(duplicatedBoard, boards, 4);
            //foreach (Square s in boards[1].SquaresOnBoard)
            //{
            //    Console.WriteLine("posisi X " + s.XCoordinate + "================");
            //    Console.WriteLine("posisi Y " + s.YCoordinate + "================");
            //    Console.WriteLine("ada player X?  " + s.HasX + "================");
            //}

            actions = new List<Action>();
            //Possibility setiap stone
            Console.WriteLine("mulai evaluator================ ");
            //passive = 0, 2
            int angka = 0;
            int cekangka = 0;
            //agressive = 1,3
            foreach(Square s in boards[0].SquaresOnBoard)
            {
                cekangka++;
                Console.WriteLine("batas loop cek ke " + s.ToString() + " ================");
                foreach (Square xx in boards[0].SquaresOnBoard)
                {
                    Console.WriteLine("square " + xx.ToString() + "================");
                    Console.WriteLine("posisi X " + xx.XCoordinate + "================");
                    Console.WriteLine("posisi Y " + xx.YCoordinate + "================");
                    Console.WriteLine("ada player X?  " + xx.HasX + "================");
                }
                Console.WriteLine("Loop passive=================================");

                angka++;
               
                if (s.HasX) {
                    //Console.WriteLine("X passive :" + s.XCoordinate);
                    //Console.WriteLine("Y passive :" + s.YCoordinate);
                    for (int i = 0; i < 16; i++)
                    {
                        //Console.WriteLine("Loop passive posibility ke "+i+"=================================");
                        Square destination = new Square(s.XCoordinate + setMoves[i, 0], s.YCoordinate + setMoves[i, 1]);
                        Console.WriteLine("batas loop cek dest ke " + destination.ToString() + " ================");
                        if (destination.XCoordinate < 1 || destination.XCoordinate > 4 || destination.YCoordinate > 4 || destination.YCoordinate < 1)
                        {
                            //Console.WriteLine("target melebihi batas");
                        }
                        else
                        {
                            //Console.WriteLine("masuk sini ga ");

                            StartSquarepassive = s.deepCopy();
                            EndSquarepassive =destination.deepCopy();
                            Console.WriteLine("start: " + StartSquarepassive);
                            Console.WriteLine("end: " + EndSquarepassive);
                            Move m = new Move(StartSquarepassive, EndSquarepassive, boards[0], PlayerName.X, true);
                        
                            if (MoveLogic.MoveIsLegal(m))
                            {
                                
                                Console.WriteLine("Move is legal");
                                foreach (Square s1 in boards[1].SquaresOnBoard)
                                {
                                    Console.WriteLine("Loop aggresive 1=================================");
                                    //Console.WriteLine("aggresive 1 ke ================");
                                    //Console.WriteLine("posisi X " + s1.XCoordinate + "================");
                                    //Console.WriteLine("posisi Y " + s1.YCoordinate + "================");
                                    //Console.WriteLine("ada player X?  " + s1.HasX + "================");
                                    if (s1.HasX) {
                                        Square aggresiveDest = new Square(s1.XCoordinate + setMoves[i, 0], s1.YCoordinate + setMoves[i, 1]);

                                        if (aggresiveDest.XCoordinate < 1 || aggresiveDest.XCoordinate > 4 || aggresiveDest.YCoordinate > 4 || aggresiveDest.YCoordinate < 1)
                                        {
                                            //Console.WriteLine("Move illegal, counting next possibilities");
                                        }
                                        else
                                        {
                                    //Console.WriteLine("batas loop agresive===========");
                                    
                                            StartSquareaggresive = s.deepCopy();
                                            EndSquareaggresive=aggresiveDest.deepCopy();
                                            Console.WriteLine("start: " + StartSquareaggresive);
                                            Console.WriteLine("end: " + EndSquareaggresive);
                                            Move m1 = new Move(StartSquareaggresive, EndSquareaggresive, boards[1], PlayerName.X, false);

                                            if (MoveLogic.MoveIsLegal(m1))
                                            {
                                                Console.WriteLine("Move is legal");
                                                Board newPassiveBoard = (Board)boards[0].deepCopy();
                                                //StartSquarepassive = GetStartSquareFromUser(s, newPassiveBoard);
                                                //EndSquarepassive = GetEndSquareFromUser(destination, newPassiveBoard);
                                                Move movepasive =new Move(StartSquarepassive, EndSquarepassive, newPassiveBoard, PlayerName.X, true);
                                                ExecuteCurrentMove(movepasive);
                                                //foreach (Square target in newPassiveBoard.SquaresOnBoard)
                                                //{
                                                //    if (target.XCoordinate == s.XCoordinate && target.YCoordinate == s.YCoordinate && target.HasX)
                                                //    {
                                                //        target.HasX = false;

                                                //    }
                                                //    if (target.XCoordinate == destination.XCoordinate && target.YCoordinate == destination.YCoordinate)
                                                //    {
                                                //        target.HasX = true;
                                                //    }
                                                //}
                                                Board newAggresiveBoard = (Board)boards[1].deepCopy();
                                                //StartSquareaggresive = GetStartSquareFromUser(s, newAggresiveBoard);
                                                //EndSquareaggresive = GetEndSquareFromUser(aggresiveDest, newAggresiveBoard);
                                                Move moveaggresive = new Move(StartSquareaggresive, EndSquareaggresive, newAggresiveBoard, PlayerName.X, true);
                                                ExecuteCurrentMove(moveaggresive);
                                                //foreach (Square target in newPassiveBoard.SquaresOnBoard)
                                                //{
                                                //    if (target.XCoordinate == s1.XCoordinate && target.YCoordinate == s1.YCoordinate && target.HasX)
                                                //    {
                                                //        target.HasX = false;

                                                //    }
                                                //    if (target.XCoordinate == destination.XCoordinate && target.YCoordinate == destination.YCoordinate)
                                                //    {
                                                //        target.HasX = true;
                                                //    }
                                                //}
                                                if(MoveLogic.MatchesPassiveMoveWhileAggressive(movepasive, moveaggresive))
                                                {
                                                    int result = countSBE(newPassiveBoard, newAggresiveBoard, boards[1]);
                                                    //Console.WriteLine(newPassiveBoard.BoardNumber);
                                                    //Console.WriteLine(newAggresiveBoard.BoardNumber);
                                                    //Console.WriteLine(s.XCoordinate + " " + s.YCoordinate);
                                                    //Console.WriteLine(destination.XCoordinate + " " + destination.YCoordinate);
                                                    //Console.WriteLine(s1.XCoordinate + " " + s.YCoordinate);
                                                    //Console.WriteLine(aggresiveDest.XCoordinate + " " + aggresiveDest.YCoordinate);
                                                    Console.WriteLine("Result SBE: " + result);
                                                    Console.WriteLine("=================================================");
                                                    actions.Add(new Action(newPassiveBoard, newAggresiveBoard, s, destination, s1, aggresiveDest, result));
                                                }
                                                else
                                                {
                                                    Console.WriteLine("move passive dan aggresive tidak sama");
                                                }
                                                
                                            }
                                            else
                                            {
                                                Console.WriteLine("move tidak legal");
                                            }
                                        }
                                    }
                                   

                                }
                                foreach (Square s2 in boards[3].SquaresOnBoard)
                                {
                                    //Console.WriteLine("aggresive 3 ke ================");
                                    //Console.WriteLine("posisi X " + s2.XCoordinate + "================");
                                    //Console.WriteLine("posisi Y " + s2.YCoordinate + "================");
                                    //Console.WriteLine("ada player X?  " + s2.HasX + "================");
                                    Console.WriteLine("Loop aggresive 3=================================");
                                    if (s2.HasX) {
                                        Square aggresiveDest = new Square(s2.XCoordinate + setMoves[i, 0], s2.YCoordinate + setMoves[i, 1]);
                                        if (aggresiveDest.XCoordinate < 1 || aggresiveDest.XCoordinate > 4 || aggresiveDest.YCoordinate > 4 || aggresiveDest.YCoordinate < 1)
                                        {
                                            Console.WriteLine("Move illegal, counting next possibilities");
                                        }
                                        else
                                        {

                                            StartSquareaggresive = s.deepCopy();
                                            EndSquareaggresive = aggresiveDest.deepCopy();
                                            Console.WriteLine("start: " + StartSquareaggresive);
                                            Console.WriteLine("end: " + EndSquareaggresive);
                                            Move m2 = new Move(StartSquareaggresive, EndSquareaggresive, boards[3], PlayerName.X, false);
                                            if (MoveLogic.MoveIsLegal(m2))
                                            {
                                                Console.WriteLine("Move is legal");

                                                Board newPassiveBoard = (Board)boards[0].deepCopy();
                                                //StartSquareaggresive = GetStartSquareFromUser(s, newPassiveBoard);
                                                //EndSquareaggresive = GetEndSquareFromUser(destination, newPassiveBoard);
                                                Move movepasive = new Move(StartSquarepassive, EndSquarepassive, newPassiveBoard, PlayerName.X, true);
                                                ExecuteCurrentMove(movepasive);
                                                //foreach (Square target in newPassiveBoard.SquaresOnBoard)
                                                //{
                                                //    if (target.XCoordinate == s.XCoordinate && target.YCoordinate == s.YCoordinate && target.HasX)
                                                //    {
                                                //        target.HasX = false;

                                                //    }
                                                //    if (target.XCoordinate == destination.XCoordinate && target.YCoordinate == destination.YCoordinate)
                                                //    {
                                                //        target.HasX = true;
                                                //    }
                                                //}
                                                Board newAggresiveBoard = (Board)boards[3].deepCopy();
                                                //StartSquareaggresive = GetStartSquareFromUser(s, newAggresiveBoard);
                                                //EndSquareaggresive = GetEndSquareFromUser(aggresiveDest, newAggresiveBoard);
                                                Move moveAgressive = new Move(StartSquareaggresive, EndSquareaggresive, newAggresiveBoard, PlayerName.X, true);
                                                ExecuteCurrentMove(moveAgressive);
                                                //foreach (Square target in newAggresiveBoard.SquaresOnBoard)
                                                //{
                                                //    if (target.XCoordinate == s2.XCoordinate && target.YCoordinate == s2.YCoordinate && target.HasX)
                                                //    {
                                                //        target.HasX = false;

                                                //    }
                                                //    if (target.XCoordinate == destination.XCoordinate && target.YCoordinate == destination.YCoordinate)
                                                //    {
                                                //        target.HasX = true;
                                                //    }
                                                //}

                                                if (MoveLogic.MatchesPassiveMoveWhileAggressive(movepasive, moveAgressive))
                                                {
                                                    int result = countSBE(newPassiveBoard, newAggresiveBoard, boards[3]);
                                                    //Console.WriteLine(newPassiveBoard.BoardNumber);
                                                    //Console.WriteLine(newAggresiveBoard.BoardNumber);
                                                    //Console.WriteLine(s.XCoordinate + " " + s.YCoordinate);
                                                    //Console.WriteLine(destination.XCoordinate + " " + destination.YCoordinate);
                                                    //Console.WriteLine(s1.XCoordinate + " " + s.YCoordinate);
                                                    //Console.WriteLine(aggresiveDest.XCoordinate + " " + aggresiveDest.YCoordinate);
                                                    Console.WriteLine("Result SBE: " + result);
                                                    Console.WriteLine("=================================================");
                                                    actions.Add(new Action(newPassiveBoard, newAggresiveBoard, s, destination, s2, aggresiveDest, result));
                                                }
                                                else
                                                {
                                                    Console.WriteLine("move passive dan aggresive tidak sama");
                                                }
                                               
                                            }
                                            else
                                            {
                                                Console.WriteLine("move tidak legal");
                                            }
                                        }
                                    }
                                    
                                }
                            }
                            else
                            {
                                Console.WriteLine("move tidak legal");
                            }
                        }
                    }
                }
                
            }

            foreach (Square s in boards[2].SquaresOnBoard)
            {
                cekangka++;
                Console.WriteLine("batas loop cek ke " + s.ToString() + " ================");
                foreach (Square xx in boards[2].SquaresOnBoard)
                {
                    Console.WriteLine("square " + xx.ToString() + "================");
                    Console.WriteLine("posisi X " + xx.XCoordinate + "================");
                    Console.WriteLine("posisi Y " + xx.YCoordinate + "================");
                    Console.WriteLine("ada player X?  " + xx.HasX + "================");
                }
                Console.WriteLine("Loop passive=================================");

                angka++;

                if (s.HasX)
                {
                    //Console.WriteLine("X passive :" + s.XCoordinate);
                    //Console.WriteLine("Y passive :" + s.YCoordinate);
                    for (int i = 0; i < 16; i++)
                    {
                        //Console.WriteLine("Loop passive posibility ke "+i+"=================================");
                        Square destination = new Square(s.XCoordinate + setMoves[i, 0], s.YCoordinate + setMoves[i, 1]);
                        Console.WriteLine("batas loop cek dest ke " + destination.ToString() + " ================");
                        if (destination.XCoordinate < 1 || destination.XCoordinate > 4 || destination.YCoordinate > 4 || destination.YCoordinate < 1)
                        {
                            //Console.WriteLine("target melebihi batas");
                        }
                        else
                        {
                            //Console.WriteLine("masuk sini ga ");

                            StartSquarepassive = s.deepCopy();
                            EndSquarepassive = destination.deepCopy();
                            Console.WriteLine("start: " + StartSquarepassive);
                            Console.WriteLine("end: " + EndSquarepassive);
                            Move m = new Move(StartSquarepassive, EndSquarepassive, boards[2], PlayerName.X, true);

                            if (MoveLogic.MoveIsLegal(m))
                            {

                                Console.WriteLine("Move is legal");
                                foreach (Square s1 in boards[1].SquaresOnBoard)
                                {
                                    Console.WriteLine("Loop aggresive 1=================================");
                                    //Console.WriteLine("aggresive 1 ke ================");
                                    //Console.WriteLine("posisi X " + s1.XCoordinate + "================");
                                    //Console.WriteLine("posisi Y " + s1.YCoordinate + "================");
                                    //Console.WriteLine("ada player X?  " + s1.HasX + "================");
                                    if (s1.HasX)
                                    {
                                        Square aggresiveDest = new Square(s1.XCoordinate + setMoves[i, 0], s1.YCoordinate + setMoves[i, 1]);

                                        if (aggresiveDest.XCoordinate < 1 || aggresiveDest.XCoordinate > 4 || aggresiveDest.YCoordinate > 4 || aggresiveDest.YCoordinate < 1)
                                        {
                                            //Console.WriteLine("Move illegal, counting next possibilities");
                                        }
                                        else
                                        {
                                            //Console.WriteLine("batas loop agresive===========");

                                            StartSquareaggresive = s.deepCopy();
                                            EndSquareaggresive = aggresiveDest.deepCopy();
                                            Console.WriteLine("start: " + StartSquareaggresive);
                                            Console.WriteLine("end: " + EndSquareaggresive);
                                            Move m1 = new Move(StartSquareaggresive, EndSquareaggresive, boards[1], PlayerName.X, false);

                                            if (MoveLogic.MoveIsLegal(m1))
                                            {
                                                Console.WriteLine("Move is legal");
                                                Board newPassiveBoard = (Board)boards[2].deepCopy();
                                                //StartSquarepassive = GetStartSquareFromUser(s, newPassiveBoard);
                                                //EndSquarepassive = GetEndSquareFromUser(destination, newPassiveBoard);
                                                Move movepasive = new Move(StartSquarepassive, EndSquarepassive, newPassiveBoard, PlayerName.X, true);
                                                ExecuteCurrentMove(movepasive);
                                                //foreach (Square target in newPassiveBoard.SquaresOnBoard)
                                                //{
                                                //    if (target.XCoordinate == s.XCoordinate && target.YCoordinate == s.YCoordinate && target.HasX)
                                                //    {
                                                //        target.HasX = false;

                                                //    }
                                                //    if (target.XCoordinate == destination.XCoordinate && target.YCoordinate == destination.YCoordinate)
                                                //    {
                                                //        target.HasX = true;
                                                //    }
                                                //}
                                                Board newAggresiveBoard = (Board)boards[1].deepCopy();
                                                //StartSquareaggresive = GetStartSquareFromUser(s, newAggresiveBoard);
                                                //EndSquareaggresive = GetEndSquareFromUser(aggresiveDest, newAggresiveBoard);
                                                Move moveaggresive = new Move(StartSquareaggresive, EndSquareaggresive, newAggresiveBoard, PlayerName.X, true);
                                                ExecuteCurrentMove(moveaggresive);
                                                //foreach (Square target in newPassiveBoard.SquaresOnBoard)
                                                //{
                                                //    if (target.XCoordinate == s1.XCoordinate && target.YCoordinate == s1.YCoordinate && target.HasX)
                                                //    {
                                                //        target.HasX = false;

                                                //    }
                                                //    if (target.XCoordinate == destination.XCoordinate && target.YCoordinate == destination.YCoordinate)
                                                //    {
                                                //        target.HasX = true;
                                                //    }
                                                //}
                                                if (MoveLogic.MatchesPassiveMoveWhileAggressive(movepasive, moveaggresive))
                                                {
                                                    int result = countSBE(newPassiveBoard, newAggresiveBoard, boards[1]);
                                                    //Console.WriteLine(newPassiveBoard.BoardNumber);
                                                    //Console.WriteLine(newAggresiveBoard.BoardNumber);
                                                    //Console.WriteLine(s.XCoordinate + " " + s.YCoordinate);
                                                    //Console.WriteLine(destination.XCoordinate + " " + destination.YCoordinate);
                                                    //Console.WriteLine(s1.XCoordinate + " " + s.YCoordinate);
                                                    //Console.WriteLine(aggresiveDest.XCoordinate + " " + aggresiveDest.YCoordinate);
                                                    Console.WriteLine("Result SBE: " + result);
                                                    Console.WriteLine("=================================================");
                                                    actions.Add(new Action(newPassiveBoard, newAggresiveBoard, s, destination, s1, aggresiveDest, result));
                                                }
                                                else
                                                {
                                                    Console.WriteLine("move passive dan aggresive tidak sama");
                                                }

                                            }
                                            else
                                            {
                                                Console.WriteLine("move tidak legal");
                                            }
                                        }
                                    }


                                }
                                foreach (Square s2 in boards[3].SquaresOnBoard)
                                {
                                    //Console.WriteLine("aggresive 3 ke ================");
                                    //Console.WriteLine("posisi X " + s2.XCoordinate + "================");
                                    //Console.WriteLine("posisi Y " + s2.YCoordinate + "================");
                                    //Console.WriteLine("ada player X?  " + s2.HasX + "================");
                                    Console.WriteLine("Loop aggresive 3=================================");
                                    if (s2.HasX)
                                    {
                                        Square aggresiveDest = new Square(s2.XCoordinate + setMoves[i, 0], s2.YCoordinate + setMoves[i, 1]);
                                        if (aggresiveDest.XCoordinate < 1 || aggresiveDest.XCoordinate > 4 || aggresiveDest.YCoordinate > 4 || aggresiveDest.YCoordinate < 1)
                                        {
                                            Console.WriteLine("Move illegal, counting next possibilities");
                                        }
                                        else
                                        {

                                            StartSquareaggresive = s.deepCopy();
                                            EndSquareaggresive = aggresiveDest.deepCopy();
                                            Console.WriteLine("start: " + StartSquareaggresive);
                                            Console.WriteLine("end: " + EndSquareaggresive);
                                            Move m2 = new Move(StartSquareaggresive, EndSquareaggresive, boards[3], PlayerName.X, false);
                                            if (MoveLogic.MoveIsLegal(m2))
                                            {
                                                Console.WriteLine("Move is legal");

                                                Board newPassiveBoard = (Board)boards[2].deepCopy();
                                                //StartSquareaggresive = GetStartSquareFromUser(s, newPassiveBoard);
                                                //EndSquareaggresive = GetEndSquareFromUser(destination, newPassiveBoard);
                                                Move movepasive = new Move(StartSquarepassive, EndSquarepassive, newPassiveBoard, PlayerName.X, true);
                                                ExecuteCurrentMove(movepasive);
                                                //foreach (Square target in newPassiveBoard.SquaresOnBoard)
                                                //{
                                                //    if (target.XCoordinate == s.XCoordinate && target.YCoordinate == s.YCoordinate && target.HasX)
                                                //    {
                                                //        target.HasX = false;

                                                //    }
                                                //    if (target.XCoordinate == destination.XCoordinate && target.YCoordinate == destination.YCoordinate)
                                                //    {
                                                //        target.HasX = true;
                                                //    }
                                                //}
                                                Board newAggresiveBoard = (Board)boards[3].deepCopy();
                                                //StartSquareaggresive = GetStartSquareFromUser(s, newAggresiveBoard);
                                                //EndSquareaggresive = GetEndSquareFromUser(aggresiveDest, newAggresiveBoard);
                                                Move moveAgressive = new Move(StartSquareaggresive, EndSquareaggresive, newAggresiveBoard, PlayerName.X, true);
                                                ExecuteCurrentMove(moveAgressive);
                                                //foreach (Square target in newAggresiveBoard.SquaresOnBoard)
                                                //{
                                                //    if (target.XCoordinate == s2.XCoordinate && target.YCoordinate == s2.YCoordinate && target.HasX)
                                                //    {
                                                //        target.HasX = false;

                                                //    }
                                                //    if (target.XCoordinate == destination.XCoordinate && target.YCoordinate == destination.YCoordinate)
                                                //    {
                                                //        target.HasX = true;
                                                //    }
                                                //}

                                                if (MoveLogic.MatchesPassiveMoveWhileAggressive(movepasive, moveAgressive))
                                                {
                                                    int result = countSBE(newPassiveBoard, newAggresiveBoard, boards[3]);
                                                    //Console.WriteLine(newPassiveBoard.BoardNumber);
                                                    //Console.WriteLine(newAggresiveBoard.BoardNumber);
                                                    //Console.WriteLine(s.XCoordinate + " " + s.YCoordinate);
                                                    //Console.WriteLine(destination.XCoordinate + " " + destination.YCoordinate);
                                                    //Console.WriteLine(s1.XCoordinate + " " + s.YCoordinate);
                                                    //Console.WriteLine(aggresiveDest.XCoordinate + " " + aggresiveDest.YCoordinate);
                                                    Console.WriteLine("Result SBE: " + result);
                                                    Console.WriteLine("=================================================");
                                                    actions.Add(new Action(newPassiveBoard, newAggresiveBoard, s, destination, s2, aggresiveDest, result));
                                                }
                                                else
                                                {
                                                    Console.WriteLine("move passive dan aggresive tidak sama");
                                                }

                                            }
                                            else
                                            {
                                                Console.WriteLine("move tidak legal");
                                            }
                                        }
                                    }

                                }
                            }
                            else
                            {
                                Console.WriteLine("move tidak legal");
                            }
                        }
                    }
                }

            }

            //actions.Add(new Action(..));
            //foreach (Action a in actions)
            //{
            //    Console.WriteLine("PASSIVE:{" + a.passiveStart.XCoordinate + "," + a.passiveStart.YCoordinate + "}");
            //    Console.WriteLine("AGGRESIVE:{" + a.passiveDestination.XCoordinate + "," + a.passiveDestination.YCoordinate + "}");
            //    Console.WriteLine("--------------------------------------------");
            //}

            //index = minimax(0,0,true,actions.Count-2);
            bestAction = getBestAction();
            
            Console.WriteLine("BEST MOVE = == == =");
            Console.WriteLine("BOARD PASSIVE : " + bestAction.passive.BoardNumber);
            Console.WriteLine("PASSIVE START:{" + bestAction.passiveStart.XCoordinate + "," + bestAction.passiveStart.YCoordinate + "}");
            Console.WriteLine("PASSIVE END:{" + bestAction.passiveDestination.XCoordinate + "," + bestAction.passiveDestination.YCoordinate + "}");
            Console.WriteLine("BOARD AGGRESIVE : " + bestAction.aggresive.BoardNumber);
            Console.WriteLine("AGGRESSIVE START:{" + bestAction.aggresiveStart.XCoordinate + "," + bestAction.aggresiveStart.YCoordinate + "}");
            Console.WriteLine("AGGRESIVE END:{" + bestAction.aggresiveDestination.XCoordinate + "," + bestAction.aggresiveDestination.YCoordinate + "}");
            Console.WriteLine("Result SBE: " + bestAction.result);
            Console.WriteLine("--------------------------------------------");

        }

        public int activeStones(Board passiveBoard, Board aggresiveBoard)
        {
            int stoneCount = 0;
            foreach(Square s in passiveBoard.SquaresOnBoard)
            {
                if (s.HasX)
                {
                    stoneCount++;
                }
            }
            foreach (Square s in aggresiveBoard.SquaresOnBoard)
            {
                if (s.HasX)
                {
                    stoneCount++;
                }
            }
            return stoneCount;
        }
        public int threatenedStones(Board passiveBoard)
        {
            int stoneCount = 0;

            foreach (Square s in passiveBoard.SquaresOnBoard)
            {
                if (s.HasX)
                {
                    int xNew = 0;
                    int yNew = 0;
                    for(int i = 0; i<16; i++)
                    {
                        xNew = setMoves[i, 0];
                        yNew = setMoves[i, 1];
                        foreach(Square enemy in passiveBoard.SquaresOnBoard)
                        {
                            if(enemy.XCoordinate == xNew && enemy.YCoordinate == yNew)
                            {
                                if (enemy.HasO)
                                {
                                    stoneCount++;
                                }
                            }
                        }
                    }
                }
            }

            return stoneCount*-1;
        }
        public int enemyStones(Board aggresiveBoard)
        {
            int stoneCount = 0;
            int result = 0;
            
            foreach (Square s in aggresiveBoard.SquaresOnBoard)
            {
                if (s.HasO)
                {
                    stoneCount++;
                }
            }
            if (stoneCount < 1)
            {
                result = 999;
            }

            return result;
        }

        public int pushedstone(Board aggresiveBoard,Board aggresivebeforeBoard)
        {
            int stoneCountbefore = 0;
            int stoneCountafter = 0;
            int result = 0;

            foreach (Square s in aggresivebeforeBoard.SquaresOnBoard)
            {
                if (s.HasO)
                {
                    stoneCountbefore++;
                }
            }
            foreach (Square s in aggresiveBoard.SquaresOnBoard)
            {
                if (s.HasO)
                {
                    stoneCountafter++;
                }
            }
            if (stoneCountafter < stoneCountbefore)
            {
                result += 20;
            }


            return result;
        }

        public int possibleMoves(Board passiveBoard, Board aggresiveBoard)
        {
            int result = 0;
            foreach(Square s in aggresiveBoard.SquaresOnBoard)
            {
                if (s.HasX)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        int newX = s.XCoordinate + setMoves[i, 0];
                        int newY = s.YCoordinate + setMoves[i, 1];
                        foreach(Square searched in aggresiveBoard.SquaresOnBoard)
                        {
                            if (searched.XCoordinate == newX && searched.YCoordinate == newY)
                            {
                                if (searched.HasO)
                                {
                                    result += 3;
                                }
                                else if (!searched.HasX)
                                {
                                    result++;
                                }
                            }
                        }
                    }
                }
            }

            foreach (Square s in passiveBoard.SquaresOnBoard)
            {
                if (s.HasX)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        int newX = s.XCoordinate + setMoves[i, 0];
                        int newY = s.YCoordinate + setMoves[i, 1];
                        foreach (Square searched in passiveBoard.SquaresOnBoard)
                        {
                            if (searched.XCoordinate == newX && searched.YCoordinate == newY)
                            {
                                if (!searched.HasO&&!searched.HasX)
                                {
                                    result += 1;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        public int countSBE(Board passiveBoard, Board aggresiveBoard,Board aggresivebeforeboard)
        {
            int result =0;//hitung result dari SBE
            result += activeStones(passiveBoard, aggresiveBoard);
            result += threatenedStones(passiveBoard);
            result += pushedstone(aggresiveBoard,aggresivebeforeboard);
            result += enemyStones(aggresiveBoard);
            result += possibleMoves(passiveBoard, aggresiveBoard);

            return result;
        }
        public int minimax(int curDepth, int nodeIndex, bool maxTurn, int targetDepth)
        {

            if (curDepth == targetDepth)
            {
                //bestAction = actions[nodeIndex];
                Console.WriteLine(nodeIndex);
                Console.WriteLine(actions.Count);
                return actions[nodeIndex].result;
            }

            if (maxTurn)
            {
                
                return Math.Max(minimax(curDepth + 1, nodeIndex * 2, false, targetDepth), minimax(curDepth + 1, nodeIndex * 2 + 1, false, targetDepth));
            }
            else
            {
                return Math.Min(minimax(curDepth + 1, nodeIndex * 2, true, targetDepth), minimax(curDepth + 1, nodeIndex * 2 + 1, true, targetDepth));
            }

        }

        public Action getBestAction()
        {
            Action best = actions[0];
            Console.WriteLine("posibility move");
            int angka = 0;
            foreach(Action a in actions)
            {
                angka++;
                Console.WriteLine("posibility move ke"+angka);
                Console.WriteLine("board " + a.passive.BoardNumber);
                Console.WriteLine("action dari " + a.passiveStart + "ke " + a.passiveDestination + " dengan result " + a.result);
                Console.WriteLine("board " + a.aggresive.BoardNumber);
                Console.WriteLine("action dari " + a.aggresiveStart + "ke " + a.aggresiveDestination+" dengan result "+a.result);
                if (a.result > best.result)
                {
                    best = a;
                }
            }

            return best;
        }
        private Square GetStartSquareFromUser(Square kotak,Board bord)
        {
           
            string userInputpasive = "";
            string userInputaggresive = "";

            //Console.WriteLine("X :" + kotak.XCoordinate.ToString());
            //Console.WriteLine("Y :" + kotak.YCoordinate.ToString());

            if (kotak.XCoordinate.ToString() == "1")
            {
                userInputpasive = "a";
            }
            else if (kotak.XCoordinate.ToString() == "2")
            {
                userInputpasive = "b";
            }
            else if (kotak.XCoordinate.ToString() == "3")
            {
                userInputpasive = "c";
            }
            else if (kotak.XCoordinate.ToString() == "4")
            {
                userInputpasive = "d";
            }
            if (kotak.YCoordinate.ToString() == "1")
            {
                userInputpasive += "1";
            }
            else if (kotak.YCoordinate.ToString() == "2")
            {
                userInputpasive += "2";
            }
            else if (kotak.YCoordinate.ToString() == "3")
            {
                userInputpasive += "3";
            }
            else if (kotak.YCoordinate.ToString() == "4")
            {
                userInputpasive += "4";
            }



            //Console.WriteLine("start passive: " + userInputpasive);
            //Console.WriteLine("start agrgesive: " + userInputaggresive);



            int startSquareInputpassive = Conversion.ConvertLetterNumInputToBoardIndex(userInputpasive);
            //Console.WriteLine("start int passive: " + startSquareInputpassive);
            //int startSquareInputaggresive = Conversion.ConvertLetterNumInputToBoardIndex(userInputaggresive);

            //this.StartSquarepassive = bord.SquaresOnBoard[startSquareInputpassive];
            return bord.SquaresOnBoard[startSquareInputpassive];
            //Console.WriteLine("start square passive: " + StartSquarepassive);
            //this.StartSquareaggresive = bord.SquaresOnBoard[startSquareInputaggresive];

        }
        /// <summary>
        /// Gets end square from user.  Returns false if illegal. Assigns to EndSquare if legal.
        /// </summary>
        private Square GetEndSquareFromUser(Square kotak, Board bord)
        {
            string userInputpasive = "";
            string userInputaggresive = "";

            //Console.WriteLine("X :" + kotak.XCoordinate.ToString());
            //Console.WriteLine("Y :" + kotak.YCoordinate.ToString());

            if (kotak.XCoordinate.ToString() == "1")
            {
                userInputpasive = "a";
            }
            else if (kotak.XCoordinate.ToString() == "2")
            {
                userInputpasive = "b";
            }
            else if (kotak.XCoordinate.ToString() == "3")
            {
                userInputpasive = "c";
            }
            else if (kotak.XCoordinate.ToString() == "4")
            {
                userInputpasive = "d";
            }
            if (kotak.YCoordinate.ToString() == "1")
            {
                userInputpasive += "1";
            }
            else if (kotak.YCoordinate.ToString() == "2")
            {
                userInputpasive += "2";
            }
            else if (kotak.YCoordinate.ToString() == "3")
            {
                userInputpasive += "3";
            }
            else if (kotak.YCoordinate.ToString() == "4")
            {
                userInputpasive += "4";
            }



            //Console.WriteLine("start passive: " + userInputpasive);
            //Console.WriteLine("start agrgesive: " + userInputaggresive);



            int endSquareInputpassive = Conversion.ConvertLetterNumInputToBoardIndex(userInputpasive);
            //int endSquareInputaggresive = Conversion.ConvertLetterNumInputToBoardIndex(userInputaggresive);

            //this.EndSquarepassive = bord.SquaresOnBoard[endSquareInputpassive];
            return bord.SquaresOnBoard[endSquareInputpassive];
            //this.EndSquareaggresive = bord.SquaresOnBoard[endSquareInputaggresive];


        }
        private void ExecuteCurrentMove(Move currentMove)
        {
            //Console.WriteLine("batas==================");
            //Console.WriteLine(currentMove.StartSquare.HasO);
            //Console.WriteLine(currentMove.EndSquare.HasO);
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

    }
}
