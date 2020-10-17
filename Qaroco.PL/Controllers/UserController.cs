using Newtonsoft.Json;
//using NLog.Internal;
using Qaroco.DL;
using Qaroco.DL.ViewModels;
using Qaroco.PL.Models;
using Qaroco.PL.TcControlService;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;

namespace Qaroco.PL.Controllers
{
    public class UserController : Controller
    {
        KPSPublicSoapClient tcControl = new KPSPublicSoapClient();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // GET: Login
        [HttpPost]
        public ActionResult Login(User model)
        {
            if (model.Email != null && model.Password != null)
            {

                CaptchaModel captchaModel;
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    captchaModel = new CaptchaModel(System.Text.Encoding.Default.GetString(webClient.UploadValues("https://www.google.com/recaptcha/api/siteverify", new NameValueCollection()
                    {
                        ["secret"] = "6Lch1uoUAAAAALTxp59SX0NAe9BZ9n8_yjT0VeFV",
                        ["response"] = Request.Form["g-recaptcha-response"],
                        ["remoteip"] = Request.ServerVariables["REMOTE_ADDR"]
                    })));
                }
                if (!captchaModel.success)
                {
                    TempData["Mesaj"] = new TempDataDictionary { { "class", "alert-danger" }, { "Msg", "Güvenlik adımını tamamlayın" } };
                }
                /* Burda Başlar Post eder */
                DataContractJsonSerializer ser =
                        new DataContractJsonSerializer(typeof(User));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, model);
                string data =
                Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClientt = new WebClient();
                webClientt.Headers["Content-type"] = "application/json";
                webClientt.Encoding = Encoding.UTF8;
                var response = webClientt.UploadString("http://localhost:65132/QarocoService.svc/UserLogin/User", "POST", data);
                User user = JsonConvert.DeserializeObject<User>(response);
                /* Burda Biter */

                if (user != null)
                {
                    Session["LoginUser"] = user;

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    DataContractJsonSerializer serializer =
                       new DataContractJsonSerializer(typeof(string));

                    MemoryStream memoryStream = new MemoryStream();
                    serializer.WriteObject(memoryStream, model.Email);
                    string _data =
                    Encoding.UTF8.GetString(memoryStream.ToArray(), 0, (int)memoryStream.Length);
                    WebClient web = new WebClient();
                    web.Headers["Content-type"] = "application/json";
                    web.Encoding = Encoding.UTF8;
                    var result = web.UploadString("http://localhost:65132/QarocoService.svc/User/UserLog", "POST", _data);
                    User _user = JsonConvert.DeserializeObject<User>(result);

                    if (string.IsNullOrEmpty(ipAddress) && _user != null)
                    {
                        Log log = new Log();
                        ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                        log.LogEmail = _user.Email;
                        log.LogDate = DateTime.Now;
                        log.LogDescription = log.LogDate.Value.ToShortDateString() + " Tarihindeki giriş hatanız";
                        log.LogIp = ipAddress;

                        DataContractJsonSerializer ser2 =
                            new DataContractJsonSerializer(typeof(Log));

                        MemoryStream memory = new MemoryStream();
                        ser2.WriteObject(memory, log);
                        string data2 =
                        Encoding.UTF8.GetString(memory.ToArray(), 0, (int)memory.Length);
                        WebClient _webClientt = new WebClient();
                        _webClientt.Headers["Content-type"] = "application/json";
                        _webClientt.Encoding = Encoding.UTF8;
                        var response2 = _webClientt.UploadString("http://localhost:65132/QarocoService.svc/Log/LogAdd", "POST", data2);
                        if (response2 == "true")
                        {
                            TempData["Mesaj"] = new TempDataDictionary { { "class", "alert-danger" }, { "Msg", "Kullanıcı Adı veya şifre hatalı!" } };
                            return View();
                        }
                        else
                        {
                            return View();
                        }

                    }
                    else
                    {
                        return View();
                    }


                }
            }
            else
            {
                TempData["Mesaj"] = new TempDataDictionary { { "class", "alert-danger" }, { "Msg", "Kullanıcı Adı veya şifre Girmediniz!" } };
                return View();
            }

        }

        [HttpPost]
        public ActionResult UserPasswordUpdate(string email)
        {
            DataContractJsonSerializer ser =
                          new DataContractJsonSerializer(typeof(string));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, email);
            string data =
            Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClientt = new WebClient();
            webClientt.Headers["Content-type"] = "application/json";
            webClientt.Encoding = Encoding.UTF8;
            var response = webClientt.UploadString("http://localhost:65132/QarocoService.svc/User/UserLog", "POST", data);
            User user = JsonConvert.DeserializeObject<User>(response);


            Random rnd = new Random();
            int random = rnd.Next(1000, 9999);

            user.Password = random.ToString();
            
            

            DataContractJsonSerializer ser2 =
                   new DataContractJsonSerializer(typeof(User));
            MemoryStream mem2 = new MemoryStream();
            ser2.WriteObject(mem2, user);
            string data22 = Encoding.UTF8.GetString(mem2.ToArray(), 0, (int)mem2.Length);
            WebClient webClientt2 = new WebClient();
            webClientt2.Headers["Content-type"] = "application/json";
            webClientt2.Encoding = Encoding.UTF8;
            
            var _response2 = webClientt2.UploadString("http://localhost:65132/QarocoService.svc/User/UserEdit", "POST", data22);
            if (_response2=="true")
            {
                MailMessage _mm = new MailMessage();
                _mm.SubjectEncoding = Encoding.Default;
                _mm.Subject = "Yeni Şifreniz";
                _mm.BodyEncoding = Encoding.Default;
                _mm.Body = "Maili Gönderen : QAROCO" + "\n" + "Yeni Şifreniz:" + random + "\nKimse ile şifrenizi paylaşmayınız!";


                _mm.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["emailAccount"]);
                _mm.To.Add(user.Email);


                SmtpClient _smtpClient = new SmtpClient();
                _smtpClient.Host = System.Configuration.ConfigurationManager.AppSettings["emailHost"];
                _smtpClient.Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["emailPort"]);
                _smtpClient.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["emailAccount"], System.Configuration.ConfigurationManager.AppSettings["emailPassword"]);
                _smtpClient.EnableSsl = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["emailSLLEnable"]);

                _smtpClient.Send(_mm);
                TempData["Mesaj"] = new TempDataDictionary { { "class", "alert-success" }, { "Msg", "Şifreniz mailinize gönderilmiştir." } };
                return RedirectToAction("Login");
            }
            else
            {
                TempData["Mesaj"] = new TempDataDictionary { { "class", "alert-danger" }, { "Msg", "Hatalı mail!" } };
            }

            return View();
           
        }

        // GET: Register
        [HttpPost]
        public ActionResult Register(User model)
        {
            if (model.UserId == 0)
            {
                ViewBag.result = 1;
                return RedirectToAction("Register");

            }
            else if (model.Email != null && model.BirthYear != null && model.TcNo != null && model.Password != null && model.FirstName != null && model.LastName != null && model.Phone != null)
            {

                CaptchaModel captchaModel;
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    captchaModel = new CaptchaModel(System.Text.Encoding.Default.GetString(webClient.UploadValues("https://www.google.com/recaptcha/api/siteverify", new NameValueCollection()
                    {
                        ["secret"] = "6Lch1uoUAAAAALTxp59SX0NAe9BZ9n8_yjT0VeFV",
                        ["response"] = Request.Form["g-recaptcha-response"],
                        ["remoteip"] = Request.ServerVariables["REMOTE_ADDR"]
                    })));
                }
                if (!captchaModel.success)
                {
                    TempData["Mesaj"] = new TempDataDictionary { { "class", "alert-danger" }, { "Msg", "Güvenlik adımını tamamlayın" } };
                }
                var year = model.BirthYear.Value.Date.Year;

                var result = tcControl.TCKimlikNoDogrula(Convert.ToInt64(model.TcNo), model.FirstName.ToUpper(), model.LastName.ToUpper(), year);

                if (result)
                {
                    if (model.UserId == 2)
                    {

                        CourierVM item = new DL.ViewModels.CourierVM { AddressId = 1, TcNo = model.TcNo, Phone = model.Phone, FirstName = model.FirstName, Password = model.Password, LastName = model.LastName, Email = model.Email, BirthYear = model.BirthYear.Value.Date };
                        DataContractJsonSerializer ser =
                          new DataContractJsonSerializer(typeof(CourierVM));
                        MemoryStream mem = new MemoryStream();
                        ser.WriteObject(mem, item);
                        string data =
                        Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                        WebClient webClientt = new WebClient();
                        webClientt.Headers["Content-type"] = "application/json";
                        webClientt.Encoding = Encoding.UTF8;
                        var response = webClientt.UploadString("http://localhost:65132/QarocoService.svc/Courier/CourierAdd", "POST", data);
                        var resultx = JsonConvert.DeserializeObject(response);
                        return RedirectToAction("Login");
                        // resultx doğru ise ekrana kayıt başarılı çıkar
                    }
                    else if (model.UserId == 1)
                    {
                        CustomerVM item = new DL.ViewModels.CustomerVM { AddressId = 1, TcNo = model.TcNo, Phone = model.Phone, FirstName = model.FirstName, Password = model.Password, LastName = model.LastName, Email = model.Email, BirthYear = model.BirthYear.Value.Date };
                        DataContractJsonSerializer ser =
                          new DataContractJsonSerializer(typeof(CustomerVM));
                        MemoryStream mem = new MemoryStream();
                        ser.WriteObject(mem, item);
                        string data =
                        Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                        WebClient webClientt = new WebClient();
                        webClientt.Headers["Content-type"] = "application/json";
                        webClientt.Encoding = Encoding.UTF8;
                        var response = webClientt.UploadString("http://localhost:65132/QarocoService.svc/Customer/CustomerAdd", "POST", data);
                        var resultx = JsonConvert.DeserializeObject(response);
                        return RedirectToAction("Login");
                        // resultx doğru ise ekrana kayıt başarılı çıkar
                    }
                    else
                    {
                        ViewBag.result = 1;
                        return RedirectToAction("Register");
                    }
                }
                else
                {
                    ViewBag.result = 0;
                    return RedirectToAction("Register");

                }
            }
            else
            {
                ViewBag.result = 1;
                return RedirectToAction("Register");
            }


        }
    }
}