using Newtonsoft.Json;
using Qaroco.DL;
using Qaroco.DL.ViewModels;
using Qaroco.PL.Filters;
using Qaroco.PL.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;

namespace Qaroco.PL.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var user = (User)Session["LoginUser"];
            if (user == null)
            {
                ViewBag.LoginId = 0;
            }
            else
            {
                ViewBag.LoginId = user.RoleId;
            }
            return View();
        }
        // GET: Pricing
        public ActionResult Pricing()
        {
            return View();
        }
        // GET: FAQ
        public ActionResult FAQ()
        {
            return View();
        }

        // GET: BlogDetail
        [HttpGet]
        public ActionResult BlogDetail(int id)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string value = client.DownloadString("http://localhost:65132/QarocoService.svc/Blog/BlogVMList");
            List<BlogVM> Blogdetail = JsonConvert.DeserializeObject<List<BlogVM>>(value);
            var result = Blogdetail.FirstOrDefault(x => x._Blog.BlogId == id);
            return View(result);
        }
        //GEt
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(MessageSystem model)
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
            if (response == "true")
            {
                if (string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Content))
                {
                    return RedirectToAction("Contact");
                }

                if (ModelState.IsValid)
                {
                    MailMessage _mm = new MailMessage();
                    _mm.SubjectEncoding = Encoding.Default;
                    _mm.Subject = model.Title;
                    _mm.BodyEncoding = Encoding.Default;
                    _mm.Body = "Maili Gönderen : " + model.FirstName + model.LastName + "\n" + " Maili : " + model.Email + "\n" + model.Content;


                    _mm.From = new MailAddress(ConfigurationManager.AppSettings["emailAccount"]);
                    _mm.To.Add("cardoor2019@outlook.com");


                    SmtpClient _smtpClient = new SmtpClient();
                    _smtpClient.Host = ConfigurationManager.AppSettings["emailHost"];
                    _smtpClient.Port = int.Parse(ConfigurationManager.AppSettings["emailPort"]);
                    _smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailAccount"], ConfigurationManager.AppSettings["emailPassword"]);
                    _smtpClient.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["emailSLLEnable"]);

                    _smtpClient.Send(_mm);
                    TempData["Success"] = "Teşekkürler mesajınız bize iletildi en kısa zamanda tarafınıza dönüş yapılacaktır.";

                    return RedirectToAction("Contact");
                }
            }
            return RedirectToAction("Contact");


        }

        [HttpGet]
        public ActionResult Blog(string srcBlog) 
        {
            int page = 1;

            //Get işlemi için
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string value = client.DownloadString("http://unligteningsoft.com/QarocoService.svc/Blog/BlogList");
            List<Blog> blogs = JsonConvert.DeserializeObject<List<Blog>>(value);
            //Get işlemi için

            //Sayfalama
            double totalBlog = blogs.Count();

            ViewBag.totalBlog = totalBlog;

            ViewBag.PageCount = Math.Ceiling(totalBlog / 9);
            page = page < 1 ? 1 : page > ViewBag.PageCount ? ViewBag.PageCount : page;

            ViewBag.CurrentPage = page;
            ViewBag.StartPage = page - 2 < 1 ? 1 : page - 2;
            ViewBag.EndPage = page + 2 > ViewBag.PageCount ? ViewBag.PageCount : page + 2;
	
			//Arama işlemi
			//var result = from b in blogs select b;

			if (!string.IsNullOrEmpty(srcBlog))
            {
                var result = blogs.Where(c => c.BlogTitle.Contains(srcBlog));
                return View(result.ToList());
            }
            // Arama işlemi 

            //Sayfalama
            return View(blogs.OrderBy(l => l.BlogId).Skip((page - 1) * 9).Take(9).ToList());
        }


        [HttpGet]
        public ActionResult BlogCommentList()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string value = client.DownloadString("http://unligteningsoft.com/QarocoService.svc/BlogComment/BlogCommentList");
            List<BlogComment> blogComments = JsonConvert.DeserializeObject<List<BlogComment>>(value);
            return View(blogComments);
        }

        [HttpPost]
        public ActionResult BlogDetail(BlogComment model)
        {
            User user = (User)Session["LoginUser"];
            if (user != null)
            {
                model.UserId = user.UserId;
            }
            if (model.UserId == null)
            {
                model.UserId = 1;
            }
            DataContractJsonSerializer ser =
                  new DataContractJsonSerializer(typeof(BlogComment));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, model);
            string data =
            Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClientt = new WebClient();
            webClientt.Headers["Content-type"] = "application/json";
            webClientt.Encoding = Encoding.UTF8;
            var response = webClientt.UploadString("http://unligteningsoft.com/QarocoService.svc/BlogComment/BlogCommentAdd", "POST", data);
            if (response=="true")
            {
                return View();
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

        public ActionResult Team()
        {
            return View();

        }
    }
}