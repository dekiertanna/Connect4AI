using Connect4;
using System;
using System.Collections.Generic;

public class BNode
{

    private Board board;
    private int depth=0;
    int threshold;
    private char symbolToPlay;
    private char opponentSymbol;
    private char headSymbol;
    private char headOpponent;
    private bool pruningEnabled;
    private bool heuristicEnabled;
    private ObjectCounter counter;

    public BNode(Board board, int depth, int threshold, char symbol, char headSymbol, bool pruningEnabled,ObjectCounter counter,bool heuristicEnabled)
    {
        this.board = board;
        
        this.depth = depth;
        this.threshold = threshold;
        this.headSymbol = headSymbol;
        if(headSymbol=='1')
        {
            headOpponent = '2';
        }
        else
        {
            headOpponent = '1';
        }
        this.symbolToPlay = symbol;
        if (symbolToPlay == '1')
        {
            opponentSymbol = '2';
        }
        else
        {
            opponentSymbol = '1';
        }

        this.pruningEnabled = pruningEnabled;
        this.heuristicEnabled = heuristicEnabled;
        this.counter = counter;
        counter.increment();
    }

    public int evaluate(int currentParentEvaluation)
    {
        if(board.isWon(headSymbol))
        {
            return Int32.MaxValue;
        }
        if(board.isWon(headOpponent))
        {
            return Int32.MinValue;
        }

        if (depth == threshold) 
        {
            return heuristicEvaluation();
        }
        List<BNode> children = getChild();

        int evaluation = 0;
        //MAX
        if(depth%2==0)
        {
            evaluation = Int32.MinValue;
            foreach(BNode child in children)
            {
                int value = child.evaluate(evaluation);
                if (value > evaluation)
                {
                    evaluation = value;
                }
                if(pruningEnabled && evaluation >currentParentEvaluation)
                {
                    break;
                }
            }
        }

        else
        {
            //MIN
            evaluation = Int32.MaxValue;
            foreach(BNode child in children)
            {
                int value = child.evaluate(evaluation);
                if(value<evaluation)
                {
                    evaluation = value;
                }
                if(pruningEnabled && evaluation < currentParentEvaluation)
                {
                    break;
                }
            }
        }
        Console.WriteLine(evaluation);
        return evaluation;
    }

    //Heurystyka: trzy w rzedzie i 2 w rzedzie

    private int heuristicEvaluation()
    {
        if(board.isWon(headSymbol))
        {
            return Int32.MaxValue;
        }
        if(board.isWon(headOpponent))
        {
            return Int32.MinValue;
        }

        int threepos = 100 * board.symInARow(headSymbol, headOpponent, 3);
        int threeneg = -100 * board.symInARow(headOpponent, headSymbol, 3);
        int twopos = 10 * board.symInARow(headSymbol, headOpponent, 2);
        int twoneg = -10 * board.symInARow(headOpponent, headSymbol, 2);
        return threepos + threeneg + twopos + twoneg;
    }

    public List<BNode> getChild()
    {
        List<BNode> children = new List<BNode>();
        for(int i=0;i<7;i++)
        {
            if(board.dropValidity(i) == 2)
            {
                Board child = board.copy();
                child.DropDisk(symbolToPlay, i,board.f);
                BNode childNode = new BNode(child, depth + 1,threshold, opponentSymbol, headSymbol, pruningEnabled, counter,heuristicEnabled);
                children.Add(childNode);
            }
        }
        return children;
    }

    public Board getBoard()
    {
        return board;
    }
	
}
