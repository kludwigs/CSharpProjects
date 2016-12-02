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
        // When selected use for editing
        [Display(Name = "User Select")]
        public int SelectedUserId { get; set; }

        // Use to assign a client to a user
        [Display(Name = "User Client Select")]
        public int SelectedUserToClientId { get; set; }

        // when selected use for editing
        [Display(Name = "Client Select")]
        public int SelectedClientId { get; set; }

        public string AddEditError { get; set; }

        // client drop down list for editing
        public IEnumerable<SelectListItem> ClientDropDownList { get; set; }
        // user drop down list for editing
        public IEnumerable<SelectListItem> UserDropDownList { get; set; }
        // dropdown list for assigning a user to a client
        public IEnumerable<SelectListItem> UserToClientDropDownList { get; set; }

        public string Title { get; set; }

        public List<User> Users;

        public List<Client> Clients;
    }
}