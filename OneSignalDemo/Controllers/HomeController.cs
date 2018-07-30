using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OneSignal.CSharp.SDK;
using OneSignal.CSharp.SDK.Resources;
using OneSignal.CSharp.SDK.Resources.Notifications;
using OneSignalDemo.Models;

namespace OneSignalDemo.Controllers
{
    public class HomeController : Controller
    {
        public NotificationCreateResult ret { get; private set; }

        public ActionResult Index()
        {
            // Creo el cliente one signal
            var client = new OneSignalCli()
            {
                AppId = "444ebbf4-2456-48b8-9b03-95bd9df4bb0d",
                ApiKey = "Y2Y1MDFlZTktNjk3My00NTAxLWE3OTctYTAyN2ExNDQ1OTE0"
            };

            // envio notificacion
            string[] segments = new string[] { "All" };
            client.CreateNotification("Juan Perez aplicó a tu oferta", segments);

            return View("Index");
        }


        // Otra forma de hacerlo con el SDK
        public ActionResult Index_SDK()
        {
            var client = new OneSignalClient("Y2Y1MDFlZTktNjk3My00NTAxLWE3OTctYTAyN2ExNDQ1OTE0");

            var options = new OneSignal.CSharp.SDK.Resources.Notifications.NotificationCreateOptions()
            {
                AppId = Guid.Parse("444ebbf4-2456-48b8-9b03-95bd9df4bb0d"),
                IncludedSegments = new List<string> { "All" }
            };

            options.Contents.Add(LanguageCodes.Spanish, "Juan Perez aplicó a tu oferta");

            NotificationCreateResult ret = client.Notifications.Create(options);

            return View();
        }
    }
}