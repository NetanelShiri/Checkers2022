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
        private bool m_isEatingMove = false;
        private bool m_MustEat = false;

        public Movement(string from,string to)
        {
            m_FromAsString = from;
            m_ToAsString = to;
            m_From = new Point();
            m_From = m_From.convertStringToPoint(from);
            m_To = m_From.convertStringToPoint(to);

        }

        public bool IsEatingMove
        {
            get { return m_isEatingMove; }    
            set { m_isEatingMove = value; }

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

        public bool isLegalMovement(Board i_Board, Player i_Player)
        {
            bool isLegal = false;

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
                            isLegal = true;

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
                            isLegal = true;

                        }
                        else if (currPlayerNumber == 2
                            && curr == (char)Properties.eView.Player1
                            || curr == (char)Properties.eView.Player1_King)
                        {
                            isLegal = true;

                        }
                        else
                        {
                            IsEatingMove = false;
                        }

                        IsEatingMove = true;
                        m_Eaten = point;

                    }

                    if (!IsEatingMove)
                    {
                        canPlayerEat(i_Player, i_Board);
                        if(m_MustEat) { isLegal = false; }
                    }
                }
                else
                    Console.WriteLine("nooooooooooooooo YAAAAAAAAAAAAAAAAAAA");






            }
            else { Console.WriteLine("No key : " + m_FromAsString); }

  
            return isLegal;
        }


        public void setMovement(Board board,List<Player> players,Player player)
        {
            player.Soldiers.Remove(m_FromAsString);
            player.Soldiers.Add(m_ToAsString,new Soldier(m_ToAsString, player.PlayerSign));
            
            board[From.X,From.Y] = (char)Properties.eView.None;
            board[To.X,To.Y] = (char)player.PlayerSign;

            if (IsEatingMove)
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

        public void canPlayerEat(Player player,Board board)
        {
            Dictionary<string, Soldier> playerSoldiers = player.Soldiers;
           // Dictionary<string, Soldier> potentialEaters = new Dictionary<string,Soldier>();
            Point point = new Point();
            Point diagonal1 = new Point() , diagonal2 = new Point();
            int upOrDown = player.PlayerNumber == 1 ? -1 : 1;
            int sideMove = 1;
            bool isKing;

        

            foreach (KeyValuePair<string, Soldier> entry in playerSoldiers)
            {
                   isKing = (entry.Value.level > 1) ? true : false;
                   point = point.convertStringToPoint(entry.Key);
                if (m_MustEat)
                {
                    break;
                }

                
                for (int i = 0; i < 2; i++)
                {
                    diagonal1 = new Point(point.X + upOrDown, point.Y - sideMove);
                    diagonal2 = new Point(point.X + upOrDown * 2, point.Y - sideMove * 2);
                    m_MustEat = checkIfDiagonalEatable(diagonal1, diagonal2, player, board);
                    if (m_MustEat) break;
                    sideMove *= -1;
                }

                //counter-wise
                if (isKing && !m_MustEat)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        diagonal1 = new Point(point.X - upOrDown, point.Y - sideMove);
                        diagonal2 = new Point(point.X - upOrDown * 2, point.Y - sideMove * 2);
                        m_MustEat = checkIfDiagonalEatable(diagonal1, diagonal2, player, board);
                        if (m_MustEat) break;
                        sideMove *= -1;
                    }
                }

            }
            
        }


        public bool checkIfDiagonalEatable(Point diagonal1,Point diagonal2,Player player ,Board board)
        {
            bool isEatable = false;
            char currSign;
            bool isEnemy, canJump;

            if (isPointInBorder(diagonal1, board.BoardSize) && isPointInBorder(diagonal2, board.BoardSize))
            {
                currSign = board[diagonal1.X, diagonal1.Y];
                isEnemy = (currSign != (char)player.PlayerSign && currSign != (char)player.PlayerSignKing
                    && currSign != (char)Properties.eView.None);

                canJump = board[diagonal2.X, diagonal2.Y] == (char)Properties.eView.None;
                
                if (isEnemy && canJump)
                {
                    isEatable = true;
                }
            }
            return isEatable;
        }
        public bool isPointInBorder(Point p,int boardSize) {
               
            return (p.X < boardSize && p.Y < boardSize && p.X > 0 && p.Y > 0);
        }
    }
}
