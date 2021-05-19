using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace cruise_management
{
    public partial class PassengersMaster : Form
    {
        public PassengersMaster()
        {
            InitializeComponent();
            show(); 
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2RadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PassengersMaster_Load(object sender, EventArgs e)
        {
            var list = CultureInfo.GetCultures(CultureTypes.
                SpecificCultures).Select(p => new RegionInfo(p.Name).EnglishName).
                Distinct().OrderBy(s => s).ToList();
            cbNat.DataSource = list;
        }

        SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=cruiseDb;Integrated Security=True");

        private void show()
        {
            con.Open();
            string query = "select * from PASSENGER";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            var ds = new DataSet();
            sda.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            con.Close();

        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string gender = "";
            if (pName.Text == "" || pAdr.Text == "" || pPhone.Text == "")
            {
                MessageBox.Show("Missing Informations");
            }
            else
            {
                if (RDmale.Checked == true)
                {
                    gender = "Male";
                }
                else if (RDfemale.Checked == true)
                {
                    gender = "Female";
                }
                try
                {
                    con.Open();
                    string Query = "insert into PASSENGER values('" + pName.Text + "','" + pAdr.Text + "'" +
                        ",'" + gender + "','" + cbNat.SelectedItem.ToString() + "','" + pPhone.Text+"')";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger added successfully");
                    con.Close();
                    show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        private void reset()
        {
            pName.Text = pAdr.Text = pPhone.Text = "";
            cbNat.SelectedIndex = 1;
            RDfemale.Checked = RDmale.Checked = false;
            key = 0;
        }
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string gender = "";
            if (pName.Text == "" || pAdr.Text == "" || pPhone.Text == "")
            {
                MessageBox.Show("Missing Informations");
            }
            else
            {
                if (RDmale.Checked == true)
                {
                    gender = "Male";
                }
                else if (RDfemale.Checked == true)
                {
                    gender = "Female";
                }
                try
                {
                    con.Open();
                    string Query = "update PASSENGER set pName='" + pName.Text + "'," +
                        " pAdr='" + pAdr.Text + "', pGender='" + gender + "', pNat='" + cbNat.SelectedItem.ToString() + "', pPhone='"+pPhone.Text+"' where pId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger added successfully");
                    con.Close();
                    show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int key = 0;
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pName.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            pAdr.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            cbNat.SelectedItem = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            pPhone.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            if (pName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the passenger to be deleted");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "delete from PASSENGER where pId=" + key + "";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger deleted successfully");
                    con.Close();
                    show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            mainF mf = new mainF();
            mf.Show();
            this.Hide();
        }
    }
}
