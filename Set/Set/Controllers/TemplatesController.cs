using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AV.Web.Set.Controllers
{
    public class TemplatesController : Controller
    {
        //
        // GET: /Templates/

        public ActionResult Card()
        {
            HttpContext.Response.ContentType = "text/javascript";
            return View();
        }

    }
}
