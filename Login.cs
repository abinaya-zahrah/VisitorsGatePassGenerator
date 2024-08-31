using System;
using System.Data;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    public partial class Loginform : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        String query;
        public Loginform()
        {
            InitializeComponent();
            chkShowPassword.CheckedChanged += new EventHandler(chkShowPassword_CheckedChanged);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtpassword.UseSystemPasswordChar = true; // Ensure the password is hidden initially
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                query = "Select * from appUser where username='" + txtusername.Text + "' and upass ='" + txtpassword.Text + "' and uenabled=1";
                DataSet ds = databaseOperations.getData(query);
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    String role = ds.Tables[0].Rows[0][3].ToString();
                    Int64 appUserPK = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                    Dashboard dashboard = new Dashboard(role);
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Bad credentials, Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in btnlogin Click : " + ex);
                MessageBox.Show("Something went Wrong" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtpassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}
