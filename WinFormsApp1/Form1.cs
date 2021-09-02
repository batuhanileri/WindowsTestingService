using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;
        SqlCommand command;

        public Form1()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(@"data source= DESKTOP-5KB93N3\SQLEXPRESS; Initial Catalog = SendMailDb; Integrated Security = True");

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string query = "INSERT INTO Sends(FirstName,LastName,Status,Information,Mail) VALUES (@FirstName,@LastName,@Status,@Information,@Mail) Select " +
               "SCOPE_Identity()";

                command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddWithValue("@FirstName", textBox1.Text);
                command.Parameters.AddWithValue("@LastName", textBox2.Text);
                command.Parameters.AddWithValue("@Mail", textBox3.Text);
                command.Parameters.AddWithValue("@Status", true);
                command.Parameters.AddWithValue("@Information", false);
                sqlConnection.Open();
                int id = Convert.ToInt32(command.ExecuteScalar());
                sqlConnection.Close();

            }
            catch (Exception)
            {

                throw;
            }

            this.Close();

        }
    }
}
