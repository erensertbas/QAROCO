using Newtonsoft.Json;
using Qaroco.DL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace Qaroco.PL.Helpers
{
    public class Logger
    {
        public static void AddLog(string ActionSave,string email)
        {
            try
                {
                    Log log = new Log()
                    {
                        LogDate = DateTime.Now,
                        LogDescription = ActionSave,
                        LogIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"],
                    };
                   
                    if (email==null)
                    {
                        log.LogEmail = "Anonymous";
                    }
                    else
                    {
                        log.LogEmail = email;
                    }
                DataContractJsonSerializer ser =
            new DataContractJsonSerializer(typeof(Log));

                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, log);
                string data =
                Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                WebClient webClientt = new WebClient();
                webClientt.Headers["Content-type"] = "application/json";
                webClientt.Encoding = Encoding.UTF8;
                var response = webClientt.UploadString("http://unligteningsoft.com/QarocoService.svc/Log/LogAdd", "POST", data);
               
            }
                catch (Exception ex)
                {

                    throw;
                }
            
        }

    }

}