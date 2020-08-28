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
    public partial class frmMenu : Form
    {
        #region Constructors 

        public frmMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRentals_Click(object sender, EventArgs e)
        {
            frmRentalList frm = new frmRentalList();
            frm.ShowDialog();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            frmCustomerList frm = new frmCustomerList();
            frm.ShowDialog();
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            frmToolList frm = new frmToolList();
            frm.ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmReport frm = new frmReport();
            frm.ShowDialog();
        }

        #endregion

        #region Form Events

        private void frmMenu_Paint(object sender, PaintEventArgs e)
        {
            // read the new Colour selected and apply it to this form's back color
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion

    }
}
