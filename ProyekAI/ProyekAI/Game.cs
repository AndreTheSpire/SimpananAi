using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyekAI
{
    public partial class Game : Form
    {
        public Board[] mainBoards = new Board[4]
        {
            new Board(1),
            new Board(2),
            new Board(3),
            new Board(4)
        };

        public bool GameIsDone { get; set; }
        public Player PlayerX;
        public Player PlayerO;
        public Player currentPlayer;
        private Move CurrentMovepassive { get; set; }

        private Move CurrentMoveaggresive { get; set; }
        public Game()
        {
            InitializeComponent();
            this.PlayerX = new Player(PlayerName.X, new int[2] { 1, 2 });
            this.PlayerO = new Player(PlayerName.O, new int[2] { 3, 4 });
            this.currentPlayer = PlayerO;
            this.GameIsDone = false;
            for (int i = 1; i < 5; i++)
            {
                cbBoard.Items.Add(i);
                comboBox1.Items.Add(i);
            }
            cbBoard.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            turnplayer.Text = "Turn :Player O";
            Refresh();

        }
        public void RunGame()
        {
                TakeTurn(currentPlayer);
                if (EndGame.BoardHasOnlyXsOrOs(mainBoards))
                {
                //console.writeline(endgame.determinewinner(mainboards) + " is the winner!");
                MessageBox.Show(EndGame.DetermineWinner(mainBoards) + " is the winner!");
                    GameIsDone = true;
                }
                reset();
                
                
        }
        public void reset()
        {
            tbX.Text = "";
            tbXX.Text = "";
            tbbX.Text = "";
            tbbXX.Text = "";
            tbY.Text = "";
            tbYY.Text = "";
            tbbY.Text = "";
            tbbYY.Text = "";
            cbBoard.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
        }

        public void TakeTurn(Player player)
        {
            Refresh();
            if (currentPlayer==PlayerX)//kalau bot
            {
                
            }
            else
            {
                Turn turn = new Turn(this);
                //AI Jalan
                //minimax
                Board[] duplicatedBoard = new Board[4];
                for (int i = 0; i < 4; i++)
                {
                    duplicatedBoard[i] = (Board)mainBoards[i].deepCopy();
                }
                Evaluator eval = new Evaluator(duplicatedBoard);
                Action best = eval.bestAction;
                int angka = 0;
                foreach (Square item in mainBoards[0].SquaresOnBoard)
                {
                    Console.WriteLine("square ke" + angka);
                    Console.WriteLine("punya X?" + item.HasX);
                    angka++;
                }


                Console.WriteLine("BEST MOVE = == == =");
                Console.WriteLine("BOARD PASSIVE : " + best.passive.BoardNumber);
                Console.WriteLine("PASSIVE START:{" + best.passiveStart.XCoordinate + "," + best.passiveStart.YCoordinate + "}");
                Console.WriteLine("PASSIVE END:{" + best.passiveDestination.XCoordinate + "," + best.passiveDestination.YCoordinate + "}");
                Console.WriteLine("BOARD AGGRESIVE : " + best.aggresive.BoardNumber);
                Console.WriteLine("AGGRESSIVE START:{" + best.aggresiveStart.XCoordinate + "," + best.aggresiveStart.YCoordinate + "}");
                Console.WriteLine("AGGRESIVE END:{" + best.aggresiveDestination.XCoordinate + "," + best.aggresiveDestination.YCoordinate + "}");
                Console.WriteLine("Result SBE: " + best.result);
                Console.WriteLine("--------------------------------------------");

                cbBoard.SelectedIndex = best.passive.BoardNumber - 1;
                tbX.Text = (best.passiveStart.XCoordinate - 1).ToString();
                tbY.Text = (best.passiveStart.YCoordinate - 1).ToString();
                tbXX.Text = (best.passiveDestination.XCoordinate - 1).ToString();
                tbYY.Text = (best.passiveDestination.YCoordinate - 1).ToString();

                Console.WriteLine("board :" + cbBoard.SelectedItem.ToString());
                comboBox1.SelectedIndex = best.aggresive.BoardNumber -1;
                tbbX.Text = (best.aggresiveStart.XCoordinate - 1).ToString();
                tbbY.Text = (best.aggresiveStart.YCoordinate - 1).ToString();
                tbbXX.Text = (best.aggresiveDestination.XCoordinate - 1).ToString();
                tbbYY.Text = (best.aggresiveDestination.YCoordinate - 1).ToString();
                Console.WriteLine("board :" + comboBox1.SelectedItem.ToString());
                turn = new Turn(this);
                //this.CurrentMovepassive = new Move(best.passiveStart, best.passiveDestination, best.passive, this.PlayerX.Name, true);
                //this.CurrentMoveaggresive = new Move(best.aggresiveStart, best.aggresiveDestination, best.aggresive, this.PlayerX.Name, false);
                //ExecuteCurrentMove(this.CurrentMovepassive);
                //ExecuteCurrentMove(this.CurrentMoveaggresive);

                //currentPlayer = PlayerO;
                //turnplayer.Text = "Turn :Player O";
                Refresh();
            }
            
        }
        /// <summary>
        /// Refreshes console with updated piece positions
        /// </summary>
        public void Refresh()
        {
            createLabel(panel1, mainBoards[0],0);
            createLabel(panel2, mainBoards[1],1);
            createLabel(panel3, mainBoards[2],2);
            createLabel(panel4, mainBoards[3],3);
            
        }

        /// <summary>
        /// Prints 2 boards according to required colors and offsets, along with row labels
        /// </summary>
        
        public void createLabel(Panel panelx, Board board,int warna)
        {
            int angka = -1;
            panelx.Controls.Clear();
            int x = 0, y = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    angka++;
                    Label l = new Label();
                    l.BorderStyle = BorderStyle.Fixed3D;
                    l.Size = new Size(50, 50);
                    l.TextAlign = ContentAlignment.MiddleCenter;
                    l.Location = new Point(x, y);
                    l.Font = new Font("Arial", 24, FontStyle.Bold);

                    
                    if (warna == 1 || warna == 3)
                    {
                        l.BackColor = Color.DimGray;
                    }

                    if (board.SquaresOnBoard[angka].HasO)
                    {
                        l.Text = "0";
                    }else if (board.SquaresOnBoard[angka].HasX)
                    {
                        l.Text = "X";
                    }
                    

                    panelx.Controls.Add(l);
                    x += 50;
                }
                x = 0;
                y += 50;
            }
        }

        private void btnsubmitmove_Click(object sender, EventArgs e)
        {
            if (GameIsDone == false)
            {
                RunGame();
            }
            else
            {
                MessageBox.Show("Game sudah berakhir!");
            }


        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }
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
    }
}
