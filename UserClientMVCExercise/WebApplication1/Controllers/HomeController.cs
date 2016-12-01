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
        
        Repositories.ClientRepository client_repo = new Repositories.ClientRepository();
        Repositories.UserRepository user_repo = new Repositories.UserRepository();

        public ActionResult Index()
		{
            ClientUserViewModel mymodel = new ClientUserViewModel() { Title = "Sample Code Clients and Users" };
            var vm = mymodel;

            var clientrows= client_repo.GetUsers().Tables[0].Rows;
            var usersrows = user_repo.GetUsers().Tables[0].Rows;

            for (int i = 0; i < usersrows.Count; i++)
            {
                int id = usersrows[i][0].ToInt(-1);
                string username = usersrows[i][1].ToString();
                int clientid = usersrows[i][2].ToInt(-1);
                vm.AddUser(username,id,clientid);
                
            }

            for (int i = 0;i<clientrows.Count;i++)
            {
                int id = clientrows[i][0].ToInt(-1);
                string clientname = clientrows[i][1].ToString();
                vm.AddClient(clientname, id);
                
            }
            vm.ClientDropDownList = vm.Clients.Select(x =>new SelectListItem{Value = x.Id.ToString(),Text = x.Name});

            return View(vm);
		}
        private IEnumerable<SelectListItem> GetClientListHelper(ClientUserViewModel vm)
        {
            var clientrows = client_repo.GetUsers().Tables[0].Rows;

            for (int i = 0; i < clientrows.Count; i++)
            {
                int id = clientrows[i][0].ToInt(-1);
                string clientname = clientrows[i][1].ToString();
                vm.AddClient(clientname, id);
            }
            var clients = vm.Clients.Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(clients, "Value", "Text");
        }

        public ActionResult GetClientList()
		{
            ClientUserViewModel mymodel = new ClientUserViewModel() { Title = "Sample Code Clients and Users" };
            var vm = mymodel;
            vm.ClientDropDownList = GetClientListHelper(vm);
            return View(vm);
        }

        public ActionResult GetUserList()
        {
            ClientUserViewModel mymodel = new ClientUserViewModel() { Title = "Sample Code Clients and Users" };
            var vm = mymodel;
            var usersrows = user_repo.GetUsers().Tables[0].Rows;

            for (int i = 0; i < usersrows.Count; i++)
            {
                int id = usersrows[i][0].ToInt(-1);
                string username = usersrows[i][1].ToString();
                int clientid = usersrows[i][2].ToInt(-1);
                vm.AddUser(username, id, clientid);
            }
            return View(vm);
        }

        [ValidateInput(true)]
        public ActionResult UpdateClients(string clientname, int? id)
        {
            ClientUserViewModel mymodel = new ClientUserViewModel() { Title = "Sample Code Clients and Users" };
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
            return RedirectToAction("Index");
        }

        [ValidateInput(true)]
        public ActionResult UpdateUsers(string username, int? id, int? clientid, ClientUserViewModel vm)
        {
            int? returnId;
            int clientid = vm.SelectedClientId;
            //if save
            if (!id.HasValue)
            {
                // update database
                returnId = MergeUser(id, clientid, username);
                // update model
                if (returnId.HasValue)
                    vm.AddUser(username, clientid, returnId.Value);
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
            return RedirectToAction("Index");
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