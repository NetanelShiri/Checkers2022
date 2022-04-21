using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damka
{
    internal class Menu
    {

        public void initiate(ref Board board, ref List<Player> players )
        {
            string boardSize, name;
            bool validSize = false;
            bool validName = false;
            byte i = 1;
            int size;
        
            while (!validSize)
            {
                Console.WriteLine("Enter Board Size:");
                boardSize = Console.ReadLine();
                if (boardSize == "6" || boardSize == "8" || boardSize == "10")
                {
                    validSize = true;
                    Int32.TryParse(boardSize, out size);
                    board = new Board(size);
                }
                else
                    Console.WriteLine("Invalid size, please insert valid size.");
            }

            while (!validName)
            {
                Console.WriteLine("Enter Player " + i + " Name:");
                name = Console.ReadLine();
                if(name.Length > 20)
                {
                    continue;
                }   
                players.Add(new Player(name, i));
                if(i == 2)
                {
                    validName = true;
                    Console.WriteLine("Invalid name, please insert valid name.");

                }
                i++;
            }

            foreach (Player player in players)
            {
                player.createSoldiers(board.BoardSize);
                board.placePlayerOnBoard(player);
            }
        }

        public string getInputFromPlayer(Player currPlayer , Board board)
        {
            string input = "";
            bool isValid = false;
            string left, right;

            while (!isValid)
            {
                Console.Write(currPlayer.Name + "'s Turn : ");
                input = Console.ReadLine();
                if(isValidInput(input) && isInBorders(input, board.BoardSize))
                {
                    left = input.Substring(0, 2);
                    right = input.Substring(3, 2);
                    currPlayer.setMove(left, right);
                    if (currPlayer.Move.isLegalMovement(board, currPlayer))
                    {
                        isValid = true;
                    }
                }
                
            }
            return input;
        }

        public bool isValidInput(string input)
        {
            bool isValid = false;
            if (input.Length == 5)
            {
                if(input[2] == '>'
                    && (input [0] >= 'a' && input[0] <= 'z') && (input[1] >= 'A' && input[1] <= 'Z') &&
                    (input[3] >= 'a' && input[3] <= 'z') && (input[4] >= 'A' && input[4] <= 'Z'))
                {
                    isValid = true;
                }
                
            }

            return isValid;      
        }

        public bool isInBorders(string input,int boardSize)
        {
            bool isValid = false;
            Point p1 = new Point();
            Point p2 = new Point();
            p1 = p1.convertStringToPoint(input.Substring(0, 2));
            p2 = p2.convertStringToPoint(input.Substring(3, 2));

            if (p1.X < boardSize && p1.Y < boardSize && p2.X < boardSize && p2.Y < boardSize)
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
