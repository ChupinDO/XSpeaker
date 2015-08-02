using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
namespace XSpeaker
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            short flag = 0;

            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains("XSpeaker"))
                {
                    flag++;

                    if (flag > 1)
                    {
                        MessageBox.Show("Другая копия приложения уже запущена!", "Ошибка запуска", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Application.Exit();
                        return;
                    }
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
