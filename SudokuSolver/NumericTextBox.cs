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
        /// Dzięki tej funkcji dopuszczalne jest jedynie wprowadzanie znaków
        /// 0-9, ponadto pozwala ona na automatyczne przejście do następnej
        /// linii po wprowadzeniu jakiejś liczby
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if ((!Char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
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
