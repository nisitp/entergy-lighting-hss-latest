using security_lighting_microsite.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace security_lighting_microsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string OpCo)
        {
            ViewBag.OpCo = !String.IsNullOrWhiteSpace(OpCo) ? OpCo.ToLower() : null;

            ViewBag.OpCoHasLeds = false;
            if (!String.IsNullOrEmpty(ViewBag.OpCo))
            {
                ViewBag.OpCoCode = Opco.GetOpcoTLA(OpCo);
                var fixtures = security_lighting_microsite.models.Fixture.GetFixtures(ViewBag.OpCoCode);
                ViewBag.OpCoHasLeds = fixtures.HasLEDs;
                ViewBag.PageTitle = "Entergy " + Opco.GetOpcoFullName(OpCo) + " | Security Lighting"; 
            } else {
                ViewBag.PageTitle = "Entergy | Security Lighting"; 
            }

            return View();
        }

        public ActionResult Fixtures(string OpCo)
        {
            var OpCoCode = Opco.GetOpcoTLA(OpCo);
            ViewBag.OpCoCode = OpCoCode;

            return Json(models.Fixture.GetFixtures(OpCoCode), JsonRequestBehavior.AllowGet);
        }

        public ActionResult FixtureSummary(string OpCo, string FixtureType)
        {
            ViewBag.OpCo = OpCo;

            var OpCoCode = Opco.GetOpcoTLA(OpCo);

            var model = models.Fixture.GetFixture(OpCoCode, FixtureType);

            return View("_fixtureSummary", model);
        }

        public ActionResult Fixture(string OpCo, string FixtureType)
        {
            ViewBag.OpCo = OpCo;
            ViewBag.OpCoCode = Opco.GetOpcoTLA(OpCo);

            var model = models.Fixture.GetFixture(ViewBag.OpCoCode, FixtureType);

            ViewBag.PageTitle = "Entergy " + Opco.GetOpcoFullName(OpCo) + " | Security Lighting - " + model.Type;

            return View(model);
        }
    }
}