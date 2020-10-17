using Newtonsoft.Json;
using Qaroco.BL.Repository.Concrete;
using Qaroco.DL;
using Qaroco.DL.ViewModels;
using Qaroco.PL.Filters;
using Qaroco.PL.QarocoServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;

namespace Qaroco.PL.Controllers
{

    public class AdminController : Controller
    {

        [AdminFilter]
		[HttpGet]
		public ActionResult Index()
        {
            User user = (User)Session["LoginUser"];
            ViewBag.User = user;

			WebClient client = new WebClient();
			client.Encoding = Encoding.UTF8;
			// get blog
			string blogvalue = client.DownloadString("http://localhost:65132/QarocoService.svc/Blog/BlogVMList");
			List<BlogVM> blogs = JsonConvert.DeserializeObject<List<BlogVM>>(blogvalue);
			// get message
			string messagevalue = client.DownloadString("http://localhost:65132/QarocoService.svc/MessageSystem/MessageSystemList");
			List<MessageSystem> messageSystems = JsonConvert.DeserializeObject<List<MessageSystem>>(messagevalue);
			// get User List User ekle servise
			string uservalue = client.DownloadString("http://localhost:65132/QarocoService.svc/User/UserList");
			List<User> userscount = JsonConvert.DeserializeObject<List<User>>(uservalue);

			int Totalblog = blogs.Count();
			ViewBag.totalBlog = Totalblog;

			int TotalMessage = messageSystems.Count();
			ViewBag.totalMessage = TotalMessage;

			int TotalUser = userscount.Count();
			ViewBag.totalUser = TotalUser;


			return View();
        }
        [AdminFilter]
        [HttpGet]
        public ActionResult ProfileDetail()
        {
            User user = (User)Session["LoginUser"];
            if (user != null)
            {
                ViewBag.User = user;
            }
            else
            {
                ViewBag.User = null;
            }
            return View(user);
        }
        [AdminFilter]
        [HttpPost]
        public ActionResult ProfileDetail(User model)
        {
            User userx = (User)Session["LoginUser"];

            ViewBag.User = userx;
            userx.AddressId = model.AddressId;
            userx.RoleId = model.RoleId;
            userx.PictureId = model.PictureId;
            userx.UserId = model.UserId;
            userx.TcNo = model.TcNo;
            userx.BirthYear = model.BirthYear;

            DataContractJsonSerializer ser =
                   new DataContractJsonSerializer(typeof(User));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, userx);
            string data =
            Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClientt = new WebClient();
            webClientt.Headers["Content-type"] = "application/json";
            webClientt.Encoding = Encoding.UTF8;
            var response = webClientt.UploadString("http://localhost:65132/QarocoService.svc/User/UserEdit", "POST", data);
            if (response == "true")
            {
                TempData["Message"] = "Profil güncelleme işleminiz başarıyla gerçekleşti.";
                return View(userx);
            }
            else
            {
                return View();
            }

        }
        [AdminFilter]
        [HttpGet]
        public ActionResult Messages(MessageSystem model)
        {
			int page = 1;
			User user = (User)Session["LoginUser"];
            ViewBag.User = user;
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string value = client.DownloadString("http://localhost:65132/QarocoService.svc/MessageSystem/MessageSystemList");
            List<MessageSystem> messageSystems = JsonConvert.DeserializeObject<List<MessageSystem>>(value);

			double TotalMessage = messageSystems.Count();

			ViewBag.totalMessage = TotalMessage;

			ViewBag.PageCount = Math.Ceiling(TotalMessage / 9);

			page = page < 1 ? 1 : page > ViewBag.PageCount ? ViewBag.PageCount : page;
			ViewBag.CurrentPage = page;
			ViewBag.StartPage = page - 2 < 1 ? 1 : page - 2;
			ViewBag.EndPage = page + 2 > ViewBag.PageCount ? ViewBag.PageCount : page + 2;

			return View(messageSystems.OrderBy(l => l.MessageId).Skip((page - 1) * 9).Take(9).ToList());

			//return View(messageSystems);
		


		}

		[AdminFilter]
        [HttpGet]
		public ActionResult MessageDetail(int id)
		{
            User user = (User)Session["LoginUser"];
            ViewBag.User = user;
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string value = client.DownloadString("http://localhost:65132/QarocoService.svc/MessageSystem/MessageSystemDetail?id="+id+"");
            MessageSystem messageSystems = JsonConvert.DeserializeObject<MessageSystem>(value);
            return View(messageSystems);
        }


		[AdminFilter]
        [HttpPost]
        public ActionResult MessageDelete(int id)
        {
            User user = (User)Session["LoginUser"];
            ViewBag.User = user;
            DataContractJsonSerializer ser =
                    new DataContractJsonSerializer(typeof(MessageSystem));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, id);
            string data =
            Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClientt = new WebClient();
            webClientt.Headers["Content-type"] = "application/json";
            webClientt.Encoding = Encoding.UTF8;
            var response = webClientt.UploadString("http://localhost:65132/QarocoService.svc/MessageSystem/MessageSystemDelete", "POST", data);
            if (response == "true")
            {
                return RedirectToAction("MessageDelete");
            }
            else
            {
                return View();
            }
        }
        [AdminFilter]
        [HttpGet]
        public ActionResult BlogAdd()
        {
            User user = (User)Session["LoginUser"];
            ViewBag.User = user;
            return View();
        }
        [AdminFilter]
        public ActionResult BlogDetail(int id)
        {
            return View();
        }
        [AdminFilter]
        public ActionResult BlogDelete(int id)
        {
            return View();
        }
        [AdminFilter]
        [HttpGet]
        public ActionResult Blogs(BlogVM model)
        {
            User user = (User)Session["LoginUser"];
            ViewBag.User = user;
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string value = client.DownloadString("http://localhost:65132/QarocoService.svc/Blog/BlogVMList");
            List<BlogVM> blogs = JsonConvert.DeserializeObject<List<BlogVM>>(value);
            return View(blogs);
        }

        [HttpPost]
        public ActionResult BlogAdd(BlogVM model) //Blog Ekleme
        {
            User user = (User)Session["LoginUser"];
            ViewBag.User = user;
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var modelblog = JsonConvert.SerializeObject(model);
            var postvalue = client.UploadString("http://localhost:65132/QarocoService.svc/Blog/BlogAdd", modelblog);
            string returnvalue = client.DownloadString("http://localhost:65132/QarocoService.svc/Blog/BlogList");
            List<Blog> blogs = JsonConvert.DeserializeObject<List<Blog>>(returnvalue);
            TempData["Message"] = "Bloğunuz başarıyla paylaşıldı.";
            return View(blogs);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}