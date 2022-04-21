using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damka
{
    internal class GamePlay
    {

        private Menu menu;
        private Board m_Board;
        private List<Player> m_Players = new List<Player>();
       
        public void run()
        {
            string input, left, right;
            Player currPlayer;
            menu = new Menu();
            menu.initiate(ref m_Board,ref m_Players);
            
            currPlayer = m_Players[0];
            while (true)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                m_Board.printBoard();
                input = menu.getInputFromPlayer(currPlayer, m_Board);
                currPlayer.Move.setMovement(m_Board,m_Players,currPlayer);
                currPlayer = currPlayer.PlayerNumber == 1 ? m_Players[1] : m_Players[0];

                System.Threading.Thread.Sleep(850);
             
              
            }
         
        }
    }
}
