using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.Data.SqlClient;

namespace Log_In
{
    public partial class LogIn : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["DBSC"].ConnectionString;
        public string ReceivedName { get; set; }
        public string ReceivedPassword { get; set; }
        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            textBox1.Text = ReceivedName;

            textBox3.Text = ReceivedPassword;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox3.Text != ""))
            {
                SqlConnection conn = new SqlConnection(cs);
                string query = "select * from registrationData where username = @user and password = @pass";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@user", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox3.Text);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.HasRows == true)
                {
                    MessageBox.Show("Log In Successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Log In Failed.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 register = new Form1();
            register.Show();
            this.Hide();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                errorProvider1.Icon = Properties.Resources.error;
                errorProvider1.SetError(textBox1, "Please set your name.");
            }
            else
            {
                errorProvider1.Icon = Properties.Resources.check;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.Focus();
                errorProvider2.Icon = Properties.Resources.error;
                errorProvider2.SetError(textBox3, "Please set your password.");
            }
            else
            {
                errorProvider2.Icon = Properties.Resources.check;
            }
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.BackColor = Color.MediumSlateBlue;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.DarkGray;
        }
    }
}
