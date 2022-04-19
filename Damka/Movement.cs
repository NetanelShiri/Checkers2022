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
        private string m_FromAsString, m_ToAsString; 
      //  private Point m_Eaten;
        private bool m_isSkipMove = false;

        public Movement(string from,string to)
        {
            m_FromAsString = from;
            m_ToAsString = to;
            m_From = ConvertToPoint(from);
            m_To = ConvertToPoint(to);

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


        public Point ConvertToPoint(string input)
        {
            Point newPoint;
            int x, y;
            x = input[0] - 'a';
            y = input[1] - 'A';
            newPoint = new Point(x, y);
            return newPoint;
        }

        public bool isLegalMovement(Board i_Board, Player i_Player)
        {
            bool sherry = false;

            int row, col;
            row = m_From.X - m_To.X;
            col = m_To.Y - m_From.Y;


            if (i_Player.Soldiers.ContainsKey(m_FromAsString)) { 
                if (i_Board[m_To.X, m_To.Y] == (char)Properties.eView.None)
                {


                    if (Math.Abs(m_To.Y - m_From.Y) == 1)
                    {


                        if (row == 1 && i_Player.PlayerNumber == 1 || row == -1 && i_Player.PlayerNumber == 2)
                        {
                            sherry = true;

                        }
                        else
                            Console.WriteLine("nooooooooooooooo");
                    }
                    else if (Math.Abs(m_To.Y - m_From.Y) == 2)
                    {

                    }


                }
                else
                    Console.WriteLine("nooooooooooooooo YAAAAAAAAAAAAAAAAAAA");






            }
            return sherry;
        }


        public void setMovement(Board board)
        {

            board[From.X,From.Y] = (char)Properties.eView.None;
            board[To.X,To.Y] = (char)Properties.eView.Player1;
        }
    }
}
