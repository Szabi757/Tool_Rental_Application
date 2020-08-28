using SQLConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public static class Context
    {
        /// <summary>
        /// Connects to SQL connection to create the database
        /// </summary>
        #region Member Variables

        static SQL _sql = new SQL();

        #endregion

        #region Constructors

        #endregion

        #region Accessors

        /// <summary>
        /// This method will return all records from the specified database table.
        /// </summary>
        /// <param name="tableName">The database table name where the records will come from</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string tableName)
        {
            return _sql.GetDataTable(tableName);
        }

        /// <summary>
        /// This method will return the records based on the specified SQL query
        /// </summary>
        /// <param name="sqlQuery">the SELECT query that will be used to filter the records</param>
        /// <param name="tableName">The database table name where the records will come from</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sqlQuery, string tableName)
        {
            return _sql.GetDataTable(sqlQuery, tableName);
        }


        /// <summary>
        /// This method will return the records based on the specified SQL query.
        /// </summary>
        /// <param name="sqlQuery">The SELECT query that will be used to filter the records</param>
        /// <param name="tableName">The database table name where the records will come from.</param>
        /// <param name="isReadOnly">To indicate whether the returned database table is updateable or not.</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sqlQuery, string tableName, bool isReadOnly)
        {
            return _sql.GetDataTable(sqlQuery, tableName, isReadOnly);
        }

        #endregion

        #region Mutators
        /// <summary>
        /// This method will save the table.
        /// </summary>
        /// <param name="table">Datatable to save</param>
        public static void SaveDatabaseTable(DataTable table)
        {
            _sql.SaveDatabaseTable(table);
        }
        /// <summary>
        /// This method will insert a parent record to a table
        /// </summary>
        /// <param name="tableName">The table name to insert parent record into</param>
        /// <param name="columnNames">The column name to insert parent record into</param>
        /// <param name="columnValues">The column value which represent table names</param>
        /// <returns></returns>
        public static int InsertParentTable(string tableName, string columnNames, string columnValues)
        {
            return _sql.InsertParentRecord(tableName, columnNames, columnValues);
        }
        /// <summary>
        /// This method will delete a record form a table
        /// </summary>
        /// <param name="tableName">The table from where it's deleted from</param>
        /// <param name="pkName">The name of the primary key</param>
        /// <param name="pkId">The number/value of the primary key</param>
        public static void DeleteRecord(string tableName, string pkName, string pkId)
        {
            _sql.DeleteRecord(tableName, pkName, pkId);
        }

        #endregion
    }
}
