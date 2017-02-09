using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace SudokuSolver
{
    /// <summary>
    /// A class representing a whole sudoku grid. It makes mathematical
    /// operations a lot easier.
    /// </summary>
    class SudokuGrid
    {
        /// <summary>
        /// A 2-D array holding all the NumericTextBoxes.
        /// </summary>
        private NumericTextBox[,] Grid = new NumericTextBox[9, 9];

        /// <param name="form">Pass your main form here.</param>
        public SudokuGrid(Form form)
        {
            // Kill me please
            for (int i = 0; i < form.Controls[1].Controls.Count; i++)
            {
                for (int j = 0; j < form.Controls[1].Controls[i].Controls.Count; j++)
                {
                    Grid[i, j] = (NumericTextBox)form.Controls[1].Controls[i].Controls[j];
                }
            }

            // fills the first row and column - delete later
            for (int i = 1; i < 10; i++)
            {
                Grid[i - 1, 0].Text = i.ToString();
                Grid[0, i - 1].Text = i.ToString();
            }

        }

        public NumericTextBox[] GetRow(int row)
        {
            NumericTextBox[] selectedRow = new NumericTextBox[9];
            for (int i = 0; i < 9; i++)
            {
                selectedRow[i] = Grid[row, i];
            }

            return selectedRow;
        }
        public NumericTextBox[] GetColumn(int col)
        {
            NumericTextBox[] selectedCol = new NumericTextBox[9];
            for (int i = 0; i < 9; i++)
            {
                selectedCol[i] = Grid[i, col];
            }

            return selectedCol;
        }
        public NumericTextBox[,] Get3x3(int smallSquareIndex)
        {
            int startingpoint_x = smallSquareIndex / 3 * 3;
            int startingpoint_y = smallSquareIndex % 3 * 3;

            NumericTextBox[,] smallSquare = new NumericTextBox[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    smallSquare[i, j] = Grid[startingpoint_x + i, startingpoint_y + j];
                }
            }

            return smallSquare;
        }

        public bool Validate3x3(int smallSquareIndex)
        {
            NumericTextBox[,] smallSquare = Get3x3(smallSquareIndex);


            int[] repeating = new int[10];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int BoxValue = 0;
                    if (smallSquare[i, j].Text == "")
                    {
                        BoxValue = 0;
                    }
                    else
                    {
                        BoxValue = int.Parse(smallSquare[i, j].Text);
                    }

                    repeating[BoxValue]++;
                }
            }

            for (int i = 1; i <= 9; i++)
            {
                if (repeating[i] > 1)
                {
                    return false;
                }
            }

            return true;
        }
        public bool ValidateRow(int row)
        {
            NumericTextBox[] selectedRow = GetRow(row);
            int[] repeating = new int[10];
            for (int i = 0; i < 9; i++)
            {
                int BoxValue = 0;
                if (selectedRow[i].Text == "")
                {
                    BoxValue = 0;
                }
                else
                {
                    BoxValue = int.Parse(selectedRow[i].Text);
                }

                repeating[BoxValue]++;
            }

            for (int i = 1; i <= 9; i++)
            {
                if (repeating[i] > 1)
                {
                    return false;
                }
            }

            return true;
        }
        public bool ValidateColumn(int col)
        {
            NumericTextBox[] selectedColumn = GetColumn(col);
            int[] repeating = new int[10];
            for (int i = 0; i < 9; i++)
            {
                int BoxValue = 0;
                if (selectedColumn[i].Text == "")
                {
                    BoxValue = 0;
                }
                else
                {
                    BoxValue = int.Parse(selectedColumn[i].Text);
                }

                repeating[BoxValue]++;
            }

            for (int i = 1; i <= 9; i++)
            {
                if (repeating[i] > 1)
                {
                    return false;
                }
            }

            return true;
        }
        public bool ValidateGrid()
        {
            bool flag = true;

            for (int i = 0; i < 9; i++)
            {
                if (!Validate3x3(i))
                {
                    MessageBox.Show(String.Format("Your {0} small square is not valid!", i + 1));
                    Highlight3x3(i);
                    flag = false;
                }
                if (!ValidateRow(i))
                {
                    MessageBox.Show(String.Format("Your {0} row is not valid!", i + 1));
                    HighlightRow(i);
                    flag = false;
                }
                if (!ValidateColumn(i))
                {
                    MessageBox.Show(String.Format(@"Your {0} column is not valid!", i + 1));
                    HighlightColumn(i);
                    flag = false;
                }
            }
            return flag;
        }

        private void HighlightColumn(int col)
        {
            NumericTextBox[] column = GetColumn(col);
            foreach (NumericTextBox box in column)
            {
                box.BackColor = System.Drawing.Color.Red;
            }
        }
        private void Highlight3x3(int smallSquareIndex)
        {
            NumericTextBox[,] smallSquare = Get3x3(smallSquareIndex);

            foreach (NumericTextBox box in smallSquare)
            {
                box.BackColor = System.Drawing.Color.Red;
            }
        }
        private void HighlightRow(int row)
        {
            NumericTextBox[] Row = GetRow(row);
            foreach (NumericTextBox box in Row)
            {
                box.BackColor = System.Drawing.Color.Red;
            }
        }
        public void ClearHighlighting()
        {
            foreach (NumericTextBox box in Grid)
            {
                box.BackColor = System.Drawing.SystemColors.Window;
            }
        }
    }
}
