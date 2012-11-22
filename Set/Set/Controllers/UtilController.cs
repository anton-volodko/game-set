using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AV.Web.Set.Controllers
{
    public class UtilController : Controller
    {
        //
        // GET: /Util/

        public ActionResult Template(string templateName, string act, string ctrl)
        {
            ViewBag.Name = templateName;
            ViewBag.Action = act;
            ViewBag.Controller = ctrl;
            return View();
        }

    }
}
