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
using System.Data;
using System.Diagnostics;
using System.Net.Sockets;


namespace ex
{
    public partial class Form1 : Form
    {
        public SqlConnection con = new SqlConnection();
        public Form1()
        {
            InitializeComponent();
        }

        public void connecter()
        {
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
            {
                con.ConnectionString = @"data source=localhost; initial catalog=company; integrated security=true";
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("select * from users order by id desc");
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dtClient = new DataTable("users");
            dtClient.Load(dr);
            dataGridView1.DataSource = dtClient;
            dr.Close();
        }
        public void deconnecter()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                this.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connecter();
            var p = new Process();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Close();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(String.Format("select * from users where id = {0}", textBox4.Text));
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read() == true) {
                textBox1.Text = dr["id"].ToString();
                textBox2.Text = dr["nom"].ToString();
                textBox3.Text = dr["prenom"].ToString();
            }
            else
            {
                MessageBox.Show("Resultats est empty");
            }
        }
            
        

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = string.Format("insert into users(id, nom, prenom) values ('{0}', '{1}', '{2}')", textBox1.Text, textBox2.Text, textBox3.Text);
            cmd.Connection = con;
            int dr = (int)cmd.ExecuteNonQuery();
            MessageBox.Show("Mission Complete");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            connecter();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            connecter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = string.Format("delete from users where id = '{0}'", textBox1.Text);
            cmd.Connection = con;
            int dr = (int)cmd.ExecuteNonQuery();
            MessageBox.Show("Mission Complete");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            connecter();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = string.Format("update users set nom='{1}', prenom='{2}' where id={0}", textBox1.Text, textBox2.Text, textBox3.Text);
            cmd.Connection = con;
            int dr = (int)cmd.ExecuteNonQuery();
            MessageBox.Show("Mission Complete");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            connecter();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            deconnecter();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string command = String.Format("/C {0}", AppInput.Text);
            Process.Start("cmd.exe", command);
        }
    }
}
