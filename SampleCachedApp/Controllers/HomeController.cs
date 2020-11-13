using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using SampleCachedApp.Models;

namespace SampleCachedApp.Controllers
{
    public class HomeController : Controller
    {
        //[OutputCache(Duration = 30, VaryByParam = "id", Location = System.Web.UI.OutputCacheLocation.Server)]
        [OutputCache(CacheProfile = "Multitenant")]
        public ActionResult Index()
        {
            var  db = new SampleDataEntities();
            int institution = 1;
            if (!int.TryParse(Request.QueryString["id"], out institution))
            {
                institution = 1;
            }

            var classes = db.Classes.Where(x => x.InstitutionId.Value == institution);

            ViewData["DateTime"] = DateTime.Now;
            ViewData["institution"] = institution + ".png";

            //Simulate long running queries
            Thread.Sleep(5000);

            System.Diagnostics.Debug.WriteLine("Server Side Processing");
            return View(classes);
        }

        //[OutputCache(Duration = 30, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Client)]
        [OutputCache(CacheProfile = "UserSpecificContent")]
        public ActionResult UserSpecificCache()
        {
            var db = new SampleDataEntities();

            int institution = 1;
            int.TryParse(Request.QueryString["id"], out institution);

            var classes = db.Classes.ToList();

            ViewData["Username"] = "Student-" + new Random(10).Next();
            ViewData["DateTime"] = DateTime.Now;

            //Simulate long running queries
            Thread.Sleep(5000);

            System.Diagnostics.Debug.WriteLine("Server Side Processing");
            return View(classes);
        }

        public ActionResult NotCached()
        {
            ViewBag.Message = "Your application description page.";
            var db = new SampleDataEntities();
            int institution = 2;
            if (!int.TryParse(Request.QueryString["id"], out institution))
            {
                institution = 2;
            }

            var classes = db.Classes.Where(x => x.InstitutionId.Value == institution);

            ViewData["DateTime"] = DateTime.Now;
            ViewData["institution"] = institution + ".png";

            //Simulate long running queries
            Thread.Sleep(5000);

            System.Diagnostics.Debug.WriteLine("Server Side Processing");
            return View(classes);
        }


    }
}