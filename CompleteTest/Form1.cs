using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CompleteTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (!checkBox1.Checked)
            {
                pass_txt.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection())
                {
                    //kreiranje konekcije
                    conn.ConnectionString = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlCommand comm = new MySqlCommand("SELECT * FROM testdata.users WHERE BINARY user_name = '" + this.user_name.Text + "' AND user_pass = '" + this.pass_txt.Text + "';", conn);
                    conn.Open();
                    MySqlDataReader read = comm.ExecuteReader();
                  
                        int count = 0;
                        while (read.Read())
                        {
                            count = count + 1;
                        }

                        if (count == 1) 
                        {
                            MessageBox.Show("Povezan na bazu!");
                            this.Hide();
                            Podatci p = new Podatci();
                            p.ShowDialog();
                        }
                        else if (count > 1)
                        {
                            MessageBox.Show("Dupla registracija!!");
                        }
                        else if (user_name.Text == "" && pass_txt.Text == "") 
                        {
                            MessageBox.Show("Niste ništa upisali!!!");
                        }
                        else
                        {
                            MessageBox.Show("Krivi user ili pass!! \nProbajte ponovo");
                        }
                        read.Close();
                        conn.Close();
                    }                    
                
            }
            catch (Exception) {
                MessageBox.Show("Greška!");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                pass_txt.UseSystemPasswordChar = true;
            }
            else
            {
                pass_txt.UseSystemPasswordChar = false;
            }
        }      
    }
}
