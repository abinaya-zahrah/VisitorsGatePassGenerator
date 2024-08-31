namespace VisitorsGatePassGenerator
{
    partial class ViewVisitors
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewVisitors));
            this.btnSearchView = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.Label();
            this.Login = new System.Windows.Forms.Label();
            this.dataGridViewVisitors = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpdateVis = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVisitors)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearchView
            // 
            this.btnSearchView.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSearchView.Location = new System.Drawing.Point(601, 132);
            this.btnSearchView.Name = "btnSearchView";
            this.btnSearchView.Size = new System.Drawing.Size(106, 32);
            this.btnSearchView.TabIndex = 28;
            this.btnSearchView.Text = "Search";
            this.btnSearchView.UseVisualStyleBackColor = false;
            this.btnSearchView.Click += new System.EventHandler(this.btnSearchView_Click);
            // 
            // btnexit
            // 
            this.btnexit.ForeColor = System.Drawing.Color.White;
            this.btnexit.Image = ((System.Drawing.Image)(resources.GetObject("btnexit.Image")));
            this.btnexit.Location = new System.Drawing.Point(1334, 12);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(71, 54);
            this.btnexit.TabIndex = 27;
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(162, 132);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(432, 32);
            this.txtSearch.TabIndex = 26;
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.ForeColor = System.Drawing.Color.Black;
            this.username.Location = new System.Drawing.Point(51, 140);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(59, 24);
            this.username.TabIndex = 25;
            this.username.Text = "Name";
            // 
            // Login
            // 
            this.Login.AutoSize = true;
            this.Login.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Login.ForeColor = System.Drawing.Color.Blue;
            this.Login.Location = new System.Drawing.Point(683, 12);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(241, 49);
            this.Login.TabIndex = 24;
            this.Login.Text = "View Visitors";
            // 
            // dataGridViewVisitors
            // 
            this.dataGridViewVisitors.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridViewVisitors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVisitors.GridColor = System.Drawing.Color.Black;
            this.dataGridViewVisitors.Location = new System.Drawing.Point(55, 221);
            this.dataGridViewVisitors.Name = "dataGridViewVisitors";
            this.dataGridViewVisitors.ReadOnly = true;
            this.dataGridViewVisitors.RowHeadersWidth = 51;
            this.dataGridViewVisitors.RowTemplate.Height = 24;
            this.dataGridViewVisitors.Size = new System.Drawing.Size(1309, 506);
            this.dataGridViewVisitors.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(784, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 30;
            this.label3.Text = "Update";
            // 
            // btnUpdateVis
            // 
            this.btnUpdateVis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnUpdateVis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpdateVis.BackgroundImage")));
            this.btnUpdateVis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdateVis.Location = new System.Drawing.Point(768, 112);
            this.btnUpdateVis.Name = "btnUpdateVis";
            this.btnUpdateVis.Size = new System.Drawing.Size(79, 52);
            this.btnUpdateVis.TabIndex = 29;
            this.btnUpdateVis.UseVisualStyleBackColor = false;
            this.btnUpdateVis.Click += new System.EventHandler(this.btnUpdateVis_Click);
            // 
            // ViewVisitors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1417, 794);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnUpdateVis);
            this.Controls.Add(this.btnSearchView);
            this.Controls.Add(this.btnexit);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.username);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.dataGridViewVisitors);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewVisitors";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewVisitors";
            this.Load += new System.EventHandler(this.ViewVisitors_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVisitors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearchView;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label username;
        private System.Windows.Forms.Label Login;
        private System.Windows.Forms.DataGridView dataGridViewVisitors;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpdateVis;
    }
}