using BEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OLIC_Web.Controllers
{
    public class FinanceController : Controller
    {
        private readonly BLL.AbstractLayer applicationRepository;

        OLICEntities db = new OLICEntities();

        public FinanceController(BLL.AbstractLayer apprepo)
        {
            applicationRepository = apprepo;
        }
        // GET: Finance
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

        public ActionResult Total_application_deep_borewell()
        {
            ViewBag.DeepBorewell = applicationRepository.DbAppDetails();
            return View();
        }

        public ActionResult Total_paid_application_deep_borewell()
        {
            ViewBag.PaidDeepBorewell = applicationRepository.DbAppDetails().Where(x => x.Is_Bank == true).ToList();
            return View();
        }

        public ActionResult Total_rejected_application_deep_borewell()
        {
            return View();
        }

        public ActionResult UpdatePaymentStatus()
        {
            Paymentstatus nh = new Paymentstatus();
            nh.Vw_DeepBorewellRegdDtl = applicationRepository.DbAppDetails().Where(x => x.Is_Bank == false).ToList();

            return View(nh);
        }
        [HttpPost]
        public ActionResult UpdatePaymentStatus(Paymentstatus nh)
        {
            try
            {
                nh.BankUpdatePayment.Is_Active = true;
                nh.BankUpdatePayment.Is_Delete = false;
                nh.BankUpdatePayment.Add_On = DateTime.Now;

                int count = applicationRepository.SavePaymentStatus(nh);

                if (count == 1)
                {
                    TempData["Message"] = "Payment Update Completed Successfully";
                }
                else
                {
                    TempData["Message"] = "Payment Update Completed Successfully";
                }
                return RedirectToAction("UpdatePaymentStatus", "Finance");
            }

            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something Went Wrong";
            }
            return RedirectToAction("UpdatePaymentStatus", "Finance");
        }


        public ActionResult Details(int Id)
        {
            ShowRegistratondetails frnds = new ShowRegistratondetails();

            var data = applicationRepository.sp_deepBorewell_REgdeatils_Result(Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReturnPayment()
        {
            return View();
        }
        public ActionResult ShowRegistationdtls(int id)
        {
            string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];
            ShowRegistratondetails ny = new ShowRegistratondetails();
            ny.sp_DeepBorewell_PattaDetails_Result = applicationRepository.sp_DeepBorewell_PattaDetails_Result(id, baseurl);
            ny.sp_deepBorewell_REgdeatils_Result = applicationRepository.sp_deepBorewell_REgdeatils_Result(id);
            return View(ny);




        }
    }
}