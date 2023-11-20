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
    public partial class User : Form
    {
        public string user;
        public User(string a)
        {
            InitializeComponent();
            setupGrid(a);
            user = a;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            checkedListBox1.CheckOnClick = true;
            this.listBox1.Hide();
            this.label1.Text = "Здесь будет несколько упражнений\nна выбранную группу мышц";
            this.label2.Text = System.DateTime.Now.Day.ToString()+":"+ System.DateTime.Now.Month.ToString()+ ":" +System.DateTime.Now.Year.ToString();
            this.label3.Text = System.DateTime.Now.DayOfWeek.ToString();
        }

        private void setupGrid(string a)
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
            string[] sk;
            sk = a.Split(':');
            this.dataGridView1.Rows.Insert(0, sk);
            this.dataGridView1.ReadOnly = true;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add dlg = new Add(dataGridView1.Rows[0], true);
            string[] sk = File.ReadAllLines(@"..\..\ListGymUser.sk");
            for (int i =0; i < sk.Length; i++)
            {
                if (sk[i] == user)
                {
                    sk[i] = "suck";
                }
            } 
            if (sk[0] != "suck")
            {
                File.WriteAllText(@"..\..\ListGymUser.sk", sk[0]+"\n");
            }
            else
            {
                File.WriteAllText(@"..\..\ListGymUser.sk", "");
            }
            for (int i = 1; i < sk.Length; i++)
            {
                if (sk[i] != "suck")
                {
                    File.AppendAllText(@"..\..\ListGymUser.sk", sk[i]+"\n");
                }
            }
            dlg.ShowDialog();
            sk = File.ReadAllLines(@"..\..\ListGymUser.sk");
            setupGrid(sk[sk.Length - 1]);

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox ddd = new CheckedListBox();
            
        }
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                if (checkedListBox1.CheckedItems.Count == 1)
                {
                    this.listBox1.Hide();
                    try
                    {
                        this.listBox1.SetSelected(listBox1.SelectedIndex, false);
                    }
                    catch(Exception ex) { Console.WriteLine(ex); }
                    this.label1.Text = "Здесь будет несколько упражнений\nна выбранную группу мышц";
                    this.Width = 630;
                    this.Height = 200;
                }
            }
            else
            {
                this.Height = 340;
                this.listBox1.Show();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    this.label1.Text = "1. Отжимания от пола\n2. Отжимания на брусьях\n3. Жим штанги лежа\n4. Разведение гантелей лежа\n5. Жим гантелей на наклонной скамье";
                    this.Width = 630;
                    break;
                case 1:
                    this.label1.Text = "1. Становая тяга со штангой\n2. Подтягивания на турнике\n3. Тяга верхнего блока\n4. Тяга нижнего блока одной рукой\n5. Т-тяга штанги узким хватом";
                    this.Width = 630;
                    break;
                case 2:
                    this.label1.Text = "1. Приседания со штангой\n2. Жим ногами в тренажере\n3. Разгибания ног в тренажере сидя\n4. Сгибания ног в тренажере лежа\n5. Разведения\\Сведения ног сидя";
                    this.Width = 630;
                    break;
                case 3:
                    this.label1.Text = "1. Жим гантелей от плеч сидя\n2. Поднимание гантелей в бок из положения стоя\n3. Поднимание гантелей с поддержкой груди\n4. Пулловер\n5. Разводка в кроссовере на заднюю дельту стоя";
                    this.Width = 730;
                    break;
                case 4:
                    this.label1.Text = "1. Подъём штанги или гантелей на бицепс\n2. Разгибание на трицепс с гантелью\n3. Сгибание предплечий с EZ-штангой\n4. Сгибание предплечий с поворотом наружу с гантелями\n5. Французский жим с гантелями лёжа";
                    this.Width = 800;
                    break;
            }
        }
    }
}
