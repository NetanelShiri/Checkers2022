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
        private bool m_MustEatAgain = false;

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
        public bool MustEatAgain
        {
            get { return m_MustEatAgain; }
            set { m_MustEatAgain = value; }
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
            int upOrDown;
            bool isKing = false;    
            row = m_From.X - m_To.X;
            col = m_From.Y - m_To.Y;
            Point point;

            if (i_Player.Soldiers.ContainsKey(m_FromAsString)) {

                if (i_Board[m_To.X, m_To.Y] == (char)Properties.eView.None)
                {
                    isKing = i_Player.Soldiers[m_FromAsString].level > 1;
                    upOrDown = m_To.X - m_From.X;

                    if (Math.Abs(upOrDown) == 1)
                    {



                        if (row == 1 && currPlayerNumber == 1 || row == -1 && currPlayerNumber == 2
                            || (Math.Abs(row) == 1 && isKing))
                        {
                            if (Math.Abs(col) == 1)
                            {
                                isLegal = true;
                            }

                        }
                        else
                            Console.WriteLine("nooooooooooooooo");
                    }
                    else if (Math.Abs(upOrDown) == 2 && Math.Abs(col) == 2)
                    {
                        Console.WriteLine("TRYING TO EAT");


                        if (upOrDown > 0)
                        {
                            row1 = m_From.X + 1;
                        }
                        else
                        {
                            row1 = m_From.X - 1;
                        }

                        if (row == 2 && col == 2)
                        {
                            col1 = m_From.Y - 1;
                        }
                        else if (row == 2 && col == -2)
                        {
                            col1 = m_From.Y + 1;
                        }
                        else if (row == -2 && col == 2)
                        {
                            col1 = m_From.Y - 1;
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
                            if (upOrDown > 0 && !isKing)
                            {
                                isLegal = false;
                            }

                            IsEatingMove = true;
                            m_Eaten = point;
                        }
                        else if (currPlayerNumber == 2
                            && curr == (char)Properties.eView.Player1
                            || curr == (char)Properties.eView.Player1_King)
                        {
                            isLegal = true;
                            if (upOrDown < 0 && !isKing)
                            {
                                isLegal = false;
                            }
                            IsEatingMove = true;
                            m_Eaten = point;
                        }
                        else
                        {
                            IsEatingMove = false;
                        }


                    }

                    if (!IsEatingMove && isLegal)
                    {
                        canPlayerEat(i_Player, i_Board);
                        if (m_MustEat) { isLegal = false; }
                    }
                }
                else
                {
                    Console.WriteLine("nooooooooooooooo YAAAAAAAAAAAAAAAAAAA");
                }



            }
            else { Console.WriteLine("No key : " + m_FromAsString); }

  
            return isLegal;
        }


        public void setMovement(Board board,List<Player> players,Player player)
        {
            bool isKing = false;
            byte soldierLevel;
            Properties.eView currSign;
            Soldier currSoldier = player.Soldiers[m_FromAsString];
            soldierLevel = currSoldier.level;
            currSign = currSoldier.soldierSign;

            player.Soldiers.Remove(m_FromAsString);
            if (soldierLevel <= 1)
            {
                isKing = player.checkIfKing(m_ToAsString,board.BoardSize);
                if (isKing) { currSign = player.PlayerSignKing; }
            }
            else
            {
                isKing = true;
            }
         

            player.Soldiers.Add(m_ToAsString, new Soldier(m_ToAsString, currSign, isKing));

            board[From.X,From.Y] = (char)Properties.eView.None;
            board[To.X,To.Y] = (char)currSign;

            if (IsEatingMove)
            {
                foreach(Player currPlayer in players){
                    if (currPlayer != player)
                    {
                        currPlayer.Soldiers.Remove(m_Eaten.convertPointToString(m_Eaten));
                        board[m_Eaten.X, m_Eaten.Y] = (char)Properties.eView.None;
                    }
                }
                canPlayerEat(player, board);
                m_MustEatAgain = m_MustEat;
            }


        }

        public void canPlayerEat(Player player,Board board)
        {
            Dictionary<string, Soldier> playerSoldiers = player.Soldiers;
            Point point = new Point();
            Point diagonal1 = new Point() , diagonal2 = new Point();
            int upOrDown = player.PlayerNumber == 1 ? -1 : 1;
            int sideMove = 1;
            bool isKing;
            m_MustEat = false;

        

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
               
            return (p.X < boardSize && p.Y < boardSize && p.X >= 0 && p.Y >= 0);
        }
    }
}
