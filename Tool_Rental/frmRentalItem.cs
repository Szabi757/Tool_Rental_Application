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
    public partial class frmRentalItem : Form
    {

        #region Member Variables

        long _PKID = 0, _RentalID = 0;
        DataTable _dtbRentalItems = null;
        bool _isNew = false;

        #endregion

        #region Constructors

        /// <summary>
        /// This constructor is to create a NEW Rental Item and it requires the RentalID (foreign key)
        /// so that this NEW Rental Item will know its parent record in the Rental table.
        /// Since we already have a Constructor that accepts a parameter of type long, in this constructor we will accept
        /// a parameter of type string.
        /// </summary>
        public frmRentalItem( string RentalId)
        {
            _isNew = true;
            _RentalID = long.Parse(RentalId);
            InitializeComponent();
            InitializeDataTable();
        }

        /// <summary>
        /// This constructor will open an existing Rental Item based on the pKID parameter.
        /// </summary>
        /// <param name="pKID"></param>
        public frmRentalItem(long pKID)
        {
            InitializeComponent();
            _PKID = pKID;
            InitializeDataTable();
        }

        #endregion

        #region Button Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_isNew)
            {
                // this block of code is a hack to make sure that the Validate event of the
                // TextBox txtRentalId will trigger and subsequently will store the value
                // of txtRentalId in the DataTable _dtbRentalItems
                txtRentalId.Focus();
                txtRentalId.Text = _RentalID.ToString();
                btnSave.Focus();
            }

            _dtbRentalItems.Rows[0].EndEdit();
            Context.SaveDatabaseTable(_dtbRentalItems);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Form Events

        private void frmRentalItem_Paint(object sender, PaintEventArgs e)
        {
            // read the new Colour selected and apply it to this form's back color
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmRentalItem_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
            BindControls();
            if (_isNew)
                txtRentalId.Text = _RentalID.ToString();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Initialize the data table
        /// </summary>
        private void InitializeDataTable()
        {
            string sql = $"SELECT * FROM RentalItem WHERE RentalItemId = {_PKID}";
            _dtbRentalItems = Context.GetDataTable(sql, "RentalItem");

            if (_isNew)
            {
                DataRow row = _dtbRentalItems.NewRow();
                _dtbRentalItems.Rows.Add(row);
            }
        }

        /// <summary>
        /// Binds the data
        /// </summary>
        private void BindControls()
        {
            txtRentalId.DataBindings.Add("Text", _dtbRentalItems, "RentalId");
            cboToolId.DataBindings.Add("SelectedValue", _dtbRentalItems, "ToolId");
        }

        /// <summary>
        /// Populates the combobox with tool details
        /// </summary>
        private void PopulateComboBox()
        {
            DataTable dtb = new DataTable();
            dtb = Context.GetDataTable("Tool");
            cboToolId.ValueMember = "ToolId";
            cboToolId.DisplayMember = "Description";
            cboToolId.DataSource = dtb;
        }
        #endregion
    }
}
