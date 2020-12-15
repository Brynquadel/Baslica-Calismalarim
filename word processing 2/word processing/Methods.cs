using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace word_processing
{
    class Methods
    {


        public static string TypeValue(string subline)
        {

            int value = 0; string returnvalue = null;
            if(subline.IndexOf("[TEXT]") != -1)
            {
                value++; returnvalue = "[TEXT]";
            }
            if (subline.IndexOf("[CODE]") != -1)
            {
                value++; returnvalue = "[CODE]";
            }

            // # Control
            if(value > 1) { returnvalue = "ERROR"; return returnvalue; }
            else if(value == 0) { returnvalue = "NOLIST"; return returnvalue; }
            else
            {
                return returnvalue;
            }     

        }


    }
}
