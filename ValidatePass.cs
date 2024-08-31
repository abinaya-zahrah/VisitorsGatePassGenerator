using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    public partial class ValidatePass : Form
    {
        DatabaseOperations dbOps = new DatabaseOperations();

        public ValidatePass()
        {
            InitializeComponent();
        }

        private void ValidatePass_Load(object sender, EventArgs e)
        {
            LoadVisitorsData();
        }

        private void LoadVisitorsData(string filter = "")
        {
            try
            {
                string query = "SELECT * FROM Visitors";
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(filter))
                {
                    query += " WHERE Name LIKE @filter OR " +
                             "EmailAddress LIKE @filter OR " +
                             "Contact LIKE @filter OR " +
                             "Address LIKE @filter OR " +
                             "City LIKE @filter OR " +
                             "State LIKE @filter OR " +
                             "UniqueID LIKE @filter OR " +
                             "PurposeOfVisit LIKE @filter OR " +
                             "Department LIKE @filter";

                    parameters.Add(new SqlParameter("@filter", "%" + filter + "%"));
                }

                DataSet ds = dbOps.getData(query, parameters);

                if (ds.Tables.Count > 0)
                {
                    dataGridViewpass.DataSource = ds.Tables[0];
                }
                else
                {
                    dataGridViewpass.DataSource = null;
                    MessageBox.Show("No visitors found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadVisitorsData(searchTerm);
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (dataGridViewpass.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a visitor.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow selectedRow = dataGridViewpass.SelectedRows[0];
            string uniqueID = selectedRow.Cells["UniqueID"].Value.ToString();

            // Fetch pass details for the selected visitor
            string query = "SELECT * FROM Pass WHERE UniqueID = @UniqueID";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UniqueID", uniqueID)
            };
            DataSet dsPass = dbOps.getData(query, parameters);

            if (dsPass.Tables.Count > 0 && dsPass.Tables[0].Rows.Count > 0)
            {
                DataTable passTable = dsPass.Tables[0];
                int passCount = passTable.Rows.Count;

                // Display the number of passes generated and list all pass numbers
                StringBuilder passDetails = new StringBuilder();
                passDetails.AppendLine($"Pass Generated! No of Pass Generated = {passCount}, ");
                foreach (DataRow row in passTable.Rows)
                {
                    passDetails.AppendLine($"Pass No: {row["PassNo"]}. ");
                }
                txtdetails.Text = passDetails.ToString();

                // Display pass details for the first pass (or modify as needed)
                DataRow firstPassRow = passTable.Rows[0];
                txtpassno.Text = firstPassRow["PassNo"].ToString();
                txtpassid.Text = firstPassRow["PassID"].ToString();
                lblName.Text = firstPassRow["Visitor_Name"].ToString();
                lblvalidDateFrm.Text = Convert.ToDateTime(firstPassRow["Valid_from"]).ToString("yyyy-MM-dd");
                lblvalidDateTo.Text = Convert.ToDateTime(firstPassRow["Valid_to"]).ToString("yyyy-MM-dd");
                lblFrmTime.Text = Convert.ToDateTime(firstPassRow["Valid_from_time"]).ToString("HH:mm");
                lblToTime.Text = Convert.ToDateTime(firstPassRow["Valid_to_time"]).ToString("HH:mm");
                lblVisId.Text = firstPassRow["UniqueID"].ToString();
                lblPurpose.Text = firstPassRow["PurposeOfVisit"].ToString();
                lblDepartment.Text = firstPassRow["Department"].ToString();

                // Update color based on validity
                DateTime validFrom = Convert.ToDateTime(firstPassRow["Valid_from"]);
                DateTime validTo = Convert.ToDateTime(firstPassRow["Valid_to"]);
                DateTime now = DateTime.Now;

                if (selectedRow.Cells["Photo"].Value != DBNull.Value)
                {
                    byte[] photoBytes = (byte[])selectedRow.Cells["Photo"].Value;
                    using (var ms = new System.IO.MemoryStream(photoBytes))
                    {
                        pictureBoxPass.Image = Image.FromStream(ms);
                        pictureBoxPass.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    pictureBoxPass.Image = null; // Clear image if no photo is available
                }



                if (now >= validFrom && now <= validTo)
                {
                    // Valid pass
                    panel1.BackColor = Color.LimeGreen; // Update panel color to green
                }
                else if (now > validTo)
                {
                    // Expired pass
                    panel1.BackColor = Color.Tomato; // Update panel color to tomato (red)
                }
                else
                {
                    // Not yet valid (if applicable)
                    panel1.BackColor = Color.Yellow; // Update panel color to yellow or any other indication
                }
            }
            else
            {
                txtdetails.Text = "No pass generated.";

                // Clear all fields if no pass is generated
                txtpassno.Text = string.Empty;
                txtpassid.Text = string.Empty;
                lblName.Text = string.Empty;
                lblvalidDateFrm.Text = string.Empty;
                lblvalidDateTo.Text = string.Empty;
                lblFrmTime.Text = string.Empty;
                lblToTime.Text = string.Empty;
                lblVisId.Text = string.Empty;
                lblPurpose.Text = string.Empty;
                lblDepartment.Text = string.Empty;
                pictureBoxPass.Image = null; // Clear the picture box if no pass is available

                panel1.BackColor = Color.DarkGray; // Set panel color to gray or any default color
            }
        }

    }
}
