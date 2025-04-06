using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IncomeAndExpensesTrackingSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you close?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = true;
            categoryForm2.Visible = false;
            incomeForm2.Visible = false;
            expensesForm2.Visible = false;
        }

        private void category_btn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = false;
            categoryForm2.Visible = true;
            incomeForm2.Visible = false;
            expensesForm2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = false;
            categoryForm2.Visible = false;
            incomeForm2.Visible = true;
            expensesForm2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = false;
            categoryForm2.Visible = false;
            incomeForm2.Visible = false;
            expensesForm2.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();

                this.Hide();
            }
        }

      
    }
}
