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
        ClientUserViewModel mymodel = new ClientUserViewModel() { Title = "Sample Code Clients and Users" };
        Repositories.ClientRepository client_repo = new Repositories.ClientRepository();
        Repositories.UserRepository user_repo = new Repositories.UserRepository();

        public ActionResult Index()
		{
            var vm = mymodel;
            return View(vm);
		}

		public ActionResult GetClientList()
		{
            var vm = mymodel;
            var users =  client_repo.GetUsers();
            return View(vm);
        }

        public ActionResult GetUserList()
        {
            var vm = mymodel;
            var users = user_repo.GetUsers();
            return View(vm);
        }
  
        [ValidateInput(true)]
        public ActionResult UpdateClients(string clientname, int? id)
        {
            var vm = mymodel;
            int? returnId;
            //if save
            if(!id.HasValue)
            { 
                // update database
                returnId =  MergeClient(id, clientname);
                // update model
                if (returnId.HasValue)
                    vm.AddClient(clientname, returnId.Value);
            }
            else
            // if edit
            {
                // update database
                returnId = MergeClient(id, clientname);
                // update model
                if (returnId.HasValue)
                { 
                    var obj = vm.Clients.FirstOrDefault(x => x.Id == returnId.Value);
                    if (obj != null) obj.Name = clientname;
                }
            } 
            return View("Index",vm);
        }

        [ValidateInput(true)]
        public ActionResult UpdateUsers(string username, int? id, int clientid)
        {
            var vm = mymodel;
            int? returnId;
            //if save
            if (!id.HasValue)
            {
                // update database
                returnId = MergeUser(id, clientid, username);
                // update model
                if (returnId.HasValue)
                    vm.AddUsers(username, clientid, returnId.Value);
            }
            else
            // if edit
            {
                // update database
                returnId = MergeUser(id, clientid, username);
                // update model
                if (returnId.HasValue)
                {
                    var obj = vm.Users.FirstOrDefault(x => x.Id == returnId.Value);
                    if (obj != null)
                    {
                        obj.Name = username;
                        obj.ParentId = clientid;
                    }
                }
            }
            return View("Index", vm);
        }

		public int? MergeClient(int? clientId, string clientName)
		{
           return client_repo.MergeClient(clientId, clientName);
		}

		public int? MergeUser(int? userId, int clientId, string username)
		{
            return user_repo.MergeUser(userId, username, clientId);
		}
	}
}