using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Memory
    {
       public double[,] Other_block;
       public double[,] bestfit_block;
       public int Index;

        public Memory()
        {
            this.bestfit_block = new double[9, 4];
            this.Other_block = new double[9, 4];
            this.FillBlock();
        }//end of memory constructor

        public void FillBlock()
        {
            ////Memory size                  //location

            bestfit_block[0, 0] = 500;  bestfit_block[0, 1] = 210;
            bestfit_block[1, 0] = 400;  bestfit_block[1, 1] = 628;
            bestfit_block[2, 0] = 350;  bestfit_block[2, 1] = 462;
            bestfit_block[3, 0] = 200;  bestfit_block[3, 1] = 382;
            bestfit_block[4, 0] = 100;  bestfit_block[4, 1] = 134;
            bestfit_block[5, 0] = 80;   bestfit_block[5, 1] = 533;
            bestfit_block[6, 0] = 50;   bestfit_block[6, 1] = 324;
            bestfit_block[7, 0] = 10;   bestfit_block[7, 1] = 285;
            bestfit_block[8, 0] = 5;    bestfit_block[8, 1] = 570;

        
            //height
            bestfit_block[0, 2] = 90;
            bestfit_block[1, 2] = 80;
            bestfit_block[2, 2] = 75;
            bestfit_block[3, 2] = 60;
            bestfit_block[4, 2] = 40;
            bestfit_block[5, 2] = 40;
            bestfit_block[6, 2] = 30;
            bestfit_block[7, 2] = 22;
            bestfit_block[8, 2] = 10;

            //memory Other_block hold process or not check
            bestfit_block[0, 3] = 0;
            bestfit_block[1, 3] = 0;
            bestfit_block[2, 3] = 0;
            bestfit_block[3, 3] = 0;
            bestfit_block[4, 3] = 0;
            bestfit_block[5, 3] = 0;
            bestfit_block[6, 3] = 0;
            bestfit_block[7, 3] = 0;
            bestfit_block[8, 3] = 0;  

            //memory size
            Other_block[1, 0] = 500;
            Other_block[8, 0] = 400;
            Other_block[5, 0] = 350;
            Other_block[4, 0] = 200;
            Other_block[0, 0] = 100;
            Other_block[6, 0] = 80;
            Other_block[3, 0] = 50;
            Other_block[2, 0] = 10;
            Other_block[7, 0] = 5;

            //location

            Other_block[1, 1] = 210;
            Other_block[8, 1] = 628;
            Other_block[5, 1] = 462;
            Other_block[4, 1] = 382;
            Other_block[0, 1] = 134;
            Other_block[6, 1] = 533;
            Other_block[3, 1] = 324;
            Other_block[2, 1] = 285;
            Other_block[7, 1] = 570;

            //height
           
            Other_block[1, 2] = 90;
            Other_block[8, 2] = 80;
            Other_block[5, 2] = 75;
            Other_block[4, 2] = 60;
            Other_block[0, 2] = 40;
            Other_block[6, 2] = 40;
            Other_block[3, 2] = 30;
            Other_block[2, 2] = 22;
            Other_block[7, 2] = 10;

            //memory Other_block hold process or not check
            Other_block[0, 3] = 0;
            Other_block[1, 3] = 0;
            Other_block[2, 3] = 0;
            Other_block[3, 3] = 0;
            Other_block[4, 3] = 0;
            Other_block[5, 3] = 0;
            Other_block[6, 3] = 0;
            Other_block[7, 3] = 0;
            Other_block[8, 3] = 0;          
        }//end assigning block
    }
}
