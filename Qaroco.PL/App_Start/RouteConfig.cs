using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Qaroco.PL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region AdminController URL
            routes.MapRoute(
              name: "AdminBlogAdd",
              url: "AdminBlogAdd",
              defaults: new { controller = "Admin", action = "BlogAdd", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "AdminBlogDelete",
              url: "AdminBlogDelete",
              defaults: new { controller = "Admin", action = "BlogDelete", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "AdminBlogDetail",
              url: "AdminBlogDetail",
              defaults: new { controller = "Admin", action = "BlogDetail", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "AdminBlogs",
              url: "AdminBlogList",
              defaults: new { controller = "Admin", action = "Blogs", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "AdminMessages",
              url: "Mesajlar",
              defaults: new { controller = "Admin", action = "Messages", id = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "AdminMessageDetail",
              url: "MesajDetay",
              defaults: new { controller = "Admin", action = "MessageDetail", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "AdminMessageDelete",
              url: "MesajSil",
              defaults: new { controller = "Admin", action = "MessageDelete", id = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "AdminProfil",
              url: "AdminProfilDetay",
              defaults: new { controller = "Admin", action = "ProfileDetail", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "AdminDashboard",
              url: "AdminDashboard",
              defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
          );
            #endregion

            #region CustomerController URL

            routes.MapRoute(
              name: "CustomerDashboard",
              url: "İsletmePanel",
              defaults: new { controller = "Customer", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "CustomerHelp",
              url: "İsletmeYardim",
              defaults: new { controller = "Customer", action = "Help", id = UrlParameter.Optional }

             );
            routes.MapRoute(
            name: "CustomerProfil",
            url: "MusteriProfilDetay",
            defaults: new { controller = "Customer", action = "CustomerProfile", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "CustomerLog",
            url: "MüsteriHata",
            defaults: new { controller = "Customer", action = "CustomerLogs", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "LatestCargo",
             url: "GecmisKargolarim",
            defaults: new { controller = "Customer", action = "LatestCargo", id = UrlParameter.Optional }
            );
            #endregion

            #region CourierController URL

            routes.MapRoute(
              name: "CourierDashboard",
              url: "KuryePanel",
              defaults: new { controller = "Courier", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "CourierHelp",
              url: "KuryeYardim",
              defaults: new { controller = "Courier", action = "Help", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "CourierProfile",
             url: "KuryeProfilDetay",
             defaults: new { controller = "Courier", action = "CourierProfileDetail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "CourierLog",
            url: "KuryeHata",
            defaults: new { controller = "Courier", action = "CourierLogs", id = UrlParameter.Optional }
           );
            routes.MapRoute(
            name: "GetCargo",
            url: "KargoAl",
            defaults: new { controller = "Courier", action = "GetCargo", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "CourierLatestCargo",
            url: "KuryeIslemeleri",
            defaults: new { controller = "Courier", action = "LatestCargo", id = UrlParameter.Optional }
    );
            #endregion

            #region SolutionsController URL

            routes.MapRoute(
             name: "Courier",
             url: "KuryeOl",
             defaults: new { controller = "Solutions", action = "Courier", id = UrlParameter.Optional }
         );
            routes.MapRoute(
              name: "BusinessManager",
              url: "İsletme",
              defaults: new { controller = "Solutions", action = "BusinessManager", id = UrlParameter.Optional }
          );
            #endregion

            #region UserController URL

            routes.MapRoute(
              name: "Login",
              url: "GirisYap",
              defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
          );

            routes.MapRoute(
              name: "Register",
              url: "KayitOl",
              defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional }
          );
            routes.MapRoute(
           name: "ForgetPassword",
           url: "SifremiUnuttum",
           defaults: new { controller = "User", action = "ForgetPassword", id = UrlParameter.Optional }
       );
            #endregion

            #region HomeController URL

            routes.MapRoute(
               name: "Blog",
               url: "Blog",
               defaults: new { controller = "Home", action = "Blog", id = UrlParameter.Optional }
           );

            // Düzenlemeler Yapılacak
            // routes.MapRoute(
            //    name: "BlogDetail",""""""""
            //    url: "BlogDetay",
            //    defaults: new { controller = "Home", action = "BlogDetail", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
               name: "Pricing",
               url: "Fiyatlandirma",
               defaults: new { controller = "Home", action = "Pricing", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Team",
               url: "Team",
               defaults: new { controller = "Home", action = "Team", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "FAQ",
               url: "SSS",
               defaults: new { controller = "Home", action = "FAQ", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Contact",
               url: "İletisim",
               defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Homepage",
               url: "Anasayfa",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "Security",
             url: "DemoGiris",
             defaults: new { controller = "Home", action = "Security", id = UrlParameter.Optional }
         );


            #endregion
            routes.MapRoute(
               name: "BlogRewrite",
               url: "{blogtitle}-{id}",
               defaults: new { controller = "Home", action = "BlogDetail", id = UrlParameter.Optional }


           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
