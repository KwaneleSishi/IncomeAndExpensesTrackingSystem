using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace IncomeAndExpensesTrackingSystem
{
    public partial class ExpensesForm : UserControl
    {
        string stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\expense.mdf;Integrated Security=True;Connect Timeout=30";

        public ExpensesForm()
        {
            InitializeComponent();
            displayCategoryList();
            displayExpenseDataList();
        }

        public void RefreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)RefreshData);
                return;
            }

            displayCategoryList();
            displayExpenseDataList();
        }

        public void displayExpenseDataList()
        {
            ExpensesData eData = new ExpensesData();
            List<ExpensesData> listData = eData.expenseListData();

            dataGridView1.DataSource = listData;
        }

        public void displayCategoryList()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string selecttData = "SELECT category FROM categories WHERE type = @type AND status = @status";

                using (SqlCommand cmd = new SqlCommand(selecttData, connect))
                {
                    cmd.Parameters.AddWithValue("@type", "Expense");
                    cmd.Parameters.AddWithValue("@status", "Active");

                    expense_category.Items.Clear();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        expense_category.Items.Add(reader["category"].ToString());
                    }
                }

                connect.Close();
            }
        }

        private void expense_addBtn_Click(object sender, EventArgs e)
        {
            if (expense_category.SelectedIndex == -1 || expense_item.Text == "" || expense_cost.Text == "" || expense_description.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (SqlConnection connect = new SqlConnection(stringConnection))
                {
                    connect.Open();

                    string inserttData = "INSERT INTO  expenses (category, item, cost, description, date_expense, date_insert)" +
                                         "VALUES(@cat, @item, @cost, @desc, @date_ex, @date)";

                    using (SqlCommand cmd = new SqlCommand(inserttData, connect))
                    {
                        cmd.Parameters.AddWithValue("@cat", expense_category.SelectedItem);
                        cmd.Parameters.AddWithValue("@item", expense_item.Text.Trim());
                        cmd.Parameters.AddWithValue("@cost", expense_cost.Text.Trim());
                        cmd.Parameters.AddWithValue("@desc", expense_description.Text.Trim());
                        cmd.Parameters.AddWithValue("@date_ex", expense_date.Value);

                        DateTime today = DateTime.Today;
                        cmd.Parameters.AddWithValue("@date", today);

                        cmd.ExecuteNonQuery();
                        clearFields();

                        MessageBox.Show("Added successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }

                    connect.Close();
                }
            }
            displayExpenseDataList();
        }

        public void clearFields()
        {
            expense_category.SelectedIndex = -1;
            expense_item.Text = "";
            expense_cost.Text = "";
            expense_description.Text = "";
        }

        private void expense_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private int getID = 0;

        private void expense_updateBtn_Click(object sender, EventArgs e)
        {

            if (expense_category.SelectedIndex == -1 || expense_item.Text == "" || expense_cost.Text == "" || expense_description.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (MessageBox.Show("Are you sure you want to Update ID: " + getID + "?", "Confirmation Message",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection connect = new SqlConnection(stringConnection))
                    {
                        connect.Open();

                        string updateData = "UPDATE expenses SET category = @cat, item = @item, " +
                            "cost = @cost, description = @desc, date_expense = @date_ex WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(updateData, connect))
                        {

                            cmd.Parameters.AddWithValue("@cat", expense_category.SelectedItem);
                            cmd.Parameters.AddWithValue("@item", expense_item.Text.Trim());
                            cmd.Parameters.AddWithValue("@cost", expense_cost.Text.Trim());
                            cmd.Parameters.AddWithValue("@desc", expense_description.Text.Trim());
                            cmd.Parameters.AddWithValue("@date_ex", expense_date.Value);
                            cmd.Parameters.AddWithValue("@id", getID);

                            cmd.ExecuteNonQuery();
                            clearFields();

                            MessageBox.Show("Updated successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        }
                        connect.Close();
                    }
                }
            }
            displayExpenseDataList();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                getID = Convert.ToInt32(row.Cells[0].Value);
                expense_category.SelectedItem = row.Cells[1].Value.ToString();
                expense_item.Text = row.Cells[2].Value.ToString();
                expense_cost.Text = row.Cells[3].Value.ToString();
                expense_description.Text = row.Cells[4].Value.ToString();
                expense_date.Value = Convert.ToDateTime(row.Cells[5].Value.ToString());

            }
        }

        private void expense_deleteBtn_Click(object sender, EventArgs e)
        {
            if (expense_category.SelectedIndex == -1 || expense_item.Text == "" || expense_cost.Text == "" || expense_description.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Delete ID: " + getID + "?", "Confirmation Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection connect = new SqlConnection(stringConnection))
                    {
                        connect.Open();

                        string updateData = "DELETE FROM expenses WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(updateData, connect))
                        {
                            cmd.Parameters.AddWithValue("@id", getID);
                            cmd.ExecuteNonQuery();
                            clearFields();

                            MessageBox.Show("Delete successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                        connect.Close();
                    }
                }
                displayExpenseDataList();
            }
        }
    }
}
