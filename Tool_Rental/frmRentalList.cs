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
    public partial class frmRentalList : Form
    {
        #region Constructors

        public frmRentalList()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void lnkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRental frm = new frmRental();
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();
        }

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
                    long PKID = long.Parse(dgvRentals[0, dgvRentals.CurrentCell.RowIndex]
                        .Value.ToString());

                    // use the DeleteRecord method of the Context class and pass the primary key value
                    // to delete
                    Context.DeleteRecord("Rental", "RentalId", PKID.ToString());
                    PopulateGrid();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("No record exists.", Properties.Settings.Default.ProjectName);
                }
            }
        }

        #endregion

        #region Form Events

        private void frmRentalList_Paint(object sender, PaintEventArgs e)
        {
            // read the new Colour selected and apply it to this form's back color
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmRentalList_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        #endregion

        #region DataGridView Events

        private void dgvRentals_DoubleClick(object sender, EventArgs e)
        {
            if (dgvRentals.CurrentCell == null) return;

            long PKID = long.Parse(dgvRentals[0, dgvRentals.CurrentCell.RowIndex].Value.ToString());

            frmRental frm = new frmRental(PKID);
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();
        }

        #endregion

        #region Helper Method

        /// <summary>
        /// Populates the grid with given SQL query
        /// </summary>
        private void PopulateGrid()
        {
            string sqlQuery = "SELECT Rental.RentalId, Customer.CustomerName, Rental.Workshop, Rental.DateRented, Rental.DateReturned " +
                              "FROM Rental INNER JOIN " +
                              "Customer ON Rental.CustomerId = Customer.CustomerId " +
                              "ORDER BY Rental.RentalId DESC";
            DataTable table = Context.GetDataTable(sqlQuery, "Rentals");
            dgvRentals.DataSource = table;
        } 

        #endregion

    }
}
