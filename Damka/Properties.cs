using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damka
{
    internal class Properties
    {
        public enum eView
        {
            Player1 ='X',
            Player2 ='O',
            Player1_King = 'K',
            Player2_King = 'U',
            None = ' ',

        }

        public enum ePlayerNumber
        {
            Player1 = 1,
            Player2 = 2,
        }

    }
}
