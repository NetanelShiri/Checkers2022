using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damka
{
    internal class Movement
    {
        private Point m_From;
        private Point m_To;
        private Point m_Eaten;
        private string m_FromAsString, m_ToAsString; 
        private bool m_isSkipMove = false;

        public Movement(string from,string to)
        {
            m_FromAsString = from;
            m_ToAsString = to;
            m_From = new Point();
            m_From = m_From.convertStringToPoint(from);
            m_To = m_From.convertStringToPoint(to);

        }

        public bool IsSkipMove
        {
            get { return m_isSkipMove; }    
            set { m_isSkipMove = value; }

        }

        public Point From
        {
            get { return m_From; }
            set { m_From = value; }
        }

        public Point To
        {
            get { return m_To; }
            set { m_To = value; }
        }

        public bool isLegalMovement(Board i_Board, Player i_Player)
        {
            bool sherry = false;

            int row, col;
            char curr;
            int currPlayerNumber = i_Player.PlayerNumber;
            int row1=0, col1=0;
            row = m_From.X - m_To.X;
            col = m_From.Y - m_To.Y;
            Point point;

            if (i_Player.Soldiers.ContainsKey(m_FromAsString)) { 
                if (i_Board[m_To.X, m_To.Y] == (char)Properties.eView.None)
                {


                    if (Math.Abs(m_To.Y - m_From.Y) == 1)
                    {


                        if (row == 1 && currPlayerNumber == 1 || row == -1 && currPlayerNumber == 2)
                        {
                            sherry = true;

                        }
                        else
                            Console.WriteLine("nooooooooooooooo");
                    }
                    else if (Math.Abs(m_To.Y - m_From.Y) == 2)
                    {
                        Console.WriteLine("TRYING TO EAT");
                        row1 = currPlayerNumber == 1 ? (m_From.X-1) :(m_From.X+1);

                        if(row == 2 && col == 2)
                        {
                            col1 = m_From.Y-1; 
                        }
                        else if(row == 2 && col == -2)
                        {
                            col1 = m_From.Y+1;  
                        }
                        else if(row == -2 && col == 2)
                        {
                            col1 = m_From.Y-1;
                        }
                        else if (row == -2 && col == -2)
                        {
                            col1 = m_From.Y + 1;

                        }
                        curr = i_Board[row1, col1];
                        point = new Point(row1, col1);
                        if (currPlayerNumber == 1
                            && curr == (char)Properties.eView.Player2
                            || curr == (char)Properties.eView.Player2_King)
                        {
                            sherry = true;

                        }
                        else if (currPlayerNumber == 2
                            && curr == (char)Properties.eView.Player1
                            || curr == (char)Properties.eView.Player1_King)
                        {
                            sherry = true;

                        }
                        else
                        {
                            IsSkipMove = false;
                        }

                        IsSkipMove = true;
                        m_Eaten = point;

                    }


                }
                else
                    Console.WriteLine("nooooooooooooooo YAAAAAAAAAAAAAAAAAAA");






            }
            else { Console.WriteLine("No key : " + m_FromAsString); }
            return sherry;
        }


        public void setMovement(Board board,List<Player> players,Player player)
        {
            player.Soldiers.Remove(m_FromAsString);
            player.Soldiers.Add(m_ToAsString,new Soldier(m_ToAsString, player.PlayerSign));
            
            board[From.X,From.Y] = (char)Properties.eView.None;
            board[To.X,To.Y] = (char)player.PlayerSign;

            if (IsSkipMove)
            {
                foreach(Player currPlayer in players){
                    if (currPlayer != player)
                    {
                        currPlayer.Soldiers.Remove(m_Eaten.convertPointToString(m_Eaten));
                        board[m_Eaten.X, m_Eaten.Y] = (char)Properties.eView.None;
                    }
                }
            }

        }

        public Point Eaten
        {
            get
            {
                
                return m_Eaten;
            }
            set
            {
                Eaten = value;
            }
        }
    }
}
