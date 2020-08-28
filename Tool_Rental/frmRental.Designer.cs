namespace Tool_Rental
{
    partial class frmRental
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
            this.txtRentalId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpDateRented = new System.Windows.Forms.DateTimePicker();
            this.dtpDateReturned = new System.Windows.Forms.DateTimePicker();
            this.gbxItems = new System.Windows.Forms.GroupBox();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.btnInsertItem = new System.Windows.Forms.Button();
            this.dgvRentalItems = new System.Windows.Forms.DataGridView();
            this.btnCreate = new System.Windows.Forms.Button();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWorkshop = new System.Windows.Forms.TextBox();
            this.gbxItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRentalItems)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRentalId
            // 
            this.txtRentalId.Location = new System.Drawing.Point(158, 53);
            this.txtRentalId.Name = "txtRentalId";
            this.txtRentalId.Size = new System.Drawing.Size(100, 20);
            this.txtRentalId.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Customer ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Rental ID:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(346, 475);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 27);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(427, 475);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 27);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Rental";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Date Rented:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "Date Returned:";
            // 
            // dtpDateRented
            // 
            this.dtpDateRented.CustomFormat = "dd-MMM-yyyy";
            this.dtpDateRented.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateRented.Location = new System.Drawing.Point(162, 160);
            this.dtpDateRented.Name = "dtpDateRented";
            this.dtpDateRented.Size = new System.Drawing.Size(100, 20);
            this.dtpDateRented.TabIndex = 21;
            // 
            // dtpDateReturned
            // 
            this.dtpDateReturned.CustomFormat = "dd-MMM-yyyy";
            this.dtpDateReturned.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateReturned.Location = new System.Drawing.Point(162, 200);
            this.dtpDateReturned.Name = "dtpDateReturned";
            this.dtpDateReturned.Size = new System.Drawing.Size(100, 20);
            this.dtpDateReturned.TabIndex = 22;
            this.dtpDateReturned.ValueChanged += new System.EventHandler(this.dtpDateReturned_ValueChanged);
            // 
            // gbxItems
            // 
            this.gbxItems.Controls.Add(this.btnDeleteItem);
            this.gbxItems.Controls.Add(this.btnInsertItem);
            this.gbxItems.Controls.Add(this.dgvRentalItems);
            this.gbxItems.Location = new System.Drawing.Point(16, 226);
            this.gbxItems.Name = "gbxItems";
            this.gbxItems.Size = new System.Drawing.Size(492, 243);
            this.gbxItems.TabIndex = 24;
            this.gbxItems.TabStop = false;
            this.gbxItems.Text = "Rental Items";
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteItem.Location = new System.Drawing.Point(123, 200);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(111, 36);
            this.btnDeleteItem.TabIndex = 26;
            this.btnDeleteItem.Text = "Delete Item";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // btnInsertItem
            // 
            this.btnInsertItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertItem.Location = new System.Drawing.Point(6, 200);
            this.btnInsertItem.Name = "btnInsertItem";
            this.btnInsertItem.Size = new System.Drawing.Size(111, 37);
            this.btnInsertItem.TabIndex = 25;
            this.btnInsertItem.Text = "Insert Item";
            this.btnInsertItem.UseVisualStyleBackColor = true;
            this.btnInsertItem.Click += new System.EventHandler(this.btnInsertItem_Click);
            // 
            // dgvRentalItems
            // 
            this.dgvRentalItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRentalItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvRentalItems.Location = new System.Drawing.Point(3, 16);
            this.dgvRentalItems.Name = "dgvRentalItems";
            this.dgvRentalItems.Size = new System.Drawing.Size(486, 170);
            this.dgvRentalItems.TabIndex = 25;
            this.dgvRentalItems.DoubleClick += new System.EventHandler(this.dgvRentalItems_DoubleClick);
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(274, 160);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(111, 60);
            this.btnCreate.TabIndex = 27;
            this.btnCreate.Text = "Create Rental";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // cboCustomer
            // 
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(158, 89);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(227, 21);
            this.cboCustomer.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 20);
            this.label4.TabIndex = 29;
            this.label4.Text = "Workshop:";
            // 
            // txtWorkshop
            // 
            this.txtWorkshop.Location = new System.Drawing.Point(158, 128);
            this.txtWorkshop.Name = "txtWorkshop";
            this.txtWorkshop.Size = new System.Drawing.Size(227, 20);
            this.txtWorkshop.TabIndex = 30;
            // 
            // frmRental
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 526);
            this.Controls.Add(this.txtWorkshop);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboCustomer);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.gbxItems);
            this.Controls.Add(this.dtpDateReturned);
            this.Controls.Add(this.dtpDateRented);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRentalId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmRental";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rental";
            this.Load += new System.EventHandler(this.frmRental_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmRental_Paint);
            this.gbxItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRentalItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtRentalId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDateRented;
        private System.Windows.Forms.DateTimePicker dtpDateReturned;
        private System.Windows.Forms.GroupBox gbxItems;
        private System.Windows.Forms.DataGridView dgvRentalItems;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.Button btnInsertItem;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWorkshop;
    }
}