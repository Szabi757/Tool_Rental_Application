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
    public partial class frmCustomerList : Form
    {
        #region Member Variables

        public frmCustomerList()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Events

        private void frmCustomerList_Paint(object sender, PaintEventArgs e)
        {
            // read the new Colour selected and apply it to this form's back color
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmCustomerList_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        #endregion

        #region Button Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmCustomer frm = new frmCustomer();
            if (frm.ShowDialog() == DialogResult.OK) PopulateGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the item?", Properties.Settings.Default.ProjectName,
        MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    long PKID = long.Parse(dgvCustomers[0, dgvCustomers.CurrentCell.RowIndex]
                        .Value.ToString());

                    // use the DeleteRecord method of the Context class and pass the primary key value
                    // to delete
                    Context.DeleteRecord("Customer", "CustomerId", PKID.ToString());
                    PopulateGrid();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No record exists.", Properties.Settings.Default.ProjectName);
                }
            }
        }

        private void dgvCustomers_DoubleClick(object sender, EventArgs e)
        {
            // if no current row or cell selected, do nothing
            if (dgvCustomers.CurrentCell == null) return;

            // Get the primary key id of the selected row, which is in column 0
            long pkId = long.Parse(dgvCustomers[0, dgvCustomers.CurrentCell.RowIndex].Value.ToString());

            frmCustomer frm = new frmCustomer(pkId);
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Populates the grid
        /// </summary>
        private void PopulateGrid()
        {
            DataTable table = new DataTable();
            table = Context.GetDataTable("Customer");
            dgvCustomers.DataSource = table;
        }

        #endregion
    }
}
