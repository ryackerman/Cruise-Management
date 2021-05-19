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
    public partial class CancellationMaster : Form
    {
        public CancellationMaster()
        {
            InitializeComponent();
            show();
            fillTCode();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=cruiseDb;Integrated Security=True");

        private void show()
        {
            con.Open();
            string query = "select * from CANCELLATION";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            var ds = new DataSet();
            sda.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            con.Close();

        }
        private void fillTCode()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select ticketId from RESERVATION", con);
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ticketId", typeof(int));
            dt.Load(sdr);
            ticketID.ValueMember = "ticketId";
            ticketID.DataSource = dt;
            con.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (ticketID.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Informations");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "insert into CANCELLATION values(" + ticketID.SelectedValue.ToString() + ",'"+DateTime.Today.Date+"')";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ticket Cancelled");
                    con.Close();
                    show();
                    remove();
                    fillTCode();    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void remove()
        {
                try
                {
                    con.Open();
                    string Query = "delete from RESERVATION where ticketId=" + ticketID.SelectedValue.ToString() + "";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
