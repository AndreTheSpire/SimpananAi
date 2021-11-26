using System;
using System.Collections;
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
    public partial class Form1 : Form
    {
        static List<Stone> stones;
        public Form1()
        {
            InitializeComponent();
            Console.WriteLine("Welcome to Shobu!\n");
            Console.WriteLine(Game.rules);
            Console.WriteLine("Press enter to begin...");
            Console.ReadLine();
            while (true)
            {
                Game game = new Game();
                //game.RunGame();
                //Console.WriteLine("Play again? (Y/N)");
                //if (!(Console.ReadLine().ToUpper() == "Y"))
                //{
                //    break;
                //}
            }
            //Console.WriteLine("Thanks for playing!  Press enter to exit...");
            //Console.ReadLine();
            //initStones();

            //createLabel(panel1, 1);
            //createLabel(panel2, 2);
            //createLabel(panel3, 3);
            //createLabel(panel4, 4);

            //for(int i=1; i<5; i++)
            //{
            //    cbBoard.Items.Add(i);
            //}

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void createLabel(Panel panelx, int board)
        {
            //int x = 0, y = 0;
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        Label l = new Label();
            //        l.BorderStyle = BorderStyle.Fixed3D;
            //        l.Size = new Size(100, 100);
            //        l.TextAlign = ContentAlignment.MiddleCenter;
            //        l.Location = new Point(x, y);
            //        l.Font = new Font("Arial", 24, FontStyle.Bold);

            //        if(board==2 || board == 4)
            //        {
            //            l.BackColor = Color.Brown;
            //        }
            //        else
            //        {
            //            l.BackColor = Color.BurlyWood;
            //        }

            //        foreach (Stone stone in stones)
            //        {
                        
            //            if(stone.x*100==x && stone.y*100 == y && stone.board == board)
            //            {
            //                if (stone.side == 1)
            //                {
            //                    l.Text = "X";
            //                }
            //                else
            //                {
            //                    l.Text = "0";
            //                }

            //            }
            //        }

            //        panelx.Controls.Add(l);
            //        x += 100;
            //    }
            //    x = 0;
            //    y += 100;
            //}
        }

        public void initStones()
        {
            //stones = new List<Stone>();
            
            
            //for(int indexBoard = 1; indexBoard<=4; indexBoard++)
            //{
            //    for(int y=0; y<4; y++)
            //    {
            //        if (y == 0)
            //        {
            //            for(int x = 0; x < 4; x++)
            //            {
            //                stones.Add(new Stone(indexBoard, x, y, 1));
            //            }
            //        }
            //        else if (y == 3)
            //        {
            //            for (int x = 0; x < 4; x++)
            //            {
            //                stones.Add(new Stone(indexBoard, x, y, 0));
            //            }
            //        }
            //    }
            //}
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnsubmitmove_Click(object sender, EventArgs e)
        {
            //int xstart = Int32.Parse(tbX.Text);
            //int ystart = Int32.Parse(tbY.Text);
            //int xend = Int32.Parse(tbXX.Text);
            //int yend = Int32.Parse(tbYY.Text);
            //Console.WriteLine("test "+xstart);
        }
    }
}
