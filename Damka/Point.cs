using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damka
{
    internal class Point
    {
        public static readonly Point Empty;

        private int x;
        private int y;
        private string pointAsString;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
            pointAsString = convertIntToString(x, y);
        }



        public Point() { }

        public Point convertStringToPoint(string input)
        {
            Point newPoint;
            int x, y;
            x = input[0] - 'a';
            y = input[1] - 'A';
            newPoint = new Point(x, y);
            return newPoint;
        }

        public string convertPointToString(Point point)
        {
            char row, col;
            row = (char)(point.x + 'a');
            col = (char)(point.y + 'A');
            return row.ToString() + col.ToString();
        }

        public string convertIntToString(int x,int y)
        {
            char row, col;
            row = (char)(x + 'a');
            col = (char)(y + 'A');
            return row.ToString() + col.ToString();
        }


        public bool IsEmpty
        {
            get
            {
                if (x == 0)
                {
                    return y == 0;
                }

                return false;
            }
        }

      
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

       
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }


       
    }
}
