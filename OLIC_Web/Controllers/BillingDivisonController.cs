using BEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OLIC_Web.Controllers
{
    public class BillingDivisonController : Controller
    {
        private readonly BLL.AbstractLayer applicationRepository;

        OLICEntities db = new OLICEntities();

        public BillingDivisonController(BLL.AbstractLayer apprepo)
        {
            applicationRepository = apprepo;
        }

        // GET: BillingDivison
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult VendorAgreement()
        {
            return View();
        }
        
        public ActionResult TestReport()
        {
            return View();
        }
       
        public ActionResult VendorAgreementSDBW()
        {
            return View();
        }
        public ActionResult TestReportSDBW()
        {
            return View();
        }

        public ActionResult DBW_CheckBillStatus()
        {
            ViewBag.CheckDprEstimation = applicationRepository.vw_DprEstimations();

            return View();
        }

    }
}