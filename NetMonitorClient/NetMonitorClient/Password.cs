using Microsoft.Win32;
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
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
            RegistryKey registryUserKey = Registry.CurrentUser;
            RegistryKey userKey = registryUserKey.CreateSubKey("NetMonitorClient");
            if (userKey.GetValue("password") == null)
            {
                labelPassword.Text = "Придумайте пароль:";    
            }
            userKey.Close();
        }

        private void buttonPassword_Click(object sender, EventArgs e)
        {
            RegistryKey registryUserKey = Registry.CurrentUser;
            RegistryKey userKey = registryUserKey.CreateSubKey("NetMonitorClient");
            userKey.SetValue("password", textBoxPassword.Text);
        }
    }
}
