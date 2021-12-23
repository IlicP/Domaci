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

namespace firmavs
{
    public partial class Form1 : Form
    {

        DataTable info = new DataTable();

        int rows = 0;
        string cs = "Data source=DESKTOP-83FS8TR; Initial catalog=firmavs; Integrated security=true";


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection(cs);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from info ", veza);
            adapter.Fill(info);

            refresh(rows);

            if (rows == 0)
            {
                button4.Enabled = false;
            }
            if (rows == info.Rows.Count - 1)
            {
                button7.Enabled = false;
            }
        }

        private void refresh(int x)
        {
            textBox1.Text = info.Rows[x]["id"].ToString();
            textBox2.Text = info.Rows[x]["naziv"].ToString();
            textBox3.Text = info.Rows[x]["pib"].ToString();
            textBox4.Text = info.Rows[x]["adresa"].ToString();
            textBox5.Text = info.Rows[x]["imejl"].ToString();
            textBox6.Text = info.Rows[x]["tek_racun"].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {


            SqlConnection veza = new SqlConnection(cs);
            SqlCommand naredba = new SqlCommand("insert into info (id , naziv, pib, adresa, imejl, tek_racun) values (" + textBox1.Text + ", '" + textBox2.Text + "' ,'" + textBox3.Text + "', '" + textBox4.Text + "' , '" + textBox5.Text + "' ,'" + textBox6.Text + "' ) ", veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from info", veza);
            info.Clear();
            adapter.Fill(info);
            refresh(rows);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection(cs);
            SqlCommand naredba = new SqlCommand("delete from info where id=" + textBox1.Text, veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from info", veza);
            info.Clear();
            adapter.Fill(info);
            if (rows == info.Rows.Count) rows = rows - 1;
            if (rows == 0)
            {
                button2.Enabled = false;
            }
            if (info.Rows.Count > 1)
            {
                button3.Enabled = true;
            }
            if (rows == info.Rows.Count - 1)
            {
                button3.Enabled = false;
            }

            refresh(rows);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection(cs);
            SqlCommand naredba = new SqlCommand("Update info Set naziv= '" + textBox2.Text + "' , pib= '" + textBox3.Text + "', adresa= '" + textBox4.Text + "' , imejl= '" + textBox5.Text + "' , tek_racun= '" + textBox6.Text + "'  where id= " + textBox1.Text, veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from info", veza);
            info.Clear();
            adapter.Fill(info);
            refresh(rows);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rows = 0;
            refresh(rows);
            button2.Enabled = false;
            button3.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (rows < info.Rows.Count - 1)
            {
                rows++;
                refresh(rows);
                button2.Enabled = true;
            }
            if (rows == info.Rows.Count - 1)
            {
                button3.Enabled = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (rows > 0)
            {
                rows--;
                refresh(rows);
                button3.Enabled = true;
            }
            if (rows == 0)
            {
                button2.Enabled = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            rows = info.Rows.Count - 1;
            refresh(rows);
            button2.Enabled = true;
            button3.Enabled = false;
        }
    }
}