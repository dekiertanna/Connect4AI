
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Connect4
{
   public class AlphaBeta : PlayerSymbol
    {
        private MainWindow f;
        private bool heuristicEnabled;
        private int depth;
        public AlphaBeta(char symbol, MainWindow f, int depth,bool heuristicEnabled):base(symbol)
        {
            this.f = f;
            this.heuristicEnabled = heuristicEnabled;
            this.depth = depth;
        }


        public override Board makeMove(Board board, ObjectCounter counter,MainWindow f)
        {
            BNode bNode = new BNode(board, 0,depth, symbol, symbol, true, counter,heuristicEnabled);
            List<BNode> nodes = bNode.getChild();
            int maximumEval = Int32.MinValue;
            int maxIndex = 0;
            for (int i = 0; i < nodes.Count; i++)
            {
                int evaluation = nodes.ElementAt(i).evaluate(Int32.MinValue);
                if (evaluation > maximumEval)
                {
                    maximumEval = evaluation;
                    maxIndex = i;
                }
            }

            board = nodes.ElementAt(maxIndex).getBoard();
            return board;
        }
    }
}