using Newtonsoft.Json;
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
using System.Web;
using System.Web.Mvc;

namespace Qaroco.PL.Controllers
{
	public class CustomerController : Controller
	{
		// GET: Customer
		[CustomerFilter]
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
			string value = client.DownloadString("http://localhost:65132/QarocoService.svc/Order/ListCargoByCustomer?id=" + user.UserId + "");
			List<CargoOrderVM> orders = JsonConvert.DeserializeObject<List<CargoOrderVM>>(value);

			int TotalSendCargo = orders.Count();
			ViewBag.totalSendCargo = TotalSendCargo;

			return View();
		}

		[CustomerFilter]
		public ActionResult SendCargo()
		{
			User user = (User)Session["LoginUser"];
			ViewBag.User = user;
			return View();
		}
		[CustomerFilter]
		[HttpPost]
		public ActionResult SendCargo(ProductInfo info, string lat, string lng, string OrderNote)
		{
			User user = (User)Session["LoginUser"];
			ViewBag.User = user;
			CargoVM cargoVM = new CargoVM();

			cargoVM._ProductInfo = info;
			cargoVM._ProductInfo.ProductDate = DateTime.Now;
			cargoVM._Location = new Location()
			{
				Lat = lat,
				Lng = lng,
				LocationDate = DateTime.Now,
				Description = OrderNote,
				Address = "Şişli"

			};
			cargoVM._Order = new Order()
			{
				CustomerId = user.UserId,
				OrderDate = DateTime.Now,
				OrderNote = OrderNote
			};


			DataContractJsonSerializer ser =
				 new DataContractJsonSerializer(typeof(CargoVM));
			MemoryStream mem = new MemoryStream();
			ser.WriteObject(mem, cargoVM);
			string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
			WebClient webClientt = new WebClient();
			webClientt.Headers["Content-type"] = "application/json";
			webClientt.Encoding = Encoding.UTF8;
			var response = webClientt.UploadString("http://localhost:65132/QarocoService.svc/SendCargo", "POST", data);
			//CargoVM message = JsonConvert.DeserializeObject<CargoVM>(response);
			if (response == "true")
			{
                TempData["Success"] = "Kargo gönderme işlemeniniz başarıyla gerçekleşmiştir.";
                return RedirectToAction("SendCargo");
                
            }
			else
			{
				return View();
			}


		}

		[CustomerFilter]
		[HttpGet]
		public ActionResult Help()
		{
			WebClient client = new WebClient();
			client.Encoding = Encoding.UTF8;
			string value = client.DownloadString("http://unligteningsoft.com/QarocoService.svc/MessageSystem/MessageSystemList");
			List<MessageSystem> messageSystems = JsonConvert.DeserializeObject<List<MessageSystem>>(value);

			User user = (User)Session["LoginUser"];
			ViewBag.User = user;
			return View(messageSystems);
		}
		[CustomerFilter]
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
			//MessageSystem message = JsonConvert.DeserializeObject<MessageSystem>(response);
			if (response == "true")
			{
				TempData["Success"] = "Teşekkürler mesajınız bize iletildi en kısa zamanda tarafınıza dönüş yapılacaktır.";
				return RedirectToAction("Help");
			}
			else
			{
				return View();
			}


		}
		[CustomerFilter]
		[HttpGet]
		public ActionResult CustomerProfile()
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
		[CustomerFilter]
		[HttpPost]
		public ActionResult CustomerProfile(User model)
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
			if (response == "true")
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

		[CustomerFilter]
		[HttpGet]
		public ActionResult CustomerLogs()
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

				return View();
			}
		}

		[CustomerFilter]
		[HttpGet]
		public ActionResult LatestCargo()
		{
			User user = (User)Session["LoginUser"];
			ViewBag.User = user;
			WebClient client = new WebClient();
			client.Encoding = Encoding.UTF8;
			string value = client.DownloadString("http://localhost:65132/QarocoService.svc/Order/ListCargoByCustomer?id=" + user.UserId + "");
			List<CargoOrderVM> orders = JsonConvert.DeserializeObject<List<CargoOrderVM>>(value);
			return View(orders);
		}

	}
}