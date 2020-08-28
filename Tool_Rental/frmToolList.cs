using System;
using Controller;
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
    public partial class frmToolList : Form
    {
        #region Constructors

        public frmToolList()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the item?", Properties.Settings.Default.ProjectName,
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    long PKID = long.Parse(dgvTools[0, dgvTools.CurrentCell.RowIndex]
                        .Value.ToString());

                    // use the DeleteRecord method of the Context class and pass the primary key value
                    // to delete
                    Context.DeleteRecord("Tool", "ToolId", PKID.ToString());
                    PopulateGrid();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("No record exists.", Properties.Settings.Default.ProjectName);
                }
            }
        }

        private void lnkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmTool frm = new frmTool();
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();
        }

        #endregion

        #region Form Events

        private void frmToolList_Paint(object sender, PaintEventArgs e)
        {
            // read the new Colour selected and apply it to this form's back color
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmToolList_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        #endregion

        #region DataGridView Events

        private void dgvTools_DoubleClick(object sender, EventArgs e)
        {
            // if no current row or cell selected, do nothing
            if (dgvTools.CurrentCell == null) return;

            // Get the primary key id of the selected row, which is in column 0
            long pkId = long.Parse(dgvTools[0, dgvTools.CurrentCell.RowIndex].Value.ToString());

            frmTool frm = new frmTool(pkId);
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();
        }

        #endregion

        #region Helper Method

        /// <summary>
        /// Pupulates the grid
        /// </summary>
        private void PopulateGrid()
        {
            DataTable dtb = new DataTable();
            dtb = Context.GetDataTable("Tool");
            dgvTools.DataSource = dtb;
        }

        #endregion
    }
}
