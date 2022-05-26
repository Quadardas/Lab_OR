using System;
using System.IO;
using System.Windows.Forms;

namespace Lab_OR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            for(int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                comboBox1.Items.Add(dataGridView1.Columns[i].HeaderText);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var line in File.ReadLines("File.txt"))
            {
                var array = line.Split();
                dataGridView1.Rows.Add(array);
                dataGridView2.Rows.Add(array);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double[] p = { 0.3, 0.2, 0.2, 0.3 };
            double sum = 0;

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 1; j < dataGridView2.ColumnCount - 1; j++)
                {
                    sum += Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value) * p[j - 1];
                }
                dataGridView2.Rows[i].Cells[5].Value = sum;
                sum = 0;
            }

            double max_criteria = 0;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (Convert.ToDouble(dataGridView2.Rows[i].Cells[5].Value) > max_criteria)
                {
                    max_criteria = Convert.ToDouble(dataGridView2.Rows[i].Cells[5].Value);
                    dataGridView2.ClearSelection();
                    dataGridView2.Rows[i].Selected = true;
                    label2.Text = Convert.ToString(dataGridView2.Rows[i].Cells[0].Value);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Нормализация
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                if (Convert.ToString(row.Cells[0].Value) == "")
                    break;
                double maximum = 0;

                for (int i = 1; i < 5; i++)
                {
                    if (i == 1)
                        maximum = Convert.ToDouble(row.Cells[i].Value);
                    if (maximum < Convert.ToDouble(row.Cells[i].Value))
                        maximum = Convert.ToDouble(row.Cells[i].Value);
                }

                for (int i = 1; i < 5; i++)
                {
                    row.Cells[i].Value = Convert.ToDouble(row.Cells[i].Value) / maximum;
                }
            }

            int main_criteria = 1;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                if (dataGridView1.Columns[i].HeaderText == comboBox1.Text.ToString())
                {
                    main_criteria = i;
                }
            }

            double sum = 0, max_main_criteria = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 1; j < dataGridView1.ColumnCount - 1; j++)
                {
                    if (j != main_criteria)
                    {
                        sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                    }
                    else
                    {
                        if (Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value) > max_main_criteria)
                        {
                            max_main_criteria = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                            dataGridView1.ClearSelection();
                            dataGridView1.Rows[i].Selected = true;
                        }
                    }
                }
                dataGridView1.Rows[i].Cells[5].Value = sum / 4;
                sum = 0;
            }

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Selected == true &&
                    Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value) >= 0.3)
                {
                    label1.Text = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                }
            }

            textBox1.Clear();
        }


    }
}
