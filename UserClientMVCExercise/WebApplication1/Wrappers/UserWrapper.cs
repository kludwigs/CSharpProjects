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
		public User GetUserById(int id)
		{
			throw new NotImplementedException();
		}

		public List<User> GetUsers()
		{
			throw new NotImplementedException();
		}

		public void UpdateUser(int userId, string username)
		{
			throw new NotImplementedException();
		}
	}
}