using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OLIC_Web.Controllers
{
    public class SubDivisonController : Controller
    {
        // GET: SubDivison

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

        public ActionResult Total_assigned_application_DBW()
        {
            return View();
        }
        public ActionResult Total_Feasible_deep_borewell()
        {
            return View();
        }
        public ActionResult Total_cluster_deep_borewell()
        {
            return View();
        }

        public ActionResult Cluster_details_list()
        {
            return View();
        }
        public ActionResult Total_rejected_application_deep_borewell()
        {
            return View();
        }

        public ActionResult Assign_application()
        {
            return View();
        }
        public ActionResult Assigned_application_list()
        {
            return View();
        }

        public ActionResult DPR_DBW()
        {
            return View();
        }

        #endregion

        #region-----------------SDBW Details------------------------

        public ActionResult Total_assigned_application_SDBW()
        {
            return View();
        }
        public ActionResult Total_Feasible_SDBW()
        {
            return View();
        }

        public ActionResult Total_rejected_application_SDBW()
        {
            return View();
        }
        public ActionResult Assign_application_SDBW()
        {
            return View();
        }
        public ActionResult Assigned_application_list_SDBW()
        {
            return View();
        }




        #endregion



        #region-----------------CLIP Details------------------------
        public ActionResult Total_assigned_application_CLIP()
        {
            return View();
        }
        public ActionResult Total_Feasible_Farmer_CLIP()
        {
            return View();
        }
        public ActionResult Total_rejected_application_CLIP()
        {
            return View();
        }
        public ActionResult Assign_application_CLIP()
        {
            return View();
        }

        public ActionResult Assigned_application_list_CLIP()
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