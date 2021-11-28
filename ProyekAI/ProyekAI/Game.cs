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
                //AI Jalan
                //minimax
                Board[] duplicatedBoard = new Board[4];
                Array.Copy(mainBoards, duplicatedBoard, 4);
                //boardEvaluator(duplicatedBoard);
                Evaluator eval = new Evaluator(duplicatedBoard);
                Action best = eval.bestAction;
                currentPlayer = PlayerO;
                turnplayer.Text = "Turn :Player O";
                Refresh();
            }
            else
            {
                Turn turn = new Turn(this);
                
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
    }
}
