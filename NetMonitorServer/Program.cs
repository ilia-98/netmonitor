using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetMonitorServer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /// <summary>
            /// Список имен локальных компьютеров
            /// </summary>
            var root = new DirectoryEntry("WinNT:");
            foreach (DirectoryEntry dom in root.Children)
            {
                foreach (DirectoryEntry entry in dom.Children)
                {
                    if (entry.Name != "Schema")
                    {
                        Console.WriteLine(entry.Name);
                    }
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
