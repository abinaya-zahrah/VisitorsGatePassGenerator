using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    public partial class UpdateVisitor : Form
    {
        private DatabaseOperations databaseOperations;
        private string query;
        private DataSet ds;
        private string visitorId;
        private bool imageUploaded = false;

        public UpdateVisitor()
        {
            InitializeComponent();
            InitializeForm();
        }

        public UpdateVisitor(string uniqueID)
        {
            InitializeComponent();
            visitorId = uniqueID;
            InitializeForm();
            LoadVisitorDetails(visitorId);
        }

        private void InitializeForm()
        {
            databaseOperations = new DatabaseOperations();
            ds = new DataSet(); // Ensure the DataSet is initialized
            PopulateComboBox();
        }



        private void LoadVisitorDetails(string uniqueID)
        {
            if (string.IsNullOrEmpty(uniqueID))
            {
                MessageBox.Show("Visitor ID is required to load details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            query = "SELECT * FROM Visitors WHERE UniqueID = @VisitorId";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@VisitorId", uniqueID)
            };

            try
            {
                ds = databaseOperations.getData(query, parameters);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    txtName.Text = row["Name"].ToString();
                    txtEmailAddress.Text = row["EmailAddress"].ToString();
                    txtContact.Text = row["Contact"].ToString();
                    txtVisitdate.Value = Convert.ToDateTime(row["VisitDate"]);
                    txtVisitTime.Value = DateTime.Today.Add(Convert.ToDateTime(row["VisitTime"]).TimeOfDay);
                    txtAddress.Text = row["Address"].ToString();
                    txtCity.Text = row["City"].ToString();
                    txtState.Text = row["State"].ToString();
                    txtVisitorId.Text = row["UniqueID"].ToString();
                    cmbGender.SelectedItem = row["Gender"].ToString();
                    cmbVisitPurpose.SelectedItem = row["PurposeOfVisit"].ToString();
                    cmbVisitDepartment.SelectedItem = row["Department"].ToString();
                    cmbIDProof.SelectedItem = row["IDProofType"].ToString();
                    txtIdNmbr.Text = row["IDNumber"].ToString();

                    if (row["Photo"] != DBNull.Value)
                    {
                        byte[] imageBytes = (byte[])row["Photo"];
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }

                    imageUploaded = false; // No new image uploaded
                }
                else
                {
                    MessageBox.Show("Visitor ID not found. Please enter a valid ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving visitor details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnexit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtEmailAddress_Leave(object sender, EventArgs e)
        {
            if (!Utility.IsValidEmail(txtEmailAddress.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailAddress.Focus();
            }
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.onlyNumber(e);
        }

        private void PopulateComboBox()
        {
            // Populate the Gender ComboBox
            cmbGender.Items.Clear();
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            cmbGender.Items.Add("Other");

            // Optionally, set a default value
            cmbGender.SelectedIndex = 0; // Selects the first item by default (e.g., "Male")

            // Populate the Visit Purpose ComboBox
            cmbVisitPurpose.Items.Clear(); // Clear existing items, if any
            cmbVisitPurpose.Items.Add("Business Meeting");
            cmbVisitPurpose.Items.Add("Factory Tour");
            cmbVisitPurpose.Items.Add("Training");
            cmbVisitPurpose.Items.Add("Inspection");
            cmbVisitPurpose.Items.Add("Recruitment");
            cmbVisitPurpose.Items.Add("Official Visit");
            cmbVisitPurpose.Items.Add("Customer Visit");
            cmbVisitPurpose.Items.Add("Vendor Meet");
            cmbVisitPurpose.Items.Add("Research");
            cmbVisitPurpose.Items.Add("Induction");
            cmbVisitPurpose.Items.Add("Conference");
            cmbVisitPurpose.Items.Add("Quality Control");
            cmbVisitPurpose.Items.Add("Safety Audit");
            cmbVisitPurpose.Items.Add("Certification");
            cmbVisitPurpose.Items.Add("Educational Visit");
            cmbVisitPurpose.Items.Add("Internship");

            // Optionally, set a default value
            cmbVisitPurpose.SelectedIndex = 0; // Selects the first item by default (e.g., "Business Meeting")

            // Populate the Visit Department ComboBox
            cmbVisitDepartment.Items.Clear(); // Clear existing items, if any
            cmbVisitDepartment.Items.Add("Hawk Machine Shop");
            cmbVisitDepartment.Items.Add("Export Machine Shop");
            cmbVisitDepartment.Items.Add("NC Shop & NC Programming");
            cmbVisitDepartment.Items.Add("Process Shop");
            cmbVisitDepartment.Items.Add("Honeycomb Shop");
            cmbVisitDepartment.Items.Add("Work Test Lab");
            cmbVisitDepartment.Items.Add("Customer Service");
            cmbVisitDepartment.Items.Add("Design Liaison Engineering");
            cmbVisitDepartment.Items.Add("Management Service Department");
            cmbVisitDepartment.Items.Add("Human Resources");
            cmbVisitDepartment.Items.Add("Tool Room & Tool Engineering");
            cmbVisitDepartment.Items.Add("Hawk Structure and Final Assembly");
            cmbVisitDepartment.Items.Add("Hawk Winy Assembly");
            cmbVisitDepartment.Items.Add("Maintenance & Safety");
            cmbVisitDepartment.Items.Add("NC Pipe Bending");
            cmbVisitDepartment.Items.Add("Stores & Inventory");
            cmbVisitDepartment.Items.Add("External Sources");
            cmbVisitDepartment.Items.Add("Methods & Programming Engineering");
            cmbVisitDepartment.Items.Add("IT Department & Finance");
            cmbVisitDepartment.Items.Add("Export Assembly / Marketing");
            cmbVisitDepartment.Items.Add("Sheet Metal / Welding");
            cmbVisitDepartment.Items.Add("Drop Tank");
            cmbVisitDepartment.Items.Add("Heat Treatment & Plastics");
            cmbVisitDepartment.Items.Add("Hawk Equipping Loom Shop");
            cmbVisitDepartment.Items.Add("All");

            // Optionally, set a default value
            cmbVisitDepartment.SelectedIndex = 0; // Selects the first item by default (e.g., "Hawk Machine Shop")

            // Populate the ID Proof ComboBox
            cmbIDProof.Items.Clear(); // Clear existing items, if any
            cmbIDProof.Items.Add("Aadhaar Card");
            cmbIDProof.Items.Add("Passport");
            cmbIDProof.Items.Add("Voter ID Card (EPIC)");
            cmbIDProof.Items.Add("Driving License");
            cmbIDProof.Items.Add("PAN Card");

            // Optionally, set a default value
            cmbIDProof.SelectedIndex = 0; // Selects the first item by default (e.g., "Aadhaar Card")
        }

        private void btnResetUpdate_Click(object sender, EventArgs e)
        {
            // Reset all fields
            ResetForm();
        }

        private void UpdateVisitor_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate all required fields
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmailAddress.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtState.Text) ||
                cmbGender.SelectedIndex == -1 ||
                cmbVisitPurpose.SelectedIndex == -1 ||
                cmbVisitDepartment.SelectedIndex == -1 ||
                cmbIDProof.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtIdNmbr.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate the visit date
            DateTime selectedDate = txtVisitdate.Value.Date;
            if (selectedDate < DateTime.Now.Date)
            {
                MessageBox.Show("Please select today's date or a date after today.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Parse the visit time
            DateTime visitDateTime;
            if (!DateTime.TryParse(txtVisitTime.Text, out visitDateTime))
            {
                MessageBox.Show("Please enter a valid time (in hh:mm AM/PM format).", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TimeSpan visitTimeSpan = visitDateTime.TimeOfDay;

            try
            {
                string name = txtName.Text;
                string email = txtEmailAddress.Text;
                long contact = Convert.ToInt64(txtContact.Text);
                string visitDate = txtVisitdate.Value.ToString("yyyy-MM-dd"); // Correct date format
                string visitTime = visitTimeSpan.ToString(); // TimeSpan as string
                string gender = cmbGender.SelectedItem.ToString();
                string address = txtAddress.Text;
                string city = txtCity.Text;
                string state = txtState.Text;
                string purposeOfVisit = cmbVisitPurpose.SelectedItem.ToString();
                string department = cmbVisitDepartment.SelectedItem.ToString();
                string idProofType = cmbIDProof.SelectedItem.ToString();
                string idNumber = txtIdNmbr.Text;

                // Prepare SQL query and parameters
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@Name", SqlDbType.VarChar) { Value = name },
            new SqlParameter("@Email", SqlDbType.VarChar) { Value = email },
            new SqlParameter("@Contact", SqlDbType.BigInt) { Value = contact },
            new SqlParameter("@VisitDate", SqlDbType.Date) { Value = selectedDate },
            new SqlParameter("@VisitTime", SqlDbType.Time) { Value = visitTimeSpan },
            new SqlParameter("@Gender", SqlDbType.VarChar) { Value = gender },
            new SqlParameter("@Address", SqlDbType.VarChar) { Value = address },
            new SqlParameter("@City", SqlDbType.VarChar) { Value = city },
            new SqlParameter("@State", SqlDbType.VarChar) { Value = state },
            new SqlParameter("@UniqueID", SqlDbType.VarChar) { Value = visitorId },
            new SqlParameter("@PurposeOfVisit", SqlDbType.VarChar) { Value = purposeOfVisit },
            new SqlParameter("@Department", SqlDbType.VarChar) { Value = department },
            new SqlParameter("@IDProofType", SqlDbType.VarChar) { Value = idProofType },
            new SqlParameter("@IDNumber", SqlDbType.VarChar) { Value = idNumber }
        };

                if (imageUploaded && pictureBox1.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        byte[] imageBytes = ms.ToArray();
                        parameters.Add(new SqlParameter("@Photo", SqlDbType.VarBinary) { Value = imageBytes });
                    }
                }

                string query = imageUploaded && pictureBox1.Image != null ? @"
            UPDATE Visitors 
            SET Name = @Name, 
                EmailAddress = @Email, 
                Contact = @Contact, 
                VisitDate = @VisitDate, 
                VisitTime = @VisitTime, 
                Address = @Address, 
                City = @City, 
                State = @State, 
                Gender = @Gender, 
                PurposeOfVisit = @PurposeOfVisit, 
                Department = @Department, 
                IDProofType = @IDProofType, 
                IDNumber = @IDNumber, 
                Photo = @Photo
            WHERE UniqueID = @UniqueID" : @"
            UPDATE Visitors 
            SET Name = @Name, 
                EmailAddress = @Email, 
                Contact = @Contact, 
                VisitDate = @VisitDate, 
                VisitTime = @VisitTime, 
                Address = @Address, 
                City = @City, 
                State = @State, 
                Gender = @Gender, 
                PurposeOfVisit = @PurposeOfVisit, 
                Department = @Department, 
                IDProofType = @IDProofType, 
                IDNumber = @IDNumber
            WHERE UniqueID = @UniqueID";

                // Execute the update query
                databaseOperations.setData(query, "Visitor details updated successfully.", parameters);
                imageUploaded = false; // Reset the imageUploaded flag
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating visitor details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ResetForm()
        {
            // Clear all input fields and reset to default values
            txtName.Clear();
            txtEmailAddress.Clear();
            txtContact.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtState.Clear();
            txtIdNmbr.Clear();
            cmbGender.SelectedIndex = -1;
            cmbVisitPurpose.SelectedIndex = -1;
            cmbVisitDepartment.SelectedIndex = -1;
            cmbIDProof.SelectedIndex = -1;
            txtVisitdate.Value = DateTime.Now;
            txtVisitTime.Value = DateTime.Now;

            pictureBox1.Image = null;
            imageUploaded = false; // Reset the flag or handle image upload separately
        }

        private void btnGetId_Click(object sender, EventArgs e)
        {
            visitorId = txtVisId.Text.Trim();
            if (!string.IsNullOrEmpty(visitorId))
            {
                LoadVisitorDetails(visitorId);
            }
            else
            {
                MessageBox.Show("Please enter a valid Visitor ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
