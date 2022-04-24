using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damka
{

    
    internal class Soldier
    {
        
        private string m_location;
        private byte m_level = 1;
        private Properties.eView i_soldierSign;

        public Soldier(string location, Properties.eView sign,bool isKing)
        {
            this.m_location = location;
            this.i_soldierSign = sign;
            m_level = isKing ? (byte)2 : (byte)1;
        }
        
        public string location
        {
            get { return m_location; }
            set { m_location = value; }
        }

        public byte level
        {
            get { return m_level; }
            set { m_level = value; }
        }

        public Properties.eView soldierSign
        {
            get
            {
                return i_soldierSign;
            }

            set
            {
                i_soldierSign = value;
            }
        }

    }
}
