using Sample_AzureMediaPlayer.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample_AzureMediaPlayer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AzureMediaServices ams = new AzureMediaServices();
            return View(ams.ListAssets());
        }
    }
}