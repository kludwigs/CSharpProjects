using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1.App_Start
{
	public class SqliteConfig
	{
		private static SqliteConfig _sqliteConfig;
		private SQLiteConnection _sqlite_conn;
		private SQLiteCommand _sqlite_cmd;
		private string _dbName;


		private SqliteConfig()
		{
			_dbName = String.Format("Data Source={0}",
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "SampleDb.db"));
			_sqlite_conn = new SQLiteConnection(_dbName);

		}

		public void PopulateDb()
		{
			_sqlite_conn.Open();
			_sqlite_cmd = _sqlite_conn.CreateCommand();

			//UserData            
			_sqlite_cmd.CommandText = "DROP TABLE IF EXISTS Users";
			_sqlite_cmd.ExecuteNonQuery();

			_sqlite_cmd.CommandText = "CREATE TABLE Users (userId integer primary key, Username  varchar(100), clientId integer, foreign key(clientId) REFERENCES Clients(clientId));";
			_sqlite_cmd.ExecuteNonQuery();

			//Client Data
			_sqlite_cmd.CommandText = "DROP TABLE IF EXISTS Clients";
			_sqlite_cmd.ExecuteNonQuery();
			_sqlite_cmd.CommandText = "CREATE TABLE Clients (clientId integer primary key, clientName varchar(100));";
			_sqlite_cmd.ExecuteNonQuery();
            

		}

		public DataSet ExecuteQuery(string query)
		{
			DataSet ds = new DataSet();
			var da = new SQLiteDataAdapter(query,_sqlite_conn);
			da.Fill(ds);
			return ds;
		}

		public void ExecuteNonQuery(string query)
		{
			_sqlite_cmd.CommandText = query;
			_sqlite_cmd.ExecuteNonQuery();
		}


		public static SqliteConfig GetInstance()
		{
			if(_sqliteConfig == null)
				_sqliteConfig = new SqliteConfig();
			return _sqliteConfig;
		}
	}
}