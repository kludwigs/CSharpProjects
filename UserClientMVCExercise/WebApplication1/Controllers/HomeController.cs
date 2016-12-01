using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

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
            returnId = MergeClient(id, clientname);
            return RedirectToAction("Index");
        }

        [ValidateInput(true)]
        public ActionResult UpdateUsers(string username, int? id, int? clientid, ClientUserViewModel vm)
        {
            int? returnId;
            if (!clientid.HasValue)
                clientid = vm.SelectedClientId;
            returnId = MergeUser(id, clientid.Value, username);

            return RedirectToAction("Index");
        }

		public int? MergeClient(int? clientId, string clientName)
		{
           return cw.MergeClient(clientId, clientName);
		}

		public int? MergeUser(int? userId, int clientId, string username)
		{
            return uw.MergeUser(userId, username, clientId);
		}
	}
}