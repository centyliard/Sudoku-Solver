using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    [ToolboxItem(true)]
    class NumericTextBox : TextBox
    {
        public bool[] viableNumbers { get; set; } = new bool[10];


        public int CountValidValues()
        {
            int ValidValuesCount = 0;
            for (int i = 1; i <= 9; i++)
            {
                if (viableNumbers[i] == true)
                {
                    ValidValuesCount++;
                }
            }
            return ValidValuesCount;
        }
        public int GetTheOnlyValidValue()
        {
            if(CountValidValues() == 1)
            {
                for (int i = 1; i <= 9; i++)
                {
                    if (viableNumbers[i] == true)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// This function allows a user to input 1-9 numbers only and goes to
        /// the next box after entering the number
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (((!Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back) ||
                e.KeyChar.ToString() == "0")
            {
                e.Handled = true;
            }
            else if (Char.IsDigit(e.KeyChar))
            {
                if (Text.Length >= 1)
                {
                    Text = "";
                }
                this.Parent.Parent.SelectNextControl(this, true, true, true,
                    true);
            }
        }
    }
}
