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
		public ActionResult Index()
		{
			var vm = new ClientUserViewModel() {Title = "Sample Code Clients and Users"};
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

		public void MergeClient(int clientId, string clientName)
		{
			throw new NotImplementedException();
		}

		public void MergeUser(int userId, int clientId, string username)
		{
			throw new NotImplementedException();
		}
	}
}