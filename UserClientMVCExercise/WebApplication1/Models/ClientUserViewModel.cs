using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Domain;

namespace WebApplication1.Models
{
	public class ClientUserViewModel
	{

        [Display(Name = "User Select")]
        public int SelectedUserId { get; set; }

        [Display(Name = "Client Select")]
        public int SelectedClientId { get; set; }

        public IEnumerable<SelectListItem> ClientDropDownList { get; set; }
        public IEnumerable<SelectListItem> UserDropDown { get; set; }

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

        //testing function
        public void AddClient(Client client)
        {
            Clients.Add(client);
        }
        public void AddClient(string clientname, int id)
        {
            Clients.Add(new Client { Name = clientname, Id = id });
        }
        //testing function
        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void AddUser(string username, int id, int parentid)
        {
            Users.Add(new User { Name = username, Id = id,ParentId = parentid });
        }
    }
}