using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    public partial class AddEmployee : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();
        String query;
        DataSet ds;

        public AddEmployee()
        {
            InitializeComponent();
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            // Populate the Gender ComboBox
            cmbGender.Items.Clear();  // Clear existing items, if any
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            cmbGender.Items.Add("Other");

            // Optionally, set a default value
            cmbGender.SelectedIndex = 0; // Selects the first item by default (e.g., "Male")
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetNextEmployeeId()
        {
            string nextId = "24HALEMPAC001"; // Default fallback value

            try
            {
                // Query to get the current highest employee ID
                string query = "SELECT TOP 1 employee_pk FROM employee ORDER BY employee_pk DESC";
                DataSet ds = databaseOperations.getData(query);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string lastId = ds.Tables[0].Rows[0]["employee_pk"].ToString();

                    // Extract the sequential part of the ID
                    string sequentialPart = lastId.Substring(10); // Assuming sequential part starts at position 10

                    // Increment the sequential part
                    int newSequentialNumber = int.Parse(sequentialPart) + 1;
                    nextId = $"24HALEMPAC{newSequentialNumber:D3}"; // Format with leading zeros
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetNextEmployeeId: " + ex.Message);
                MessageBox.Show("Error generating Employee ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return nextId;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Trim all input fields to remove leading and trailing spaces
                String name = txtName.Text.Trim();
                String hireDate = txtHireDate.Text.Trim();
                String contact = txtContact.Text.Trim();
                String gender = cmbGender.Text.Trim();
                String address = txtAddress.Text.Trim();
                String city = txtCity.Text.Trim();
                String state = txtState.Text.Trim();
                String userName = txtUserName.Text.Trim();
                String password = txtPassword.Text.Trim();
                String role = "Employee";  // Example role, you can change this as needed

                // Ensure no fields are empty
                if (!String.IsNullOrEmpty(name) &&
                    !String.IsNullOrEmpty(hireDate) &&
                    !String.IsNullOrEmpty(contact) &&
                    !String.IsNullOrEmpty(gender) &&
                    !String.IsNullOrEmpty(address) &&
                    !String.IsNullOrEmpty(city) &&
                    !String.IsNullOrEmpty(state) &&
                    !String.IsNullOrEmpty(userName) &&
                    !String.IsNullOrEmpty(password))
                {
                    Int64 contactInt = Int64.Parse(contact);

                    // Check if username already exists in appUser table
                    query = "SELECT * FROM appUser WHERE username = @username AND uenabled = 1";
                    ds = databaseOperations.getData(query, new List<SqlParameter>
            {
                new SqlParameter("@username", userName)
            });

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
                    {
                        // Insert into appUser
                        query = "INSERT INTO appUser (username, upass, urole, uenabled) OUTPUT INSERTED.appuser_pk " +
                                "VALUES (@username, @upass, @urole, 1)";
                        string appuser_fk = databaseOperations.executeInsertAndGetId(query, new List<SqlParameter>
                {
                    new SqlParameter("@username", userName),
                    new SqlParameter("@upass", password),
                    new SqlParameter("@urole", role)
                });

                        if (!string.IsNullOrEmpty(appuser_fk))
                        {
                            // Generate new employee ID
                            string newEmployeeId = GetNextEmployeeId();

                            // Insert into employee table with the generated ID
                            query = "INSERT INTO employee (employee_pk, ename, hiredate, contact, gender, eaddress, city, estate, appuser_fk) " +
                                    "VALUES (@employee_pk, @ename, @hiredate, @contact, @gender, @eaddress, @city, @estate, @appuser_fk)";
                            int rowsAffected = databaseOperations.executeNonQuery(query, new List<SqlParameter>
                    {
                        new SqlParameter("@employee_pk", newEmployeeId),
                        new SqlParameter("@ename", name),
                        new SqlParameter("@hiredate", hireDate),
                        new SqlParameter("@contact", contactInt),
                        new SqlParameter("@gender", gender),
                        new SqlParameter("@eaddress", address),
                        new SqlParameter("@city", city),
                        new SqlParameter("@estate", state),
                        new SqlParameter("@appuser_fk", appuser_fk)
                    });

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Employee added successfully! Employee ID: " + newEmployeeId, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearFormFields();
                            }
                            else
                            {
                                MessageBox.Show("Failed to add employee. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failed to add user. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username is linked with another account.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Make sure no fields are incomplete and try again!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Add Employee btnSave Click: " + ex);
                MessageBox.Show("Something went wrong! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ClearFormFields()
        {
            txtName.Text = "";
            txtHireDate.Text = "";
            txtContact.Text = "";
            cmbGender.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.onlyNumber(e);
        }

        private void btnResetUpdate_Click(object sender, EventArgs e)
        {
            ClearFormFields();
        }
    }
}
