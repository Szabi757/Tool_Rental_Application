using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool_Rental
{
    public partial class frmReport : Form
    {
        #region Member Variables 

        DataView _dvHistory = null;

        #endregion

        #region Constructors

        public frmReport()
        {
            InitializeComponent();
            PopulateGrid();
            // This will have the "Default" query selected when page opens
            cboExport.SelectedIndex = 0;
        }

        #endregion

        #region Form Events

        private void frmReport_Paint(object sender, PaintEventArgs e)
        {
            // read the new Colour selected and apply it to this form's back color
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        #endregion

        #region Button Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // Create and assign a new StringBuilder
            StringBuilder csv = new StringBuilder();

            // Loop through the DataGridView rows
            foreach (DataGridViewRow row in dgvReports.Rows)
            {
                // Keep track of the row count 
                int rowCount = 1;
                // Loop through all the cells in that row
                foreach (DataGridViewCell cell in row.Cells)
                {
                    // Append the cell to the file with a "," ("," is not added if it is the last cell)
                    csv.Append(cell.Value + (rowCount == row.Cells.Count ? "" : ","));
                    // increment the row count
                    rowCount++;
                }
                // append the new line
                csv.AppendLine();
            }
            // Write the StringBuilder to the ToolRental csv
            // Show a messageBox
            File.WriteAllText(Application.StartupPath + $@"\ToolRentalHistory_{cboExport.Text}_{DateTime.Now.ToString("dd-MM-yyyy")}.csv", csv.ToString());
            MessageBox.Show($"{cboExport.SelectedItem.ToString()} exported to CSV as \"ToolRentalsHistory_{cboExport.Text}.csv\"",
                Properties.Settings.Default.ProjectName);
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Populates the grid data view given SQL query
        /// </summary>
        private void PopulateGrid()
        {
            string sqlQuery = "SELECT RentalItem.RentalItemId, Customer.CustomerName, Tool.Description, Rental.Workshop, Rental.DateRented, Rental.DateReturned " +
                              "FROM Customer INNER JOIN " +
                              "Rental ON Customer.CustomerId = Rental.CustomerId INNER JOIN " +
                              "RentalItem ON Rental.RentalId = RentalItem.RentalId INNER JOIN " +
                              "Tool ON RentalItem.ToolId = Tool.ToolId"; 
            DataTable table = Context.GetDataTable(sqlQuery, "ToolHistory", true);

            _dvHistory = new DataView(table);
            dgvReports.DataSource = _dvHistory;
        }

        /// <summary>
        /// Searches name, tool description or workshop from user input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _dvHistory.RowFilter = $"CustomerName LIKE '%{txtSearch.Text}%' " +
                                   $"OR Description LIKE '%{txtSearch.Text}%' " +
                                   $"OR Workshop LIKE '%{txtSearch.Text}%'";
        }


        #endregion

        #region Combobox Events
        /// <summary>
        /// This method will allow to select a pre determined filtered search, which will update the DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sqlQuery;
            // Set the datatable
            DataTable tempTable = null;
            switch (cboExport.SelectedIndex)
            {
                case 0:
                    PopulateGrid();
                    txtSearch.Enabled = true;
                    return;
                case 1:
                    //Check if tool is active
                    sqlQuery = "SELECT ToolId, Description, Brand, Status FROM Tool " +
                               "WHERE Status = 1";
                    tempTable = Context.GetDataTable(sqlQuery, "Tool");
                    txtSearch.Enabled = false;
                    break;
                case 2:
                    //Check if tool is active by brand
                    sqlQuery = "SELECT ToolId, Description, Brand, Status FROM Tool " +
                               "WHERE Status = 1" +
                               "ORDER BY Brand";
                    tempTable = Context.GetDataTable(sqlQuery, "Tool");
                    txtSearch.Enabled = false;
                    break;
                case 3:
                    // Check if tool is retired by brand
                    sqlQuery = "SELECT ToolId, Description, Brand, Status FROM Tool " +
                               "WHERE Status IS NULL " +
                               "ORDER BY Brand";
                    tempTable = Context.GetDataTable(sqlQuery, "Tool");
                    txtSearch.Enabled = false;
                    break;
                case 4:
                    // Check if tool is retired
                    sqlQuery = "SELECT ToolId, Description, Brand, Status FROM Tool " +
                               "WHERE Status IS NULL";
                    tempTable = Context.GetDataTable(sqlQuery, "Tool");
                    txtSearch.Enabled = false;
                    break;
                case 5:
                    // Check currently rented tools
                    sqlQuery = "SELECT tool.ToolId, Rental.DateRented, Customer.CustomerName, Tool.Description,  Rental.Workshop, Rental.DateReturned " +
                              "FROM Customer INNER JOIN " +
                              "Rental ON Customer.CustomerId = Rental.CustomerId INNER JOIN " +
                              "RentalItem ON Rental.RentalId = RentalItem.RentalId INNER JOIN " +
                              "Tool ON RentalItem.ToolId = Tool.ToolId " +
                              "WHERE DateReturned IS NULL " +
                              "ORDER BY Rental.DateRented DESC";
                    tempTable = Context.GetDataTable(sqlQuery, "Tool");
                    txtSearch.Enabled = false;
                    break;
            }
            dgvReports.DataSource = tempTable;
        }
    }
        #endregion
}
