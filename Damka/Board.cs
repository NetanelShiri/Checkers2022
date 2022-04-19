using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damka
{
    internal class Board
    {

        private int m_BoardSize;
        private readonly char[,] board;
        
        public Board(int _boardSize)
        { 
            this.m_BoardSize = _boardSize;
            board = new char[m_BoardSize, m_BoardSize];
            initializeBoard();

        }

        public char this[int i_Row,int i_Col]
        {
            get
            {
                return board[i_Row,i_Col];
            }

            set
            {
                board[i_Row,i_Col] = value;
            }
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        
            
       
        //8-2 = 6 / 2 = 3
        //8/2 = 4           
        public void printBoard()
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if (board[i, j] != ' ')
                    {
                        Console.Write(board[i, j]);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write(Environment.NewLine);
            }

        }

        private void initializeBoard()
        {
            for(int i = 0; i < m_BoardSize; i++)
            {
                for ( int j = 0; j < m_BoardSize; j++)
                {
                    board[i,j] = (char)Properties.eView.None;
                }
            }
        }

        public void placePlayerOnBoard(Player i_Player)
        {

            Dictionary<string, Soldier> soldiers = i_Player.Soldiers;
            int row, col;
            string soldierLocation;
            char sign = (char)i_Player.PlayerSign;

            foreach (Soldier soldier in soldiers.Values)
            {
                soldierLocation = soldier.location;
                row = soldierLocation[0] - 'a';
                col = soldierLocation[1] - 'A';
                board[row,col] = sign;
            }
            
        }

       

    }
}
