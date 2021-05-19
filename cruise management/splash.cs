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
    public partial class splash : Form
    {
        public splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (guna2CircleProgressBar1.Value==100)
            {
                timer1.Stop();
                login fl = new login();
                fl.Show();
                this.Hide();
            }
            else
            {
                guna2CircleProgressBar1.Value += 3;
                guna2HtmlLabel1.Text = "Loading . . ." + guna2CircleProgressBar1.Value.ToString() + "%";
            }
        }

        private void splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
