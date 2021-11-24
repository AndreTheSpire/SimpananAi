
namespace ProyekAI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tbX = new System.Windows.Forms.TextBox();
            this.tbY = new System.Windows.Forms.TextBox();
            this.cbBoard = new System.Windows.Forms.ComboBox();
            this.Board = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbYY = new System.Windows.Forms.TextBox();
            this.tbXX = new System.Windows.Forms.TextBox();
            this.btnsubmitmove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(24, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 492);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(580, 12);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(533, 492);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(24, 523);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(533, 492);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(580, 523);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(533, 492);
            this.panel4.TabIndex = 2;
            // 
            // tbX
            // 
            this.tbX.Location = new System.Drawing.Point(1222, 79);
            this.tbX.Margin = new System.Windows.Forms.Padding(4);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(255, 22);
            this.tbX.TabIndex = 3;
            // 
            // tbY
            // 
            this.tbY.Location = new System.Drawing.Point(1221, 109);
            this.tbY.Margin = new System.Windows.Forms.Padding(4);
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(256, 22);
            this.tbY.TabIndex = 4;
            // 
            // cbBoard
            // 
            this.cbBoard.FormattingEnabled = true;
            this.cbBoard.Location = new System.Drawing.Point(1222, 47);
            this.cbBoard.Margin = new System.Windows.Forms.Padding(4);
            this.cbBoard.Name = "cbBoard";
            this.cbBoard.Size = new System.Drawing.Size(255, 24);
            this.cbBoard.TabIndex = 5;
            // 
            // Board
            // 
            this.Board.AutoSize = true;
            this.Board.Location = new System.Drawing.Point(1134, 47);
            this.Board.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Board.Name = "Board";
            this.Board.Size = new System.Drawing.Size(54, 17);
            this.Board.TabIndex = 6;
            this.Board.Text = "Board :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1134, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "From X :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1134, 114);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "From Y :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1134, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Passive Move";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1134, 180);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "To Y :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1134, 148);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "To X :";
            // 
            // tbYY
            // 
            this.tbYY.Location = new System.Drawing.Point(1221, 175);
            this.tbYY.Margin = new System.Windows.Forms.Padding(4);
            this.tbYY.Name = "tbYY";
            this.tbYY.Size = new System.Drawing.Size(256, 22);
            this.tbYY.TabIndex = 11;
            // 
            // tbXX
            // 
            this.tbXX.Location = new System.Drawing.Point(1222, 145);
            this.tbXX.Margin = new System.Windows.Forms.Padding(4);
            this.tbXX.Name = "tbXX";
            this.tbXX.Size = new System.Drawing.Size(255, 22);
            this.tbXX.TabIndex = 10;
            // 
            // btnsubmitmove
            // 
            this.btnsubmitmove.Location = new System.Drawing.Point(1137, 214);
            this.btnsubmitmove.Name = "btnsubmitmove";
            this.btnsubmitmove.Size = new System.Drawing.Size(75, 23);
            this.btnsubmitmove.TabIndex = 14;
            this.btnsubmitmove.Text = "Submit";
            this.btnsubmitmove.UseVisualStyleBackColor = true;
            this.btnsubmitmove.Click += new System.EventHandler(this.btnsubmitmove_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1641, 1044);
            this.Controls.Add(this.btnsubmitmove);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbYY);
            this.Controls.Add(this.tbXX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Board);
            this.Controls.Add(this.cbBoard);
            this.Controls.Add(this.tbY);
            this.Controls.Add(this.tbX);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tbX;
        private System.Windows.Forms.TextBox tbY;
        private System.Windows.Forms.ComboBox cbBoard;
        private System.Windows.Forms.Label Board;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbYY;
        private System.Windows.Forms.TextBox tbXX;
        private System.Windows.Forms.Button btnsubmitmove;
    }
}

