using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace SuckGym
{
    public partial class Form1 : Form
    {
        private string password = " ";// УБРАТЬ ПРОБЕЛ ПРИ ВОЗВРАТЕ ЗНАЧЕНИЯ
        private string login = " ";
        public StringBuilder sb = new StringBuilder();
        public Form1()
        {
            InitializeComponent();
            this.pictureBox1.Hide();
            this.button2.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            int count = 0;
            for (int i = 0; i< textBox2.Text.Length; i++)
            {
                if (textBox2.Text[i] == '*')
                {
                    count++;
                }
            }
            if (count != textBox2.Text.Length) {
                if (textBox2.Text.Length < password.Length)
                {

                    password = password.Substring(0, textBox2.Text.Length);
                }
                sb = new StringBuilder(textBox2.Text);
                int p = password.Length;
                p--;
                char sub = sb[p];
                password += sub;

                sb[p] = '*';
                textBox2.Text = sb.ToString();
                textBox2.SelectionStart+= password.Length-1;
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool res = false;
            login = textBox1.Text;
            string Ainfo = login + ":" + password.Substring(1, password.Length-1);
            textBox1.Text = Ainfo;
            string[] ak  = File.ReadAllLines(@"..\..\Config.sk");
            if(Ainfo == ak[0])
            {
                res = true;
                Admin Lol = new SuckGym.Admin();
                this.Hide();
                Lol.FormClosed += new FormClosedEventHandler(retry_FormClosed);
                Lol.Show();
            }
            else
            {
                ak = File.ReadAllLines(@"..\..\ListGymUser.sk");
                foreach(string user in ak)
                {
                    if(Ainfo == user.Substring(0, Ainfo.Length))
                    {
                        res = true;
                        User first = new SuckGym.User(user);
                        first.Show();
                        this.Hide();
                        first.FormClosed += new FormClosedEventHandler(retry_FormClosed);
                    }
                }
            }
            if(res == false)
            {
                this.pictureBox1.Show();
                this.button2.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 retry = new Form1();
            retry.FormClosed += new FormClosedEventHandler(retry_FormClosed);
            this.Hide();
            retry.Show();
            
        }

        void retry_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
