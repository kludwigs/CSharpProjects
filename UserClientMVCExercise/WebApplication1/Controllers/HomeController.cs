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

            ViewBag.InputMessage = TempData["shortMessage"];
            
            vm.Users = uw.GetUsers();
            vm.Clients = cw.GetClients();
            vm.ClientDropDownList = vm.Clients.Select(x =>new SelectListItem{Value = x.Id.ToString(),Text = x.Name});
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
        public ActionResult UpdateClients(string clientname, int? id)
        {
            int? returnId;
            if (!ValidateInputName(clientname))
            {
                TempData["shortMessage"] = "You must input letters or numbers";
                return Redirect("Index");
            }
            returnId = MergeClient(id, clientname);
            TempData["shortMessage"] = "Insert Successfully";
            return Redirect("Index");
        }

        [ValidateInput(true)]
        public ActionResult UpdateUsers(string username, int? id, int? clientid, ClientUserViewModel vm)
        {
            int? returnId;
            if (!ValidateInputName(username))
            {
                TempData["shortMessage"] = "You must input letters or numbers";
                return Redirect("Index");
            }
            if (!clientid.HasValue)
                clientid = vm.SelectedClientId;
            returnId = MergeUser(id, clientid.Value, username);
            TempData["shortMessage"] = "Insert Successfully";

            return Redirect("Index");
        }
        public ActionResult EditUser(int id)
        {
            ClientUserViewModel mymodel = new ClientUserViewModel() { Title = "Edit User Page" };
            var vm = mymodel;
            vm.Users = new List<Domain.User>();
            var user = uw.GetUserById(id);
            vm.Users.Add(user);
            vm.Clients = cw.GetClients();
            ViewBag.AssignedClient = vm.Clients.Find(x => x.Id == user.ClientId).Name;
            vm.ClientDropDownList = vm.Clients.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(vm);
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