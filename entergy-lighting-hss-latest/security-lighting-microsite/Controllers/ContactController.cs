using security_lighting_microsite.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace security_lighting_microsite.Controllers
{
    public class ContactController : Controller
    {
        [HttpPost]
        public ActionResult Send(ContactViewModel Model)
        {
            string result = Model.Send();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}