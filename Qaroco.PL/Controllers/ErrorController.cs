using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qaroco.PL.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult PageError()
        {
            Response.TrySkipIisCustomErrors = true;
            ViewBag.Kaynak = "Hata Meydana Geldi (Yetkisiz Kullanıcı)";
            return View();
        }
        public ActionResult Page404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            ViewBag.Kaynak = "Hata Meydana Geldi Sayfa Bulunamadı";
            return View("PageError");
        }
        public ActionResult Page403()
        {
            ViewBag.Kaynak = "İnternet bağlantınızın ulaşılmaya çalışılan sayfa veya kaynağa herhangi bir kısıtlamadan dolayı erişemediği anlamına gelmektedir.";
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }
        public ActionResult Page500()
        {
            ViewBag.Kaynak = "Web sitenizin sunucusunda meydana gelen bir hata";
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }
    }
}