using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    public partial class DiscardEmployee : Form
    {
        DatabaseOperations databaseOperations = new DatabaseOperations();

        public DiscardEmployee()
        {
            InitializeComponent();
        }

        private void DiscardEmployee_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        private void LoadEmployeeData(string filter = "")
        {
            try
            {
                string query = "SELECT * FROM employee";
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(filter))
                {
                    // Added space before "employee_pk"
                    query += " WHERE ename LIKE @filter OR " +
                             "CAST(hiredate AS VARCHAR) LIKE @filter OR " +
                             "CAST(contact AS VARCHAR) LIKE @filter OR " +
                             "gender LIKE @filter OR " +
                             "eaddress LIKE @filter OR " +
                             "city LIKE @filter OR " +
                             "estate LIKE @filter OR " +
                             "employee_pk LIKE @filter";

                    parameters.Add(new SqlParameter("@filter", "%" + filter + "%"));
                }

                DataSet ds = databaseOperations.getData(query, parameters);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dataGridViewEmployees.DataSource = ds.Tables[0];
                }
                else
                {
                    dataGridViewEmployees.DataSource = null;
                    MessageBox.Show("No data found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSearchView_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadEmployeeData(searchTerm);
        }

        private void btnDeleteEmp_Click(object sender, EventArgs e)
        {

            if (dataGridViewEmployees.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewEmployees.SelectedRows[0];
                string employeeId = selectedRow.Cells["employee_pk"].Value.ToString();

                DialogResult result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Get the associated appuser_fk from the employee table
                        string queryGetUserId = "SELECT appuser_fk FROM employee WHERE employee_pk = @employeeId";
                        List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@employeeId", employeeId)
                };

                        DataSet ds = databaseOperations.getData(queryGetUserId, parameters);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            int appuserFk = Convert.ToInt32(ds.Tables[0].Rows[0]["appuser_fk"]);

                            // First delete from the employee table
                            string queryDeleteEmployee = "DELETE FROM employee WHERE employee_pk = @employeeId";
                            List<SqlParameter> deleteEmpParams = new List<SqlParameter>
                    {
                        new SqlParameter("@employeeId", employeeId)
                    };
                            databaseOperations.setData(queryDeleteEmployee, null, deleteEmpParams);

                            // Then delete from the appUser table
                            string queryDeleteUser = "DELETE FROM appUser WHERE appuser_pk = @appuserFk";
                            List<SqlParameter> userParams = new List<SqlParameter>
                    {
                        new SqlParameter("@appuserFk", appuserFk)
                    };
                            databaseOperations.setData(queryDeleteUser, "Employee deleted successfully.", userParams);

                            // Refresh the data
                            LoadEmployeeData();
                        }
                        else
                        {
                            MessageBox.Show("Employee data not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
