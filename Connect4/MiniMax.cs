using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public class MiniMax : PlayerSymbol
        
    {
        private Form f;
        private int depth;
        private bool heuristicEnabled;
        public MiniMax(char symbol,Form f,bool heuristicEnabled,int depth):base(symbol)
        {
            this.f = f;
            this.depth = depth;
            this.heuristicEnabled = heuristicEnabled;
        }
        public override Board makeMove(Board board, ObjectCounter counter,MainWindow f)
        {
            BNode bNode = new BNode(board, 0,depth, symbol, symbol, false, counter,heuristicEnabled);
            List<BNode> nodes = bNode.getChild();
            int maximumEval = Int32.MinValue;
            int maxIndex = 0;
            for(int i = 0;i<nodes.Count;i++)
            {
                int evaluation = nodes.ElementAt(i).evaluate(Int32.MinValue);
                if(evaluation>maximumEval)
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

