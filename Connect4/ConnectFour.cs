using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    class ConnectFour
    {
        private MainWindow f;
        
        private Board board;
        private PlayerSymbol player1, player2;
        string p1t, p2t = "";
        private int depth;
        private bool heuristicEnabled;
        public ConnectFour(MainWindow f,bool heuristicEnabled, int depth)
        {
            this.f = f;
            this.heuristicEnabled = heuristicEnabled;
            this.depth = depth;

            // newWindow.ShowDialog();
            board = new Board(f);
             player1 = new Human('1',f);
           // player1 = new AlphaBeta('1',f);
            player2 = new AlphaBeta('2',f,depth,heuristicEnabled);
            
        }

        public ConnectFour(MainWindow f, string player1Type, string player2Type,bool p1heuristicEnabled,bool p2heuristicEnabled,int depth1, int depth2)
        {
            this.f = f;
            board = new Board(f);
            if(player1Type=="Human")
            {
                player1 = new Human('1', f);
                p1t = "Human";
            }
            if(player1Type=="AI-minimax")
            {
                player1 = new MiniMax('1', f,true,depth1);
            }
            if(player1Type == "AI-AlphaBeta")
            {
                player1 = new AlphaBeta('1', f,depth1,true);
            }
            if (player2Type == "Human")
            {
                player2 = new Human('2', f);
                p2t = "Human";
            }
            if (player2Type == "AI-minimax")
            {
                player2 = new MiniMax('2', f,true,depth2);
            }
            if (player2Type == "AI-AlphaBeta")
            {
                player2 = new AlphaBeta('2', f,depth2,true);
            }
        }
        public MainWindow get()
        {
            return f;
        }

        public void set(MainWindow mw)
        {
            this.f = mw;
        }
        public void play()
        {
            ObjectCounter counter1 = new ObjectCounter();
            ObjectCounter counter2 = new ObjectCounter();
            board.printBoard();
            int prevCounter = 0;
            int p1Moves = 0;
            int p2Moves = 0;
            long p1Time = 0;
            long p2Time = 0;

            while (true)
            {
                if (board.isWon('2'))
                {
                    Label win = (Label)f.Controls.Find("WinLabel",true)[0];
                    win.Text = "Wygrał gracz 2";
                    win.ForeColor = Color.Yellow;
                    f.Refresh();
                    Console.WriteLine("Player 2 wins!");
                    break;
                }
                
                prevCounter = counter1.get();
                long startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                
                board = player1.makeMove(board, counter1,f);
                long endTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                p1Moves++;
                p1Time += (endTime - startTime);
                board.printBoard();
                if (board.isWon('1'))
                {
                    Label win = (Label)f.Controls.Find("WinLabel", true)[0];
                    win.Text = "Wygrał gracz 1";
                    win.ForeColor = Color.Red;
                    f.Refresh();
                    Console.WriteLine("Player 1 wins!");
                    break;
                }
                prevCounter = counter2.get();
                startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                board = player2.makeMove(board, counter2,f);
                endTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                p2Moves++;
                p2Time += (endTime - startTime);
                board.printBoard();

            }

        }

        public async Task playAsync()
        {
            ObjectCounter counter1 = new ObjectCounter();
            ObjectCounter counter2 = new ObjectCounter();
            board.printBoard();
            int prevCounter = 0;
            int p1Moves = 0;
            int p2Moves = 0;
            long p1Time = 0;
            long p2Time = 0;
            
            while (true)
            {
                if (board.isWon('2'))
                {
                    Label win = (Label)f.Controls.Find("WinLabel", true)[0];
                    win.Text = "Wygrał gracz 2";
                    win.ForeColor = Color.Yellow;
                    f.Refresh();
                    Console.WriteLine("Player 2 wins!");
                    break;
                }
                prevCounter = counter1.get();
                long startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                if (p1t == "Human")
                {
                    await f.tcs.Task;
                }
                
                board = player1.makeMove(board, counter1, f);
                f.tcs = new TaskCompletionSource<bool>();
                long endTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                p1Moves++;
                p1Time += (endTime - startTime);
                board.printBoard();
                if (board.isWon('1'))
                {
                    Label win = (Label)f.Controls.Find("WinLabel", true)[0];
                    win.Text = "Wygrał gracz 1";
                    win.ForeColor = Color.Red;
                    f.Refresh();
                    Console.WriteLine("Player 1 wins!");
                    break;
                }
                prevCounter = counter2.get();
                startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                if (p2t == "Human")
                {
                    await f.tcs.Task;
                }
                board = player2.makeMove(board, counter2, f);
                endTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                p2Moves++;
                p2Time += (endTime - startTime);
                board.printBoard();

            }

            Label time1 = (Label)f.Controls.Find("time1Label",true)[0];
            time1.Text = "Średni czasu ruchu: " + ((double)p1Time / (double)p1Moves) / 1000 + " sekund";
            Label nodes1 = (Label)f.Controls.Find("nodes1Label", true)[0];
            nodes1.Text = "Średnia liczba węzłow na ruch: " + counter1.get() / p1Moves;

            Label time2 = (Label)f.Controls.Find("time2Label", true)[0];
            time2.Text = "Średni czasu ruchu: " + ((double)p2Time / (double)p2Moves) / 1000 + " sekund";
            Label nodes2 = (Label)f.Controls.Find("nodes2Label", true)[0];
            nodes2.Text = "Średnia liczba węzłow na ruch: " + counter2.get() / p2Moves;
            f.Refresh();
        }
    }
}
