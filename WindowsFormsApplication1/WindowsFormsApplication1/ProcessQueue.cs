using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    class ProcessQueue
    {
        public Queue<Process> Que;
        Control.ControlCollection control;
        Process process, process_copy;
        Memory memory;
        ListView listview1;
        public Timer timer6, timer2, timer7, timer5;
        int count, index, waste;
        double fragment;
        bool check, que_check, process_check, trash_check, Memory_Status;
        public static string algo;
        public int ProcessID;
        double totaltime, timetick;
        public PictureBox que_colidebox, trash;

        public ProcessQueue(Control.ControlCollection control,Memory memory,ListView listview1)
        {
            this.ProcessID = 0;
            this.listview1 = listview1;
            this.memory = memory;
            this.control = control;
            this.count = this.index = 1;
            this.check = this.que_check = this.process_check = this.trash_check = this.Memory_Status = false;
            Que = new Queue<Process>();
        }//end of constructor

        public double Get_Fragment
        {
            get
            {
                double frag = 1695 - fragment;
                frag = frag / 1695 * 100;
                frag = Math.Ceiling(frag);
                return frag;
            }
        }//end of getting fragmentation

        public double Get_waste
        {
            get
            {
                int wast = 1695 - waste;
                return wast;
            }
        }//end of geting waste memory

        public bool memory_status
        {
            get
            {
                return Memory_Status;
            }
        }//end of getting memory status property

        public void Check_memory()
        {
            if (algo.ToLower()=="firstfit")
            {
                for (int i = 0; i < 9; i++)
                {
                    if (memory.Other_block[i, 3] == 0)
                    {
                        Memory_Status = false;
                        break;
                    }
                    else
                    {
                        Memory_Status = true;
                    }
                }
            }
            else if (algo.ToLower() == "bestfit")
            {
                for (int i = 0; i < 9; i++)
                {
                    if (memory.bestfit_block[i, 3] == 0)
                    {
                        Memory_Status = false;
                        break;
                    }
                    else
                    {
                        Memory_Status = true;
                    }
                }
            }
            else
            {
                if (memory.Other_block[8, 3] == 1)
                {
                    Memory_Status = true;
                }
            }
        }//end of memory checking

        public void Add_Process(int input_size)
        {
            try
            {
                if (timer7.Enabled)
                {
                    MessageBox.Show("please wait while process is going in trash.");
                }
                else if (process.Process_Image.Bounds.IntersectsWith(que_colidebox.Bounds))
                {
                    adding_process(input_size);  
                }
                else if (Que.Count==0)
                {
                    adding_process(input_size);
                }
                else
                {
                    MessageBox.Show("please wait while process is going in queue.");
                }
            }
            catch
            {
                adding_process(input_size);
            }
        }//end of process creation method

        void adding_process(int input_size)
        {
            timetick = 0;
            ListViewItem listitm = new ListViewItem();
            listitm.Text = ProcessID.ToString();
            listitm.SubItems.Add(input_size.ToString());
            listitm.SubItems.Add("waiting");
            listitm.SubItems.Add("");
            listitm.SubItems.Add("");
            listview1.Items.Add(listitm);
            process = new Process(input_size, memory, algo, ProcessID);
            process.Create_Process(this.control);
            if (process.Check)
            {
                fragment = fragment + input_size;
                waste = waste + input_size;
            }
            Que.Enqueue(process);
            ProcessID++;
            Start_Processing();
        }//end of adding process

        void Start_Processing()
        {
            if (process_check)
            {
                this.timer6.Start();
            }
            else
            {
                process_check = true;
                this.timer2.Start();
            }
        }//end of start processing

        public void Step1(Timer step2_timer, PictureBox colide_box)
        {
            if (check == false)
            {
                process_copy = Que.Dequeue();
                check = true;
            }
            if (!process_copy.Process_Image.Bounds.IntersectsWith(colide_box.Bounds))
            {
                Move_processImage(process_copy.Process_Image, 10, 0);
            }
            else
            {
                colide_box.Location = new Point(process_copy.Process_Image.Location.X + 2, process_copy.Process_Image.Location.Y);
                if (!process_copy.Check)
                {
                    this.timer7.Start();
                }
                else
                {
                    listview1.Items[process_copy.ID].SubItems[2].Text = "Search";
                    step2_timer.Start();
                }
                this.timer2.Stop();
            }
        }//end of going in queue method

        public void Step2(Timer step2_timer, Timer step3_timer,Timer step4_timer, PictureBox arrow)
        {
            calculate_time();
            listview1.Items[process_copy.ID].SubItems[3].Text = totaltime.ToString();
           
            if (algo.ToLower()=="bestfit")
            {
                if (count <= 140)
                {
                    Move_processImage(arrow, 0, 4);
                    count++;
                }
                else
                {
                    Move_processImage(arrow, 0, -1);
                    ReszingProcess(arrow, step2_timer, step3_timer);
                }//end of else statment   
            }//end of best fit if statment
            else
            {
                Move_processImage(arrow, 0, 1);
                ReszingProcess(arrow, step2_timer, step3_timer);
            }
        }//end of searching method

        void ReszingProcess(PictureBox arrow,Timer step2_timer,Timer step3_timer)
        {
            if (arrow.Location.Y == process_copy.Process_location)
            {
                index = 1;
                count = 1;
                step2_timer.Stop();
                process_copy.Process_Image.Size = new System.Drawing.Size(100, Convert.ToInt16(process_copy.Orig_Process_Height));
                listview1.Items[process_copy.ID].SubItems[4].Text = process_copy.Assig_space.ToString();
                listview1.Items[process_copy.ID].SubItems[2].Text = "Assigned";
                Image img = process_copy.Process_Image.Image;
                img.RotateFlip(RotateFlipType.Rotate90FlipXY);
                process_copy.Process_Image.Image = img;
                step3_timer.Start();
            }
        }//end of resizing process method

        public void Step3(Timer step3_timer, Timer step4_timer,PictureBox colide_box2)
        {
            if (!process_copy.Process_Image.Bounds.IntersectsWith(colide_box2.Bounds))
            {
                Move_processImage(process_copy.Process_Image, 4, 0);
            }
            else
            {
                step3_timer.Stop();
                step4_timer.Start();
            }
        }//end of dequeuing from queue method

        public void Step4(Timer step4_timer, PictureBox arrow)
        {
            if (process_copy.Process_location >= 134 && process_copy.Process_location < 382)
            {
                Move_processImage(process_copy.Process_Image, 0, -4);
                if (process_copy.Process_Image.Bounds.IntersectsWith(arrow.Bounds))
                {
                    if (process_copy.Size > 400 && process_copy.Size <= 500)
                    {
                        Move_processImage(process_copy.Process_Image, 0, -60);
                        index = -2;
                    }
                    else
                    {
                        Move_processImage(process_copy.Process_Image, 0, -32);
                    }
                    step4_timer.Stop();
                    timer5.Start();
                }
            }
            else
            {
                Move_processImage(process_copy.Process_Image, 0, 4);
              
                if (process_copy.Process_Image.Bounds.IntersectsWith(arrow.Bounds))
                {
                    if (process_copy.Size <= 5)
                    {
                        Move_processImage(process_copy.Process_Image, 0, 13);
                        index = 2;
                    }
                    else if (process_copy.Size >= 350 && process_copy.Size <= 400 || (process_copy.Size < 350 && process_copy.Size > 200))
                    {
                        Move_processImage(process_copy.Process_Image, 0, 43);
                        index = -2;
                    }
                    else if (process_copy.Size <= 200 && process_copy.Size > 100)
                    {
                        Move_processImage(process_copy.Process_Image, 0, 25);
                        index = -2;
                    }
                    else if (process_copy.Size <= 80 && process_copy.Size > 50)
                    {
                        Move_processImage(process_copy.Process_Image, 0, 18);
                    }
                    else
                    {
                        Move_processImage(process_copy.Process_Image, 0, 33);
                    }
                    step4_timer.Stop();
                    timer5.Start();
                }
            }
        }//end of reaching process into memory location method

        public void Step5(Timer step1_timer, PictureBox arrow, PictureBox collide_box3)
        {
            if (index <= 84)
            {
                Move_processImage(process_copy.Process_Image, 2, 0);
                index++;
            }
            else
            {
                if (algo.ToLower() != "nextfit")
                {
                    Move_processImage(arrow, 0, -5);
                   
                    if (arrow.Bounds.IntersectsWith(collide_box3.Bounds))
                    {
                        que_check = true;
                        check = false;
                        timer5.Stop();
                        if (Que.Count == 0 && que_check == true)
                        {
                            this.timer2.Stop();
                        }
                        else
                        {
                            Trash_Check_OR_Not(step1_timer);
                        }
                    }//end of inner if statment
                }//end of best and first fit if statment
                else
                {
                    timer5.Stop();
                    if (Que.Count > 0)
                    {
                        Trash_Check_OR_Not(step1_timer);
                    }
                }//end of next fit else
               
            }//end of most outer else statment
        }//end of placing process in memory Other_block method

        void Trash_Check_OR_Not(Timer step1_timer)
        {
            process_copy = Que.Dequeue();
            check = true;
            if (!process_copy.Check)
            {
                listview1.Items[process_copy.ID].SubItems[4].Text = "Trashed";
                this.timer7.Start();
            }
            else
            {
                step1_timer.Start();
            }
        }//end of piece of step5 method

       public void Step_next_2(Timer step1_timer,PictureBox arrow,PictureBox colidebox,PictureBox arrow_colideBox,PictureBox pic1)
        {
            if (Que.Count == 1 && colidebox.Location.X < 588)
            {
                colidebox.Location = new Point(colidebox.Location.X + 10, colidebox.Location.Y);
            }
            if (!process.Process_Image.Bounds.IntersectsWith(colidebox.Bounds))
            {
                Move_processImage(process.Process_Image, 10, 0);
            }
            else if (algo.ToLower() == "nextfit")
            {
                colidebox.Location = new Point(process.Process_Image.Location.X + 2, process.Process_Image.Location.Y);
                this.timer6.Stop();
                if (index>=84)
                {
                    Trash_Check_OR_Not(step1_timer);
                }
            }
            else
            {
                colidebox.Location = new Point(process.Process_Image.Location.X + 2, process.Process_Image.Location.Y);
                this.timer6.Stop();
                if (arrow.Bounds.IntersectsWith(arrow_colideBox.Bounds) && timer7.Enabled==false)
                {
                    Trash_Check_OR_Not(step1_timer);
                }
            }//end of best and first fit work
        }//end of geting next process in queue

        public void go_InTrash(PictureBox colide_trashbox,Timer step1_timer)
        {
            if (process_copy.Process_Image.Bounds.IntersectsWith(colide_trashbox.Bounds))
            {
                trash_check = true;
            }
            else if(trash_check==false)
            {
                Move_processImage(process_copy.Process_Image, 7, 0);
            }
            if (trash_check)
            {
                Move_processImage(process_copy.Process_Image, 0, 3);
            }
            if (this.trash.Bounds.IntersectsWith(process_copy.Process_Image.Bounds))
            {
                trash.Load(@"r2.png");
                trash_check = false;
                listview1.Items[process_copy.ID].SubItems[2].Text = "waste";
                process_copy.Process_Image.Dispose();
            
                this.timer7.Stop();
                if (Que.Count > 0)
                {
                    Trash_Check_OR_Not(step1_timer);
                }
            }
        }//end of going in trash method

        public void calculate_time()
        {
            timetick++;
            totaltime = (timetick / 3600) * 100;
            totaltime = Math.Floor(totaltime);
        }//end of calcualte_time

        void Move_processImage(PictureBox pic,int X,int Y)
        {
            pic.Location = new Point(pic.Location.X + X, pic.Location.Y + Y);
        }//end of moving process method
    }//end of queue process class
}
