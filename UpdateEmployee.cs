using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    public partial class UpdateEmployee : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        private String employeeId;

        public UpdateEmployee(string empId = null)
        {
            InitializeComponent();
            employeeId = empId; // Set employeeId if provided
        }

        private void UpdateEmployee_Load(object sender, EventArgs e)
        {

            // Populate the Gender ComboBox
            cmbGender.Items.Clear();  // Clear existing items, if any
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            cmbGender.Items.Add("Other");

            // Optionally, set a default value
            cmbGender.SelectedIndex = 0; // Selects the first item by default (e.g., "Male")

            if (!string.IsNullOrEmpty(employeeId))
            {
                LoadEmployeeData();
            }
        }


        private void LoadEmployeeData()
        {
            try
            {
                string query = @"
                    SELECT e.*, a.username 
                    FROM employee e
                    INNER JOIN appUser a ON e.appuser_fk = a.appuser_pk
                    WHERE e.employee_pk = @employeeId";

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@employeeId", employeeId)
                };

                DataSet ds = databaseOperations.getData(query, parameters);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    txtName.Text = row["ename"].ToString();
                    txtHireDate.Value = Convert.ToDateTime(row["hiredate"]);
                    txtContact.Text = row["contact"].ToString();
                    cmbGender.Text = row["gender"].ToString();
                    txtAddress.Text = row["eaddress"].ToString();
                    txtCity.Text = row["city"].ToString();
                    txtState.Text = row["estate"].ToString();
                    txtUpUsername.Text = row["username"].ToString(); // Set username
                }
                else
                {
                    MessageBox.Show("Employee not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsUsernameUnique(string username)
        {
            string query = "SELECT COUNT(*) FROM appUser WHERE username = @username AND appuser_pk <> (SELECT appuser_fk FROM employee WHERE employee_pk = @employeeId)";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@username", username),
                new SqlParameter("@employeeId", employeeId)
            };

            int count = Convert.ToInt32(databaseOperations.getData(query, parameters).Tables[0].Rows[0][0]);
            return count == 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                DateTime hireDate = txtHireDate.Value; // Use DateTimePicker
                string contact = txtContact.Text.Trim();
                string gender = cmbGender.Text.Trim();
                string address = txtAddress.Text.Trim();
                string city = txtCity.Text.Trim();
                string state = txtState.Text.Trim();
                string username = txtUpUsername.Text.Trim();

                // Ensure no fields are empty
                if (!String.IsNullOrEmpty(name) &&
                    !String.IsNullOrEmpty(contact) &&
                    !String.IsNullOrEmpty(gender) &&
                    !String.IsNullOrEmpty(address) &&
                    !String.IsNullOrEmpty(city) &&
                    !String.IsNullOrEmpty(state) &&
                    !String.IsNullOrEmpty(username))
                {
                    // Check if the new username is unique
                    if (!IsUsernameUnique(username))
                    {
                        MessageBox.Show("The username is already taken. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update employee and username in the respective tables
                    string query = @"
                        UPDATE employee SET 
                            ename = @name,
                            hiredate = @hireDate,
                            contact = @contact,
                            gender = @gender,
                            eaddress = @address,
                            city = @city,
                            estate = @state
                        WHERE employee_pk = @employeeId;

                        UPDATE appUser SET
                            username = @username
                        WHERE appuser_pk = (
                            SELECT appuser_fk FROM employee WHERE employee_pk = @employeeId
                        );";

                    var parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@name", name),
                        new SqlParameter("@hireDate", hireDate),
                        new SqlParameter("@contact", contact),
                        new SqlParameter("@gender", gender),
                        new SqlParameter("@address", address),
                        new SqlParameter("@city", city),
                        new SqlParameter("@state", state),
                        new SqlParameter("@username", username),
                        new SqlParameter("@employeeId", employeeId)
                    };

                    databaseOperations.executeNonQuery(query, parameters);
                    MessageBox.Show("Employee updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFormFields();
                }
                else
                {
                    MessageBox.Show("You are required to fill all fields to proceed with the Update!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFormFields()
        {
            txtEmpId.Text = "";
            txtName.Text = "";
            txtHireDate.Text = "";
            txtContact.Text = "";
            cmbGender.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtUpUsername.Text = "";

        }

        private void btnGetId_Click(object sender, EventArgs e)
        {
            employeeId = txtEmpId.Text.Trim(); // Directly assign string
            if (!string.IsNullOrEmpty(employeeId))
            {
                LoadEmployeeData();
            }
            else
            {
                MessageBox.Show("Please enter a valid employee ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEmpId_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearFormFields();
        }


        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.onlyNumber(e);
        }

    }
}

