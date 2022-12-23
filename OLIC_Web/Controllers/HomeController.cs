using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEL;
using OLIC_Web.Models;

namespace OLIC_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BLL.AbstractLayer applicationRepository;
        OLICEntities db = new OLICEntities();
        public HomeController(BLL.AbstractLayer apprepo)
        {
            applicationRepository = apprepo;
        }

        //private readonly BLL.AbstractLayer applicationRepository;

        //OLICEntities db = new OLICEntities();

        //public HomeController(BLL.AbstractLayer appRepository)
        //{
        //    applicationRepository = appRepository;
        //}
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(string uname,string password)
        //{
        //    Session["projecttype"] = 1;
        //    if (SecurityModel.eeusername == uname && SecurityModel.eepassword == password)
        //    {
        //        Session["usertype"] = 1;
        //        return RedirectToAction("dashboard", "divison");
        //    }
        //    else if (SecurityModel.jeusername == uname && SecurityModel.jepassword == password)
        //    {
        //        Session["usertype"] = 2;
        //        return RedirectToAction("dashboard", "section");
        //    }
        //    else if (SecurityModel.bankusername == uname && SecurityModel.bankpassword == password)
        //    {
        //        Session["usertype"] = 8;
        //        return RedirectToAction("dashboard", "finance");
        //    }
        //    else if (SecurityModel.aeusername == uname && SecurityModel.aepassword == password)
        //    {
        //        Session["usertype"] = 10;
        //        return RedirectToAction("dashboard", "SubDivison");
        //    }
        //    else if (SecurityModel.mdusername == uname && SecurityModel.mdpassword == password)
        //    {
        //        Session["usertype"] = 11;
        //        return RedirectToAction("dashboard", "ho");
        //    }
        //    else if (SecurityModel.OBSusername == uname && SecurityModel.OBSpassword == password)

        //    {
        //        Session["usertype"] = 20;
        //        return RedirectToAction("dashboard", "Billing");
        //    }
        //    else if (SecurityModel.OBSjeusername == uname && SecurityModel.OBSjepassword == password)

        //    {
        //        Session["usertype"] = 21;
        //        return RedirectToAction("Dashboard", "BillingDivison");
        //    }
        //    else if (SecurityModel.OBSaeusername == uname && SecurityModel.OBSaepassword == password)

        //    {
        //        Session["usertype"] = 22;
        //        return RedirectToAction("Dashboard", "BillingSubDivison");
        //    }
        //    else if (SecurityModel.HcmUsername == uname && SecurityModel.HcmPassword == password)

        //    {
        //        Session["usertype"] = 24;
        //        return RedirectToAction("Dashboard", "Hcm");
        //    }
        //    else if (SecurityModel.AdminUsername == uname && SecurityModel.AdminPassword == password)

        //    {
        //        Session["usertype"] = 25;
        //        return RedirectToAction("Dashboard", "Admin");
        //    }
        //    else if (SecurityModel.LcmsDivisionUsername == uname && SecurityModel.LcmsDivisionPassword == password)

        //    {
        //        Session["usertype"] = 26;
        //        return RedirectToAction("Dashboard", "Lcms");
        //    }
        //    else if (uname != null && SecurityModel.HcmPassword == password)

        //    {
        //        //var userid = db.HCM_EmployeeDtl.Find();
        //        //HCM_EmployeeDtl item = db.HCM_EmployeeDtl.Find(userid);
        //        var employees = (from e in db.HCM_EmployeeDtl
        //                         where e.Employee_Code == uname
        //                         select e).FirstOrDefault();
        //        if (employees != null)
        //        {

        //            Session["username"] = employees.Employee_Name;
        //            Session["usercode"] = employees.Employee_Code;
        //            Session["userid"] = employees.Employee_Id;
        //            //TempData["username"]= uname;

        //            ViewBag.password = password;
        //            //Session["usertype"] = employees.Emp_Class;
        //            Session["usertype"] = 24;
        //            return RedirectToAction("Dashboard", "Hcm", uname);
        //        }
        //        return View();
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
        //public ActionResult logout()
        //{
        //    Session.Abandon();
        //    return RedirectToAction("Login", "Home");
        //}


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
            try
            {
                string pwd = EncodeDecode.EncodeBase64(password);
                var data = applicationRepository.CheckLogin(uname, pwd);
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
                    Session["projecttype"] = data.Project_Typeid;
                    Session["usertype"] = data.UserType_Id;
                    Session["username"] = data.UserName;
                    Session["useriid"] = data.User_Id;
                    //Session["division"] = data.DivisionID;
                    //Session["subdivision"] = data.SubdivisionID;
                    //Session["Section"] = data.SectionID;
                    //Session["circle"] = data.CircleID;
                    //Session["district"] = data.DistrictID;
                    //Session["block"] = data.BlockID;
                    return RedirectToAction("Index", "Home");
                }
                else if (data != null && data.IsDelete == true)
                {
                    TempData["Message"] = "User does not exist";
                    return View();
                }
                else
                {
                    TempData["Message"] = "Username or password is incorrect";
                    return View();
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public ActionResult logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }


       
        public ActionResult ManageUser(int? id = 0)
        {
            try
            {
                if (Convert.ToInt32(Session["usertype"]) == 25)
                {
                    ManageUsermodel nh = new ManageUsermodel();
                    if (id > 0)
                    {
                        int idd = Convert.ToInt32(id);
                        var data = applicationRepository.Mst_Ho(idd);
                        nh.mangeuser = data;
                        nh.GetBlock = applicationRepository.Block();
                        nh.GetCircle = applicationRepository.GetCircle();
                        nh.GetDistrcit = applicationRepository.District();
                        nh.GetDivision = applicationRepository.GetDivision();
                        nh.Getsection = applicationRepository.Getsection();
                        nh.GetSubdivision = applicationRepository.GetSubDivision();
                        nh.GetUserRole = applicationRepository.GetUserRole();
                       // ViewBag.data = db.vw_showmanageuser.Where(x => x.Project_Typeid == 1).ToList();
                        return View(nh);

                    }


                    else
                    {

                        nh.GetBlock = applicationRepository.Block();
                        nh.GetCircle = applicationRepository.GetCircle();
                        nh.GetDistrcit = applicationRepository.District();
                        nh.GetDivision = applicationRepository.GetDivision();
                        nh.Getsection = applicationRepository.Getsection();
                        nh.GetSubdivision = applicationRepository.GetSubDivision();
                        nh.GetUserRole = applicationRepository.GetUserRole();
                        //ViewBag.data = db.vw_showmanageuser.Where(x => x.Project_Typeid == 1).ToList();
                        return View(nh);
                    }

                }
                else
                {
                    TempData["WarningMessage"] = "Something went wrong";
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        [HttpPost]
        public ActionResult ManageUser(ManageUsermodel bg)
        {
            try
            {
                if (Convert.ToInt32(Session["usertype"]) == 25)
                {
                    string pwd = EncodeDecode.EncodeBase64(bg.mangeuser.Password);
                    bg.mangeuser.Password = pwd;
                    bg.mangeuser.CreatedBy = Session["usertype"].ToString();
                    int count = applicationRepository.savemanageuser(bg);
                    if (count > 1)
                    {
                        TempData["Message"] = "Manageuser Deatils Created Successfully";
                        return RedirectToAction("ManageUser", "Home");
                    }
                    else
                    {
                        TempData["Message"] = "HO Deatils Updated Successfully";
                        return RedirectToAction("ManageUser", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["WarningMessage"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult DeleteManageUser(int id)
        {
            var data = applicationRepository.DeleteMangeuser(id);
            if (data == true)
            {
                TempData["WarningMessage"] = "User Deleted Successfully";
            }
            else
            {
                TempData["WarningMessage"] = "Something went wrong..!";
            }
            return RedirectToAction("ManageUser", "Home");
        }
        [HttpPost]
        public ActionResult GetDivision(int cccc)
        {
            var list = db.Mst_Division.Where(x => x.CircleId == cccc.ToString()).ToList();
            List<SelectListItem> Division = new List<SelectListItem>();
            if (cccc != 0)
            {
                list.ForEach(x =>
                {
                    Division.Add(new SelectListItem { Text = x.DivisionName, Value = x.DivisionId.ToString() });
                });
            }
            return Json(Division, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetSubDivisionn(string cccc)
        {
            var list = db.Mst_SubDivision.Where(x => x.DivisionId == cccc.ToString()).ToList();
            List<SelectListItem> Division = new List<SelectListItem>();
            if (cccc != null || cccc != "")
            {
                list.ForEach(x =>
                {
                    Division.Add(new SelectListItem { Text = x.SubDivisionName, Value = x.SubDivisionId.ToString() });
                });
            }
            return Json(Division, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSectionn(string cccc)
        {
            var list = db.Mst_Section.Where(x => x.SubDivisionId == cccc.ToString()).ToList();
            List<SelectListItem> Division = new List<SelectListItem>();
            if (cccc != null || cccc != "")
            {
                list.ForEach(x =>
                {
                    Division.Add(new SelectListItem { Text = x.SectionName, Value = x.SectionId.ToString() });
                });
            }
            return Json(Division, JsonRequestBehavior.AllowGet);
        }
    }
}