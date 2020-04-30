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
    public partial class LoginWindow : Form
    {
        private LoginController loginController;

        public LoginWindow(LoginController loginController)
        {
            InitializeComponent();
            this.loginController = loginController;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try {
                    loginController.login(textBox1.Text, textBox2.Text);
                    }
                catch
                {
                    MessageBox.Show("Datele sunt gresite sau userul este logat deja!");
                }
            }
            else
            {
                MessageBox.Show("Introduceti date in ambele casute!");
            }
        }
        public void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

    }
}
