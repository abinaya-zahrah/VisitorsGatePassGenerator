using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    public partial class ViewVisitors : Form
    {
        private DatabaseOperations databaseOperations; // Instance of DatabaseOperations

        public ViewVisitors()
        {
            InitializeComponent();
            databaseOperations = new DatabaseOperations();
        }

        // Load employee data into the DataGridView
        private void LoadVisitorData(string filter = "")
        {
            try
            {
                // Check if databaseOperations is null
                if (databaseOperations == null)
                {
                    MessageBox.Show("DatabaseOperations is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = "SELECT * FROM Visitors";
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(filter))
                {
                    query += " WHERE Name LIKE @filter OR " +
                             "EmailAddress LIKE @filter OR " +
                             "CAST(Contact AS VARCHAR) LIKE @filter OR " +
                             "CAST(VisitDate AS VARCHAR) LIKE @filter OR " +
                             "CAST(VisitTime AS VARCHAR) LIKE @filter OR " +
                             "Gender LIKE @filter OR " +
                             "Address LIKE @filter OR " +
                             "City LIKE @filter OR " +
                             "State LIKE @filter OR " +
                             "UniqueID LIKE @filter OR " +
                             "PurposeOfVisit LIKE @filter OR " +
                             "Department LIKE @filter OR " +
                             "IDProofType LIKE @filter OR " +
                             "IDNumber LIKE @filter";

                    parameters.Add(new SqlParameter("@filter", "%" + filter + "%"));
                }

                DataSet ds = databaseOperations.getData(query, parameters);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dataGridViewVisitors.DataSource = ds.Tables[0];
                    dataGridViewVisitors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    dataGridViewVisitors.DataSource = null;
                    MessageBox.Show("No visitors found matching the criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchView_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadVisitorData(searchTerm);
        }

        private void btnUpdateVis_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure dataGridViewVisitors is not null
                if (dataGridViewVisitors == null)
                {
                    MessageBox.Show("The data grid view is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if any rows are selected
                if (dataGridViewVisitors.SelectedRows.Count == 1)
                {
                    DataGridViewRow selectedRow = dataGridViewVisitors.SelectedRows[0];

                    // Check if the UniqueID cell exists and is not null
                    if (selectedRow.Cells["UniqueID"] != null && selectedRow.Cells["UniqueID"].Value != null)
                    {
                        string uniqueID = selectedRow.Cells["UniqueID"].Value.ToString();
                        this.Close();

                        // Open the UpdateVisitor form with the selected visitor's details
                        UpdateVisitor updateVisitorForm = new UpdateVisitor(uniqueID);

                        // Show the UpdateVisitor form as a dialog
                        updateVisitorForm.ShowDialog();

                        // After closing the UpdateVisitor form, reload the data to reflect any updates
                        LoadVisitorData(txtSearch.Text.Trim()); // Use the search term to reload data if necessary
                    }
                    else
                    {
                        MessageBox.Show("The selected row does not contain a UniqueID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a Visitor to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening update form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ViewVisitors_Load(object sender, EventArgs e)
        {
            LoadVisitorData();
        }

    }
}
