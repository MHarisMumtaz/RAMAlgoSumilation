using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class startform : Form
    {
        public startform()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                ProcessQueue.algo = "FirstFit";
            }
            else if (radioButton2.Checked)
            {
                ProcessQueue.algo = "BestFit";
            }
            else
            {
                ProcessQueue.algo = "NextFit";
            }
            this.Hide();
            form1 f1 = new form1();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Aboutus abt = new Aboutus();
            abt.Show();
        }
    }
}
