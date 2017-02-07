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

            SudokuGrid = CreateGrid();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ValidateGrid();
        }

        private NumericTextBox[,] CreateGrid()
        {
            NumericTextBox[,] Grid = new NumericTextBox[9, 9];

            // Kill me please
            for(int i = 0; i < Controls[1].Controls.Count; i++)
            {
                for (int j = 0; j < Controls[1].Controls[i].Controls.Count; j++)
                {
                    Grid[i, j] = (NumericTextBox)Controls[1].Controls[i].Controls[j];
                    //Grid[i, j].Text = (i * 9 + j + 1).ToString();
                }
            }

            for (int i = 1; i < 10; i++)
            {
                Grid[i-1, 0].Text = i.ToString();
                Grid[0, i-1].Text = i.ToString();
            }

            return Grid;
        }

        private bool Validate3x3(int smallSquareIndex)
        {
            int startingpoint_x = smallSquareIndex / 3 * 3;
            int startingpoint_y = smallSquareIndex % 3 * 3;

            NumericTextBox[,] smallSquare = new NumericTextBox[3, 3];

            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    smallSquare[i, j] = SudokuGrid[startingpoint_x + i, startingpoint_y + j];
                }
            }
            return true;
        }
        private bool ValidateRow(int row)
        {
            int[] repeating = new int[10];
            for (int i = 0; i < 9; i++)
            {
                int BoxValue = 0;
                if (SudokuGrid[row, i].Text == "")
                {
                    BoxValue = 0;
                }
                else
                {
                    BoxValue = int.Parse(SudokuGrid[row, i].Text);
                }

                if (BoxValue == 0)
                {
                    repeating[0] = 0;
                }
                else
                {
                    repeating[BoxValue]++;
                }
            }

            foreach (int value in repeating)
            {
                if (value > 1)
                {
                    return false;
                }
            }

            return true;
        }
        private bool ValidateColumn(int col)
        {
            int[] repeating = new int[10];
            for (int i = 0; i < 9; i++)
            {
                int BoxValue = 0;
                if (SudokuGrid[i, col].Text == "")
                {
                    BoxValue = 0;
                }
                else
                {
                    BoxValue = int.Parse(SudokuGrid[i, col].Text);
                }
                
                if(BoxValue == 0)
                {
                    repeating[0] = 0;
                }
                else
                {
                    repeating[BoxValue]++;
                }
            }

            foreach (int value in repeating)
            {
                if (value > 1)
                {
                    return false;
                }
            }

            return true;
        }
        private bool ValidateGrid()
        {
            bool flag = true;

            for (int i = 0; i < 9; i++)
            {
                if(!Validate3x3(i))
                {
                    MessageBox.Show(String.Format("Your {0} small square is not valid!", i + 1));
                    flag = false;
                }
                if (!ValidateRow(i))
                {
                    MessageBox.Show(String.Format("Your {0} row is not valid!", i + 1));
                    flag = false;
                }
                if (!ValidateColumn(i))
                {
                    MessageBox.Show(String.Format(@"Your {0} column is not valid!", i + 1));
                    flag = false;
                }
            }
            return flag;
        }
    }
}