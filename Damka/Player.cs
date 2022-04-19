using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damka
{
    internal class Player
    {
        
        private string m_Name;
        private int m_Score;
        private byte m_PlayerNumber;
        private Properties.eView i_PlayerSign;
        private Movement m_movement;
        private Dictionary<string, Soldier> m_Soldiers;
        
        public Player(string m_name,byte m_playerNumber)
        {
            m_Name = m_name;
            m_PlayerNumber = m_playerNumber;
            m_Score = 0;
            m_Soldiers = new Dictionary<string, Soldier>();
        }

        public Properties.eView PlayerSign
        {
            get { return i_PlayerSign; }
            set { i_PlayerSign = value; }
        }

        public byte PlayerNumber
        {
            get { return m_PlayerNumber; }
            set { m_PlayerNumber = value; }
        }
        public Dictionary<string, Soldier> Soldiers
        {
            get
            {
                return m_Soldiers;
            }
            
        }

        public Movement Move
        {
            get
            {
                return m_movement;
            }
            
        }

        public void setMove(string from,string to)
        {
            m_movement = new Movement(from, to);
        }


        public void createSoldiers(int i_BoardSize)
        {
            int startLoop, endLoop;
            char row, col;
            string res;
           // Point p;
            startLoop = this.m_PlayerNumber == (int)Properties.ePlayerNumber.Player1 ? i_BoardSize-1 : (i_BoardSize / 2) -2;
            endLoop = this.m_PlayerNumber == (int)Properties.ePlayerNumber.Player1 ? (i_BoardSize / 2) + 1 : 0;

            for (int i = startLoop; i >= endLoop; i--)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    if ((i + j) % 2 != 0)
                    {
                        row = (char)(i + 'a');
                        col = (char)(j + 'A');
                       // p = new Point(i, j);
                        res = row.ToString() + col.ToString();
                        m_Soldiers.Add(res, new Soldier(res, i_PlayerSign));
                    }
                  
                }
            }

        }
    }
}
