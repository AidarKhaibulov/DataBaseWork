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
    public partial class Form2 : Form
    {
    
        List<string> l2;
        ComboBox c2;
        OleDbConnection myCon;
        public Form2(List<string> l, OleDbConnection myConnection,ComboBox c, int current)
        {

            InitializeComponent();
            if (current == 1)
            {
                button1.Visible = true;
                l2 = l;
                c2 = c;
                int j = -1;
                myCon = myConnection;
                for (int i = 1; i < l.Count; i++)
                {
                    j++;
                    if (j == 5) j = -1;
                    this.Controls.Add(new Label() { Name = "label" + i.ToString(), Location = new Point(100 * j, 10+ 70*(i / 6)), Text = "Write " + l[i].ToString() });
                    this.Controls.Add(new TextBox() { Name = "textbox" + i.ToString(), Location = new Point(100 * j, 30 + 70 * (i / 6)), Text = "" });

                }
            }
            if (current == 2)
            {
                button2.Visible = true;
                l2 = l;
                c2 = c;
                myCon = myConnection;
                int j = -1;
                for (int i = 0; i < l.Count; i++)
                {
                    j++;
                    if (j == 5) j = -1;
                    this.Controls.Add(new Label() { Name = "label" + i.ToString(), Location = new Point(100 * j, 10+70 * (i / 6)), Text = "Write " + l[i].ToString() });
                    this.Controls.Add(new TextBox() { Name = "textbox" + i.ToString(), Location = new Point(100 * j, 30 + 70 * (i / 6)), Text = "" });

                }
            }
            if (current == 3)
            {
                button3.Visible = true;
                l2 = l;
                c2 = c;
                myCon = myConnection;
               
                    this.Controls.Add(new Label() { Name = "label" + '0', Location = new Point(100 * 0, 10), Text = "Write " + l[0].ToString() });
                    this.Controls.Add(new TextBox() { Name = "textbox" + '0', Location = new Point(100 * 0, 30), Text = "" });

            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string line = "";
            string line1 = "";
            for (int i = 1; i < l2.Count - 1; i++)
            {
                line += l2[i] + ", ";
           
                line1 += "'"+this.Controls.Find("textbox" + i.ToString(), false).FirstOrDefault().Text+"'"+", ";
            }
            line += l2[l2.Count-1];

            line1 += "'" + this.Controls.Find("textbox" + (l2.Count - 1).ToString(), false).FirstOrDefault().Text+"'";
            Form1 f = new Form1();
           
            string query = "INSERT INTO " + c2.Text + " (" + line + " )" + " VALUES ("+ line1+")";
            MessageBox.Show(query);
            OleDbCommand command = new OleDbCommand(query, myCon);
            command.ExecuteNonQuery();
        
            this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
   
            string line1 = "";
            for (int i = 1; i < l2.Count - 1; i++)          
                line1 += l2[i] + " = ' "+ this.Controls.Find("textbox" + i.ToString(), false).FirstOrDefault().Text + " ', ";
         
            line1 += l2[l2.Count - 1] + " = ' " + this.Controls.Find("textbox" + (l2.Count - 1).ToString(), false).FirstOrDefault().Text + " '";          
            Form1 f = new Form1();
            MessageBox.Show("UPDATE " + c2.Text + " SET " + line1 + " WHERE " + l2[0] + " = " + this.Controls.Find("textbox" + (0).ToString(), false).FirstOrDefault().Text);
            string query = "UPDATE " + c2.Text + " SET " + line1 + " WHERE " + l2[0] + " = " + this.Controls.Find("textbox" + (0).ToString(), false).FirstOrDefault().Text ;
           
            OleDbCommand command = new OleDbCommand(query, myCon);
            command.ExecuteNonQuery();

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Form1 f = new Form1();
            //MessageBox.Show("DELETE FROM " + c2.Text + " WHERE " + l2[0] + " = " + this.Controls.Find("textbox" + (0).ToString(), false).FirstOrDefault().Text);
            string query = "DELETE FROM " + c2.Text + " WHERE " + l2[0] + " = " + this.Controls.Find("textbox" + (0).ToString(), false).FirstOrDefault().Text;
            OleDbCommand command = new OleDbCommand(query, myCon);
            command.ExecuteNonQuery();

            this.Close();
        }
    }
}
