using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TURGWEB.Context;
using TURGWEB.Models;

namespace TURGWEB.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            List<news> liste = null;
            ContextDb context = new ContextDb();
            liste = context.DbNews.ToList();

            return View(liste);
        }

        public ActionResult Listing()
        {

            return View();
        }

    }
}