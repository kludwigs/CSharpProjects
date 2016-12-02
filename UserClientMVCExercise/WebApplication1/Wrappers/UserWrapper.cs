using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Domain;

namespace WebApplication1.Wrappers
{
    //TODO User this class to wrap the datasets from the repositories into objects.  Bonus point for using depencency injection
    public class UserWrapper
	{
        Repositories.UserRepository user_repo = new Repositories.UserRepository();
        // implemented but not used in program
        public User GetUserById(int id)
        {
            var userrow = user_repo.GetUserById(id).Tables[0].Rows;
            string username = userrow[0][1].ToString();
            int clientid = userrow[0][2].ToInt(-1);
            return new User { Id = id, Name = username, ClientId = clientid };
        }
        // instantiates object to map to data model
        public List<User> GetUsers()
		{
            List<User> users = new List<User>();
            
            var usersrows = user_repo.GetUsers().Tables[0].Rows;

            for (int i = 0; i < usersrows.Count; i++)
            {
                int id = usersrows[i][0].ToInt(-1);
                string username = usersrows[i][1].ToString();
                int clientid = usersrows[i][2].ToInt(-1);

                users.Add(new User { Id = id, Name = username, ClientId = clientid });

            }
            return users;
        }
        //changed function name from update changed return value and userId argument to int?
		public int? MergeUser(int? userId, string username, int clientid)
		{
            return user_repo.MergeUser(userId, username, clientid);
        }
	}
}