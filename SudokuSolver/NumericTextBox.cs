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
