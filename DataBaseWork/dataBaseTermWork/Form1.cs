using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dataBaseTermWork
{
    public partial class Form1 : Form
    {
        int current;
        public List<string> l;
   

        public static string connectString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Environment.CurrentDirectory + @"\DB.mdb;";
        //public static string connectString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\79021\source\repos\dataBaseTermWork\DB.mdb;";
        private OleDbConnection myConnection;
        DataSet ds = new DataSet();

           
        public Form1()
        {
            InitializeComponent();
           

            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            listBox1.HorizontalScrollbar = true;
            listBox1.ScrollAlwaysVisible = true;
            comboBox1.Items.Clear();
            OleDbConnection myCon = new OleDbConnection(
            connectString);
            /*OleDbConnection myCon = new OleDbConnection(
             @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DB.mdb;");*/
            myCon.Open();
            DataTable tbls = myCon.GetSchema("Tables", new string[] { null, null, null, "TABLE" }); //список всех таблиц
            foreach (DataRow row in tbls.Rows)
            {
                string TableName = row["TABLE_NAME"].ToString();
                comboBox1.Items.Add(TableName);
            }
            myCon.Close();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ;         
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
   
            myConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            OleDbConnection myCon = new OleDbConnection(
             @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DB.mdb;");
            myCon.Open();
            DataTable tbls = myCon.GetSchema("Tables", new string[] { null, null, null, "TABLE" }); //список всех таблиц
            foreach (DataRow row in tbls.Rows)
            {
                string TableName = row["TABLE_NAME"].ToString();
                comboBox1.Items.Add(TableName);
            }
            myCon.Close();   
        }//unimportant

      
        public void button3_Click(object sender, EventArgs e)
        {
            
            if (comboBox1.Text=="")
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("CНАЧАЛА ВЫБЕРИТЕ ТАБЛИЦУ");
            }
            else
            {
                l = new List<string>();

                string query1 = "SELECT " + comboBox1.Text + ".* FROM " + comboBox1.Text;
                OleDbCommand command = new OleDbCommand(query1, myConnection);
                OleDbDataReader reader = command.ExecuteReader();
                listBox1.Items.Clear();
                DataSet ds1 = new DataSet();
                OleDbDataAdapter myAdapter = new OleDbDataAdapter("SELECT * from " + comboBox1.Text, myConnection);
                myAdapter.Fill(ds1, "w_name");
                string line = null;
                for (int i = 0; i < ds1.Tables[0].Columns.Count; i++)
                {
                    l.Add(ds1.Tables[0].Columns[i].ColumnName);
                    line += ds1.Tables[0].Columns[i].ColumnName + "\t" + "\t";
                }

                listBox1.Items.Add(line);
                while (reader.Read())
                {

                    line = "";
                    for (int i = 0; i < ds1.Tables[0].Columns.Count; i++)
                    {

                        line += reader[i].ToString() + "\t" + "\t";
                    }
                    listBox1.Items.Add(line);
                }
                reader.Close();
            }
       
        }//download\refresh table

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (l==null)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("CНАЧАЛА ВЫБЕРИТЕ ТАБЛИЦУ");
            }
            else
            {
                current = 1;
                Form2 f = new Form2(l, myConnection, comboBox1, current);
                f.ShowDialog();
                button3_Click(null, null);
            }
        }//add data

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (l == null)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("CНАЧАЛА ВЫБЕРИТЕ ТАБЛИЦУ");
            }
            else
            {
                current = 2;
                Form2 f = new Form2(l, myConnection, comboBox1, current);
                f.ShowDialog();
                button3_Click(null, null);
            }
        }//edit data

        private void button4_Click(object sender, EventArgs e)
        {
            if (l == null)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("CНАЧАЛА ВЫБЕРИТЕ ТАБЛИЦУ");
            }
            else
            {
                current = 3;
                Form2 f = new Form2(l, myConnection, comboBox1, current);
                f.ShowDialog();
                button3_Click(null, null);
            }
        }//remove data
    }
}
