using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrosSalesianos.Controllers
{
    public class AnnounceController : Controller
    {
        //
        // GET: /Announce/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Advertisement()
        {
            return View();
        }

        public ActionResult AnnounceForm()
        {
            return View();
        }
    }
}
