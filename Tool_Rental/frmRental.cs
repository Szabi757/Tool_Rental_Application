using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool_Rental
{
    public partial class frmRental : Form
    {

        #region Member Variables

        long _PKID = 0;
        DataTable _rentalTable = null, _rentalItemsTable = null;
        bool _isNew = false;

        #endregion

        #region Constructors

        public frmRental()
        {
            InitializeComponent();
            InitializeNewRental();
        }

        public frmRental(long pkId)
        {
            InitializeComponent();
            InitializeExistingRental(pkId);
        }

        #endregion

        #region Form Events

        private void frmRental_Paint(object sender, PaintEventArgs e)
        {
            // read the new Colour selected and apply it to this form's back color
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmRental_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
            BindControls();
        }

        #endregion

        #region Button Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboCustomer.Text))
            {
                MessageBox.Show("Please select a customer.", Properties.Settings.Default.ProjectName);
            }
            else if (_isNew && _PKID <= 0)
            {
                string columnNames = "CustomerId, Workshop, DateRented, DateReturned";

                // When sending dates in SQL, we will use a string using the format of 'yyyy-MM-dd'
                string dateRented = dtpDateRented.Value.ToString("yyyy-MM-dd");
                long customerId = long.Parse(cboCustomer.SelectedValue.ToString());
                string columnValues = $"{customerId},'{txtWorkshop.Text}', '{dateRented}', null";
                // Push the Parent data to the database using the InsertParentTable of the Context class.  It
                // will then return the primary key Id of the newly created Parent record and we simply store
                // it in the _PKID variable.
                _PKID = Context.InsertParentTable("Rental", columnNames, columnValues);
                // Display the _PKID value in the txtRentalId textbox
                txtRentalId.Text = _PKID.ToString();
                // Call the InitializeDataTable method again to refresh it using the newly created Parent 
                // record from the database.
                InitializeDataTable();
                gbxItems.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtpDateReturned.Text.Equals(" ") == false)
                _rentalTable.Rows[0]["DateReturned"] = dtpDateReturned.Value.ToString("yyyy-MM-dd");

            // always do an EndEdit before saving, otherwise the data will not persist in the
            // database
            _rentalTable.Rows[0].EndEdit();
            Context.SaveDatabaseTable(_rentalTable);
        }

        private void btnInsertItem_Click(object sender, EventArgs e)
        {
            frmRentalItem frm = new frmRentalItem(txtRentalId.Text);
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the item?", Properties.Settings.Default.ProjectName,
        MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    long PKID = long.Parse(dgvRentalItems[0, dgvRentalItems.CurrentCell.RowIndex]
                        .Value.ToString());

                    // use the DeleteRecord method of the Context class and pass the primary key value
                    // to delete
                    Context.DeleteRecord("RentalItem", "RentalItemId", PKID.ToString());
                    PopulateGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No record exists.", Properties.Settings.Default.ProjectName);
                }
            }
        }

        #endregion

        #region DataGridView Events

        /// <summary>
        /// Allows to select a column's details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRentalItems_DoubleClick(object sender, EventArgs e)
        {
            if (dgvRentalItems.CurrentCell == null) return;
            long pkId = long.Parse(dgvRentalItems[0, dgvRentalItems.CurrentCell.RowIndex].Value.ToString());

            frmRentalItem frm = new frmRentalItem(pkId);
            if (frm.ShowDialog() == DialogResult.OK) PopulateGrid();
        }

        #endregion

        #region DateTimePicker Events

        /// <summary>
        /// Allows for return date to be selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpDateReturned_ValueChanged(object sender, EventArgs e)
        {
            dtpDateReturned.CustomFormat = "yyyy-MM-dd";
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Initilaizes the new rental
        /// </summary>
        private void InitializeNewRental()
        {
            _isNew = true;
            InitializeDataTable();
            gbxItems.Enabled = false;
        }

        /// <summary>
        /// Initalize the exisitng rental
        /// </summary>
        /// <param name="pkId">The number/value of the primary key</param>
        private void InitializeExistingRental(long pkId)
        {
            _PKID = pkId;
            InitializeDataTable();
            gbxItems.Enabled = true;
        }

        /// <summary>
        /// Initialize the data table
        /// </summary>
        private void InitializeDataTable()
        {
          _rentalTable = Context.GetDataTable($"SELECT * FROM Rental WHERE RentalId = {_PKID}", "Rental");
           PopulateGrid();
        }

        /// <summary>
        /// Populates the grid with given SQL query
        /// </summary>
        private void PopulateGrid()
        {
            string sqlQuery = "SELECT RentalItem.RentalItemId, RentalItem.RentalId, Tool.Description, Tool.AssetNumber, Tool.Brand, Tool.Status, Tool.Comments " +
                              "FROM RentalItem INNER JOIN " +
                              "Tool ON RentalItem.ToolId = Tool.ToolId " +
                              $"WHERE RentalId = {_PKID} " +
                              "ORDER BY RentalItem.RentalItemId DESC";

            _rentalItemsTable = Context.GetDataTable(sqlQuery, "RentalItem");
            dgvRentalItems.DataSource = _rentalItemsTable;
        }

        /// <summary>
        /// Populates the combo box with the customer details
        /// </summary>
        private void PopulateComboBox()
        {
            // Get all records from our source database table - Customer
            DataTable dtb = Context.GetDataTable("Customer");

            // Set the ValueMember.  The ValueMember is the name of the primary key field of your source database
            // table.  This is the value that will be stored in the database when a user selects a row from the 
            // ComboBox.
            cboCustomer.ValueMember = "CustomerId";

            // Set the DisplayMember.  The DisplayMember is the name of the field of your source database table
            // that we want to use to display in the ComboBox.
            cboCustomer.DisplayMember = "CustomerName";

            // Set the data source of the ComboBox by using the DataTable we have created above - dtb.
            cboCustomer.DataSource = dtb;
        }

        /// <summary>
        /// Binds the data 
        /// </summary>
        private void BindControls()
        {
            txtRentalId.DataBindings.Add("Text", _rentalTable, "RentalId");
            cboCustomer.DataBindings.Add("SelectedValue", _rentalTable, "CustomerId");
            txtWorkshop.DataBindings.Add("Text", _rentalTable, "Workshop");
            dtpDateRented.DataBindings.Add("Text", _rentalTable, "DateRented");
            dtpDateReturned.DataBindings.Add("Text", _rentalTable, "DateReturned");

            // When creating a NEW Rental, we want to make sure that our Customer ComboBox is empty or nothing is
            // selected and our DateReturned DateTimePicker is also empty.
            if (_isNew)
                cboCustomer.SelectedIndex = -1;

            if (_isNew || string.IsNullOrEmpty(_rentalTable.Rows[0]["DateReturned"].ToString()))
            {
                dtpDateReturned.Format = DateTimePickerFormat.Custom;
                dtpDateReturned.CustomFormat = " ";
            }
        }

        #endregion 

    }
}
