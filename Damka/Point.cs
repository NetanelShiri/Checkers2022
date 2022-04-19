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

      
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point() { }
       
       
    }
}
