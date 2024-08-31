using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace VisitorsGatePassGenerator
{
    public partial class ViewPass : Form
    {

        public ViewPass()
        {
            InitializeComponent();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            string passNo = txtPassSearch.Text.Trim();

            if (string.IsNullOrEmpty(passNo))
            {
                MessageBox.Show("Please enter a Pass Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Define the SQL query to search for the PassNo
            string query = "SELECT PassNo, PassID, Visitor_Name, Valid_from, Valid_to, Valid_from_time, Valid_to_time, UniqueID, PurposeOfVisit, Department, Photo FROM Pass WHERE PassNo = @PassNo";

            try
            {
                DatabaseOperations dbOps = new DatabaseOperations();
                List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@PassNo", passNo)
        };

                DataSet dataSet = dbOps.getData(query, parameters);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    dataGridViewpass.DataSource = dataSet.Tables[0];
                }
                else
                {
                    MessageBox.Show("No record found for the provided Pass Number.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridViewpass.DataSource = null; // Clear DataGridView if no record found
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassSearch.Clear();
            // Clear text fields
            txtpassno.Clear();
            txtpassid.Clear();

            // Reset labels
            lblName.Text = string.Empty;
            lblvalidDateFrm.Text = string.Empty;
            lblvalidDateTo.Text = string.Empty;
            lblFrmTime.Text = string.Empty;
            lblToTime.Text = string.Empty;
            lblVisId.Text = string.Empty;
            lblPurpose.Text = string.Empty;
            lblDepartment.Text = string.Empty;

            // Clear image
            pictureBoxPass.Image = null;

            // Reset DateTimePickers to today's date or any default value
            dateTimePickerValidFrom.Value = DateTime.Now;
            dateTimePickerValidTo.Value = DateTime.Now;

            // Clear DataGridView
            dataGridViewpass.DataSource = null;
        }



        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdatePanelColor()
        {
            if (DateTime.TryParse(dateTimePickerValidFrom.Text, out DateTime validFromDate) &&
                DateTime.TryParse(dateTimePickerValidTo.Text, out DateTime validToDate))
            {
                int daysDifference = (validToDate - validFromDate).Days;

                if (daysDifference == 1)
                {
                    panel1.BackColor = Color.Aqua; // Example color for 1 day
                }
                else if (daysDifference >= 2 && daysDifference <= 7)
                {
                    panel1.BackColor = Color.Yellow; // Example color for 2-7 days
                }
                else if (daysDifference >= 8 && daysDifference <= 15)
                {
                    panel1.BackColor = Color.SpringGreen; // Example color for 8-15 days
                }
                else if (daysDifference > 15)
                {
                    panel1.BackColor = Color.Coral; // Example color for more than 15 days
                }
            }
        }


        private void ViewPass_Load(object sender, EventArgs e)
        {

        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            if (dataGridViewpass.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewpass.SelectedRows[0];

                txtpassno.Text = selectedRow.Cells["PassNo"].Value.ToString();
                txtpassid.Text = selectedRow.Cells["PassID"].Value.ToString();
                lblName.Text = selectedRow.Cells["Visitor_Name"].Value.ToString();
                lblvalidDateFrm.Text = Convert.ToDateTime(selectedRow.Cells["Valid_from"].Value).ToString("yyyy-MM-dd");
                lblvalidDateTo.Text = Convert.ToDateTime(selectedRow.Cells["Valid_to"].Value).ToString("yyyy-MM-dd");
                lblFrmTime.Text = Convert.ToDateTime(selectedRow.Cells["Valid_from_time"].Value).ToString("HH:mm");
                lblToTime.Text = Convert.ToDateTime(selectedRow.Cells["Valid_to_time"].Value).ToString("HH:mm");
                dateTimePickerValidFrom.Text = Convert.ToDateTime(selectedRow.Cells["Valid_from"].Value).ToString("yyyy-MM-dd");
                dateTimePickerValidTo.Text = Convert.ToDateTime(selectedRow.Cells["Valid_to"].Value).ToString("yyyy-MM-dd");
                lblVisId.Text = selectedRow.Cells["UniqueID"].Value.ToString();
                lblPurpose.Text = selectedRow.Cells["PurposeOfVisit"].Value.ToString();
                lblDepartment.Text = selectedRow.Cells["Department"].Value.ToString();

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

                // Update panel color based on date difference
                UpdatePanelColor();
            }
            else
            {
                MessageBox.Show("Please select a Visitor to fetch Details.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PrintPanel(object sender, PrintPageEventArgs e)
        {
            // Set your desired DPI scaling factor (e.g., 2.0 for 200% size)
            float dpiScaleFactor = 2.0f;

            // Create a bitmap with higher resolution
            int width = (int)(panel1.Width * dpiScaleFactor);
            int height = (int)(panel1.Height * dpiScaleFactor);
            Bitmap highResBitmap = new Bitmap(width, height);

            // Set the DPI (dots per inch) for the bitmap
            highResBitmap.SetResolution(300, 300); // Set to 300 DPI for high resolution

            using (Graphics g = Graphics.FromImage(highResBitmap))
            {
                // Scale up the panel content to match the new bitmap size
                g.ScaleTransform(dpiScaleFactor, dpiScaleFactor);

                // Draw the panel content onto the high-resolution bitmap
                panel1.DrawToBitmap(highResBitmap, new Rectangle(0, 0, panel1.Width, panel1.Height));
            }

            // Draw the high-resolution bitmap on the PrintPageEventArgs graphics
            e.Graphics.DrawImage(highResBitmap, 0, 0, e.PageBounds.Width, e.PageBounds.Height);

            // Dispose of the bitmap to free up resources
            highResBitmap.Dispose();
        }


        private void GeneratePDF(string fileName)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Visitor Pass Details";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Draw the content of the panel into the PDF
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(bitmap, new Rectangle(0, 0, panel1.Width, panel1.Height));

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                XImage xImage = XImage.FromStream(ms);
                gfx.DrawImage(xImage, 0, 0, page.Width.Point, page.Height.Point);
            }

            // Save the PDF document
            document.Save(fileName);
            document.Close();
        }


        private void btnDownload_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();

            // Check if any printers are installed
            if (PrinterSettings.InstalledPrinters.Count > 0)
            {
                // Print the panel
                printDoc.PrintPage += new PrintPageEventHandler(PrintPanel);
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDoc;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
            else
            {
                // No printers installed, generate PDF
                if (MessageBox.Show("No printers found. Do you want to save the details as a PDF?",
                                    "Save as PDF",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.FileName = "VisitorDetails.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        GeneratePDF(saveFileDialog.FileName);
                        MessageBox.Show("PDF saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            // Clear fields after successful PDF generation
            btnClear_Click(sender, e); // Call the clear method to reset all fields
        }

    }
}
