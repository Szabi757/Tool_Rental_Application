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
    public partial class frmTool : Form
    {
        #region Member Variables

        private long _pkId = 0;
        private DataTable _toolTable = null;
        private bool _isNew = false;

        #endregion

        #region Constructors

        public frmTool()
        {
            InitializeComponent();
            _isNew = true;
            SetupForm();
        }

        // Constructor for updating existing record
        public frmTool(long pkId)
        {
            InitializeComponent();
            _pkId = pkId;
            SetupForm();
        }

        private void SetupForm()
        {
            InitializeDataTable();
            BindControls();
        }

        #endregion

        #region Form Events

        private void frmTool_Paint(object sender, PaintEventArgs e)
        {
            // read the new Colour selected and apply it to this form's back color
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion

        #region Button Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }   
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            _toolTable.Rows[0].EndEdit();
            Context.SaveDatabaseTable(_toolTable);
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// This method will initalize the DataTable that we will use in binding this form
        /// </summary>
        private void InitializeDataTable()
        {
            string sqlQuery = $"SELECT * FROM Tool WHERE ToolId = {_pkId}";

            // Get an existing movie record based on the _pkId and the data table will be updateable
            _toolTable = Context.GetDataTable(sqlQuery, "Tool");

            // Check if is new record
            if (_isNew)
            {
                DataRow row = _toolTable.NewRow();
                _toolTable.Rows.Add(row);
            }
        }

        /// <summary>
        /// Binds the data
        /// </summary>
        private void BindControls()
        {
            // Binding the Textbox txtMovieId with the _movieTable and mapping it to the database field
            // called 'MovieId' and using the 'Text' property of the Textbox to display the value of
            // the field
           // txtToolId.DataBindings.Add("Text", _toolTable, "ToolId");
            txtDescription.DataBindings.Add("Text", _toolTable, "Description");
            txtAssetNumber.DataBindings.Add("Text", _toolTable, "AssetNumber");
            txtBrand.DataBindings.Add("Text", _toolTable, "Brand");
            cbStatus.DataBindings.Add("Checked", _toolTable, "Status", true);
            txtComments.DataBindings.Add("Text", _toolTable, "Comments");

        }

        #endregion
    }
}
