using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Domain;

namespace WebApplication1.Models
{
	public class ClientUserViewModel
	{

        public string Title { get; set; }

		public List<User> Users
        {
            get
            {
                return _Users;
            }
            set { }
        }
        private List<User> _Users = new List<User>();

		public List<Client> Clients
        { get
            {
                return _Clients;
            }
          set { }
        }
        private List<Client> _Clients = new List<Client>();
        public void AddClient(string clientname, int id)
        {
            Clients.Add(new Client { Name = clientname, Id = id });
        }
        public void AddUsers(string username, int id, int parentid)
        {
            Users.Add(new User { Name = username, Id = id,ParentId = parentid });
        }
    }
}