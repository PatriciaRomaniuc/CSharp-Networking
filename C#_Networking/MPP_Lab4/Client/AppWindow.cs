using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class AppWindow : Form
    {
        private AppController appController;

        public AppWindow(AppController appController)
        {
            this.appController = appController;
            InitializeComponent();
        }

        private void setColor()
        {
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                if (rw.Cells["nrLocuriDisponibile"].Value.Equals(0))
                {
                    rw.DefaultCellStyle.BackColor = Color.Red;
                }
            }
            foreach (DataGridViewRow rw in dataGridView2.Rows)
            {
                if (rw.Cells["nrLocuriDisponibile"].Value.Equals(0))
                {
                    rw.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }
        private void clear()
        {
            dataGridView1.ClearSelection();
            dataGridView1.DataSource = appController.getAllExcursii();
            setColor();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AppWindow_Load(object sender, EventArgs e)
        {
            try
            {
                clear();
                setColor();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            if (Convert.ToInt32(selectedRow.Cells[4].Value) != 0)
            {
                textBox7.Text = Convert.ToString(selectedRow.Cells[5].Value);
            }

        }
        public void updateModificare(object sender, UpdateTabelEventArgs e)
        {
            dataGridView1.BeginInvoke(new UpdateTabelCallback(this.updateTabel), new Object[] { dataGridView1, e.Data });
        }

        private void updateTabel(DataGridView tabel, List<Excursie> newData)
        {
            tabel.ClearSelection();
            tabel.DataSource = newData;
            setColor();
            clear();
            
        }

        public delegate void UpdateTabelCallback(DataGridView tab, List<Excursie> data);

        private void button2_Click(object sender, EventArgs e)
        {
            //adauga rezervare
            if (textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
                string nume = textBox4.Text;
                string telefon = textBox5.Text;
                int locuri = Convert.ToInt32(textBox6.Text);
                int id = Convert.ToInt32(textBox7.Text);
                appController.addRezervare(nume, telefon, locuri, id);
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }
            else
            {
                MessageBox.Show("Selectati o excursie si completati casutele!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //logout
            appController.logout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //cauta
            string obiectiv = textBox1.Text;
            string dupa = textBox2.Text;
            string inainte = textBox3.Text;
            List<Excursie> filtrate = appController.cautaExcursii(obiectiv, dupa, inainte);
            dataGridView2.DataSource = filtrate;
            setColor();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";


        }
    }
}
