using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static VisitorsGatePassGenerator.Utility;

namespace VisitorsGatePassGenerator
{
    public partial class GeneratePass : Form
    {

        public GeneratePass()
        {
            InitializeComponent();
        }

        private void SavePassToDatabase()
        {
            // Generate a unique pass ID
            string uniquePassId = SimpleUniquePassIdGenerator.GetUniquePassId();
            txtPassid.Text = uniquePassId; // Set the generated pass ID in the text box

            string query = @"
            INSERT INTO Pass (PassID, CompanyName, DivisionName, Location, Valid_from, Valid_to, Valid_from_time, Valid_to_time, Visitor_Name, UniqueID, PurposeOfVisit, Department, Photo)
            VALUES (@PassID, @CompanyName, @DivisionName, @Location, @ValidFrom, @ValidTo, @ValidFromTime, @ValidToTime, @VisitorName, @UniqueID, @PurposeOfVisit, @Department, @Photo);
            SELECT SCOPE_IDENTITY();"; // Query to insert and get the last inserted ID

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@PassID", SqlDbType.VarChar) { Value = txtPassid.Text },
                new SqlParameter("@CompanyName", SqlDbType.VarChar) { Value = txtCompany.Text },
                new SqlParameter("@DivisionName", SqlDbType.VarChar) { Value = txtDivision.Text },
                new SqlParameter("@Location", SqlDbType.VarChar) { Value = txtSite.Text },
                new SqlParameter("@ValidFrom", SqlDbType.Date) { Value = txtValidfrom.Value.Date },
                new SqlParameter("@ValidTo", SqlDbType.Date) { Value = txtValidto.Value.Date },
                new SqlParameter("@ValidFromTime", SqlDbType.DateTime) { Value = txtValidfrmtime.Value },
                new SqlParameter("@ValidToTime", SqlDbType.DateTime) { Value = txtValidtotime.Value },
                new SqlParameter("@VisitorName", SqlDbType.VarChar) { Value = txtName.Text },
                new SqlParameter("@UniqueID", SqlDbType.VarChar) { Value = txtVisId.Text },
                new SqlParameter("@PurposeOfVisit", SqlDbType.VarChar) { Value = txtPurpose.Text },
                new SqlParameter("@Department", SqlDbType.VarChar) { Value = txtDepartment.Text }
            };

            if (pictureBoxG.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Clone the image to avoid file lock issues
                    using (var tempImage = new Bitmap(pictureBoxG.Image))
                    {
                        tempImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    byte[] imageBytes = ms.ToArray();
                    parameters.Add(new SqlParameter("@Photo", SqlDbType.VarBinary) { Value = imageBytes });
                }
            }

            // Use the executeInsertAndGetId method to execute the query and get the ID
            DatabaseOperations dbOps = new DatabaseOperations();
            string newPassId = dbOps.InsertAndGetId(query, parameters);

            if (!string.IsNullOrEmpty(newPassId))
            {
                MessageBox.Show("Pass saved successfully with Pass NO: " + newPassId, "Success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Failed to save pass.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnGetId_Click(object sender, EventArgs e)
        {
            string visitorId = txtVisId.Text.Trim();

            if (string.IsNullOrEmpty(visitorId))
            {
                MessageBox.Show("Please enter a Visitor ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT Name, PurposeOfVisit, Department, Photo FROM Visitors WHERE UniqueID = @VisitorID";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@VisitorID", SqlDbType.VarChar) { Value = visitorId }
            };

            DatabaseOperations dbOps = new DatabaseOperations();
            DataSet ds = dbOps.getData(query, parameters);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                txtName.Text = row["Name"].ToString();
                txtPurpose.Text = row["PurposeOfVisit"].ToString();
                txtDepartment.Text = row["Department"].ToString();

                byte[] photoData = row["Photo"] as byte[];
                if (photoData != null)
                {
                    using (MemoryStream ms = new MemoryStream(photoData))
                    {
                        pictureBoxG.Image = Image.FromStream(ms);
                        pictureBoxG.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    pictureBoxG.Image = null;
                }
            }
            else
            {
                MessageBox.Show("No visitor found with the provided ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            DateTime selectedFromDate = txtValidfrom.Value.Date;
            DateTime selectedToDate = txtValidto.Value.Date;
            DateTime selectedFromTime = txtValidfrmtime.Value;
            DateTime selectedToTime = txtValidtotime.Value;

            // Combined date and time checks
            if (selectedFromDate < DateTime.Now.Date || selectedToDate < DateTime.Now.Date)
            {
                MessageBox.Show("Please select today's date or date after today.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedToDate < selectedFromDate || (selectedToDate == selectedFromDate && selectedToTime < selectedFromTime))
            {
                MessageBox.Show("'Valid To' must be after 'Valid From'.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ensure the pass duration is valid
            if (selectedToTime > selectedFromTime.AddHours(8))
            {
                MessageBox.Show("The selected duration exceeds 8 hours. Please choose a 2-day pass or choose 8hours or less ", "Invalid Duration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate required fields
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPurpose.Text) ||
                string.IsNullOrEmpty(txtDepartment.Text) || string.IsNullOrEmpty(txtCompany.Text) ||
                string.IsNullOrEmpty(txtDivision.Text) || string.IsNullOrEmpty(txtSite.Text) ||
                string.IsNullOrEmpty(txtPassid.Text) || string.IsNullOrEmpty(txtValidfrom.Text) ||
                string.IsNullOrEmpty(txtValidto.Text) || string.IsNullOrEmpty(txtValidfrmtime.Text) ||
                string.IsNullOrEmpty(txtValidtotime.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Generate and set the unique pass ID
            txtPassid.Text = SimpleUniquePassIdGenerator.GetUniquePassId();

            // Save the pass details to the database
            SavePassToDatabase();

            // Open the new form with data grid view
            ViewPass viewPassesForm = new ViewPass();
            viewPassesForm.Show();
            this.Close();
        }





        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassid.Text = SimpleUniquePassIdGenerator.GetUniquePassId();
            txtName.Clear();
            txtPurpose.Clear();
            txtDepartment.Clear();
            txtCompany.Clear();
            txtDivision.Clear();
            txtSite.Clear();
            txtValidfrom.Value = DateTime.Now;
            txtValidto.Value = DateTime.Now.AddDays(1);
            DateTime now = DateTime.Now;
            // Set the 'Valid From Time' and 'Valid To Time' controls
            txtValidfrmtime.Value = now;
            txtValidtotime.Value = now.AddHours(4);
            pictureBoxG.Image = null;
            txtVisId.Clear();
        }


        private void GeneratePass_Load(object sender, EventArgs e)
        {
            // Set the minimum date to today and time to now for Valid_from and Valid_to
            txtValidfrom.MinDate = DateTime.Today;
            txtValidto.MinDate = DateTime.Today.AddDays(1);

            // Subscribe to the ValueChanged event of txtValidfrmtime
            txtValidfrmtime.ValueChanged += new EventHandler(txtValidfrmtime_ValueChanged);

            // Generate and set the initial pass ID
            txtPassid.Text = SimpleUniquePassIdGenerator.GetUniquePassId();
        }


        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HandleLongPassDuration()
        {
            // Calculate the difference in days between Valid_from and Valid_to
            TimeSpan duration = txtValidto.Value.Date - txtValidfrom.Value.Date;

            // Check if the duration is exactly 1 day
            if (duration.TotalDays == 1)
            {
                // Calculate the duration in hours
                TimeSpan timeDifference = txtValidtotime.Value - txtValidfrmtime.Value;

                // Check if the duration exceeds 8 hours
                if (timeDifference.TotalHours > 8)
                {
                    // Prompt the user to choose a 2-day pass
                    DialogResult result = MessageBox.Show(
                        "The selected duration exceeds 8 hours. Please choose a 2-day pass instead.",
                        "Long Duration Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );


                }
            }
        }



        private void txtValidfrmtime_ValueChanged(object sender, EventArgs e)
        {
            // Check if the current selection exceeds 8 hours
            if (txtValidtotime.Value > txtValidfrmtime.Value.AddHours(8))
            {
                // Call method to handle long duration
                HandleLongPassDuration();
            }
            else
            {
                // Reset to valid time constraints if duration is within bounds
                if (txtValidtotime.Value < txtValidtotime.MinDate || txtValidtotime.Value > txtValidtotime.MaxDate)
                {
                    txtValidtotime.Value = txtValidtotime.MinDate;
                }
            }

        }
    }
}
