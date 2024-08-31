using System;
using System.Linq;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        String role;
        public Dashboard(String role)
        {
            InitializeComponent();
            this.role = role;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            if ("Admin".Equals(role))
            {
                employeeToolStripMenuItem.Visible = true;
                addIsirorToolStripMenuItem.Visible = true;
                labelWelcomeText.Text = "Admin Dashboard";
            }
            else
            {
                employeeToolStripMenuItem.Visible = false;
                addIsirorToolStripMenuItem.Visible = false;
                labelWelcomeText.Text = "Employee Dashborad";
            }

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Log Out ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Hide();
                Loginform loginform = new Loginform();
                loginform.Show();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void addEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<AddEmployee>().Count() == 1)
            {
                Application.OpenForms.OfType<AddEmployee>().First().BringToFront();
            }
            else
            {
                AddEmployee addEmployee = new AddEmployee();
                addEmployee.Show();
            }
        }

        private void updateEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<UpdateEmployee>().Count() == 1)
            {
                Application.OpenForms.OfType<UpdateEmployee>().First().BringToFront();
            }
            else
            {
                UpdateEmployee updateEmployee = new UpdateEmployee();
                updateEmployee.Show();
            }
        }

        private void viewAllEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ViewEmployees>().Count() == 1)
            {
                Application.OpenForms.OfType<ViewEmployees>().First().BringToFront();
            }
            else
            {
                ViewEmployees viewEmployees = new ViewEmployees();
                viewEmployees.Show();
            }
        }

        private void discardEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<DiscardEmployee>().Count() == 1)
            {
                Application.OpenForms.OfType<DiscardEmployee>().First().BringToFront();
            }
            else
            {
                // Open a form to select or search for an employee to discard
                DiscardEmployee discardEmployee = new DiscardEmployee();
                discardEmployee.Show();
            }
        }

        private void viewVisitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ViewVisitors>().Count() == 1)
            {
                Application.OpenForms.OfType<ViewVisitors>().First().BringToFront();
            }

            else
            {
                ViewVisitors viewVisitors = new ViewVisitors();
                viewVisitors.Show();
            }

        }

        private void updateVisitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<UpdateVisitor>().Count() == 1)
            {
                Application.OpenForms.OfType<UpdateVisitor>().First().BringToFront();
            }
            else
            {
                UpdateVisitor updateVisitor = new UpdateVisitor();
                updateVisitor.Show();
            }
        }

        private void addVisitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<AddVisitor>().Count() == 1)
            {
                Application.OpenForms.OfType<AddVisitor>().First().BringToFront();
            }
            else
            {
                AddVisitor addVisitor = new AddVisitor();
                addVisitor.Show();
            }
        }


        private void filterPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FilterPass>().Count() == 1)
            {
                Application.OpenForms.OfType<FilterPass>().First().BringToFront();
            }
            else
            {
                FilterPass filterPass = new FilterPass();
                filterPass.Show();
            }
        }

        private void downloadPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ViewPass>().Count() == 1)
            {
                Application.OpenForms.OfType<ViewPass>().First().BringToFront();
            }
            else
            {
                ViewPass viewPass = new ViewPass();
                viewPass.Show();
            }
        }

        private void generatePassToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<GeneratePass>().Count() == 1)
            {
                Application.OpenForms.OfType<GeneratePass>().First().BringToFront();
            }
            else
            {
                GeneratePass generatePass = new GeneratePass();
                generatePass.Show();
            }
        }

        private void validatePassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ValidatePass>().Count() == 1)
            {
                Application.OpenForms.OfType<ValidatePass>().First().BringToFront();
            }
            else
            {
                ValidatePass validatePass = new ValidatePass();
                validatePass.Show();
            }
        }
    }
}
