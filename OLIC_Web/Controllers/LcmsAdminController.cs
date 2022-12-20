using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEL;


namespace OLIC_Web.Controllers
{
    public class LcmsAdminController : Controller
    {
        private readonly BLL.LCMSAbstractLayer applicationRepository;

        OLICEntities db = new OLICEntities();

        public LcmsAdminController(BLL.LCMSAbstractLayer apprepo)
        {
            applicationRepository = apprepo;
        }
        // GET: LcmsAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserType()
        {
            return View();
        }

        public ActionResult ManageUser()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CourtType(int? id = 0)
        {
            if (id != 0)
            {
                MasterModel masterModel = new MasterModel();
                masterModel.Mst_Court = db.LCMS_Mst_Court.Where(x => x.CourtId == id).FirstOrDefault();
                ViewBag.CourtType = applicationRepository.MstCourtType().ToList();
                return View(masterModel);
            }
            else
            {
                ViewBag.CourtType = applicationRepository.MstCourtType().ToList();
                return View();
            }
        }

        [HttpPost]
        public ActionResult CourtType(MasterModel CourtMasterModel)
        {
            int count = applicationRepository.SaveCourtType(CourtMasterModel);
            if (count == 1)
            {
                TempData["Message"] = "Court Type Added Successfully";
            }
            else
            {
                TempData["Message"] = "Court Type Updated Successfully";
            }
            return RedirectToAction("CourtType", "LcmsAdmin");
        }

        public ActionResult DeleteCourtType(int id)
        {
            var data = applicationRepository.DeleteCourtType(id);
            if (data == true)
            {
                TempData["WarningMessage"] = "Court Type Deleted Successfully";
            }
            else
            {
                TempData["WarningMessage"] = "Something went wrong..!";
            }
            return RedirectToAction("CourtType", "LcmsAdmin");
        }

        [HttpGet]
        public ActionResult CaseType(int? id = 0)
        {
            if (id != 0)
            {
                MasterModel masterModel = new MasterModel();
                masterModel.Mst_Case = db.LCMS_Mst_CaseType.Where(x => x.CaseTypeId == id).FirstOrDefault();
                ViewBag.CaseType = applicationRepository.MstCaseType().ToList();
                return View(masterModel);
            }
            else
            {
                ViewBag.CaseType = applicationRepository.MstCaseType().ToList();
                return View();
            }

        }

        [HttpPost]
        public ActionResult CaseType(MasterModel CaseMasterModel)
        {
            int count = applicationRepository.SaveCaseType(CaseMasterModel);
            if (count == 1)
            {
                TempData["Message"] = "Case Type Added Successfully";
            }
            else
            {
                TempData["Message"] = "Case Type Updated Successfully";
            }
            return RedirectToAction("CaseType", "LcmsAdmin");
        }

        public ActionResult DeleteCaseType(int id)
        {
            var data = applicationRepository.DeleteCaseType(id);
            if (data == true)
            {
                TempData["WarningMessage"] = "Case Type Deleted Successfully";
            }
            else
            {
                TempData["WarningMessage"] = "Something went wrong..!";
            }
            return RedirectToAction("CaseType", "LcmsAdmin");
        }
    }
}