using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetMonitorClient
{
    class Program
    {
        static NetMonitorClient client;
        static Settings settingForm;
        static Password passwordForm;
        static bool need_change = false;
        static RegistryKey registryUserKey;
        static RegistryKey registryUserSubKey;
        static void Main(string[] args)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (registryKey.GetValue("NetMonitorClient") == null)
            {
                registryKey.SetValue("NetMonitorClient", Application.ExecutablePath);
            }
            registryKey.Close();
            registryUserKey = Registry.CurrentUser;
            registryUserSubKey = registryUserKey.CreateSubKey("NetMonitorClient");

            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Icon = Properties.Resources.Error;
            notifyIcon.ContextMenu = new ContextMenu(
                new MenuItem[] {
                    new MenuItem("Настройки", Settings_Click),
                    new MenuItem("Перезапустить", Reboot_Click)
                });
            client = new NetMonitorClient(notifyIcon);
            settingForm = new Settings();
            passwordForm = new Password();
            client.Start();
            Application.Run();
        }

        private static void ButtonChangePassword_Click(object sender, EventArgs e)
        {
            passwordForm = new Password();
            passwordForm.buttonPassword.Click += ButtonPassword_Click;
            passwordForm.labelPassword.Text = "Придумайте пароль:";
            passwordForm.buttonPassword.Text = "Сохранить";
            passwordForm.Text = "Смена пароля";
            need_change = true;
            passwordForm.Show();
        }


        private static void Reboot_Click(object sender, EventArgs e)
        {
            client.Stop();
            client.Start();
        }

        private static void Settings_Click(object sender, EventArgs e)
        {
            passwordForm = new Password();
            passwordForm.buttonPassword.Click += ButtonPassword_Click;
      
            if (registryUserSubKey.GetValue("password") == null || need_change)
            {
                passwordForm.labelPassword.Text = "Придумайте пароль:";
                passwordForm.buttonPassword.Text = "Сохранить";
                passwordForm.Text = "Регистрация";
            }
            else
            {
                passwordForm.labelPassword.Text = "Введите пароль:";
                passwordForm.buttonPassword.Text = "Войти";
            }
            passwordForm.Show();
        }



        private static void ButtonPassword_Click(object sender, EventArgs e)
        {
            RegistryKey registryUserKey = Registry.CurrentUser;
            RegistryKey userKey = registryUserKey.CreateSubKey("NetMonitorClient");
            if (userKey.GetValue("password") == null || need_change)
            {
                userKey.SetValue("password", GetHash(passwordForm.textBoxPassword.Text));
                passwordForm.Hide();
                settingForm.Close();
                settingForm = new Settings();
                settingForm.buttonSubmit.Click += ButtonSubmit_Click;
                settingForm.buttonChangePassword.Click += ButtonChangePassword_Click;
                settingForm.textBoxIP.Text = client.Address;
                settingForm.Show();
                need_change = false;
            }
            else
            {
                if ((string)userKey.GetValue("password") == GetHash(passwordForm.textBoxPassword.Text))
                {
                    passwordForm.Hide();
                    settingForm.Close();
                    settingForm = new Settings();
                    settingForm.buttonSubmit.Click += ButtonSubmit_Click;
                    settingForm.buttonChangePassword.Click += ButtonChangePassword_Click;
                    settingForm.textBoxIP.Text = client.Address;
                    settingForm.Show();
                }
                else
                {
                    passwordForm.textBoxPassword.Text = "";
                }
            }
        }

        private static void ButtonSubmit_Click(object sender, EventArgs e)
        {
            client.Stop();
            try
            {
                client.Address = IPAddress.Parse(settingForm.textBoxIP.Text).ToString();
            }
            catch { }
            settingForm.Hide();
            client.Start();
        }


        private static string GetHash(string password)
        {
            string salt = "netmonitor";
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            return Convert.ToBase64String(hash);
        }


    }


}
