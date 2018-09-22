using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class form1 : Form
    {
        public form1()
        {
            InitializeComponent();
        }

        ProcessQueue process_Queue;
        int input_size;
        Memory memory;

        private void new_Process_button(object sender, EventArgs e)
        {
            input_size = Convert.ToInt16(textBox1.Text);
            process_Queue.Check_memory();
            if (process_Queue.memory_status == true)
            {
                MessageBox.Show("Memory has been filled.", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                label13.Text = process_Queue.Get_Fragment.ToString() + "%";
                mem_label.Text = process_Queue.Get_waste.ToString() + " KB";
            }
            else if (input_size > 500)
            {
                MessageBox.Show("There is not Enough Memory for this Process.", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                process_Queue.Add_Process(input_size);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            process_Queue.Step2(timer1, timer3, timer4, arrow);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            process_Queue.Step1(timer1, pictureBox3);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            process_Queue.Step3(timer3, timer4, pictureBox4);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            memory = new Memory();
            process_Queue = new ProcessQueue(this.Controls, memory,listView1);
            process_Queue.que_colidebox = pictureBox3;
            process_Queue.trash = trash_box;
            process_Queue.timer2 = timer2;
            process_Queue.timer6 = timer6;
            process_Queue.timer7 = timer7;
            process_Queue.timer5 = timer5;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            process_Queue.Step4(timer4, arrow);
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            process_Queue.Step5(timer1, arrow, arrow_colideBox);
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            process_Queue.Step_next_2(timer1, arrow, pictureBox3, arrow_colideBox,pictureBox1);
        }//end of timer6

        private void timer7_Tick(object sender, EventArgs e)
        {
            process_Queue.go_InTrash(trash1_collidebox, timer1);
        }//end of timer 7

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            validation.validate(e);
        }
    }
}