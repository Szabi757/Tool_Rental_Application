using System.Data;

namespace SQLConnection
{
    interface IQueryDatabase
    {
        DataTable GetDataTable(string tableName);

        DataTable GetDataTable(string tableName, bool isReadOnly);

        DataTable GetDataTable(string sqlQuery, string tableName);

        DataTable GetDataTable(string sqlQuerry, string tableName, bool isReadOnly);
    }
}
