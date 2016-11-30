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
		public List<User> Users { get; set; }
		public List<Client> Clients { get; set; } 
	}
}