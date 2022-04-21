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

        public void printBoard()
        {
            StringBuilder sb = new StringBuilder();
            string seperator1 = "     ";
            string seperator2 = " ";
            string seperator3 = " | ";
            char upper_letter = 'A';
            char lower_letter = 'a';
            string betweenRows = "===";
            betweenRows += String.Concat(Enumerable.Repeat("=", (BoardSize) * 6));

            for (int i = 0; i < BoardSize; i++)
            {
                sb.Append(seperator1 + upper_letter);
                upper_letter++;
            }
            sb.Append(Environment.NewLine);
            sb.Append(betweenRows);
            sb.Append(Environment.NewLine);

            for (int i = 0; i < m_BoardSize; i++)
            {
                sb.Append(lower_letter + seperator3 + seperator2);
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if (board[i, j] != ' ')
                    {
                        sb.Append(board[i, j]);
                    }
                    else
                    {
                        sb.Append(seperator2);
                    }
                    sb.Append(" " + seperator3 + " ");
                }
                sb.Append(Environment.NewLine + betweenRows + Environment.NewLine);

                lower_letter++;
            }
            sb.Append(Environment.NewLine);
            Console.Write(sb);
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
