using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace SQLConnection
{
    /// <summary>
    /// Method for the SQL Connection and its relation to the Query and Alter database methods
    /// </summary>
    public class SQL : IQueryDatabase, IAlterDatabase
    {
     
        #region Member Variables

        private Logger _log;
        readonly SqlConnection _sqlConnection = null;
        SqlCommand _sqlCommand = null;

        #endregion

        #region Constructor

      
        public SQL()
        {
            //NLog will recrod errirs in log file
            LogManager.LoadConfiguration("NLog.config");
            _log = LogManager.GetCurrentClassLogger();
            // Get the connection string from app.config
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            // Initialize and create a new SqlConnection object that is needed to connect to a SQL server
            _sqlConnection = new SqlConnection(connectionString);
        }

        #endregion

        #region Alter Database

        /// <summary>
        /// This method will alter the specified database table on a specified
        /// server and database
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="tableStructure">Table Structure</param>
        public void AlterDatabaseTable(string tableName, string tableStructure)
        {
            try
            {
                string sqlQuery = $"ALTER TABLE {tableName} ({tableStructure})";

                using (_sqlCommand = new SqlCommand(sqlQuery, _sqlConnection))
                {
                    if (_sqlConnection.State == ConnectionState.Closed) _sqlConnection.Open();
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _log.Error(e.ToString());
            }
        }

        /// <summary>
        /// This method will create the database on SQL server.
        /// </summary>
        public void CreateDatabase()
        {
            // Create another SqlConnection to point to the server without the database name
            string serverConnString = $"Data Source = {_sqlConnection.DataSource};" +
                                      $"Integrated Security = True";
            SqlConnection sqlServerConn = new SqlConnection(serverConnString);

            // Create a SQL script to crate the database.
            string sqlScript = $"IF NOT EXISTS (SELECT 1 FROM sys.databases " +
                               $"WHERE name = '{_sqlConnection.Database}')" +
                               $"CREATE DATABASE {_sqlConnection.Database}";

            // Create the SqlCommand object that will execute the SQL script aboce
            _sqlCommand = new SqlCommand(sqlScript, sqlServerConn);

            //Check if SqlConnection object is closed before opening. Create error otherwise
            if (sqlServerConn.State == ConnectionState.Closed)
            {
                // Open the SqlConnection object so the SqlCommand object can execute the query
                sqlServerConn.Open();

                // Run the SQL script using the SqlCommand object
                _sqlCommand.ExecuteNonQuery();
                // Close the SqLConnection object as soon as we are done with it.
                sqlServerConn.Close();
            }
        }

        /// <summary>
        /// This method will create a database table on a specified server and DB
        /// </summary>
        /// <param name="tableName">The table name to be created</param>
        /// <param name="tableStructure">The table schema to be added in the table</param>
        public void CreateDatabaseTable(string tableName, string tableStructure)
        {
            try
            {
                // Create the table in the database
                string sqlQuery = "IF NOT EXISTS (SELECT name FROM sysobjects " +
                                   $"WHERE name = '{tableName}') " +
                                   $"CREATE TABLE {tableName} ({tableStructure})";

                using (_sqlCommand = new SqlCommand(sqlQuery, _sqlConnection))
                {
                    if (_sqlConnection.State == ConnectionState.Closed)
                        _sqlConnection.Open();
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _log.Error(e.ToString());
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        /// <summary>
        /// This method will delete a record in the database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pkName"></param>
        /// <param name="pkId"></param>
        public void DeleteRecord(string tableName, string pkName, string pkId)
        {
            string sqlQuery = $"DELETE FROM {tableName} WHERE {pkName} = {pkId} SELECT SCOPE_IDENTITY()";

            try
            {
                using (_sqlCommand = new SqlCommand(sqlQuery, _sqlConnection))
                {
                    if (_sqlConnection.State == ConnectionState.Closed) _sqlConnection.Open();
                    _sqlCommand.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _log.Error(e.ToString());
            }
        }  

        #endregion

        #region Query Database

        /// <summary>
        /// This method will get an updateable table from the database
        /// </summary>
        /// <param name="tableName">Source Table</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string tableName)
        {
            DataTable table = new DataTable(tableName);

            try
            {
                // Using a SqlDataAdapter allows us to make a DataTable updateable as it represents a set of data commands and
                // connection that are use to update a SQL database
                using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", _sqlConnection))
                {
                    if (_sqlConnection.State == ConnectionState.Closed) _sqlConnection.Open();
                    // Based on the sql query we passed as a parameter, the SqlAdapter built-in Command object will send the sql query to 
                    // SQL.  SQL will return with the corresponding record set and populate our DataTable named 'table'.
                    adapter.Fill(table);
                    _sqlConnection.Close();
                    // Configure our DataTable and specify the Primary Key, which is in column 0 (or the first Column).
                    table.PrimaryKey = new DataColumn[] { table.Columns[0] };
                    // Specify that the primary key in column 0 is auto-increment
                    table.Columns[0].AutoIncrement = true;
                    // Seed the primary key value by using the last pkId value.  Seeding the primary key value is to simply set up the
                    // starting value of the auto-increment
                    // to get the current last pkId value
                    // To get the current last pkId value
                    if (table.Rows.Count > 0)
                        table.Columns[0].AutoIncrementSeed = long.Parse(table.Rows[table.Rows.Count - 1][0].ToString());
                    table.Columns[0].AutoIncrementStep = 1;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                _log.Error(e.ToString());
            }
            return table;
        }

        /// <summary>
        /// This method will get a Read Only table from the DB
        /// </summary>
        /// <param name="tableName">Source Table</param>
        /// <param name="isReadOnly">Specify if table if Read Only</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string tableName, bool isReadOnly)
        {
            if (isReadOnly == false) return GetDataTable(tableName);
            DataTable table = new DataTable(tableName);
            try
            {
                using (_sqlCommand = new SqlCommand($"SELECT * FROM {tableName}", _sqlConnection))
                {
                    if (_sqlConnection.State == ConnectionState.Closed) _sqlConnection.Open();
                    using (SqlDataReader reader = _sqlCommand.ExecuteReader())
                    {
                        table.Load(reader);
                        _sqlConnection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                _log.Error(e.ToString());
            }
            return table;
        }

        /// <summary>
        /// Tis method will get an updateable table from the DB
        /// </summary>
        /// <param name="sqlQuery">SQL query to retrieve records</param>
        /// <param name="tableName">Source Table</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string sqlQuery, string tableName)
        {
            DataTable table = new DataTable(tableName);
            try
            {
                // Using a SqlDataAdapter allows us to make a DataTable updateable as it represents a set of data commands and
                // connection that are use to update a SQL database
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, _sqlConnection))
                {
                    if (_sqlConnection.State == ConnectionState.Closed) _sqlConnection.Open();
                    // Based on the sql query we passed as a parameter, the SqlAdapter built-in Command object will send the sql query to 
                    // SQL.  SQL will return with the corresponding record set and populate our DataTable named 'table'.
                    adapter.Fill(table);
                    _sqlConnection.Close();
                    // Configure our DataTable and specify the Primary Key, which is in column 0 (or the first Column).
                    table.PrimaryKey = new DataColumn[] { table.Columns[0] };
                    // Specify that the primary key in column 0 is auto-increment
                    table.Columns[0].AutoIncrement = true;
                    // Seed the primary key value by using the last pkId value.  Seeding the primary key value is to simply set up the
                    // starting value of the auto-increment
                    // to get the current last pkId value
                    if (table.Rows.Count > 0)
                        table.Columns[0].AutoIncrementSeed = long.Parse(table.Rows[table.Rows.Count - 1][0].ToString());
                    // Set the auto-increment step by 1
                    table.Columns[0].AutoIncrementStep = 1;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                _log.Error(e.ToString());
            }
            return table;
        }

        /// <summary>
        /// This method will get an Read-Only table from the database.
        /// </summary>
        /// <param name="sqlQuery">SQL query to retrieve records.</param>
        /// <param name="tableName">Source Table<</param>
        /// <param name="isReadOnly">Specify if table is Read-Only</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string sqlQuery, string tableName, bool isReadOnly)
        {
            if (isReadOnly == false) return GetDataTable(sqlQuery, tableName);
            DataTable table = new DataTable(tableName);
            try
            {
                using (_sqlCommand = new SqlCommand(sqlQuery, _sqlConnection))
                {
                    if (_sqlConnection.State == ConnectionState.Closed) _sqlConnection.Open();
                    using (SqlDataReader reader = _sqlCommand.ExecuteReader())
                    {
                        table.Load(reader);
                        _sqlConnection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                _log.Error(e.ToString());
            }
            return table;
        }

        /// <summary>
        /// This method will insert a record in the table.
        /// </summary>
        /// <param name="tableName">Table name where record will be inserted.</param>
        /// <param name="columnNames">Column names of the table.</param>
        /// <param name="columnValues">Column values to be inserted.</param>
        /// <returns>An int representing the primary key value of the newly
        ///          inserted record.
        /// </returns>
        public int InsertParentRecord(string tableName, string columnNames, string columnValues)
        {
            int id = 0;
            string sqlQuery =
                           $"INSERT INTO {tableName} ({columnNames}) " +
                           $"VALUES ({columnValues}) " +
                           $"SELECT SCOPE_IDENTITY()";
            try
            {
                using (_sqlCommand = new SqlCommand(sqlQuery, _sqlConnection))
                {
                    if (_sqlConnection.State == ConnectionState.Closed)
                        _sqlConnection.Open();

                    // Create and assign the SQL Execute
                    var output = _sqlCommand.ExecuteScalar(); // Executes the query and returns the first value
                    // If the output is not DBNull
                    // Assign the Primary Key to id
                    if (!(output is DBNull))
                    {
                        id = (int)(decimal)output;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _log.Error(e.ToString());
            }
            finally
            {
                _sqlConnection.Close();
            }

            return id;
        }

        /// <summary>
        /// This method will insert a record in the table
        /// </summary>
        /// <param name="tableName">Table name where record will be inserted</param>
        /// <param name="columnNames">Column names of the table</param>
        /// <param name="columnValues">Column values to be inserted</param>
        /// <returns>An int representing the primary key value of the newly inserted record
        /// </returns>
        public int InsertRecord(string tableName, string columnNames, string columnValues)
        {
            int id = 0;
            string idName = columnNames.Split(new string[] { ", " }, StringSplitOptions.None)[0];
            string idValue = columnValues.Split(new string[] { ", " }, StringSplitOptions.None)[0];

            string sqlQuery = $"SET IDENTITY_INSERT {tableName} ON " +
                              $"IF NOT EXISTS (SELECT {idName} FROM {tableName} WHERE {idName}='{idValue}') " +
                              $"INSERT INTO {tableName} ({columnNames}) " +
                              $"VALUES ({columnValues}) " +
                              $"SET IDENTITY_INSERT {tableName} OFF " +
                              $"SELECT SCOPE_IDENTITY()";
            try
            {
                using (_sqlCommand = new SqlCommand(sqlQuery, _sqlConnection))
                {
                    if (_sqlConnection.State == ConnectionState.Closed)
                        _sqlConnection.Open();

                    // Create and assign the SQL Execute
                    var output = _sqlCommand.ExecuteScalar(); // Executes the query and returns the first value
                    // If the output is not DBNull
                    // Assign the Primary Key to id
                    if (!(output is DBNull))
                    {
                        id = (int)(decimal)output;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _log.Error(e.ToString());
            }
            finally
            {
                _sqlConnection.Close();
            }
            return id;
        }

        /// <summary>
        /// This method will update a database table.
        /// </summary>
        /// <param name="dataTable">Source table to be updated</param>
        public void SaveDatabaseTable(DataTable dataTable)
        {
            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM {dataTable.TableName}", _sqlConnection))
                {
                    // Using the SqlCommandBuilder to create the Insert, Update, and Delete command automatically based on the query we have specified
                    // above when initializing a SqlDataAdapter.
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.InsertCommand = commandBuilder.GetInsertCommand();
                    adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                    adapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                    if (_sqlConnection.State == ConnectionState.Closed) _sqlConnection.Open();
                    adapter.Update(dataTable);
                    _sqlConnection.Close();
                    dataTable.AcceptChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _log.Error(e.ToString());
            }
        }

        /// <summary>
        /// This method will update a record in the database
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnNamesAndValues"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public bool UpdateRecord(string tableName, string columnNamesAndValues, string criteria)
        {
            bool IsOk = false;

            string sqlQuery = $"UPDATE {tableName} SET {columnNamesAndValues} WHERE {criteria}";

            try
            {
                using (_sqlCommand = new SqlCommand(sqlQuery, _sqlConnection))
                {
                    if (_sqlConnection.State == ConnectionState.Closed) _sqlConnection.Open();
                    _sqlCommand.ExecuteNonQuery();
                    IsOk = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                IsOk = false;
                _log.Error(e.ToString());
            }
            return IsOk;
        }

        #endregion
    }
}
