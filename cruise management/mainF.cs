using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cruise_management
{
    public partial class mainF : Form
    {
        public mainF()
        {
            InitializeComponent();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            CruiseMaster cf = new CruiseMaster();
            cf.Show();
            this.Hide();
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            PassengersMaster pf = new PassengersMaster();
            pf.Show();
            this.Hide();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            TravelMaster tf = new TravelMaster();
            tf.Show();
            this.Hide();
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            ReservationMaster rf = new ReservationMaster();
            rf.Show();
            this.Hide();
        }

        private void guna2PictureBox5_Click(object sender, EventArgs e)
        {
            CancellationMaster cf = new CancellationMaster();
            cf.Show();
            this.Hide();
        }

        private void guna2PictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {
            login lf = new login();
            lf.Show();
            this.Hide();
        }
    }
}
