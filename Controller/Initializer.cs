using SQLConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// Connects to SQL connection to create the database
    /// </summary>
    public class Initializer
    {
        #region Member Variables

        static SQL _sql = new SQL();

        #endregion

        #region Create Database Schema

        /// <summary>
        /// Initialize the Database
        /// </summary>
        public static void CreateDatabaseSchema()
        {
            // Call the SQL CreateDatabase method to create the database in SQL
            _sql.CreateDatabase();
            CreateDatabaseTables();
            SeedDatabaseTables();
        }

        #endregion

        #region Create Database Tables

        /// <summary>
        /// Creates the Database tables
        /// </summary>
        private static void CreateDatabaseTables()
        {
            CreateToolTable();
            CreateCustomerTable();
            CreateRentalTable();
            CreateRentalItemTable();
        }

        /// <summary>
        /// Creates the rental item table in the Database
        /// </summary>
        private static void CreateRentalItemTable()
        {
            string schema = "RentalItemId int IDENTITY(1,1) PRIMARY KEY, " +
                            "RentalId int NOT NULL, " +
                            "ToolId int NOT NULL";

            // Call the CreateDatabaseTable method and pass the table name and schema
            _sql.CreateDatabaseTable("RentalItem", schema);
        }

        /// <summary>
        /// Creates the customer table in the Database
        /// </summary>
        private static void CreateCustomerTable()
        {
            string schema = "CustomerId int IDENTITY(1,1) PRIMARY KEY, " +
                            "CustomerName VARCHAR(70), " +
                            "CustomerPhone VARCHAR(20)";

            // Call the CreateDatabaseTable method and pass the table name and schema
            _sql.CreateDatabaseTable("Customer", schema);
        }

        /// <summary>
        /// Creates the rental table in the Database
        /// </summary>
        private static void CreateRentalTable()
        {
            string schema = "RentalId int IDENTITY(1,1) PRIMARY KEY, " +
                            "CustomerId int NOT NULL, " +
                            "Workshop VARCHAR(70), " +
                            "DateRented DATETIME NOT NULL, " +
                            "DateReturned DATETIME NULL";

            // Call the CreateDatabaseTable method and pass the table name and schema
            _sql.CreateDatabaseTable("Rental", schema);
        }

        /// <summary>
        /// Creates the tool table in the Database
        /// </summary>
        private static void CreateToolTable()
        {
            string schema = "ToolId int IDENTITY(1,1) PRIMARY KEY, " +
                            "Description VARCHAR(70), " +
                            "AssetNumber int, " +
                            "Brand VARCHAR(70), " +
                            "Status bit, " +
                            "Comments VARCHAR(70)";

            // Call the CreateDatabaseTable method and pass the table name and schema
            _sql.CreateDatabaseTable("Tool", schema);
        }

        #endregion

        #region Seed Database Tables

        /// <summary>
        /// Creates the seeds
        /// </summary>
        private static void SeedDatabaseTables()
        {
            SeedToolTable();
            SeedCustomerTable();
            SeedRentalTable();
            SeedRentalItemTable();
        }

        /// <summary>
        /// Seeds the rental item table
        /// </summary>
        private static void SeedRentalItemTable()
        {
            List<string> rentalItems = new List<string>
            {
                // RentalItemId, RentalId, MovieId
                "1, 1, 1",
                "2, 1, 2",
                "3, 2, 3",
                "4, 3, 1",
                "5, 3, 2",
                "6, 3, 3"
            };

            // ColumnNames must match the order of the initialize data above
            string columnNames = "RentalItemId, RentalId, ToolId";

            // Loop through the List of rental items and push the data to the database table
            foreach (var rentalItem in rentalItems)
            {
                _sql.InsertRecord("RentalItem", columnNames, rentalItem);
            }
        }

        /// <summary>
        /// Seeds the rental table
        /// </summary>
        private static void SeedRentalTable()
        {
                List<string> rentals = new List<string>
            {
                // RentalId, CustomerId, DateRented, DateReturned
                $"1, 2, 'Workshop 4', '01-17-2017', null",
                $"2, 3, 'Workshop 4', '06-30-2017', null",
                $"3, 1, 'Studio 3', '06-06-2017', '06-07-2017'"
            };
            // ColumnNames must match the order of the initialize data above
            string columnNames = "RentalId, CustomerId, Workshop, DateRented, DateReturned";

            // Loop through the List rentals and push the data to the database table
            foreach (var rental in rentals)
            {
                _sql.InsertRecord("Rental", columnNames, rental);
            }
        }

        /// <summary>
        /// Seeds the customer table
        /// </summary>
        private static void SeedCustomerTable()
        {
            List<string> customers = new List<string>
            {
                // CustomerId, CustomerName, CustomerPhone
                "8, 'Brandon Lacey', '1234 5678'",
                "11, 'Andrea Rueber', '8765 4321'",
                "10, 'Mitch Stanton', '3222 2233'"
            };

            // Colum Names must match the order of the data above
            string columnNames = "CustomerId, CustomerName, CustomerPhone";

            // Loop through the list of moves and push the data one customer at a time
            foreach (var customer in customers)
            {
                _sql.InsertRecord("Customer", columnNames, customer);
            }
        }

        /// <summary>
        /// Seeds the tool table
        /// </summary>
        private static void SeedToolTable()
        {
            // Create a list of dummy data
            List<string> tools = new List<string>
            {
                // ToolId, Tool description, Tool Asset number, Tool brand,  Tool status, Tool Comments 
                "9, 'Soldering Iron', '1', 'Generic', '1', 'No comments'",
                "7, 'Cordless Drill', '2', 'Generic', '1', 'No comments'",
                "8, 'Circular Saw', '3', 'Generic', '1', 'No comments'",
            };

            string columnNames = "ToolId, Description, AssetNumber, Brand, Status, Comments";

            // Loop through the list of movies and pushg the data one tool at a time
            foreach (var tool in tools)
            {
                _sql.InsertRecord("Tool", columnNames, tool);
            }
        }

        #endregion
    }
}
