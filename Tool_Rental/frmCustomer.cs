using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool_Rental
{
    public partial class frmCustomer : Form
    {

        #region Member Variables

        long _PKID = 0;
        DataTable _customerTable = null;
        bool _isNew = false;

        #endregion

        #region Constructors

        public frmCustomer()
        {
            InitializeComponent();
            _isNew = true;
            SetupForm();
        }

        public frmCustomer(long pkId)
        {
            InitializeComponent();
            _PKID = pkId;
            SetupForm();
        }

        private void SetupForm()
        {
            InitializeDataTable();
            BindControls();
        }
        #endregion

        #region Button Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // IMPORTANT: Always do the EndEdit before saving your DataTable, otherwise, the data will not save
            _customerTable.Rows[0].EndEdit();

            // Call the Save method of the Context to save the changes to the database
            Context.SaveDatabaseTable(_customerTable);
        }

        #endregion

        #region Form Events

        private void frmCustomer_Paint(object sender, PaintEventArgs e)
        {
            // read the new Colour selected and apply it to this form's back color
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Initalizes the data table
        /// </summary>
        private void InitializeDataTable()
        {
            string sqlQuery = $"SELECT * FROM Customer WHERE CustomerId = {_PKID}";

            // Get an exisiting movie record based on the _PKID and the data table should be an updateable table
            _customerTable = Context.GetDataTable(sqlQuery, "Customer");

            if (_isNew)
            {
                DataRow row = _customerTable.NewRow();
                _customerTable.Rows.Add(row);
            }
        }

        /// <summary>
        /// Binds the data
        /// </summary>
        private void BindControls()
        {
            // Binding the Textbox txtMovieId with the _movioeTable and mapping it to the database field called "MovieId".
            // ANd use the "text" property of the textbox to display the value of the field
            txtCustomerId.DataBindings.Add("Text", _customerTable, "CustomerId");
            txtCustomerName.DataBindings.Add("Text", _customerTable, "CustomerName");
            txtCustomerPhone.DataBindings.Add("Text", _customerTable, "CustomerPhone");
        }

        #endregion
    }
}
