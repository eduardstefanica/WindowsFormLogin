using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace WindowsFormsLogin.Utility
{
    public static class ManageUI
    {
        public static void manageVisibility(Label label, ComboBox combobox, Boolean isVisible)
        {
            label.Visible = isVisible;
            combobox.Visible = isVisible;
        }
    }
}
