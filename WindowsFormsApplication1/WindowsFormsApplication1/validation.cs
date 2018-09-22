using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class validation
    {
        public static void validate(KeyPressEventArgs e)
        {
            int asci = Convert.ToInt16(e.KeyChar);
            if (asci >= 48 && asci <= 57 || asci == 8) { }
            else
                e.Handled = true;
        }//end of validation
    }
}
