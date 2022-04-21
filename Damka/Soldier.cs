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

        public Soldier(string location, Properties.eView sign )
        {
            this.m_location = location;
            this.i_soldierSign = sign;
        }
        
        public string location
        {
            get { return m_location; }
            set { m_location = value; }
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
