using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using WebApplication1.App_Start;

namespace WebApplication1.Repositories
{
	public class ClientRepository
	{
		private readonly SqliteConfig _sqliteConfig = SqliteConfig.GetInstance();

		public int? MergeClient(int? clientId, string clientName)
		{
			var id = -1;
			string query = "";
			//NEW
			if (clientId == null)
			{
                // escaped quotes \" extraneous Table text removed - [] to () - replaced empty clientId to NULL at Values(...)
                query = String.Format("INSERT INTO Clients (clientId,clientName) VALUES (NULL,\"{1}\"); SELECT last_insert_rowid() FROM Clients", clientId, clientName);
                //query = "Select * from users";
			}
			//UPDATE
			else
			{
                // escaped quotes \" extraneous ' removed
				query = String.Format("UPDATE Clients SET clientName=\"{0}\" WHERE clientId={1}", clientName, clientId);
			}

			var ds = _sqliteConfig.ExecuteQuery(query);

			if (ds.HasData())
			{
                //id = ds.Tables[0].Rows[0]["clientId"].ToInt(-1);
                id = ds.Tables[0].Rows[0][0].ToInt(-1);
			}

			return id;
		}

		public DataSet GetUsers()
		{
			var query = "SELECT * FROM Clients";
			return _sqliteConfig.ExecuteQuery(query);
		}

		public DataSet GetUserById(int clientId)
		{
			var query = String.Format("SELECT * FROM Clients WHERE clientId = {0}", clientId);
			return _sqliteConfig.ExecuteQuery(query);
		}

	}
}