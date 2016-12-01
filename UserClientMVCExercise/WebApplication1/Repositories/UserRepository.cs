using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication1.App_Start;

namespace WebApplication1.Repositories
{
	public class UserRepository
	{
		private SqliteConfig _sqliteConfig = SqliteConfig.GetInstance();

		public int? MergeUser(int? userId, string username, int clientId)
		{
			var id = -1;
			string query = "";
			//NEW
			if (userId == null)
			{
                //query = String.Format("INSERT INTO TABLE Users [userId, username, clientId] VALUES ({0},{1}, {2}); SELECT last_insert_rowid() FROM Users", userId, username, clientId);
                query = "Select * from users";
			}
			//UPDATE
			else
			{
				query = String.Format("UPDATE Users SET username='{0}' WHERE userId='{1}",username, userId);
			}

			var ds = _sqliteConfig.ExecuteQuery(query);

			if (ds.HasData())
			{
				id = ds.Tables[0].Rows[0]["userid"].ToInt(-1);
			}

			return id;
		}

		public DataSet GetUsers()
		{
			var query = "SELECT * FROM Users";
			return _sqliteConfig.ExecuteQuery(query);
		}

		public DataSet GetUserById(int userId)
		{
			var query = String.Format("SELECT * FROM Users WHERE userId = {0}", userId);
			return _sqliteConfig.ExecuteQuery(query);
		}

	}
}