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
    public partial class ReservationMaster : Form
    {
        public ReservationMaster()
        {
            InitializeComponent();
            show();
            fillPCode();
            fillTCode();
        }

        SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=cruiseDb;Integrated Security=True");

        private void show()
        {
            con.Open();
            string query = "select * from RESERVATION";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            var ds = new DataSet();
            sda.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            con.Close();

        }

        private void fillPCode()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select pId from PASSENGER", con);
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("pId", typeof(int));
            dt.Load(sdr);
            pId.ValueMember = "pId";
            pId.DataSource = dt;
            con.Close();
        }

        private void fillTCode()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select travCode from Travel", con);
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("travCode", typeof(int));
            dt.Load(sdr);
            trvCode.ValueMember = "travCode";
            trvCode.DataSource = dt;
            con.Close();
        }
        string pname;
        private void getPname()
        {
            con.Open();
            string mysql = "select * from PASSENGER where pId=" + pId.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(mysql, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                pname = dr["pName"].ToString();
            }
            con.Close();
            //MessageBox.Show(pname);
        }
        string date, src, dest;
        int cost;
        private void getTravel()
        {
            con.Open();
            string mysql = "select * from TRAVEL where travCode=" + trvCode.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(mysql, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                date = dr["travDate"].ToString();
                src = dr["src"].ToString();
                dest = dr["dest"].ToString();
                cost = Convert.ToInt32(dr["cost"].ToString());
            }
            con.Close();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void trvCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getTravel();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            mainF f = new mainF();
            f.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (pId.SelectedIndex == -1 || trvCode.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Informations");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "insert into RESERVATION values(" + pId.SelectedValue.ToString() + ",'" + pname + "','" + trvCode.SelectedValue.ToString() + "','" + date + "','" + src + "','" + dest + "'," + cost + ")";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Reservation Accepted");
                    con.Close();
                    show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getPname();
        }
    }
}
