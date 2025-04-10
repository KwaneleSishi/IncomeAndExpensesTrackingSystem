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
    public partial class Dashboard : UserControl
    {
        string stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\expense.mdf;Integrated Security=True;Connect Timeout=30";

        public Dashboard()
        {
            InitializeComponent();
            incomeTodayIncome();
            incomeYesterdayIncome();
            incomeThisMonth();
            incomeThisYear();

            loadTotalIncome();

            expenseTodayExpense();
            expenseYesterdayExpense();
            expenseThisMonth();
            expenseThisYear();

            loadTotalExpenses();
        }

        public void RefreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)RefreshData);
                return;
            }

            incomeTodayIncome();
            incomeYesterdayIncome();
            incomeThisMonth();
            incomeThisYear();

            loadTotalIncome();

            expenseTodayExpense();
            expenseYesterdayExpense();
            expenseThisMonth();
            expenseThisYear();

            loadTotalExpenses();

        }

        //Income 
        public void incomeTodayIncome()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string query = "SELECT SUM(income) FROM income WHERE date_income = @date_in";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@date_in", DateTime.Today);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal todayIncome = Convert.ToDecimal(result);
                        income_totalIncome.Text = $"R{todayIncome:N2}"; // e.g., R1,234.56
                    }
                    else
                    {
                        income_totalIncome.Text = "R0.00";
                    }
                }
            }
        }

            public void incomeYesterdayIncome()
            {
                using(SqlConnection connect = new SqlConnection(stringConnection))
                {
                    connect.Open();

                string query = @"
                            SELECT SUM(income) 
                            FROM income 
                            WHERE CONVERT(DATE, date_income) = DATEADD(day, -1, CAST(GETDATE() AS DATE))";


                     using (SqlCommand cmd = new SqlCommand(query, connect))
                     {
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            decimal yesterdayCost = Convert.ToDecimal(result);

                            income_yesterdayIncome.Text = $"R{yesterdayCost:N2}";
                        }
                        else
                        {
                            income_yesterdayIncome.Text = "R0.00";

                        }
                     }

                }    
            }

        public void incomeThisMonth()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string query = @"
                            SELECT SUM(income)
                            FROM income
                            WHERE YEAR(date_income) = @year AND MONTH(date_income) = @month";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    DateTime today = DateTime.Today;
                    cmd.Parameters.AddWithValue("@year", today.Year);
                    cmd.Parameters.AddWithValue("@month", today.Month);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal monthIncome = Convert.ToDecimal(result);
                        income_thisMonthIncome.Text = $"R{monthIncome:N2}";
                    }
                    else
                    {
                        income_thisMonthIncome.Text = "R0.00";
                    }
                }
            }
        }

        public void incomeThisYear()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string query = @"
                            SELECT SUM(income)
                            FROM income
                            WHERE YEAR(date_income) = @year";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    int year = DateTime.Today.Year;
                    cmd.Parameters.AddWithValue("@year", year);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal yearIncome = Convert.ToDecimal(result);
                        income_thisYearIncome.Text = $"R{yearIncome:N2}";
                    }
                    else
                    {
                        income_thisYearIncome.Text = "R0.00";
                    }
                }
            }
        }

        // Expenses
        public void expenseTodayExpense()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string query = "SELECT SUM(cost) FROM expenses WHERE date_expense = @today";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@today", DateTime.Today);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal todayExpense = Convert.ToDecimal(result);
                        expense_today.Text = $"R{todayExpense:N2}";
                    }
                    else
                    {
                        expense_today.Text = "R0.00";
                    }
                }
            }
        }

        public void expenseYesterdayExpense()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string query = @"
                            SELECT SUM(cost) 
                            FROM expenses 
                            WHERE CONVERT(DATE, date_expense) = DATEADD(DAY, -1, CAST(GETDATE() AS DATE))";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal yesterdayExpense = Convert.ToDecimal(result);
                        expense_yesterday.Text = $"R{yesterdayExpense:N2}";
                    }
                    else
                    {
                        expense_yesterday.Text = "R0.00";
                    }
                }
            }
        }

        public void expenseThisMonth()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string query = @"
                                SELECT SUM(cost)
                                FROM expenses
                                WHERE YEAR(date_expense) = @year AND MONTH(date_expense) = @month";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    DateTime today = DateTime.Today;
                    cmd.Parameters.AddWithValue("@year", today.Year);
                    cmd.Parameters.AddWithValue("@month", today.Month);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal monthExpense = Convert.ToDecimal(result);
                        expense_thisMonth.Text = $"R{monthExpense:N2}";
                    }
                    else
                    {
                        expense_thisMonth.Text = "R0.00";
                    }
                }
            }
        }

        public void expenseThisYear()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string query = @"
                            SELECT SUM(cost)
                            FROM expenses
                            WHERE YEAR(date_expense) = @year";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@year", DateTime.Today.Year);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal yearExpense = Convert.ToDecimal(result);
                        expense_thisYear.Text = $"R{yearExpense:N2}";
                    }
                    else
                    {
                        expense_thisYear.Text = "R0.00";
                    }
                }
            }
        }
        //Totals

        public void loadTotalIncome()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string query = "SELECT SUM(income) FROM income";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal totalIncome = Convert.ToDecimal(result);
                        total_income.Text = $"R{totalIncome:N2}";
                    }
                    else
                    {
                        total_income.Text = "R0.00";
                    }
                }
            }
        }

        public void loadTotalExpenses()
        {
            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string query = "SELECT SUM(cost) FROM expenses";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        decimal totalExpenses = Convert.ToDecimal(result);
                        total_expenses.Text = $"R{totalExpenses:N2}";
                    }
                    else
                    {
                        total_expenses.Text = "R0.00";
                    }
                }
            }
        }


        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void income_totalIncome_Click(object sender, EventArgs e)
        {

        }
    }
}
