﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Domain;

namespace WebApplication1.Wrappers
{

	//TODO User this class to wrap the datasets from the repositories into objects.  Bonus point for using depencency injection
	public class ClientWrapper
	{
        Repositories.ClientRepository client_repo = new Repositories.ClientRepository();
        
        // implemented but not used by program
		public Client GetClientById(int id)
		{
            var clientrows = client_repo.GetUsers().Tables[0].Rows;
            string clientname = clientrows[0][1].ToString();

            return (new Client { Id = id, Name = clientname });
        }
        // instantiates object to map to data model
		public List<Client> GetClients()
		{
            var clientrows = client_repo.GetUsers().Tables[0].Rows;
            List<Client> clients = new List<Client>();

            for (int i = 0; i < clientrows.Count; i++)
            {
                int id = clientrows[i][0].ToInt(-1);
                string clientname = clientrows[i][1].ToString();

                clients.Add(new Client { Id = id, Name = clientname });
            }
            return clients;
        }

        // added nullable type
        // changed name to Merge Client to match other function names
		public int? MergeClient(int? clientId, string clientName)
		{
            return client_repo.MergeClient(clientId, clientName);
        }
	}
}