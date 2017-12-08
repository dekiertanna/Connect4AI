
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Connect4
{
    public class Human : PlayerSymbol
    {
        private Form f;
      
        public Human(char symbol, Form f) : base(symbol)
        {
            this.f = f;
            
        }


        public override Board makeMove(Board board, ObjectCounter counter,MainWindow f)
        {
           

                Console.WriteLine("Next move 1-7");
                try
                {
                
                
                    int column = Int32.Parse(f.getInput());
                    column--;
                    int validity = board.dropValidity(column);
                    if (validity == 0)
                    {
                        Console.WriteLine("Invalid column");
                        //continue;
                    }
                    else if (validity == 1)
                    {
                        Console.WriteLine("Draw");
                       // break;
                    }
                    board.DropDisk(this.symbol, column, f);
                    //break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("error");
                }
            
            return board;
        }
    }

    public abstract class PlayerSymbol
    {
        protected char symbol;
        protected char symbolOppnent;
        public PlayerSymbol(char symbol)
        {
            this.symbol = symbol;
            if (this.symbol == '1')
            {
                symbolOppnent = '2';
            }
            else
                symbolOppnent = '1';
        }

        public abstract Board makeMove(Board board, ObjectCounter counter,MainWindow f);
    }

}