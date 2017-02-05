using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            // Od tej pory mogę odwoływać się do pól planszy przez SudokuGrid
            // NIC NIE RUSZAĆ!
            SudokuGrid = new NumericTextBox[9, 9];
            SudokuGrid[0, 0] = numericTextBox12;

            int i = 0;
            foreach (Control C in Controls[1].Controls)
            {
                foreach (Control ctr in C.Controls)
                {
                    SudokuGrid[i % 9, i / 9] = (NumericTextBox)ctr;
                    SudokuGrid[i % 9, i / 9].Text = (i + 1).ToString();
                    i++;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ValidateGrid();
        }

        private bool ValidateGrid()
        {
            //TODO: duppa
        }
    }
}