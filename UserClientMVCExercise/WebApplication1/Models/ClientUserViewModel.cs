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

        public List<User> Users;

        public List<Client> Clients;
    }
}