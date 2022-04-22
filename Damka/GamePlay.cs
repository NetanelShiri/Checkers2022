using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damka
{
    internal class GamePlay
    {

        private Menu m_menu;
        private Board m_Board;
        private Player m_currPlayer;
        private List<Player> m_Players;
    

        public GamePlay()
        {
            m_menu = new Menu();
            m_Players = new List<Player>();
        }

        public void run()
        {
     
            m_menu.initiate(ref m_Board,ref m_Players);
            m_currPlayer = m_Players[0];    


            while (true)
            {
                Ex02.ConsoleUtils.Screen.Clear();

                m_Board.printBoard();
                m_menu.getInputFromPlayer(m_currPlayer, m_Board);
                m_currPlayer.Move.setMovement(m_Board, m_Players, m_currPlayer);
                getNextTurn();

                System.Threading.Thread.Sleep(850); 
            }
         
        }

        public void getNextTurn()
        {
            int playersAmount = m_Players.Count;
            int playerNumber = m_currPlayer.PlayerNumber;

            if(playersAmount == playerNumber)
            {
                m_currPlayer = m_Players[0];
            }
            else
            {
                m_currPlayer = m_Players[playerNumber];
            }
            
        }
    }
}
