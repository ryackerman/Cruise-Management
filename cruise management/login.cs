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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (emailTxt.Text == "" || mdpTxt.Text == "")
            {
                MessageBox.Show("Please enter your username/Email and password!");
            }
            else if(emailTxt.Text=="khalid" && mdpTxt.Text == "123")
            {
                mainF mf = new mainF();
                mf.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect email/password!");
            }
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
