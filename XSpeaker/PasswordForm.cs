using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XSpeaker
{
    public partial class PasswordForm : Form
    {
        static DateTime today = DateTime.Today;
        string Password = (int)today.DayOfWeek + today.Day.ToString();

        public PasswordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = this.Owner as Form1;
            if (string.Equals(PasswordBox.Text, Password))
            {
                Form1.isAdmin = true;
                f.toolStripMenuItem1.Enabled = true;
                f.режимадминистратораToolStripMenuItem.Visible = false;
                f.выйтиИзРежимаАдминистратораToolStripMenuItem.Visible = true;
                f.AdminModLabel.Visible = true;

                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Введен неверный пароль!", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
