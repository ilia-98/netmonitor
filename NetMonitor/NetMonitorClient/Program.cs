using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetMonitorClient
{
    class Program
    {
        static NetMonitorClient client;
        static Settings form;
        static void Main(string[] args)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (registryKey.GetValue("NetMonitorClient") == null)
            {
                registryKey.SetValue("NetMonitorClient", Application.ExecutablePath);
            }
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Icon = Properties.Resources.Ok;
            notifyIcon.ContextMenu = new ContextMenu(
                new MenuItem[] {
                    new MenuItem("Настройки", Settings_Click),
                    new MenuItem("Перезапустить", Reboot_Click)
                });
            client = new NetMonitorClient(notifyIcon);
            client.Start();
            Application.Run();

        }

        private static void Reboot_Click(object sender, EventArgs e)
        {
            client.Stop();
            client.Start();
        }

        private static void Settings_Click(object sender, EventArgs e)
        {
            form = new Settings();
            form.buttonSubmit.Click += ButtonSubmit_Click;
            form.Show();
        }

        private static void ButtonSubmit_Click(object sender, EventArgs e)
        {
            client.Stop();
            client.Address = IPAddress.Parse(form.textBoxIP.Text).ToString();
            form.Hide();
            client.Start();
        }
    }
}
