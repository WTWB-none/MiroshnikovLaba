using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Diagnostics;
using System.ComponentModel;



namespace SuckGym
{
    public partial class Admin : Form
    {
        private Panel buttonPanel = new Panel();
        //private DataGridView songsDataGridView = new DataGridView();

        public Admin()
        {
            InitializeComponent();
            SetupDataGridView();
            this.label1.Hide();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SetupDataGridView()
        {

            this.dataGridView1.ColumnCount = 6;

            this.dataGridView1.Name = "Пользователи качалки";
            this.dataGridView1.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridView1.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            this.dataGridView1.GridColor = Color.Black;
            this.dataGridView1.RowHeadersVisible = false;

            this.dataGridView1.Columns[2].Name = "ФИО";
            this.dataGridView1.Columns[3].Name = "Дата рождения";
            this.dataGridView1.Columns[4].Name = "Телефон";
            this.dataGridView1.Columns[5].Name = "Абонемент";
            this.dataGridView1.Columns[0].Name = "Логин";
            this.dataGridView1.Columns[1].Name = "Пароль";
            string[] ak = File.ReadAllLines(@"..\..\ListGymUser.sk");
            string[] sk;
            int rowsnum = 0;
            foreach (string user in ak) {
                sk = user.Split(':');
                this.dataGridView1.Rows.Insert(rowsnum, sk);
                rowsnum++;
            }
            this.dataGridView1.ReadOnly = true;


            this.dataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            //this.dataGridView1.MultiSelect = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add dlg = new Add();
            dlg.ShowDialog();
            this.dataGridView1.Rows.Clear();
            SetupDataGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.ReadOnly = false;
            foreach(DataGridViewRow rows in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(rows.Index);
            }
            string s1 ="";
            string[] s2 = new string[dataGridView1.Rows.Count];
            int count = 0;
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    s1 += cell.Value + ":";
                }
                s2[count] = s1.Substring(0, s1.Length-1);
                s1 = "";
                count++;
            }
            File.WriteAllLines(@"..\..\ListGymUser.sk", s2);
            
            this.dataGridView1.ReadOnly = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Add dlg = new SuckGym.Add(dataGridView1.SelectedRows[0]);
            foreach (DataGridViewRow rows in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(rows.Index);
            }
            string s1 = "";
            string[] s2 = new string[dataGridView1.Rows.Count];
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    s1 += cell.Value + ":";
                }
                s2[count] = s1.Substring(0, s1.Length - 1);
                s1 = "";
                count++;
            }
            File.WriteAllLines(@"..\..\ListGymUser.sk", s2);
            dlg.ShowDialog();
            this.dataGridView1.Rows.Clear();
            SetupDataGridView();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string res = "Вот список активных участников: \n";
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if (row.Cells[5].Value.ToString() != "просрочен")
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        res += cell.Value + " ";

                    }
                    res += "\n";
                }
                
                
            }
            this.label1.Text = res;
           
                string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\otchet";
                string file = "otchet.pdf";

                PrintDocument pDoc = new PrintDocument()
                {
                    PrinterSettings = new PrinterSettings()
                    {
                        PrinterName = "Microsoft Print to PDF",
                        PrintToFile = true,
                        PrintFileName = System.IO.Path.Combine(directory, file),
                    }
                };

                pDoc.PrintPage += new PrintPageEventHandler(Print_Page);
                pDoc.Print();
                Process.Start(directory+@"\otchet.pdf");
            }

            void Print_Page( object sender, PrintPageEventArgs e)
            {
    
            Font fnt = new Font("Times New Roman", 14);

    
            e.Graphics.DrawString(this.label1.Text, fnt, System.Drawing.Brushes.Black, 0, 0);
                }
        }
    
}
