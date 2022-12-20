using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEL;
using DAL;

namespace OLIC_Web.Controllers
{
    public class LcmsController : Controller
    {
        private readonly BLL.LCMSAbstractLayer applicationRepository;
        public LcmsController(BLL.LCMSAbstractLayer apprepo)
        {
            applicationRepository = apprepo;
        }
        // GET: Lcms
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            HttpCookie myCookie = Request.Cookies["mybigcookie"];
            if (myCookie != null)
            {
                ViewBag.uname = Request.Cookies["mybigcookie"]["uname"];
                ViewBag.password = Request.Cookies["mybigcookie"]["password"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string uname, string password, string rememberme)
        {
            int project_type_id = 2;
            string pwd = EncodeDecode.EncodeBase64(password);
            var data = applicationRepository.Login(uname, pwd, project_type_id);
            if (data != null)
            {
                if (rememberme == "1")
                {
                    HttpCookie cookie = new HttpCookie("mybigcookie");
                    cookie.Values.Add("uname", uname);
                    cookie.Values.Add("password", password);
                    cookie.Expires = DateTime.Now.AddYears(50);
                    Response.Cookies.Add(cookie);
                }

                Session["username"] = data.FirstName + " " + data.LastName;
                Session["projecttype"] = project_type_id;
                Session["usertype"] = data.UserType_Id;
                Session["userid"] = data.User_Id;
                return RedirectToAction("dashboard", "lcms");
            }
            else
            {
                TempData["WarningMessage"] = "Wrong username or password";
                return View();
            }
        }
        public ActionResult logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Lcms");
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult InputCaseDetails()
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 3)
                {
                    CaseViewModel caseViewModel = new CaseViewModel();
                    caseViewModel.CaseType = applicationRepository.CaseType();
                    caseViewModel.CourtType = applicationRepository.CourtType();
                    caseViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseViewModel.Status = applicationRepository.LCMS_Status();

                    return View(caseViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult InputCaseDetails(string[] docname, HttpPostedFileBase[] filename, CaseViewModel obj, string casedate)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 3)
                {
                    int id = Convert.ToInt32(Session["userid"]);

                    var data = applicationRepository.SaveCaseDetail(obj, id, casedate);
                    List<LCMS_CaseDoc> casedoc = new List<LCMS_CaseDoc>();

                    if (filename.Length > 0)
                    {
                        for (int i = 0; i < filename.Length; i++)
                        {
                            if (filename[i] != null && docname[i] != "")
                            {
                                LCMS_CaseDoc obj1;
                                obj1 = new LCMS_CaseDoc();

                                obj1 = UploadFileHelper.SaveFileIntoLocal(filename[i], docname[i], data);

                                casedoc.Add(obj1);
                            }
                        }
                    }
                    var data1 = applicationRepository.SaveCaseDoc(casedoc);
                    TempData["Message"] = "Case detail submitted successfully";
                    return RedirectToAction("InputCaseDetailsList", "lcms");
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }

        public ActionResult InputCaseDetailsList()
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 3)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult InputCaseDetailsList(CaseListViewModel obj)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 3)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();
                    caseListViewModel.ComplianceTimee = obj.ComplianceTimee;
                    caseListViewModel.CaseTypeId = obj.CaseTypeId;
                    caseListViewModel.Statuss = obj.Statuss;
                    if (obj.CaseTypeId != 0 && obj.Statuss != "0" && obj.ComplianceTimee != "0")
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().Where(x => x.CaseTypeId == obj.CaseTypeId && x.Status == obj.Statuss && x.ComplianceTime == obj.ComplianceTimee).ToList();
                    }
                    else
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().ToList();
                    }

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult viewcasedetails(string id)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 3)
                {
                    int cid = Convert.ToInt32(EncodeDecode.DecodeBase64(id));
                    string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.sp_Input_Case_Detail_Results = applicationRepository.sp_Input_Case_Detail_Results(cid);
                    caseListViewModel.sp_Input_Case_Doc_Detail_Results = applicationRepository.sp_Input_Case_Doc_Detail_Results(cid, baseurl);
                    caseListViewModel.sp_Lcms_User_Log_Results = applicationRepository.sp_lcms_user_log_Result(cid);

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult editcasedetails(string id)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 3)
                {
                    int cid = Convert.ToInt32(EncodeDecode.DecodeBase64(id));
                    string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];

                    CaseViewModel caseViewModel = new CaseViewModel();
                    caseViewModel.CaseType = applicationRepository.CaseType();
                    caseViewModel.CourtType = applicationRepository.CourtType();
                    caseViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseViewModel.Status = applicationRepository.LCMS_Status();
                    caseViewModel.sp_Input_Case_Doc_Detail_Results = applicationRepository.sp_Input_Case_Doc_Detail_Results(cid, baseurl);
                    caseViewModel.LCMS_CaseDetails = applicationRepository.LCMS_CaseDetails(cid);
                    DateTime? casedate = applicationRepository.LCMS_CaseDetails(cid).CaseFileDate;
                    caseViewModel.CaseFileDate = Convert.ToDateTime(casedate).ToString("yyyy-MM-dd");

                    return View(caseViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult editcasedetails(string[] docname, HttpPostedFileBase[] filename, CaseViewModel obj, string casedate)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 3)
                {
                    int id = Convert.ToInt32(Session["userid"]);

                    var data = applicationRepository.SaveCaseDetail(obj, id, casedate);
                    List<LCMS_CaseDoc> casedoc = new List<LCMS_CaseDoc>();

                    if (filename.Length > 0)
                    {
                        for (int i = 0; i < filename.Length; i++)
                        {
                            if (filename[i] != null && docname[i] != "")
                            {
                                LCMS_CaseDoc obj1;
                                obj1 = new LCMS_CaseDoc();

                                obj1 = UploadFileHelper.SaveFileIntoLocal(filename[i], docname[i], data);

                                casedoc.Add(obj1);
                            }
                        }
                    }
                    var data1 = applicationRepository.SaveCaseDoc(casedoc);
                    TempData["Message"] = "Case detail updated successfully";
                    return RedirectToAction("InputCaseDetailsList", "lcms");
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult deletedocument(int id)
        {
            var data = applicationRepository.deletedocument(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CaseList_law_officer()
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 4)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();
                    caseListViewModel.Lawer_List = applicationRepository.Lawer_List();

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult CaseList_law_officer(CaseListViewModel obj)

        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 4)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();
                    caseListViewModel.Lawer_List = applicationRepository.Lawer_List();
                    caseListViewModel.ComplianceTimee = obj.ComplianceTimee;
                    caseListViewModel.CaseTypeId = obj.CaseTypeId;
                    caseListViewModel.Statuss = obj.Statuss;
                    if (obj.CaseTypeId != 0 && obj.Statuss != "0" && obj.ComplianceTimee != "0")
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().Where(x => x.CaseTypeId == obj.CaseTypeId && x.Status == obj.Statuss && x.ComplianceTime == obj.ComplianceTimee).ToList();
                    }
                    else
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().ToList();
                    }

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult assignlawer(int caseid, int lawer, string message)
        {
            var data = applicationRepository.assignlawer(caseid, lawer, message);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult viewcasedetails_lo(string id)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 4)
                {
                    int cid = Convert.ToInt32(EncodeDecode.DecodeBase64(id));
                    string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.sp_Input_Case_Detail_Results = applicationRepository.sp_Input_Case_Detail_Results(cid);
                    caseListViewModel.sp_Input_Case_Doc_Detail_Results = applicationRepository.sp_Input_Case_Doc_Detail_Results(cid, baseurl);
                    caseListViewModel.sp_Lcms_User_Log_Results = applicationRepository.sp_lcms_user_log_Result(cid);

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult viewcasedetails_lo(string caseid, string remarks)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 4)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    int cid = Convert.ToInt32(caseid);

                    var data = applicationRepository.logdetails(id, cid, remarks);

                    return RedirectToAction("CaseList_law_officer", "lcms");
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult editcasedetails_lo(string id)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 4)
                {
                    int cid = Convert.ToInt32(EncodeDecode.DecodeBase64(id));
                    string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];

                    CaseViewModel caseViewModel = new CaseViewModel();
                    caseViewModel.CourtType = applicationRepository.CourtType();
                    caseViewModel.CaseType = applicationRepository.CaseType();
                    caseViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseViewModel.Status = applicationRepository.LCMS_Status();
                    caseViewModel.sp_Input_Case_Doc_Detail_Results = applicationRepository.sp_Input_Case_Doc_Detail_Results(cid, baseurl);
                    caseViewModel.LCMS_CaseDetails = applicationRepository.LCMS_CaseDetails(cid);
                    DateTime? casedate = applicationRepository.LCMS_CaseDetails(cid).CaseFileDate;
                    caseViewModel.CaseFileDate = Convert.ToDateTime(casedate).ToString("yyyy-MM-dd");

                    return View(caseViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult editcasedetails_lo(string[] docname, HttpPostedFileBase[] filename, CaseViewModel obj, string casedate)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 4)
                {
                    int id = Convert.ToInt32(Session["userid"]);

                    var data = applicationRepository.SaveCaseDetail(obj, id, casedate);
                    List<LCMS_CaseDoc> casedoc = new List<LCMS_CaseDoc>();

                    if (filename.Length > 0)
                    {
                        for (int i = 0; i < filename.Length; i++)
                        {
                            if (filename[i] != null && docname[i] != "")
                            {
                                LCMS_CaseDoc obj1;
                                obj1 = new LCMS_CaseDoc();

                                obj1 = UploadFileHelper.SaveFileIntoLocal(filename[i], docname[i], data);

                                casedoc.Add(obj1);
                            }
                        }
                    }
                    var data1 = applicationRepository.SaveCaseDoc(casedoc);
                    TempData["Message"] = "Case detail updated successfully";
                    return RedirectToAction("CaseList_law_officer", "lcms");
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult CaseList_show_osd()
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 9)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult CaseList_show_osd(CaseListViewModel obj)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 9)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();
                    caseListViewModel.ComplianceTimee = obj.ComplianceTimee;
                    caseListViewModel.CaseTypeId = obj.CaseTypeId;
                    caseListViewModel.Statuss = obj.Statuss;
                    if (obj.CaseTypeId != 0 && obj.Statuss != "0" && obj.ComplianceTimee != "0")
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().Where(x => x.CaseTypeId == obj.CaseTypeId && x.Status == obj.Statuss && x.ComplianceTime == obj.ComplianceTimee).ToList();
                    }
                    else
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().ToList();
                    }

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult viewcasedetails_osd(string id)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 9)
                {
                    int cid = Convert.ToInt32(EncodeDecode.DecodeBase64(id));
                    string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.sp_Input_Case_Detail_Results = applicationRepository.sp_Input_Case_Detail_Results(cid);
                    caseListViewModel.sp_Input_Case_Doc_Detail_Results = applicationRepository.sp_Input_Case_Doc_Detail_Results(cid, baseurl);
                    caseListViewModel.sp_Lcms_User_Log_Results = applicationRepository.sp_lcms_user_log_Result(cid);
                    caseListViewModel.Lcms_user_type = applicationRepository.Lcms_user_type();

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult viewcasedetails_osd(string caseid, string remarks, string btn, string forwardto)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 9)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    int cid = Convert.ToInt32(caseid);
                    if (btn == "1")
                    {
                        var data = applicationRepository.logdetails(id, cid, remarks, btn, forwardto);
                    }
                    else
                    {
                        var data = applicationRepository.logdetails(id, cid, remarks, btn, forwardto);
                    }

                    return RedirectToAction("CaseList_show_osd", "lcms");
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult CaseList_show_ed()
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 7)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult CaseList_show_ed(CaseListViewModel obj)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 7)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();
                    caseListViewModel.ComplianceTimee = obj.ComplianceTimee;
                    caseListViewModel.CaseTypeId = obj.CaseTypeId;
                    caseListViewModel.Statuss = obj.Statuss;
                    if (obj.CaseTypeId != 0 && obj.Statuss != "0" && obj.ComplianceTimee != "0")
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().Where(x => x.CaseTypeId == obj.CaseTypeId && x.Status == obj.Statuss && x.ComplianceTime == obj.ComplianceTimee).ToList();
                    }
                    else
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().ToList();
                    }

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult viewcasedetails_ed(string id)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 7)
                {
                    int cid = Convert.ToInt32(EncodeDecode.DecodeBase64(id));
                    string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.sp_Input_Case_Detail_Results = applicationRepository.sp_Input_Case_Detail_Results(cid);
                    caseListViewModel.sp_Input_Case_Doc_Detail_Results = applicationRepository.sp_Input_Case_Doc_Detail_Results(cid, baseurl);
                    caseListViewModel.sp_Lcms_User_Log_Results = applicationRepository.sp_lcms_user_log_Result(cid);
                    caseListViewModel.Lcms_user_type_ed = applicationRepository.Lcms_user_type_ed();

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult viewcasedetails_ed(string caseid, string remarks, string btn, string forwardto)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 7)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    int cid = Convert.ToInt32(caseid);
                    if (btn == "1")
                    {
                        var data = applicationRepository.logdetails_ed(id, cid, remarks, btn, forwardto);
                    }
                    else
                    {
                        var data = applicationRepository.logdetails_ed(id, cid, remarks, btn, forwardto);
                    }

                    return RedirectToAction("CaseList_show_ed", "lcms");
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult CaseList_show_md()
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 6)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult CaseList_show_md(CaseListViewModel obj)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 6)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();
                    caseListViewModel.ComplianceTimee = obj.ComplianceTimee;
                    caseListViewModel.CaseTypeId = obj.CaseTypeId;
                    caseListViewModel.Statuss = obj.Statuss;
                    if (obj.CaseTypeId != 0 && obj.Statuss != "0" && obj.ComplianceTimee != "0")
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().Where(x => x.CaseTypeId == obj.CaseTypeId && x.Status == obj.Statuss && x.ComplianceTime == obj.ComplianceTimee).ToList();
                    }
                    else
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().ToList();
                    }

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult viewcasedetails_md(string id)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 6)
                {
                    int cid = Convert.ToInt32(EncodeDecode.DecodeBase64(id));
                    string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.sp_Input_Case_Detail_Results = applicationRepository.sp_Input_Case_Detail_Results(cid);
                    caseListViewModel.sp_Input_Case_Doc_Detail_Results = applicationRepository.sp_Input_Case_Doc_Detail_Results(cid, baseurl);
                    caseListViewModel.sp_Lcms_User_Log_Results = applicationRepository.sp_lcms_user_log_Result(cid);
                    caseListViewModel.Lcms_user_type_md = applicationRepository.Lcms_user_type_md();
                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult viewcasedetails_md(string caseid, string remarks, string btn, string forwardto)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 6)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    int cid = Convert.ToInt32(caseid);
                    if (btn == "1")
                    {
                        var data = applicationRepository.logdetails_md(id, cid, remarks, btn, forwardto);
                    }
                    else
                    {
                        var data = applicationRepository.logdetails_md(id, cid, remarks, btn, forwardto);
                    }

                    return RedirectToAction("CaseList_show_md", "lcms");
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult CaseList_show_department()
        {
            try
            {
                if (Session["username"] != null)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult CaseList_show_department(CaseListViewModel obj)
        {
            try
            {
                if (Session["username"] != null)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();
                    caseListViewModel.ComplianceTimee = obj.ComplianceTimee;
                    caseListViewModel.CaseTypeId = obj.CaseTypeId;
                    caseListViewModel.Statuss = obj.Statuss;
                    if (obj.CaseTypeId != 0 && obj.Statuss != "0" && obj.ComplianceTimee != "0")
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().Where(x => x.CaseTypeId == obj.CaseTypeId && x.Status == obj.Statuss && x.ComplianceTime == obj.ComplianceTimee).ToList();
                    }
                    else
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().ToList();
                    }

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult viewcasedetails_department(string id)
        {
            try
            {
                if (Session["username"] != null)
                {
                    int cid = Convert.ToInt32(EncodeDecode.DecodeBase64(id));
                    string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.sp_Input_Case_Detail_Results = applicationRepository.sp_Input_Case_Detail_Results(cid);
                    caseListViewModel.sp_Input_Case_Doc_Detail_Results = applicationRepository.sp_Input_Case_Doc_Detail_Results(cid, baseurl);
                    caseListViewModel.sp_Lcms_User_Log_Results = applicationRepository.sp_lcms_user_log_Result(cid);
                    caseListViewModel.Lcms_user_type_department = applicationRepository.Lcms_user_type_department();

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult viewcasedetails_department(string caseid, string remarks, string btn, string forwardto)
        {
            try
            {
                if (Session["username"] != null)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    int cid = Convert.ToInt32(caseid);
                    if (btn == "1")
                    {
                        var data = applicationRepository.logdetails_department(id, cid, remarks, btn, forwardto);
                    }
                    else
                    {
                        var data = applicationRepository.logdetails_department(id, cid, remarks, btn, forwardto);
                    }

                    return RedirectToAction("CaseList_show_department", "lcms");
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult Assigned_case_list()
        {
            return View();
        }
        public ActionResult CaseList_lawer()
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 5)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().Where(x => x.UserId == id).ToList();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();
                    caseListViewModel.Lawer_List = applicationRepository.Lawer_List();

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult CaseList_lawer(CaseListViewModel obj)

        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 5)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.CaseType = applicationRepository.CaseType();
                    caseListViewModel.ComplianceTime = applicationRepository.ComplianceTime();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();
                    caseListViewModel.Lawer_List = applicationRepository.Lawer_List();
                    caseListViewModel.ComplianceTimee = obj.ComplianceTimee;
                    caseListViewModel.CaseTypeId = obj.CaseTypeId;
                    caseListViewModel.Statuss = obj.Statuss;
                    if (obj.CaseTypeId != 0 && obj.Statuss != "0" && obj.ComplianceTimee != "0")
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().Where(x => x.UserId == id && x.CaseTypeId == obj.CaseTypeId && x.Status == obj.Statuss && x.ComplianceTime == obj.ComplianceTimee).ToList();
                    }
                    else
                    {
                        caseListViewModel.vw_Input_Case_Lists = applicationRepository.vw_Input_Case_Lists().Where(x => x.UserId == id).ToList();
                    }

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult viewcasedetails_lawer(string id)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 5)
                {
                    int cid = Convert.ToInt32(EncodeDecode.DecodeBase64(id));
                    string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.sp_Input_Case_Detail_Results = applicationRepository.sp_Input_Case_Detail_Results(cid);
                    caseListViewModel.sp_Input_Case_Doc_Detail_Results = applicationRepository.sp_Input_Case_Doc_Detail_Results(cid, baseurl);
                    caseListViewModel.sp_Lcms_User_Log_Results = applicationRepository.sp_lcms_user_log_Result(cid);

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult case_hearing_list(string id)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 5)
                {
                    int uid = Convert.ToInt32(Session["userid"]);
                    int cid = Convert.ToInt32(EncodeDecode.DecodeBase64(id));
                    Session["caseid"] = cid;
                    string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.sp_Case_Hearing_Detail_Results = applicationRepository.sp_Case_Hearing_Detail_Results(cid, uid);
                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult input_case_hearing_detail()
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 5)
                {
                    int id = Convert.ToInt32(Session["userid"]);
                    int caseid = Convert.ToInt32(Session["caseid"]);

                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.Status = applicationRepository.LCMS_Status();
                    caseListViewModel.CaseId = caseid;

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        [HttpPost]
        public ActionResult input_case_hearing_detail(string[] docname, HttpPostedFileBase[] filename, CaseListViewModel obj, string casedate)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 5)
                {
                    int id = Convert.ToInt32(Session["userid"]);

                    var data = applicationRepository.SaveCaseHearingDetail(obj, id, casedate);
                    List<LCMS_Case_Hearing_Doc> casedoc = new List<LCMS_Case_Hearing_Doc>();

                    if (filename.Length > 0)
                    {
                        for (int i = 0; i < filename.Length; i++)
                        {
                            if (filename[i] != null && docname[i] != "")
                            {
                                LCMS_Case_Hearing_Doc obj1;
                                obj1 = new LCMS_Case_Hearing_Doc();

                                obj1 = UploadFileHelper.SaveHearingFileIntoLocal(filename[i], docname[i], data);

                                casedoc.Add(obj1);
                            }
                        }
                    }
                    var data1 = applicationRepository.SaveCaseHearingDoc(casedoc);
                    TempData["Message"] = "Case hearing detail submitted successfully";
                    return RedirectToAction("CaseList_lawer", "lcms");
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult view_hearing_case_details(string id, string hid)
        {
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 5)
                {
                    int cid = Convert.ToInt32(EncodeDecode.DecodeBase64(id));
                    int hearingid = Convert.ToInt32(EncodeDecode.DecodeBase64(hid));
                    int uid = Convert.ToInt32(Session["userid"]);

                    string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];
                    CaseListViewModel caseListViewModel = new CaseListViewModel();
                    caseListViewModel.Case_Hearing_Detail = applicationRepository.sp_Case_Hearing_Detail_Results(cid, uid).Where(x => x.Case_Hearing_Id == hearingid).FirstOrDefault();
                    caseListViewModel.sp_Case_Hearing_Doc_Detail_Results = applicationRepository.sp_Case_Hearing_Doc_Detail_Results(hearingid, baseurl);

                    return View(caseListViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }
        public ActionResult view_hearing_details(string id)
        {
            try
            {
                if (Session["username"] != null)
                {
                    int cid = Convert.ToInt32(EncodeDecode.DecodeBase64(id));

                    string baseurl = ConfigurationManager.AppSettings["BaseWebUrl"];
                    var data = applicationRepository.caseHearingModel(cid, baseurl);
                    return View(data);
                }
                else
                {
                    return RedirectToAction("Login", "lcms");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "lcms");
            }
        }

        






    }
}