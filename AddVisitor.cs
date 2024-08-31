using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    public partial class AddVisitor : Form
    {
        private DatabaseOperations databaseOperations;
        private string visitorId;
        private bool imageUploaded = false;
        private string path;

        public AddVisitor()
        {
            InitializeComponent();
            databaseOperations = new DatabaseOperations();
        }

        private void AddVisitor_Load(object sender, EventArgs e)
        {
            visitorId = Utility.UniqueIdGenerator.GetUniqueId("HALVISAC_");
            txtVisitorId.Text = visitorId;

            // Use Path.Combine to construct the path to the Images folder
            string imagesDirectory = Path.Combine(Application.StartupPath, "Images");

            // Ensure the Images directory exists
            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory); // Create directory if it doesn't exist
            }

            path = Path.Combine(imagesDirectory, visitorId + ".jpg");
            PopulateComboBox();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.InitialDirectory = "C:\\";
                open.Filter = "Image Files (*.jpg;*.jpeg;*.bmp;)|*.jpg;*.jpeg;*.bmp;";
                open.FilterIndex = 1;

                if (open.ShowDialog() == DialogResult.OK)
                {
                    // Check if no file was selected or the selected file name is empty
                    if (string.IsNullOrEmpty(open.FileName) || !File.Exists(open.FileName))
                    {
                        MessageBox.Show("No image file selected. Please select an image file.", "No File Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        // Ensure the destination path directory exists
                        string directory = Path.GetDirectoryName(path);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory); // Create directory if it doesn't exist
                        }

                        // Dispose of the current image if it exists
                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image.Dispose();
                            pictureBox1.Image = null; // Clear the current image reference to avoid locking the file
                        }

                        // Replace the existing file if it exists
                        if (File.Exists(path))
                        {
                            File.Delete(path); // Delete the existing file to replace it with the new one
                        }

                        // Copy the newly selected file to the specified path
                        File.Copy(open.FileName, path);

                        // Load and display the new image in the PictureBox
                        pictureBox1.Image = Image.FromFile(path);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        imageUploaded = true;

                        // Show a message about the permanence of the image
                        DialogResult result = MessageBox.Show(
                            "The uploaded image cannot be changed or updated in the future!",
                            "Confirm Image Upload",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Warning);

                        if (result == DialogResult.Cancel)
                        {
                            // Clear the uploaded image and reset the flag if user cancels
                            pictureBox1.Image.Dispose();
                            pictureBox1.Image = null;
                            imageUploaded = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while processing the image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select an image file to upload.", "Image Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnResetUpdate_Click(object sender, EventArgs e)
        {
            ResetForm();
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
                string.IsNullOrWhiteSpace(txtIdNmbr.Text) ||
                !imageUploaded)
            {
                MessageBox.Show("Please fill in all fields and upload an image.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Save the visitor's details to the database
            try
            {
                string name = txtName.Text;
                string email = txtEmailAddress.Text;
                long contact = Convert.ToInt64(txtContact.Text);
                string visitDate = txtVisitdate.Text;
                string visitTime = txtVisitTime.Text;
                string gender = cmbGender.SelectedItem.ToString();
                string address = txtAddress.Text;
                string city = txtCity.Text;
                string state = txtState.Text;
                string purposeOfVisit = cmbVisitPurpose.SelectedItem.ToString();
                string department = cmbVisitDepartment.SelectedItem.ToString();
                string idProofType = cmbIDProof.SelectedItem.ToString();
                string idNumber = txtIdNmbr.Text;

                string query = "INSERT INTO Visitors (Name, EmailAddress, Contact, VisitDate, VisitTime, Gender, Address, City, State, UniqueID, PurposeOfVisit, Department, IDProofType, IDNumber, Photo) " +
                               "VALUES (@Name, @Email, @Contact, @VisitDate, @VisitTime, @Gender, @Address, @City, @State, @UniqueID, @PurposeOfVisit, @Department, @IDProofType, @IDNumber, @Photo)";

                var parameters = new List<SqlParameter>
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
                    new SqlParameter("@IDNumber", SqlDbType.VarChar) { Value = idNumber },
                    new SqlParameter("@Photo", SqlDbType.VarBinary) { Value = File.ReadAllBytes(path) }
                };

                // Execute the query using setData method
                databaseOperations.setData(query, "Visitor added successfully.", parameters);

                MessageBox.Show("Visitor details saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the form
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving visitor details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            // Clear all input fields and reset to default values
            txtVisitorId.Text = Utility.UniqueIdGenerator.GetUniqueId("HALVISAC_");
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

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose(); // Dispose of the current image to release resources
                pictureBox1.Image = null;
            }

            imageUploaded = false; // Reset the flag or handle image upload separately
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
            cmbGender.SelectedIndex = 0;

            // Populate the Visit Purpose ComboBox
            cmbVisitPurpose.Items.Clear();
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
            cmbVisitPurpose.SelectedIndex = 0;

            // Populate the Visit Department ComboBox
            cmbVisitDepartment.Items.Clear();
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
            cmbVisitDepartment.SelectedIndex = 0;

            // Populate the ID Proof ComboBox
            cmbIDProof.Items.Clear();
            cmbIDProof.Items.Add("Aadhaar Card");
            cmbIDProof.Items.Add("Passport");
            cmbIDProof.Items.Add("Voter ID Card (EPIC)");
            cmbIDProof.Items.Add("Driving License");
            cmbIDProof.Items.Add("PAN Card");
            cmbIDProof.SelectedIndex = 0;
        }

        private void txtEmailAddress_Leave(object sender, EventArgs e)
        {
            if (!Utility.IsValidEmail(txtEmailAddress.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailAddress.Focus();
            }
        }
    }
}