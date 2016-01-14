using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using System.Data;


namespace CompleteTest
{
    public partial class Podatci : Form
    {

        public Podatci()
        {
            InitializeComponent();
            loadTable();
            fillCombo();
            fillComboKorisnik();
        }

        void loadTable()
        {
            commSelect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            commInsert();
            loadTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            commDelete();
            loadTable();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            commUpdate();
            loadTable();
        }

        private void Podatci_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection())
            {
                //kreiranje konekcije
                conn.ConnectionString = "datasource=localhost;port=3306;username=root;password=root";
                MySqlCommand comm = new MySqlCommand("SELECT * FROM testdata.users_books WHERE aouthor = '" + comboBox1.Text + "';", conn);
                MySqlDataReader sReader;

                try
                {
                    conn.Open();
                    sReader = comm.ExecuteReader();

                    while (sReader.Read())
                    {
                        int sId = sReader.GetInt32("id");
                        string sBook = sReader.GetString("books");
                        string sName = sReader.GetString("aouthor");

                        string idTxt = sId.ToString();
                        id_txt.Text = idTxt;
                        knjiga_txt.Text = sBook;
                        autor_txt.Text = sName;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("ne može napuniti ComboBox!!!");
                }
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection())
            {
                try
                {
                    //kreiranje konekcije
                    conn.ConnectionString = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(@"SELECT users.user_name, users_books.books FROM users LEFT JOIN users_books ON testdata.users.id = testdata.users_books.users_id WHERE testdata.users.user_name = '" + comboBox2.Text + "';", conn);

                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView2.DataSource = table;
                }
                catch (Exception)
                {
                    MessageBox.Show("Nemože JOIN!!!");
                }
            }
        }

        void commSelect()
        {
            using (MySqlConnection conn = new MySqlConnection())
            {
                //kreiranje konekcije
                conn.ConnectionString = "datasource=localhost;port=3306;username=root;password=root";
                MySqlCommand comm = new MySqlCommand("SELECT id, books, aouthor FROM testdata.users_books;", conn);

                try
                {
                    conn.Open();
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = comm;
                    DataTable dt = new DataTable();
                    myAdapter.Fill(dt);

                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dataGridView1.DataSource = bSource;

                    dataGridView1.Columns[0].Width = 25;

                    myAdapter.Update(dt);
                }
                catch (Exception)
                {
                    MessageBox.Show("Nemože napunit NE RADI!!!");
                }
            }
        }
        void commDelete()
        {
            using (MySqlConnection conn = new MySqlConnection())
            {
                //kreiranje konekcije
                conn.ConnectionString = "datasource=localhost;port=3306;username=root;password=root";
                MySqlCommand comm = new MySqlCommand("DELETE FROM testdata.users_books WHERE id = " + this.delete_id.Text + ";", conn);
                try
                {
                    conn.Open();
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = comm;
                    DataTable dt = new DataTable();
                    myAdapter.Fill(dt);

                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dataGridView1.DataSource = bSource;
                    myAdapter.Update(dt);
                }
                catch (Exception)
                {
                    MessageBox.Show("Nemože UPDATE!!!");
                }
            }
        }
        void commInsert()
        {
            using (MySqlConnection conn = new MySqlConnection())
            {
                //kreiranje konekcije
                conn.ConnectionString = "datasource=localhost;port=3306;username=root;password=root";
                MySqlCommand comm = new MySqlCommand("INSERT INTO testdata.users_books (books, aouthor)  VALUES ('" + this.knjiga_txt.Text + "', '" + this.autor_txt.Text + "');", conn);

                try
                {
                    conn.Open();
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = comm;
                    DataTable dt = new DataTable();
                    myAdapter.Fill(dt);

                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dataGridView1.DataSource = bSource;
                    myAdapter.Update(dt);
                }
                catch (Exception)
                {
                    MessageBox.Show("Nemože UPDATE!!!");
                }

            }
        }
        void commUpdate()
        {
            using (MySqlConnection conn = new MySqlConnection())
            {
                //kreiranje konekcije
                conn.ConnectionString = "datasource=localhost;port=3306;username=root;password=root";
                MySqlCommand comm = new MySqlCommand("UPDATE testdata.users_books SET books = '" + this.knjiga_txt.Text
                    + "', aouthor = '" + this.autor_txt.Text
                    + "' WHERE id = " + this.id_txt.Text + ";", conn);

                try
                {
                    conn.Open();
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    myAdapter.SelectCommand = comm;
                    DataTable dt = new DataTable();
                    myAdapter.Fill(dt);

                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = dt;
                    dataGridView1.DataSource = bSource;
                    myAdapter.Update(dt);
                }
                catch (Exception)
                {
                    MessageBox.Show("Nemože UPDATE!!!");
                }
            }
        }

        void fillCombo()
        {
            using (MySqlConnection conn = new MySqlConnection())
            {
                //kreiranje konekcije
                conn.ConnectionString = "datasource=localhost;port=3306;username=root;password=root";
                MySqlCommand comm = new MySqlCommand("SELECT * FROM testdata.users_books;", conn);
                MySqlDataReader sReader;

                try
                {
                    conn.Open();
                    sReader = comm.ExecuteReader();

                    while (sReader.Read())
                    {
                        string sName = sReader.GetString("aouthor");
                        comboBox1.Items.Add(sName);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("ne može napuniti ComboBox!!!");
                }
            }
        }
        void fillComboKorisnik()
        {
            using (MySqlConnection conn = new MySqlConnection())
            {
                //kreiranje konekcije
                conn.ConnectionString = "datasource=localhost;port=3306;username=root;password=root";
                MySqlCommand comm = new MySqlCommand("SELECT * FROM testdata.users;", conn);
                MySqlDataReader sReader;

                try
                {
                    conn.Open();
                    sReader = comm.ExecuteReader();

                    while (sReader.Read())
                    {
                        string sName = sReader.GetString("user_name");
                        comboBox2.Items.Add(sName);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("ne može napuniti ComboBox!!!");
                }
            }
        }

    }
}
