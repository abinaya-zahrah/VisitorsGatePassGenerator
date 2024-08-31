using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    public partial class FilterPass : Form
    {
        private DatabaseOperations dbOps = new DatabaseOperations();

        public FilterPass()
        {
            InitializeComponent();
        }

        private void FilterPass_Load(object sender, EventArgs e)
        {
            // Initialize combo box items
            cmbValidity.Items.AddRange(new[] { "Valid", "Expired", "Not Yet Validated" });
            cmbValidity.SelectedIndex = 0; // Set default selection to "Valid"

            // Load all pass data by default
            LoadPassData();
        }

        private void LoadPassData(string filterName = "", DateTime? startDate = null, DateTime? endDate = null, string validity = "All")
        {
            // Start with a base query
            string query = "SELECT * FROM Pass WHERE 1=1";
            List<SqlParameter> parameters = new List<SqlParameter>();

            // Check each filter and modify the query accordingly
            if (!string.IsNullOrEmpty(filterName))
            {
                query += " AND Visitor_Name LIKE @VisitorName";
                parameters.Add(new SqlParameter("@VisitorName", "%" + filterName + "%"));
            }
            if (startDate.HasValue)
            {
                query += " AND Valid_from >= @StartDate";
                parameters.Add(new SqlParameter("@StartDate", startDate.Value.Date));
            }
            if (endDate.HasValue)
            {
                query += " AND Valid_to <= @EndDate";
                parameters.Add(new SqlParameter("@EndDate", endDate.Value.Date));
            }
            if (validity != "All")
            {
                switch (validity)
                {
                    case "Valid":
                        query += " AND Valid_to >= GETDATE() AND Valid_from <= GETDATE()";
                        break;
                    case "Expired":
                        query += " AND Valid_to < GETDATE()";
                        break;
                    case "Not Yet Validated":
                        query += " AND Valid_from > GETDATE()";
                        break;
                }
            }

            // Execute the query and bind results to the DataGridView
            DataSet ds = dbOps.getData(query, parameters);
            dataGridViewfilter.DataSource = ds.Tables[0];
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get filter values
            string nameFilter = txtName.Text.ToString();
            DateTime? startDate = txtStartDate.Checked ? txtStartDate.Value : (DateTime?)null;
            DateTime? endDate = dateStartDate.Checked ? dateStartDate.Value : (DateTime?)null;
            string validity = cmbValidity.SelectedItem.ToString();

            // Debug: print values
            Console.WriteLine($"Name Filter: {nameFilter}, Start Date: {startDate}, End Date: {endDate}, Validity: {validity}");

            // Load data based on filters
            LoadPassData(nameFilter, startDate, endDate, validity);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all filters
            txtName.Clear();
            txtStartDate.Value = DateTime.Now;
            dateStartDate.Value = DateTime.Now;
            cmbValidity.SelectedIndex = 0; // Set to default selection "Valid"

            // Load all pass data
            LoadPassData();
        }
    }
}
