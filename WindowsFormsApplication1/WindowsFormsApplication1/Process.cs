using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Process
    {
        int size, Assign_Space;
        double save;
        int temp = 0;
        Memory memory;
        PictureBox process_Image;
        double block_Per;
        double orig_Process_Height;
        double process_location;
        bool check;
        int Process_ID;

        public Process(int size,Memory memory,string AlgoName,int ID)
        {
            this.Process_ID = ID;
            this.check = false;
            this.memory = memory;
            this.size = size;
            if (AlgoName.ToLower() == "bestfit")
            {
                BestFit(this.size);
            }
            else if (AlgoName.ToLower() == "firstfit")
            {
                FirstFit(this.size);
            }
            else
            {
                NextFit(this.size);
            }
        }//end of contructor

        public int ID
        {
            get
            {
                return Process_ID;
            }
        }//end of ID property

        public bool Check
        {
            get
            {
                return check;
            }
        }//end of check

        public int Size
        {
            get
            {
                return size;
            }
        }//end of size property

        public PictureBox Process_Image
        {
            get
            {
                return process_Image;
            }
        }//end of process image property

        public double Orig_Process_Height
        {
            get 
            {
                return orig_Process_Height;
            }
        }//end of process height image propety

        public double Process_location
        {
            get
            {
                return process_location;
            }
        }//end of process location property

        public int Assig_space
        {
            get
            {
                return Assign_Space;
            }
        }//end of assigning space property

        public void Create_Process(Control.ControlCollection c)
        {
            process_Image = new PictureBox();
            process_Image.Load("book.png");
            process_Image.SizeMode = PictureBoxSizeMode.StretchImage;
            if (size <= 5)
            {
                block_Per = (size / memory.bestfit_block[8, 0]) * 100;
                orig_Process_Height = (memory.bestfit_block[8, 2] / 100) * block_Per;
            }
            else if (size > 500)
            {
                orig_Process_Height = 100;
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (size >= memory.bestfit_block[i + 1, 0] && size <= memory.bestfit_block[i, 0])
                    {
                        block_Per = (size / memory.bestfit_block[i, 0]) * 100;
                        orig_Process_Height = (memory.bestfit_block[i, 2] / 100) * block_Per;
                        break;
                    }
                }
            }
            Image img = process_Image.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            process_Image.Image = img;
            process_Image.Size = new Size(Convert.ToInt16(orig_Process_Height), 100);
            process_Image.BackColor = Color.Transparent;
            process_Image.Location = new Point(12, 350);
            c.Add(process_Image);
            process_Image.BringToFront();
        }//end of creating process Image method

        public void BestFit(int Size)
        {
            save = memory.bestfit_block[0, 0];
            for (int i = 0; i < 9; i++)
            {
                if (memory.bestfit_block[i, 0] >= Size && memory.bestfit_block[i, 0] <= save && memory.bestfit_block[i, 3] != 1)
                {
                    save = memory.bestfit_block[i, 0];
                    temp = i;
                    check = true;
                }
            }
            if (check)
            {
                memory.bestfit_block[temp, 3] = 1;
                Assign_Space = Convert.ToInt16(memory.bestfit_block[temp, 0]);
                process_location = memory.bestfit_block[temp, 1];    
            }
        }//end of comparing method

        public void FirstFit(int Size)
        {
            for (int i = 0; i < 9; i++)
            {
                if (memory.Other_block[i, 0] >= Size && memory.Other_block[i, 3] != 1)
                {
                    temp = i;
                    check = true;
                    break;
                }
            }
            if (check)
            {
                Assign_Space = Convert.ToInt16(memory.Other_block[temp, 0]);
                memory.Other_block[temp, 3] = 1;
                process_location = memory.Other_block[temp, 1];
            }
        }//end of first fit method

        public void NextFit(int size)
        {
            for (int i = memory.Index; i < 9; i++)
            {
                if (memory.Other_block[i, 0] >= Size && memory.Other_block[i, 3] != 1)
                {
                    save = memory.Other_block[i, 0];
                    memory.Index = i;
                    check = true;
                    break;
                }
            }
            if (check)
            {
                Assign_Space = Convert.ToInt16(memory.Other_block[memory.Index, 0]);
                memory.Other_block[memory.Index, 3] = 1;
                process_location = memory.Other_block[memory.Index, 1];
            }
        }//end of nextfit algorithum
    }//end of class
}
