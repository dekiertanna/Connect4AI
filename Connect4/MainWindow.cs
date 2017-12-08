using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class MainWindow : Form
    {
        private ConnectFour c4;
        private string player1Type;
        private string player2Type;
        private bool p1heuristicEnabled=false;
        private bool p2heuristicEnabled = false;
        private int p1depth=0;
        private int p2depth = 0;
        private string userInput;
        public TaskCompletionSource<bool> tcs;

        public MainWindow()
        {
            InitializeComponent();
            tcs = new TaskCompletionSource<bool>();
            Shown += MainWindow_Loaded;
            player1Combo.Items.Add("Human");
            player1Combo.Items.Add("AI-minimax");
            player1Combo.Items.Add("AI-AlphaBeta");

            player2Combo.Items.Add("Human");
            player2Combo.Items.Add("AI-minimax");
            player2Combo.Items.Add("AI-AlphaBeta");

            player1ComboDepth.Items.Add(1);
            player1ComboDepth.Items.Add(2);
            player1ComboDepth.Items.Add(3);
            player1ComboDepth.Items.Add(4);
            player1ComboDepth.Items.Add(5);
            player1ComboDepth.Items.Add(6);
            player1ComboDepth.Items.Add(7);
            player1ComboDepth.Items.Add(8);
            player1ComboDepth.Items.Add(9);
            player1ComboDepth.Items.Add(10);

            player2ComboDepth.Items.Add(1);
            player2ComboDepth.Items.Add(2);
            player2ComboDepth.Items.Add(3);
            player2ComboDepth.Items.Add(4);
            player2ComboDepth.Items.Add(5);
            player2ComboDepth.Items.Add(6);
            player2ComboDepth.Items.Add(7);
            player2ComboDepth.Items.Add(8);
            player2ComboDepth.Items.Add(9);
            player2ComboDepth.Items.Add(10);
            userInput = "wait";

        }

        private void MainWindow_Loaded(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            c4 = new ConnectFour(this,player1Type,player2Type,p1heuristicEnabled,p2heuristicEnabled,p1depth,p2depth);
             c4.playAsync();
        }

        private void player1Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.player1Type = player1Combo.SelectedItem.ToString();
            Console.WriteLine(player1Type);
            if (player1Type=="Human")
            {
                player1ComboDepth.Enabled = false;
                
            }
            else
            {
                player1ComboDepth.Enabled = true;
                
            }

        }

        private void player2Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.player2Type = player2Combo.SelectedItem.ToString();
            Console.WriteLine(player2Type);
            if (player2Type == "Human")
            {
                player2ComboDepth.Enabled = false;
               
            }
            else
            {
                player2ComboDepth.Enabled = true;
                
            }
            
        }

        private void player1ComboDepth_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.p1depth =(int) player1ComboDepth.SelectedItem;
        }

        private void player2ComboDepth_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.p2depth = (int)player2ComboDepth.SelectedItem;
            
        }

        

        

        private void btn1_Click(object sender, EventArgs e)
        {
            userInput = "1";
            tcs.SetResult(false);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            userInput = "2";
            tcs.SetResult(false);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            userInput = "3";
            tcs.SetResult(false);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            userInput = "4";
            tcs.SetResult(false);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            userInput = "5";
            tcs.SetResult(false);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            userInput = "6";
            tcs.SetResult(false);

        }

        private void btn7_Click(object sender, EventArgs e)
        {
            userInput = "7";
            tcs.SetResult(false);

        }

        public string getInput()
        {
            return userInput;
        }
    }
}
