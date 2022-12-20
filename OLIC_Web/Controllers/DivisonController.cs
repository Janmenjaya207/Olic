using BEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OLIC_Web.Controllers
{
    public class DivisonController : Controller
    {
        private readonly BLL.AbstractLayer applicationRepository;

        OLICEntities db = new OLICEntities();

        public DivisonController(BLL.AbstractLayer apprepo)
        {
            applicationRepository = apprepo;
        }
        // GET: Divison

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.count = db.DeepBorewell_Registration.Count();
            ViewBag.paid = db.DeepBorewell_Registration.Where(x => x.Is_Bank == true).Count();
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }




        #region-----------------DBW Details------------------------
        public ActionResult Total_application_deep_borewell()
        {
            ViewBag.DeepBorewell = applicationRepository.DbAppDetails();
            return View();
        }
        public ActionResult Total_paid_application__deep_borewell()
        {
            ViewBag.PaidDeepBorewell = applicationRepository.DbAppDetails().Where(x => x.Is_Bank == true).ToList();
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
        public ActionResult verify_application()
        {

            ViewBag.Block = applicationRepository.EEBlock();
            ViewBag.GramPanchayat = applicationRepository.EEGramPanchayat();
            ViewBag.verifyPaidDeepBorewell = applicationRepository.DbAppDetails().Where(x => x.Is_Bank == true).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult verify_application(ApprovalprocessEE bg, string[] Applicationid)
        {
            List<Applicationid> pdetail = new List<Applicationid>();
            for (int i = 0; i < Applicationid.Length; i++)
            {
                Applicationid q = new Applicationid();
                q.Applicationidd = Convert.ToInt32(Applicationid[i]);

                pdetail.Add(q);
                q = new Applicationid();
            }

            bg.ListAssign = pdetail.ToList();
            int count = applicationRepository.Saveverify_application(bg);
            if (count == 1)
            {
                TempData["msg"] = "Data Forwarded Successfully";
            }
            else
            {
                TempData["msg"] = "Something Went Wrong";
            }
            return RedirectToAction("verify_application", "Divison");
        }
        public ActionResult Assigned_application_list()
        {
            return View();
        }
        public ActionResult ShowRegistationdtlsDivision(int id)
        {
            string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];
            ShowRegistratondetails ny = new ShowRegistratondetails();
            ny.sp_DeepBorewell_PattaDetails_Result = applicationRepository.sp_DeepBorewell_PattaDetails_Result(id, baseurl);
            ny.sp_deepBorewell_REgdeatils_Result = applicationRepository.sp_deepBorewell_REgdeatils_Result(id);
            return View(ny);
        }

        public ActionResult DPR_DBW()
        {
            return View();
        }

        public ActionResult Total_Feasible_Farmer_DBW()
        {
            return View();
        }



        #endregion

        #region-----------------SDBW Details------------------------

        public ActionResult Total_application_SDBW()
        {
            return View();
        }
        public ActionResult Total_paid_application_SDBW()
        {
            return View();
        }
        
        public ActionResult Total_rejected_application_SDBW()
        {
            return View();
        }
        public ActionResult verify_application_SDBW()
        {
            return View();
        }
        public ActionResult Assigned_application_list_SDBW()
        {
            return View();
        }
        public ActionResult Total_Feasible_Farmer_SDBW()
        {
            return View();
        }
        public ActionResult DPR_SDBW()
        {
            return View();
        }

        #endregion

        #region-----------------CLIP Details------------------------

        public ActionResult verify_application_CLIP()
        {
            return View();
        }

        public ActionResult Assigned_application_list_CLIP()
        {
            return View();
        }

        public ActionResult Total_application_CLIP()
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

        public ActionResult DPR_CLIP()
        {
            return View();
        }






        #endregion

    }
}