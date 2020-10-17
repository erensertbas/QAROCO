using Newtonsoft.Json;
using Qaroco.DL;
using Qaroco.DL.ViewModels;
using Qaroco.PL.Filters;
using Qaroco.PL.QarocoServiceReference;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Qaroco.PL.Controllers
{
    public class CourierController : Controller
    {

		// GET: Courier
        [CourierFilter]
		[HttpGet]
        public ActionResult Index()
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

			DataContractJsonSerializer ser =
			new DataContractJsonSerializer(typeof(string));
			MemoryStream mem = new MemoryStream();
			ser.WriteObject(mem, user.Email);
			string data =
			Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
			WebClient webClientt = new WebClient();
			webClientt.Headers["Content-type"] = "application/json";
			webClientt.Encoding = Encoding.UTF8;

			var response = webClientt.UploadString("http://localhost:65132/QarocoService.svc/Log/LogList", "POST", data);
			List<Log> logs = JsonConvert.DeserializeObject<List<Log>>(response);

			int TotalLog = logs.Count();
			ViewBag.totalLog = TotalLog;

			WebClient client = new WebClient();
			client.Encoding = Encoding.UTF8;
			string value = client.DownloadString("http://localhost:65132/QarocoService.svc/Order/ListCargoByCourier?id=" + user.UserId + "");
			List<CargoOrderVM> cargo = JsonConvert.DeserializeObject<List<CargoOrderVM>>(value);

			int TotalGetCargo = cargo.Count();
			ViewBag.totalGetCargo = TotalGetCargo;



			return View();
        }
        [CourierFilter]
		[HttpGet]
        public ActionResult Help()
		{
            User user = (User)Session["LoginUser"];
            ViewBag.User = user;
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string value = client.DownloadString("http://unligteningsoft.com/QarocoService.svc/MessageSystem/MessageSystemList");
            List<MessageSystem> messageSystems = JsonConvert.DeserializeObject<List<MessageSystem>>(value);
            return View(messageSystems);
		}
        [CourierFilter]
		[HttpPost]
        public ActionResult Help(MessageSystem model)
		{
            DataContractJsonSerializer ser =
         new DataContractJsonSerializer(typeof(MessageSystem));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, model);
            string data =
            Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClientt = new WebClient();
            webClientt.Headers["Content-type"] = "application/json";
            webClientt.Encoding = Encoding.UTF8;
            var response = webClientt.UploadString("http://unligteningsoft.com/QarocoService.svc/MessageSystem/MessageSystemAdd", "POST", data);
            if (response=="true")
            {
                TempData["Success"] = "Teşekkürler mesajınız bize iletildi en kısa zamanda tarafınıza dönüş yapılacaktır.";
                return RedirectToAction("Help");
            }
            else
            {
                //TODO:Mesaj Eklenecek.
                return View();
            }
           
		}
        [CourierFilter]
		[HttpGet]
        public ActionResult CourierProfileDetail()
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
        [CourierFilter]
        [HttpPost]
        public ActionResult CourierProfileDetail(User model)
        {

            User userx = (User)Session["LoginUser"];

            ViewBag.User = userx;
            model.UserId = userx.UserId;
            model.AddressId = userx.AddressId;
            model.PictureId = userx.PictureId;
            model.RoleId = userx.RoleId;
            model.TcNo = userx.TcNo;
            model.BirthYear = userx.BirthYear;


            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(User));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, model);
            string data =
            Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClientt = new WebClient();
            webClientt.Headers["Content-type"] = "application/json";
            webClientt.Encoding = Encoding.UTF8;
            var response = webClientt.UploadString("http://localhost:65132/QarocoService.svc/Customer/CustomerEdit", "POST", data);
            if (response=="true")
            {
                TempData["Message"] = "Profil güncelleme işleminiz başarıyla gerçekleşti";
                return View(userx);
            }
            else
            {
                return View();
               
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");

        }
        [CourierFilter]
        [HttpGet]
        public ActionResult CourierLogs()
        {
            User userx = (User)Session["LoginUser"];
            ViewBag.User = userx;
            DataContractJsonSerializer ser =
        new DataContractJsonSerializer(typeof(string));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, userx.Email);
            string data =
            Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClientt = new WebClient();
            webClientt.Headers["Content-type"] = "application/json";
            webClientt.Encoding = Encoding.UTF8;
            var response = webClientt.UploadString("http://localhost:65132/QarocoService.svc/Log/LogList", "POST", data);
            List<Log> logs = JsonConvert.DeserializeObject<List<Log>>(response);
            if (response != null)
            {
                return View(logs);
            }
            else
            {
                //TODO:Mesaj Eklenecek.
                return View();
            }
        }
        [CourierFilter]
        [HttpGet]
        public ActionResult GetCargo()
        {
            User user = (User)Session["LoginUser"];
            ViewBag.User = user;
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string value = client.DownloadString("http://localhost:65132/QarocoService.svc/Order/ListCargo");
            List<CargoOrderVM> orders = JsonConvert.DeserializeObject<List<CargoOrderVM>>(value);
            return View(orders);
        }
        [CourierFilter]
        [HttpGet]
        public ActionResult LatestCargo()
        {
            User user = (User)Session["LoginUser"];
            ViewBag.User = user;
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string value = client.DownloadString("http://localhost:65132/QarocoService.svc/Order/ListCargoByCourier?id="+user.UserId+"");
            List<CargoOrderVM> orders = JsonConvert.DeserializeObject<List<CargoOrderVM>>(value);
            return View(orders);
        }
        [CourierFilter]
        [HttpPost]
        public ActionResult GetCargo(Order model)
        {
            User user = (User)Session["LoginUser"];
            ViewBag.User = user;
            model.CourierId = user.UserId;

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string value = client.DownloadString("http://localhost:65132/QarocoService.svc/Order/ListCargo");
            List<CargoOrderVM> orders = JsonConvert.DeserializeObject<List<CargoOrderVM>>(value);
            Order getorder=orders.FirstOrDefault(x=>x._Order.OrderId==model.OrderId)._Order;
            getorder.CourierId = user.UserId;


            DataContractJsonSerializer ser =
        new DataContractJsonSerializer(typeof(Order));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, getorder);
            string data =
            Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClientt = new WebClient();
            webClientt.Headers["Content-type"] = "application/json";
            webClientt.Encoding = Encoding.UTF8;
            var response = webClientt.UploadString("http://localhost:65132/QarocoService.svc/UpdateCargo", "POST", data);


            if (response=="true")
            {
                WebClient client2 = new WebClient();
                client2.Encoding = Encoding.UTF8;
                string value2 = client2.DownloadString("http://localhost:65132/QarocoService.svc/Order/ListCargo");
                List<CargoOrderVM> orderss = JsonConvert.DeserializeObject<List<CargoOrderVM>>(value2);
                TempData["Success"] = "Kargo alma işleminiz başarıyla gerçekleşti.";
                return View(orderss);
            }
            else
            {
                return View();

            }
        }

    }
}