using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace IncomeAndExpensesTrackingSystem
{
    class CategoryData
    {
        string stringConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\expense.mdf;Integrated Security=True;Connect Timeout=30";
        public int Id { get; set; } // 0
        public string Category { get; set; } // 1
        public string Type { get; set; } // 2
        public string Status { get; set; } // 3
        public string Date { get; set; } // 4

        public List<CategoryData> categoryListData()
        {
            List<CategoryData> listData = new List<CategoryData>();

            using (SqlConnection connect = new SqlConnection(stringConnection))
            {
                connect.Open();

                string selectData = "SELECT * FROM categories";

                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                { 
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) 
                    {
                        CategoryData cData = new CategoryData();
                        cData.Id = (int)reader["id"];
                        cData.Category = reader["category"].ToString();
                        cData.Type = reader["type"].ToString();
                        cData.Status = reader["status"].ToString();
                        cData.Date =((DateTime)reader["date_insert"]).ToString("dd-mm-yyyy");

                        listData.Add(cData);
                    }

                }

            }
            return listData;

        }
    }
}
