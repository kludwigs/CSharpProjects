using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Text.RegularExpressions;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
	{
        Wrappers.ClientWrapper cw = new Wrappers.ClientWrapper();
        Wrappers.UserWrapper uw = new Wrappers.UserWrapper();

        public ActionResult Index()
		{
            ClientUserViewModel mymodel = new ClientUserViewModel() { Title = "Sample Code Clients and Users" };
            var vm = mymodel;
            
            vm.Users = uw.GetUsers();
            vm.Clients = cw.GetClients();
    
            var tmplist = vm.Clients.ToList();
            tmplist.Insert(0, new Domain.Client { Id = -1, Name = "Select To Edit" });
            vm.ClientDropDownList = tmplist.Select(x =>new SelectListItem{Value = x.Id.ToString(),Text = x.Name});
            vm.UserToClientDropDownList = tmplist.Where(x=>x.Id > -1).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

            var tmpulist = vm.Users.ToList();
            tmpulist.Insert(0, new Domain.User { Id = -1, Name = "Select To Edit", ClientId = -1 });
            vm.UserDropDownList = tmpulist.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
     
            return View(vm);
		}

        public ActionResult GetClientList()
		{
            ClientUserViewModel mymodel = new ClientUserViewModel() { Title = "Sample Code Clients and Users" };
            var vm = mymodel;
            vm.Clients = cw.GetClients();
            return View(vm);
        }

        public ActionResult GetUserList()
        {
            ClientUserViewModel mymodel = new ClientUserViewModel() { Title = "Sample Code Clients and Users" };
            var vm = mymodel;
            vm.Users = uw.GetUsers();
            return View(vm);
        }

        [ValidateInput(true)]
        public ActionResult UpdateClients(string clientname, int?id, ClientUserViewModel vm)
        {
            int? returnId;
           
            if (!ValidateInputName(clientname))
            {
                return Redirect("Index");
            }
            var selectedId = vm.SelectedClientId;
            // if selected we are editing
            if (selectedId > -1)
                id = selectedId;
            returnId = MergeClient(id, clientname);
            return Redirect("Index");
        }

        [ValidateInput(true)]
        public ActionResult UpdateUsers(string username, int? id, ClientUserViewModel vm)
        {
            int? returnId;
            
            int clientid;

            var selectId = vm.SelectedUserId;
            // if selected we are editing so assign id
            if (selectId > -1)
                id = selectId;
            if (!ValidateInputName(username))
            {
                TempData["shortMessage"] = "You must input letters or numbers";
                return Redirect("Index");
            }
            clientid = vm.SelectedUserToClientId;
            returnId = MergeUser(id, clientid, username);
            TempData["shortMessage"] = "Insert Successfully";

            return Redirect("Index");
        }
		public int? MergeClient(int? clientId, string clientName)
		{
           return cw.MergeClient(clientId, clientName);
		}
		public int? MergeUser(int? userId, int clientId, string username)
		{
            return uw.MergeUser(userId, username, clientId);
		}
        public bool ValidateInputName(string username)
        {
            if(!String.IsNullOrEmpty(username))
                return Regex.IsMatch(username, @"^[a-zA-Z0-9]+$");
            return false;
        }
	}
}