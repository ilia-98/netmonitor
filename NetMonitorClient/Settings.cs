using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetMonitorClient
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void checkBoxEmailSend_CheckedChanged(object sender, EventArgs e)
        {
            textBoxEmail.Enabled = ((CheckBox)(sender)).Checked;
        }
    }
}
