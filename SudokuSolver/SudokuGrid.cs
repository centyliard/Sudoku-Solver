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
    class SudokuGrid : IEnumerable
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

            FillWithDefaultValues();
        }
        
        public int getSmallSquareIndex(int row, int col)
        {
            return col / 3 * 3 + row / 3;
            
        }

        public NumericTextBox GetBox(int row, int col)
        {
            return Grid[row, col];
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
        public NumericTextBox[,] Get3x3(int row, int col)
        {
            return Get3x3(getSmallSquareIndex(row, col));
        }

        public bool Validate3x3(int smallSquareIndex)
        {
            NumericTextBox[,] smallSquare = Get3x3(smallSquareIndex);

            int[] repeatingNumbersCount = new int[10];
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

                    repeatingNumbersCount[BoxValue]++;
                }
            }

            for (int i = 1; i <= 9; i++)
            {
                if (repeatingNumbersCount[i] > 1)
                {
                    return false;
                }
            }

            return true;
        }
        public bool Validate3x3(int row, int col)
        {
            return Validate3x3(getSmallSquareIndex(row, col));
        }
        public bool ValidateRow(int row)
        {
            NumericTextBox[] selectedRow = GetRow(row);
            int[] repeatingNumbersCount = new int[10];
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

                repeatingNumbersCount[BoxValue]++;
            }

            for (int i = 1; i <= 9; i++)
            {
                if (repeatingNumbersCount[i] > 1)
                {
                    return false;
                }
            }

            return true;
        }
        public bool ValidateColumn(int col)
        {
            NumericTextBox[] selectedColumn = GetColumn(col);
            int[] repeatingNumbersCount = new int[10];
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

                repeatingNumbersCount[BoxValue]++;
            }

            for (int i = 1; i <= 9; i++)
            {
                if (repeatingNumbersCount[i] > 1)
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

        private void FillWithDefaultValues()
        {
            Grid[0, 0].Text = "8";
            Grid[0, 4].Text = "3";
            Grid[0, 7].Text = "4";
            Grid[1, 5].Text = "9";
            Grid[1, 8].Text = "5";
            Grid[2, 1].Text = "5";
            Grid[2, 2].Text = "1";
            Grid[2, 5].Text = "6";
            Grid[2, 7].Text = "3";
            Grid[3, 2].Text = "5";
            Grid[3, 4].Text = "4";
            Grid[3, 8].Text = "9";
            Grid[4, 0].Text = "9";
            Grid[4, 3].Text = "1";
            Grid[4, 5].Text = "2";
            Grid[4, 8].Text = "4";
            Grid[5, 0].Text = "2";
            Grid[5, 6].Text = "3";
            Grid[6, 1].Text = "3";
            Grid[6, 3].Text = "7";
            Grid[6, 6].Text = "6";
            Grid[6, 7].Text = "8";
            Grid[6, 8].Text = "1";
            Grid[7, 0].Text = "1";
            Grid[7, 3].Text = "9";
            Grid[8, 1].Text = "7";
            Grid[8, 4].Text = "8";
        }

        public IEnumerator GetEnumerator()
        {
            return Grid.GetEnumerator();
        }
    }
}
