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
        public int index;
        public int[,] setMoves = new int[,] {
            {0,1}, {0,-1}, {1,0}, {-1,0},
            {0,2}, {0,-2}, {2,0}, {-2,0},
            {1,1}, {-1,-1}, {-1,1}, {1,-1},
            {2,2}, {-2,-2}, {-2,2}, {2,-2}
        };
        public Evaluator(Board[] duplicatedBoard)
        {
            this.duplicatedBoard = duplicatedBoard;
            boards = new Board[4];
            Array.Copy(duplicatedBoard, boards, 4);
            actions = new List<Action>();
            //Possibility setiap stone
            //passive = 0, 2
            //agressive = 1,3
            foreach(Square s in boards[0].SquaresOnBoard)
            {
                for(int i = 0; i<16; i++)
                {
                    Square destination = new Square(s.XCoordinate + setMoves[i, 0], s.YCoordinate + setMoves[i, 1]);
                    Move m = new Move(s, destination, boards[0], PlayerName.X ,true);
                    if (destination.XCoordinate < 0 || destination.XCoordinate > 3 || destination.YCoordinate > 3 || destination.YCoordinate < 0)
                    {
                        Console.WriteLine("Move illegal, counting next possibilities");
                    }
                    else if (MoveLogic.MoveIsLegal(m)){
                        foreach(Square s1 in boards[1].SquaresOnBoard)
                        {
                            Square aggresiveDest = new Square(s1.XCoordinate + setMoves[i, 0], s1.YCoordinate + setMoves[i, 1]);
                            Move m1 = new Move(s1, aggresiveDest, boards[1], PlayerName.X, false);
                            if (aggresiveDest.XCoordinate < 0 || aggresiveDest.XCoordinate > 3 || aggresiveDest.YCoordinate > 3 || aggresiveDest.YCoordinate < 0)
                            {
                                Console.WriteLine("Move illegal, counting next possibilities");
                            }
                            else if(MoveLogic.MoveIsLegal(m1))
                            {
                                Board newPassiveBoard = new Board(boards[0]);
                                foreach (Square target in newPassiveBoard.SquaresOnBoard)
                                {
                                    if (target.XCoordinate == s.XCoordinate && target.YCoordinate == s.YCoordinate &&target.HasX)
                                    {
                                        target.HasX = false;

                                    }
                                    if (target.XCoordinate == destination.XCoordinate && target.YCoordinate == destination.YCoordinate)
                                    {
                                        target.HasX = true;
                                    }
                                }
                                Board newAggresiveBoard = new Board(boards[1]);
                                foreach (Square target in newPassiveBoard.SquaresOnBoard)
                                {
                                    if (target.XCoordinate == s1.XCoordinate && target.YCoordinate == s1.YCoordinate && target.HasX)
                                    {
                                        target.HasX = false;

                                    }
                                    if (target.XCoordinate == destination.XCoordinate && target.YCoordinate == destination.YCoordinate)
                                    {
                                        target.HasX = true;
                                    }
                                }
                                int result = countSBE(newPassiveBoard, newAggresiveBoard);
                                //Console.WriteLine(newPassiveBoard.BoardNumber);
                                //Console.WriteLine(newAggresiveBoard.BoardNumber);
                                //Console.WriteLine(s.XCoordinate + " " + s.YCoordinate);
                                //Console.WriteLine(destination.XCoordinate + " " + destination.YCoordinate);
                                //Console.WriteLine(s1.XCoordinate + " " + s.YCoordinate);
                                //Console.WriteLine(aggresiveDest.XCoordinate + " " + aggresiveDest.YCoordinate);
                                Console.WriteLine("Result SBE: "+result);
                                Console.WriteLine("=================================================");
                                actions.Add(new Action(newPassiveBoard, newAggresiveBoard, s, destination, s1, aggresiveDest, result));
                            }
                        }
                        foreach(Square s2 in boards[3].SquaresOnBoard)
                        {
                            Square aggresiveDest = new Square(s2.XCoordinate + setMoves[i, 0], s2.YCoordinate + setMoves[i, 1]);
                            Move m2 = new Move(s2, aggresiveDest, boards[3], PlayerName.X, false);
                            if (aggresiveDest.XCoordinate < 0 || aggresiveDest.XCoordinate > 3 || aggresiveDest.YCoordinate > 3 || aggresiveDest.YCoordinate < 0)
                            {
                                Console.WriteLine("Move illegal, counting next possibilities");
                            }
                            else if (MoveLogic.MoveIsLegal(m2))
                            {
                                Board newPassiveBoard = new Board(boards[0]);
                                foreach(Square target in newPassiveBoard.SquaresOnBoard)
                                {
                                    if(target.XCoordinate == s.XCoordinate && target.YCoordinate == s.YCoordinate && target.HasX)
                                    {
                                        target.HasX = false;

                                    }
                                    if(target.XCoordinate == destination.XCoordinate && target.YCoordinate == destination.YCoordinate)
                                    {
                                        target.HasX = true;
                                    }
                                }
                                Board newAggresiveBoard = new Board(boards[3]);
                                foreach (Square target in newAggresiveBoard.SquaresOnBoard)
                                {
                                    if (target.XCoordinate == s2.XCoordinate && target.YCoordinate == s2.YCoordinate && target.HasX)
                                    {
                                        target.HasX = false;

                                    }
                                    if (target.XCoordinate == destination.XCoordinate && target.YCoordinate == destination.YCoordinate)
                                    {
                                        target.HasX = true;
                                    }
                                }
                                int result = countSBE(newPassiveBoard, newAggresiveBoard);
                                //Console.WriteLine(newPassiveBoard.BoardNumber);
                                //Console.WriteLine(newAggresiveBoard.BoardNumber);
                                //Console.WriteLine(s.XCoordinate + " " + s.YCoordinate);
                                //Console.WriteLine(destination.XCoordinate + " " + destination.YCoordinate);
                                //Console.WriteLine(s2.XCoordinate + " " + s2.YCoordinate);
                                //Console.WriteLine(aggresiveDest.XCoordinate + " " + aggresiveDest.YCoordinate);
                                Console.WriteLine("Result SBE: " + result);
                                Console.WriteLine("=================================================");
                                actions.Add(new Action(newPassiveBoard, newAggresiveBoard, s, destination, s2, aggresiveDest, result));
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

        public int possibleMoves(Board passiveBoard, Board aggresiveBoard)
        {
            int stoneCount = 0;
            return stoneCount;
        }
        public int countSBE(Board passiveBoard, Board aggresiveBoard)
        {
            int result =0;//hitung result dari SBE
            result += activeStones(passiveBoard, aggresiveBoard);
            result += threatenedStones(passiveBoard);
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
            foreach(Action a in actions)
            {
                if (a.result > best.result)
                {
                    best = a;
                }
            }

            return best;
        }
    }
}
