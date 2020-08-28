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
    public partial class frmSettings : Form
    {
        #region Constructors

        public frmSettings()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChangeBackGround_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // save the Colour selected by the user in the ColorTheme
                Properties.Settings.Default.ColorTheme = colorDialog.Color;
                Properties.Settings.Default.Save();
                // read the new Colour selected and apply it to this form's back color
                this.BackColor = Properties.Settings.Default.ColorTheme;
            }
        }
        #endregion

        #region Form Events

        private void frmSettings_Paint(object sender, PaintEventArgs e)
        {
            // read the new Colour selected and apply it to this form's back color
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        #endregion
    }
}
