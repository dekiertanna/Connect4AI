
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Connect4
{
   public class Board : ICloneable
    {

        char[,] cells;
        int height;
        int width;
        bool[] col;
        public MainWindow f;

        public Board(MainWindow f)
        {
            this.f = f;
            height = 6;
            width = 7;

            cells = new char[height, width];
            col = new bool[width];

            for(int i=0;i<width;i++)
            {
                col[i] = false;
            }

            for(int i = 0;i<height;i++)
            {
                for(int j =0;j<width;j++)
                {
                    cells[i, j] = '.';

                    PictureBox pb = (PictureBox)this.f.Controls.Find("p" + i + j, true)[0];
                    pb.Image = Image.FromFile("Empty.png");
                }
            }

            

            
            
           
        }

        public Board(char[,] cells,int height, int width, MainWindow f)
        {
            this.cells = cells;
            this.height = height;
            this.width = width;
            this.f = f;
        }

        public int dropValidity(int column)
        {
            if(cells[height-1,column] !='.')
            {
                return 0;
            }
            if(cells[height-1,0] != '.' && cells[height-1,1]!='.' && cells[height-1,2]!='.' && cells[height-1,3]!='.' && cells[height - 1, 4] != '.' && cells[height - 1, 5] != '.' && cells[height - 1, 6] != '.')
            {
                return 1;
            }
            return 2;
        }

        public bool DropDisk(char symbol,int column,Form f)
        {
            if(dropValidity(column) !=2)
            {
                return false;
            }
            int i = 0;
            while(cells[i,column]!='.')
            {
                i++;
            }
            cells[i,column] = symbol;
            
            return true;
        }

        public void printBoard()
        {
            for(int i =0;i<height;i++)
            {
                for(int j=0;j<width;j++)
                {
                    
                    PictureBox pb = (PictureBox)this.f.Controls.Find("p" + (5-i) + j, true)[0];
                    if(cells[i,j] == '1')
                    {
                        
                        pb.Image = Image.FromFile("Player1.png");
                        f.Refresh();
                        
                    }
                    if (cells[i, j] == '2')
                    {

                        pb.Image = Image.FromFile("Player2.png");
                        f.Refresh();
                        
                    }
                }
            }

            for(int i=height-1;i>=0;i--)
            {
                for(int j=0;j<width;j++)
                {
                    Console.Write("|" + cells[i,j]);
                }
                Console.WriteLine("|");
            }
            for(int j = 0;j<(2*width)+1;j++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
            for(int j=0;j<width;j++)
            {
                Console.Write("|" + (j + 1));
            }
            Console.WriteLine("|");
            for(int j=0; j<(2*width)+1;j++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }

        public bool isWon(char symbol)
        {
            //row
            for(int i =0;i<height;i++)
            {
                for(int j =0; j<width-3;j++)
                {
                    if (cells[i,j] == symbol)
                    {
                        if(cells[i,j+1]==symbol && cells[i,j+2]==symbol && cells[i,j+3] == symbol)
                        {
                            return true;
                        }
                    }
                }
            }

            //column
            for(int j=0;j<width;j++)
            {
                for(int i=0;i<height-3;i++)
                {
                    if(cells[i,j] == symbol)
                    {
                        if(cells[i+1,j]==symbol && cells[i+2,j] == symbol && cells[i+3,j] ==symbol)
                        {
                            return true;
                        }
                    }
                }
            }

            //forward diagonal
            for(int i=0;i<height-3;i++)
            {
                for(int j=0;j<width-3;j++)
                {
                    if(cells[i,j] ==symbol)
                    {
                        if(cells[i+1,j+1] == symbol && cells[i+2,j+2] == symbol && cells[i+3,j+3] == symbol)
                        {
                            return true;
                        }
                    }
                }
            }

            //backward diagonal
            for(int i=height-1;i>=3;i--)
            {
                for(int j=0;j<width-3;j++)
                {
                    if(cells[i,j] == symbol)
                    {
                        if(cells[i-1,j+1] == symbol && cells[i-2,j+2] ==symbol && cells[i-3,j+3] == symbol)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public int symInARow(char symbol, char opponentSymbol,int n)
        {
            int count = 0;

            //row wise
            bool stop = false;
            for(int i=0;i<height;i++)
            {
                for(int j=0; j<width-3;j++)
                {
                    if(cells[i,j] ==symbol)
                    {
                        int sCount = 1;
                        for(int k=1;k<4; k++)
                        {
                            if(cells[i,j+k] == opponentSymbol)
                            {
                                sCount = 0;
                                break;
                            }
                            if(cells[i,j+k] ==symbol)
                            {
                                sCount++;
                            }
                        }
                        if(sCount==n)
                        {
                            count++;
                        }
                    }
                }
            }
            stop = false;

            //row wise back
            for(int i=0;i<height;i++)
            {
                for(int j=3;j<width;j++)
                {
                    if(cells[i,j] == symbol)
                    {
                        int sCount = 1;
                        for(int k=1;k<4;k++)
                        {
                            if(cells[i,j-k] == opponentSymbol)
                            {
                                sCount = 0;
                                break;
                            }
                            if(cells[i,j-k] == symbol)
                            {
                                sCount++;
                            }
                        }
                        if(sCount == n)
                        {
                            count++;
                        }
                    }
                }
            }

            //column wise
            for(int j=0;j<width;j++)
            {
                for(int i =0;i<height-3;i++)
                {
                    if(cells[i,j] ==symbol)
                    {
                        int sCount = 1;
                        for(int k=1;k<4;k++)
                        {
                            if(cells[i+k,j] == opponentSymbol)
                            {
                                sCount = 0;
                                break;
                            }

                            if(cells[i+k,j] == symbol)
                            {
                                sCount++;
                            }
                        }
                        if(sCount==n)
                        {
                            count++;
                        }
                    }
                }
            }

            stop = false;

            //forward diagonal
            for(int i=0;i<height-3;i++)
            {
                for(int j =0;j<width-3;j++)
                {
                    if(cells[i,j] ==symbol)
                    {
                        int sCount = 1;
                        for(int k=1; k<4;k++)
                        {
                            if(cells[i+k,j+k] == opponentSymbol)
                            {
                                sCount = 0;
                                break;
                            }
                            if(cells[i+k,j+k] == symbol)
                            {
                                sCount++;
                            }
                        }
                        if(sCount==n)
                        {
                            count++;
                        }
                    }
                }
            }

            stop = false;

            //backward diagonal

            for(int i =height-1;i>=3;i--)
            {
                for(int j=0;j<width-3;j++)
                {
                    if(cells[i,j] == symbol)
                    {
                        int sCount = 1;
                        for(int k=1;k<4;k++)
                        {
                            if(cells[i-k,j+k] ==opponentSymbol)
                            {
                                sCount = 0;
                                break;
                            }

                            if(cells[i-k,j+k] == symbol)
                            {
                                sCount++;
                            }
                        }
                        if(sCount == n)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        public void setValue(int i, int j, char c)
        {
            cells[i, j] = c;
            

        }

        
        public object Clone()
        {
            return (Board)base.MemberwiseClone();
        }

        public Board copy()
        {
            char[,] newCells = new char[height, width];
            for(int i=0;i<height;i++)
            {
                for(int j = 0;j<width;j++)
                {
                    newCells[i, j] = cells[i, j];
                }
            }

            Board b = new Board(newCells, height, width,f);
            return b;
        }

       
        
    }

   public class ObjectCounter
    {
        int n;
        public ObjectCounter()
        {
            n = 0;
        }
        public void increment()
        {
            n++;
        }

        public int get()
        {
            return n;
        }
    }
}