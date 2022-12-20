using BEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OLIC_Web.Controllers
{
    public class BillingSubDivisonController : Controller
    {
        private readonly BLL.AbstractLayer applicationRepository;

        OLICEntities db = new OLICEntities();

        public BillingSubDivisonController(BLL.AbstractLayer appRepository)
        {
            applicationRepository = appRepository;
        }


        // GET: BillingSubDivison
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult DBW_CheckBillStatus()
        {
            ViewBag.CheckDprEstimation = applicationRepository.vw_DprEstimations();

            return View();
        }

        public ActionResult CLIP_BillingApproval()
        {
            return View();
        }
        public ActionResult CLIP_CheckBillStatus()
        {
            return View();
        }












    }
}