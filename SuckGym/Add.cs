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
using System.Media;

namespace SuckGym
{
    public partial class Add : Form
    {
        private SoundPlayer player = new SoundPlayer();
        private DataGridViewRow a = new DataGridViewRow(); 
        public Add()
        {
            InitializeComponent();
            pictureBox1.Hide();
            button3.Hide();
            button4.Hide();
        }
        public Add(DataGridViewRow row) {
            a = row;
            InitializeComponent();
            pictureBox1.Hide();
            button3.Hide();
            button4.Hide();
            init();
        }
        public Add(DataGridViewRow row, bool x)
        {
            a = row;
            InitializeComponent();
            pictureBox1.Hide();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox6.Enabled = false;
            button3.Hide();
            button4.Hide();
            init();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ss = textBox7.Text + ":" + textBox8.Text + ":" + textBox2.Text + " " + textBox1.Text + " " + textBox3.Text + ":" + textBox4.Text + ":" + textBox5.Text + ":" + textBox6.Text+"\n";
            File.AppendAllText(@"..\..\ListGymUser.sk", ss);

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Show();
            this.player.SoundLocation = @"..\..\Resources\videoplayback.wav";
            this.player.Play();
            button3.Show();
            button4.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.player.Stop();
            string ss = textBox7.Text + ":" + textBox8.Text + ":" + textBox2.Text + " " + textBox1.Text + " " + textBox3.Text + ":" + textBox4.Text + ":" + textBox5.Text + ":" + textBox6.Text + "\n";
            File.AppendAllText(@"..\..\ListGymUser.sk", ss);

            this.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.player.Stop();
            button4.Hide();
            button3.Hide();
            pictureBox1.Hide();
        }
        private void init() {
            string[] s = new string[a.Cells.Count];
            int count = 0;
            foreach(DataGridViewCell cell in a.Cells)
            {
                s[count] += cell.Value;
                count++;
            }
            textBox7.Text = s[0];
            textBox8.Text = s[1];
            string[] s1 = s[2].Split(' ');
            textBox1.Text = s1[1];
            textBox2.Text = s1[0];
            textBox3.Text = s1[2];
            textBox4.Text = s[3];
            textBox5.Text = s[4];
            textBox6.Text = s[5];


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
