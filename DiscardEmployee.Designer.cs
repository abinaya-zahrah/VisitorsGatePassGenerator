namespace VisitorsGatePassGenerator
{
    partial class DiscardEmployee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiscardEmployee));
            this.label4 = new System.Windows.Forms.Label();
            this.btnDeleteEmp = new System.Windows.Forms.Button();
            this.btnSearchView = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.Label();
            this.Login = new System.Windows.Forms.Label();
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(914, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 16);
            this.label4.TabIndex = 34;
            this.label4.Text = "Delete";
            // 
            // btnDeleteEmp
            // 
            this.btnDeleteEmp.BackColor = System.Drawing.Color.LightCoral;
            this.btnDeleteEmp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteEmp.BackgroundImage")));
            this.btnDeleteEmp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteEmp.Location = new System.Drawing.Point(884, 102);
            this.btnDeleteEmp.Name = "btnDeleteEmp";
            this.btnDeleteEmp.Size = new System.Drawing.Size(106, 71);
            this.btnDeleteEmp.TabIndex = 31;
            this.btnDeleteEmp.UseVisualStyleBackColor = false;
            this.btnDeleteEmp.Click += new System.EventHandler(this.btnDeleteEmp_Click);
            // 
            // btnSearchView
            // 
            this.btnSearchView.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSearchView.Location = new System.Drawing.Point(622, 131);
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
            this.btnexit.Location = new System.Drawing.Point(1190, 12);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(71, 54);
            this.btnexit.TabIndex = 27;
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(183, 131);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(432, 32);
            this.txtSearch.TabIndex = 26;
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username.ForeColor = System.Drawing.Color.Black;
            this.username.Location = new System.Drawing.Point(72, 139);
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
            this.Login.Location = new System.Drawing.Point(400, 27);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(319, 49);
            this.Login.TabIndex = 24;
            this.Login.Text = "Discard Employee";
            // 
            // dataGridViewEmployees
            // 
            this.dataGridViewEmployees.BackgroundColor = System.Drawing.Color.LightGray;
            this.dataGridViewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployees.Location = new System.Drawing.Point(49, 235);
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.ReadOnly = true;
            this.dataGridViewEmployees.RowHeadersWidth = 51;
            this.dataGridViewEmployees.RowTemplate.Height = 24;
            this.dataGridViewEmployees.Size = new System.Drawing.Size(1167, 462);
            this.dataGridViewEmployees.TabIndex = 23;
            // 
            // DiscardEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1273, 731);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnDeleteEmp);
            this.Controls.Add(this.btnSearchView);
            this.Controls.Add(this.btnexit);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.username);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.dataGridViewEmployees);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DiscardEmployee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.DiscardEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDeleteEmp;
        private System.Windows.Forms.Button btnSearchView;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label username;
        private System.Windows.Forms.Label Login;
        private System.Windows.Forms.DataGridView dataGridViewEmployees;
    }
}