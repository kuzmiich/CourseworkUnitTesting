using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using WebAutopark.Tests.Infrastructure.Extensions;

namespace WebAutopark.Tests.Infrastructure.TestConnection
{
    internal class InMemoryDatabase
    {
        public IDbConnection Connection { get; }

        public InMemoryDatabase()
        {
            Connection = new SQLiteConnection("Data Source=:memory:");
        }

        public IDbConnection OpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            return Connection;
        }

        public void Insert<T>(string tableName, IEnumerable<T> items)
        {
            var connection = OpenConnection();

            connection.CreateTableIfNotExists<T>(tableName);
            connection.InsertAll(tableName, items);
        }
    }
}