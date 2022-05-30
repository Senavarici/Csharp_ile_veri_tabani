using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace sql_veri_tabani
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SQLiteConnection con;
        SQLiteCommand cmd;
        SQLiteDataAdapter da;
        DataSet ds;

        void GetList()
        {
            con = new SQLiteConnection("Data Source = kayit.db; Version = 3;");
            da = new SQLiteDataAdapter("Select *From Employee", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Employee");
            dataGridView1.DataSource = ds.Tables["Employee"];
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("kayit.db"))
            {

                SQLiteConnection.CreateFile("kayit.db");
                string sql = @"Create Table Employee(
                               Id INTEGER PRIMARY KEY AUTOINCREMENT,
                               FirstName Text  Not Null,
                               LastName  Text  Not Null,
                               WorkExperience
                            );";
                con = new SQLiteConnection("Data Source=kayit.db;Version=3;");
                con.Open();
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            GetList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new SQLiteCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Insert into Employee (FirstName,LastName,WorkExperience) values ('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            GetList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new SQLiteCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Update Employee set FirstName = '" + textBox2.Text + "',LastName = '" + textBox3.Text + "',WorkExperience ='" + textBox4.Text + "' where ID = " + textBox1.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            GetList();
      }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd = new SQLiteCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "delete from Employee where Id=" + textBox1.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            GetList();
        }
    }
}
