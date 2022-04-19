using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damka
{
    internal class main
    {
        static void Main()
        {


            Console.WriteLine("Enter Board Size:");
            string input = Console.ReadLine();
            int number;
            Int32.TryParse(input, out number);
            Board board = new Board(number);
            Player player1 = new Player("yossi", 1);
            Player player2 = new Player("nati", 2);

            player1.PlayerSign = Properties.eView.Player1;
            player2.PlayerSign = Properties.eView.Player2;

            player1.createSoldiers(number);
            player2.createSoldiers(number);

            board.placePlayerOnBoard(player1);
            board.placePlayerOnBoard(player2);

            string left, right;

            //player1.setMove("fG", "eH");


            board.printBoard();

             input = Console.ReadLine();
            left = input.Substring(0,2);
            right = input.Substring(3,2);
            player1.setMove(left, right);

            if (player1.Move.isLegalMovement(board,player1))
            {
                player1.Move.setMovement(board);
            }
            System.Threading.Thread.Sleep(3000);
            Ex02.ConsoleUtils.Screen.Clear();

            board.printBoard();



        }
    }
}