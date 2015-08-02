using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace XSpeaker
{
    public partial class AddOrChangeUser : Form
    {
        private void IP_Enter(object sender, EventArgs e)
        {
            if (IP.Text == (String)IP.Tag)
            {
                IP.Text = "";
            }
        }

        private void IP_Leave(object sender, EventArgs e)
        {
            //if (String.IsNullOrWhiteSpace(IP.Text))
            //{
            //    IP.Text = (String)IP.Tag;
            //}
        }

        private void Port_Leave(object sender, EventArgs e)
        {
            //if (String.IsNullOrWhiteSpace(Port.Text))
            //{
            //    Port.Text = (String)Port.Tag;
            //}
        }


        public AddOrChangeUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            try
            {
                IPAddress IPAdr = IPAddress.Parse(IP.Text);
                User NewUser = new User(FIO.Text, IPAdr);
                if (Form1.UserList.Contains(NewUser))
                    throw new Exception("Такой клиент уже существует.");
                Form1.UserList.Add(NewUser);//добавляем пользователя в юзер-лист.
                this.DialogResult = DialogResult.OK;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
