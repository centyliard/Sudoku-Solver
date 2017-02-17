using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SudokuSolver
{
    static class SudokuSolvingAlgorithm
    {
        public static void Run(SudokuGrid grid)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (FillSudoku(grid))
            {
                watch.Stop();
                MessageBox.Show(String.Format("Sudoku was solved succesfully in {0} ms.", watch.ElapsedMilliseconds), "Success!");
            }
            else
            {
                watch.Stop();
                MessageBox.Show("This sudoku cannot be solved.", "Error");
            }
        }
        public static bool IsAvailable(SudokuGrid grid, int row, int col, int value)
        {
            var startingPoint = new { x = row / 3 * 3, y = col / 3 * 3 };

            for (int i = 0; i < 9; i++)
            {
                if (grid.GetBox(row, i).Text == value.ToString()) return false;
                if (grid.GetBox(i, col).Text == value.ToString()) return false;
                if (grid.GetBox(startingPoint.x + i % 3, startingPoint.y + i/3).Text == value.ToString()) return false;
            }
            return true;
        }
        private static bool FillSudoku(SudokuGrid grid, int row = 0, int col = 0)
        {
            if (row < 9 && col < 9)
            {
                if (grid.GetBox(row, col).Text != "")
                {
                    if (col + 1 < 9) return FillSudoku(grid, row, col + 1);
                    else if (row + 1 < 9) return FillSudoku(grid, row + 1, 0);
                    else return true;
                }
                else
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (IsAvailable(grid, row, col, i + 1))
                        {
                            grid.GetBox(row, col).Text = (i + 1).ToString();
                            if (col + 1 < 9)
                            {
                                if (FillSudoku(grid, row, col + 1)) return true;
                                else
                                {
                                    grid.GetBox(row, col).Text = "";
                                }
                            }
                            else if (row + 1 < 9)
                            {
                                if (FillSudoku(grid, row + 1, 0)) return true;
                                else
                                {
                                    grid.GetBox(row, col).Text = "";
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
