using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace OneSignalDemo.Models
{
    public class OneSignalCli
    {
        public string AppId;
        public string ApiKey;
        private string apiURL = "https://onesignal.com/api/v1";

        public OneSignalCli()
        {

        }
            
        public bool CreateNotification(string message, string[] segments)
        {
            var request = WebRequest.Create(this.apiURL + "/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic " + this.ApiKey);

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                app_id = this.AppId,
                contents = new { en = message },
                included_segments = segments
            };
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                return false;
            }

            System.Diagnostics.Debug.WriteLine(responseContent);

            return true;
        }

    }
}

