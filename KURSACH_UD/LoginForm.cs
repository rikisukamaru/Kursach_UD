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

namespace KURSACH_UD
{
    public partial class LoginForm : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        public LoginForm()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
             SqlConnection sqlcon = new SqlConnection(@"Data Source=rikimaru;Initial Catalog=phService;Integrated Security=True");
             string query = "SELECT * from [User] WHERE login = '" + textBox1.Text.Trim() + "' AND password = '" + textBox2.Text.Trim() + "'";
             SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
             DataTable dtb = new DataTable();
             sda.Fill(dtb);
             if(dtb.Rows.Count == 1)
             {
                 MainForm mainForm= new MainForm();
                 this.Hide();
                 mainForm.Show();           
             }
             else
             {
                 MessageBox.Show("Check your username and password");
             }
             
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
