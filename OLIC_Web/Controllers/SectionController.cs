using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OLIC_Web.Controllers
{
    public class SectionController : Controller
    {
        // GET: Section


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult Report()
        {
            return View();
        }


        #region-----------------DBW Details------------------------
        public ActionResult Total_application_deep_borewell()
        {
            return View();
        }
        public ActionResult Total_paid_application__deep_borewell()
        {
            return View();
        }
        public ActionResult Total_cluster_deep_borewell()
        {
            return View();
        }
        public ActionResult Total_rejected_application_deep_borewell()
        {
            return View();
        }
        public ActionResult Feasibility_verification()
        {
            return View();
        }

        public ActionResult Create_cluster()
        {
            return View();
        }

        public ActionResult Cluster_details()
        {
            return View();
        }

        public ActionResult Cluster_details_list()
        {
            return View();
        }

        public ActionResult DPR_DBW()
        {
            return View();
        }


        #endregion

        #region-----------------SDBW Details------------------------

        public ActionResult Feasibility_verification_SDBW()
        {
            return View();
        }



        #endregion



        #region-----------------CLIP Details------------------------

        public ActionResult Feasibility_verification_CLIP()
        {
            return View();
        }

        public ActionResult DPR_CLIP()
        {
            return View();
        }


        #endregion


    }
}