using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;


namespace Qaroco.PL.Models
{
    public class CaptchaModel
    {
        public CaptchaModel(string json)
        {
            JObject jObject = JObject.Parse(json);
            try
            {
                success = (bool)jObject[nameof(success)];
            }
            catch (Exception ex)
            {
                success = false;
              
            }
            try
            {
                error_codes = (Array)jObject["error_codes"].ToArray<JToken>();
            }
            catch (Exception ex)
            {

                
            }
        }
        public bool success { get; set; }
        public DateTime challenge_ts { get; set; }
        public Array error_codes { get; set; }
        public string hostname { get; set; }

    }
}