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
    
            //populate dropdowns
            var tmplist = vm.Clients.ToList();
            // user and client drop downs will contain a bogus value to allow the option to create
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
            throw new NotImplementedException();
        }

        public ActionResult GetUserList()
        {
            throw new NotImplementedException();
        }
        [ValidateInput(false)]
        public ActionResult UpdateClients(string clientname, int?id, ClientUserViewModel vm)
        {
            int? returnId;
           
            var selectedId = vm.SelectedClientId;
            // if user dropdown selected we are editing an existing object so assign its id
            if (selectedId > -1)
                id = selectedId;
            returnId = MergeClient(id, clientname);
            return Redirect("Index");
        }
        [ValidateInput(false)]
        public ActionResult UpdateUsers(string username, int? id, ClientUserViewModel vm)
        {
            int? returnId;
            
            int clientid;

            var selectId = vm.SelectedUserId;
            // if selected we are editing - assign id
            if (selectId > -1)
                id = selectId;
            clientid = vm.SelectedUserToClientId;
            returnId = MergeUser(id, clientid, username);

            return Redirect("Index");
        }
        /*use nullable clientId for object creation*/
        // return id for reference
		public int? MergeClient(int? clientId, string clientName)
		{
           return cw.MergeClient(clientId, clientName);
		}
        /*use nullable userId for object creation*/
        // return id for reference
        public int? MergeUser(int? userId, int clientId, string username)
		{
            return uw.MergeUser(userId, username, clientId);
		}
	}
}