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

namespace cruise_management
{
    public partial class CruiseMaster : Form
    {
        public CruiseMaster()
        {
            InitializeComponent();
            show();
        }

        SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=cruiseDb;Integrated Security=True");

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void show()
        {
            con.Open();
            string query = "select * from SHIP";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            var ds = new DataSet();
            sda.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string shipStatus = "";
            if (shipN.Text == "" || shipC.Text == "")
            {
                MessageBox.Show("Missing Informations");
            }
            else
            {
                if (RDbusy.Checked == true)
                {
                    shipStatus = "Busy";
                }
                else if (RDavail.Checked == true)
                {
                    shipStatus = "Available";
                }
                try
                {
                    con.Open();
                    string Query = "insert into SHIP values('" + shipN.Text + "'," + shipC.Text + ",'" + shipStatus + "')";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ship added successfully");
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
            shipN.Text = shipC.Text = "";
            RDavail.Checked = RDbusy.Checked = false;
            key = 0;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int key = 0;
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the ship to be deleted");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "delete from SHIP where shipId="+key+"";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ship deleted successfully");
                    con.Close();
                    show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            shipN.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            shipC.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            if (shipN.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string shipStatus = "";
            if (shipN.Text == "" || shipC.Text == "")
            {
                MessageBox.Show("Missing Informations");
            }
            else
            {
                if (RDbusy.Checked == true)
                {
                    shipStatus = "Busy";
                }
                else if (RDavail.Checked == true)
                {
                    shipStatus = "Available";
                }
                try
                {
                    con.Open();
                    string Query = "update SHIP set shipName='" + shipN.Text + "'," +
                        " shipCap='" + shipC.Text + "', shipStatus='" + shipStatus + "' where shipId="+key+";";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ship updated successfully");
                    con.Close();
                    show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            mainF f = new mainF();
            f.Show();
            this.Hide();
        }
    }
}
