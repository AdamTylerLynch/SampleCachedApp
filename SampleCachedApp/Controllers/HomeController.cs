using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleCachedApp.Models;

namespace SampleCachedApp.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = 600,Location = System.Web.UI.OutputCacheLocation.Server, VaryByParam = "institution")]
        public ActionResult Index()
        {
            var  db = new SampleDataEntities();
            int institution = 1;
            int.TryParse(Request.QueryString["institution"], out institution);

            var classes = db.Classes.Where(x => x.InstitutionId.Value == 1);

            ViewData["DateTime"] = DateTime.Now;
        
            return View(classes);
        }

        public ActionResult NotCached()
        {
            ViewBag.Message = "Your application description page.";
            var db = new SampleDataEntities();
            int institution = 1;
            int.TryParse(Request.QueryString["institution"], out institution);

            var classes = db.Classes.Where(x => x.InstitutionId.Value == 2);

            ViewData["DateTime"] = DateTime.Now;

            return View(classes);
        }

        [OutputCache(Duration = 600, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult UserSpecificCache()
        {
            var db = new SampleDataEntities();

            int institution = 1;
            int.TryParse(Request.QueryString["institution"], out institution);

            var classes = db.Classes.Where(x => x.InstitutionId.Value == 2);

            ViewData["Username"] = "Student-" + new Random(10).Next();
            ViewData["DateTime"] = DateTime.Now;

            return View(classes);
        }
    }
}