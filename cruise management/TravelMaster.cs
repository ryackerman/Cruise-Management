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
    public partial class TravelMaster : Form
    {
        public TravelMaster()
        {
            InitializeComponent();
            show();
            fillShipCode();
        }

        SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=cruiseDb;Integrated Security=True");

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void show()
        {
            con.Open();
            string query = "select * from TRAVEL";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            var ds = new DataSet();
            sda.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            con.Close();

        }

        private void changeStatus()
        {
            string shipStatus = "Busy";
                try
                {
                    con.Open();
                string Query = "update SHIP set shipStatus='" + shipStatus + "' where shipId=" + ShipCode.SelectedValue.ToString() + ";";
                SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Ship updated successfully");
                    con.Close();
                    show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (TrvCost.Text == "" || ShipCode.SelectedIndex == -1 || Source.SelectedIndex == -1 || Dest.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Informations");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "insert into TRAVEL values('" + dateTrv.Value + "','" + ShipCode.SelectedValue.ToString() + "','" + Source.SelectedItem.ToString() + "','" + Dest.SelectedItem.ToString() + "','" + TrvCost.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Travel added successfully");
                    con.Close();
                    show();
                    changeStatus();
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void fillShipCode()
        {
            string sSts = "Available";
            con.Open();
            SqlCommand cmd = new SqlCommand("select shipId from SHIP where shipStatus='"+sSts+"'", con);
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("shipId", typeof(int));
            dt.Load(sdr);
            ShipCode.ValueMember = "shipId";
            ShipCode.DataSource = dt;
            con.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (TrvCost.Text == "" || ShipCode.SelectedIndex == -1 || Source.SelectedIndex == -1 || Dest.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Informations");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "update TRAVEL set travDate='" + dateTrv.Value + "', Ship='" + ShipCode.SelectedValue.ToString() + "',src='" + Source.SelectedItem.ToString() + "',dest='" + Dest.SelectedItem.ToString() + "',cost='" + TrvCost.Text + "' where travCode= '"+key+"';";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Travel updated successfully");
                    con.Close();
                    show();
                    reset();
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
            dateTrv.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            ShipCode.SelectedValue = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            Source.SelectedItem = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            Dest.SelectedItem = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            TrvCost.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            if (dateTrv.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void reset()
        {
            dateTrv.Value = DateTime.Now;
            ShipCode.SelectedIndex = Source.SelectedIndex = Dest.SelectedIndex = -1;
            TrvCost.Text = "";
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the travel to be deleted");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "delete from TRAVEL where travCode=" + key + "";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Travel deleted successfully");
                    con.Close();
                    show();
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            mainF f = new mainF();
            f.Show();
            this.Hide();
        }
    }
}
