using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Domain;

namespace WebApplication1.Wrappers
{

	//TODO User this class to wrap the datasets from the repositories into objects.  Bonus point for using depencency injection
	public class ClientWrapper
	{
		public Client GetClientById(int id)
		{
			throw new NotImplementedException();
		}

		public List<Client> GetClients()
		{
			throw new NotImplementedException();
		}

		public void UpdateClient(int clientId, string clientName)
		{
			throw new NotImplementedException();
		}
	}
}