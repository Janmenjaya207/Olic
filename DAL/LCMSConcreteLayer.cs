using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using OLIC.Models;
using System.Web.Mvc;
//using OLIC.Helper;
//using OLICcustom;
using BEL;
using BLL;
using System.Data.Entity;

namespace DAL
{
    public class LCMSConcreteLayer : LCMSAbstractLayer
    {
        OLICEntities db = new OLICEntities();
        #region---------------------Login-----------------------------------
        public ManageUser Login(string uname, string password, int projectypeid)
        {
            var data = db.ManageUsers.Where(x => x.Project_Typeid == projectypeid && x.UserName == uname && x.Password == password && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            if (data != null)
            {
                return data;
            }
            else
            {
                return data;
            }
        }
        #endregion
        #region---------------------Master------------------------------

        public int SaveCourtType(MasterModel CourtMasterModel)
        {
            try
            {
                int count = 0;

                if (CourtMasterModel.Mst_Court.CourtId != 0 && CourtMasterModel.Mst_Court.CourtId != null)
                {
                    var obj = db.LCMS_Mst_Court.Where(x => x.CourtId == CourtMasterModel.Mst_Court.CourtId).FirstOrDefault();
                    obj.CourtName = CourtMasterModel.Mst_Court.CourtName;
                    obj.IsActive = true;
                    obj.IsDelete = false;
                    db.SaveChanges();
                    count = 2;
                }
                else
                {
                    LCMS_Mst_Court obj = new LCMS_Mst_Court();
                    obj.CourtName = CourtMasterModel.Mst_Court.CourtName;
                    obj.IsActive = true;
                    obj.IsDelete = false;
                    db.LCMS_Mst_Court.Add(obj);
                    db.SaveChanges();
                    count = 1;
                }
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<LCMS_Mst_Court> MstCourtType()
        {
            var data = db.LCMS_Mst_Court.Where(x=>x.IsActive==true).ToList();
            return data;
        }

        public bool DeleteCourtType(int id)
        {
            var data = db.LCMS_Mst_Court.Where(x => x.CourtId == id).FirstOrDefault();
            data.IsActive = false;
            data.IsDelete = true;
            db.SaveChanges();
            return true;
        }

        public int SaveCaseType(MasterModel CaseMasterModel)
        {
            try
            {
                int count = 0;

                if (CaseMasterModel.Mst_Case.CaseTypeId != 0 && CaseMasterModel.Mst_Case.CaseTypeId != null)
                {
                    var obj = db.LCMS_Mst_CaseType.Where(x => x.CaseTypeId == CaseMasterModel.Mst_Case.CaseTypeId).FirstOrDefault();
                    obj.CaseTypeName = CaseMasterModel.Mst_Case.CaseTypeName;
                    obj.IsActive = true;
                    obj.IsDelete = false;
                    db.SaveChanges();
                    count = 2;
                }
                else
                {
                    LCMS_Mst_CaseType obj = new LCMS_Mst_CaseType();
                    obj.CaseTypeName = CaseMasterModel.Mst_Case.CaseTypeName;
                    obj.IsActive = true;
                    obj.IsDelete = false;
                    db.LCMS_Mst_CaseType.Add(obj);
                    db.SaveChanges();
                    count = 1;
                }
                return count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<LCMS_Mst_CaseType> MstCaseType()
        {
            var data = db.LCMS_Mst_CaseType.Where(x => x.IsActive == true).ToList();
            return data;
        }

        public bool DeleteCaseType(int id)
        {
            var data = db.LCMS_Mst_CaseType.Where(x => x.CaseTypeId == id).FirstOrDefault();
            data.IsActive = false;
            data.IsDelete = true;
            db.SaveChanges();
            return true;
        }

        #endregion

        #region--------------------------LCMS-------------------------------
        public List<SelectListItem> CaseType()
        {
            var data = db.LCMS_Mst_CaseType.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.CaseTypeName,
                    Value = item.CaseTypeId.ToString(),
                });
            }
            return items;
        }
        public List<SelectListItem> CourtType()
        {
            var data = db.LCMS_Mst_Court.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.CourtName,
                    Value = item.CourtId.ToString(),
                });
            }
            return items;
        }

        public List<SelectListItem> ComplianceTime()
        {
            var data = db.LCMS_Mst_Compliance_Time.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.Compliance_Time.ToString(),
                    Value = item.Copliance_Id.ToString(),
                });
            }
            return items;
        }
        public List<SelectListItem> LCMS_Status()
        {
            var data = db.LCMS_Status.Where(x => x.IsActive == true && x.IsDeleted == false).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.LCMS_Status_Name,
                    Value = item.LCMS_Status_Id.ToString(),
                });
            }
            return items;
        }
        public int SaveCaseDetail(CaseViewModel obj, int id, string casedate)
        {
            if (obj.LCMS_CaseDetails.CaseId != 0)
            {
                int caseid = obj.LCMS_CaseDetails.CaseId;

                int comid = Convert.ToInt32(obj.LCMS_CaseDetails.ComplianceTime);
                int days = Convert.ToInt32(db.LCMS_Mst_Compliance_Time.Where(x => x.Copliance_Id == comid).FirstOrDefault().Compliance_Time);

                var lcmscase = db.LCMS_CaseDetails.Where(x => x.CaseId == caseid).FirstOrDefault();
                lcmscase.CaseNo = obj.LCMS_CaseDetails.CaseNo;
                lcmscase.PartyName = obj.LCMS_CaseDetails.PartyName;
                lcmscase.CourtName = obj.LCMS_CaseDetails.CourtName;
                lcmscase.CaseTypeId = obj.LCMS_CaseDetails.CaseTypeId;
                lcmscase.CaseSubject = obj.LCMS_CaseDetails.CaseSubject;
                lcmscase.CourtOrder = obj.LCMS_CaseDetails.CourtOrder;
                lcmscase.Status = obj.LCMS_CaseDetails.Status;
                lcmscase.ComplianceTime = obj.LCMS_CaseDetails.ComplianceTime;
                lcmscase.CaseFileDate = MainModel.convertdateformat(casedate);
                DateTime comdate = MainModel.convertdateformat(casedate).AddDays(days);
                lcmscase.ComplianceDate = comdate;
                lcmscase.IsActive = true;
                lcmscase.IsDelete = false;
                lcmscase.ModOn = DateTime.Now;
                lcmscase.PendingAt = "4";
                db.SaveChanges();

                return caseid;
            }
            else
            {
                var data = db.LCMS_CaseDetails.Add(obj.LCMS_CaseDetails);
                db.SaveChanges();

                int caseid = data.CaseId;
                int comid = Convert.ToInt32(obj.LCMS_CaseDetails.ComplianceTime);
                int days = Convert.ToInt32(db.LCMS_Mst_Compliance_Time.Where(x => x.Copliance_Id == comid).FirstOrDefault().Compliance_Time);

                var lcmscase = db.LCMS_CaseDetails.Where(x => x.CaseId == caseid).FirstOrDefault();
                lcmscase.CaseFileDate = MainModel.convertdateformat(casedate);
                DateTime comdate = MainModel.convertdateformat(casedate).AddDays(days);
                lcmscase.ComplianceDate = comdate;
                lcmscase.IsActive = true;
                lcmscase.IsDelete = false;
                lcmscase.CreatedBy = id;
                lcmscase.CreatedOn = DateTime.Now;
                lcmscase.PendingAt = "4";
                db.SaveChanges();

                return caseid;
            }
        }
        public int SaveCaseHearingDetail(CaseListViewModel obj, int id, string casedate)
        {

            var data = db.LCMS_Case_Hearing_Dtls.Add(obj.LCMS_Case_Hearing_Dtls);
            db.SaveChanges();
            int casehid = Convert.ToInt32(data.Case_Hearing_Id);
            var lcmscase = db.LCMS_Case_Hearing_Dtls.Where(x => x.Case_Hearing_Id == casehid).FirstOrDefault();
            lcmscase.UserId = id;
            lcmscase.CaseId = obj.CaseId;
            lcmscase.HearingDate = MainModel.convertdateformat(casedate);
            lcmscase.IsActive = true;
            lcmscase.IsDeleted = false;
            lcmscase.CreatedBy = id;
            lcmscase.CreatedDate = DateTime.Now;
            db.SaveChanges();

            var lcmscasedtl = db.LCMS_CaseDetails.Where(x => x.CaseId == obj.CaseId).FirstOrDefault();
            lcmscasedtl.Status = obj.LCMS_Case_Hearing_Dtls.Status;
            db.SaveChanges();

            return casehid;
        }

        public int SaveCaseDoc(List<LCMS_CaseDoc> obj)
        {
            if (obj.ToList().Count > 0)
            {
                foreach (var item in obj)
                {
                    db.LCMS_CaseDoc.Add(item);
                    db.SaveChanges();
                }
            }
            return 1;
        }
        public int SaveCaseHearingDoc(List<LCMS_Case_Hearing_Doc> obj)
        {
            if (obj.ToList().Count > 0)
            {
                foreach (var item in obj)
                {
                    db.LCMS_Case_Hearing_Doc.Add(item);
                    db.SaveChanges();
                }
            }
            return 1;
        }
        public List<vw_input_case_list> vw_Input_Case_Lists()
        {
            var data = db.vw_input_case_list.ToList();
            return data;
        }
        public List<sp_input_case_detail_Result> sp_Input_Case_Detail_Results(int id)
        {
            var data = db.sp_input_case_detail(id).ToList();
            return data;
        }
        public List<sp_lcms_user_log_Result> sp_lcms_user_log_Result(int id)
        {
            var data = db.sp_lcms_user_log(id).ToList();
            return data;

        }
        public List<sp_input_case_doc_detail_Result> sp_Input_Case_Doc_Detail_Results(int id, string url)
        {
            var data = db.sp_input_case_doc_detail(id, url).ToList();
            return data;
        }
        public LCMS_CaseDetails LCMS_CaseDetails(int id)
        {
            var data = db.LCMS_CaseDetails.Where(x => x.CaseId == id).FirstOrDefault();
            return data;
        }
        public int deletedocument(int id)
        {
            var data = db.LCMS_CaseDoc.Where(x => x.CaseDocId == id).FirstOrDefault();
            if (data != null)
            {
                db.LCMS_CaseDoc.Remove(data);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 2;
            }
        }
        public int logdetails(int uid, int caseid, string remark)
        {
            LCMS_UserLog obj = new LCMS_UserLog();
            obj.CaseId = caseid;
            obj.Remarks = remark;
            obj.CreatedBy = uid;
            obj.CreatedOn = DateTime.Now;
            obj.IsActive = true;
            obj.IsDelete = false;
            db.LCMS_UserLog.Add(obj);
            db.SaveChanges();

            var data = db.LCMS_CaseDetails.Where(x => x.CaseId == caseid).FirstOrDefault();
            if (data != null)
            {
                data.PendingAt = "9";
                db.SaveChanges();
            }

            return 1;
        }
        public int logdetails(int uid, int caseid, string remark, string parameter, string forwardto)
        {
            if (parameter == "1")
            {
                LCMS_UserLog obj = new LCMS_UserLog();
                obj.CaseId = caseid;
                obj.Remarks = remark;
                obj.CreatedBy = uid;
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.IsDelete = false;
                db.LCMS_UserLog.Add(obj);
                db.SaveChanges();

                var data = db.LCMS_CaseDetails.Where(x => x.CaseId == caseid).FirstOrDefault();
                if (data != null)
                {
                    data.PendingAt = "4";
                    db.SaveChanges();
                }

                return 1;
            }
            else
            {
                LCMS_UserLog obj = new LCMS_UserLog();
                obj.CaseId = caseid;
                obj.Remarks = remark;
                obj.CreatedBy = uid;
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.IsDelete = false;
                db.LCMS_UserLog.Add(obj);
                db.SaveChanges();

                var data = db.LCMS_CaseDetails.Where(x => x.CaseId == caseid).FirstOrDefault();
                if (data != null)
                {
                    data.PendingAt = forwardto;
                    db.SaveChanges();
                }

                return 1;
            }
        }
        public int logdetails_ed(int uid, int caseid, string remark, string parameter, string forwardto)
        {
            if (parameter == "1")
            {
                LCMS_UserLog obj = new LCMS_UserLog();
                obj.CaseId = caseid;
                obj.Remarks = remark;
                obj.CreatedBy = uid;
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.IsDelete = false;
                db.LCMS_UserLog.Add(obj);
                db.SaveChanges();

                var data = db.LCMS_CaseDetails.Where(x => x.CaseId == caseid).FirstOrDefault();
                if (data != null)
                {
                    data.PendingAt = forwardto;
                    db.SaveChanges();
                }

                return 1;
            }
            else
            {
                LCMS_UserLog obj = new LCMS_UserLog();
                obj.CaseId = caseid;
                obj.Remarks = remark;
                obj.CreatedBy = uid;
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.IsDelete = false;
                db.LCMS_UserLog.Add(obj);
                db.SaveChanges();

                var data = db.LCMS_CaseDetails.Where(x => x.CaseId == caseid).FirstOrDefault();
                if (data != null)
                {
                    data.PendingAt = "6";
                    db.SaveChanges();
                }

                return 1;
            }
        }
        public int logdetails_md(int uid, int caseid, string remark, string parameter, string forwardto)
        {
            if (parameter == "1")
            {
                LCMS_UserLog obj = new LCMS_UserLog();
                obj.CaseId = caseid;
                obj.Remarks = remark;
                obj.CreatedBy = uid;
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.IsDelete = false;
                db.LCMS_UserLog.Add(obj);
                db.SaveChanges();

                var data = db.LCMS_CaseDetails.Where(x => x.CaseId == caseid).FirstOrDefault();
                if (data != null)
                {
                    data.PendingAt = forwardto;
                    db.SaveChanges();
                }

                return 1;
            }
            else
            {
                LCMS_UserLog obj = new LCMS_UserLog();
                obj.CaseId = caseid;
                obj.Remarks = remark;
                obj.CreatedBy = uid;
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.IsDelete = false;
                db.LCMS_UserLog.Add(obj);
                db.SaveChanges();

                var data = db.LCMS_CaseDetails.Where(x => x.CaseId == caseid).FirstOrDefault();
                if (data != null)
                {
                    data.PendingAt = "100";
                    db.SaveChanges();
                }

                return 1;
            }
        }
        public int logdetails_department(int uid, int caseid, string remark, string parameter, string forwardto)
        {
            if (parameter == "1")
            {
                LCMS_UserLog obj = new LCMS_UserLog();
                obj.CaseId = caseid;
                obj.Remarks = remark;
                obj.CreatedBy = uid;
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.IsDelete = false;
                db.LCMS_UserLog.Add(obj);
                db.SaveChanges();

                var data = db.LCMS_CaseDetails.Where(x => x.CaseId == caseid).FirstOrDefault();
                if (data != null)
                {
                    data.PendingAt = forwardto;
                    db.SaveChanges();
                }

                return 1;
            }
            else
            {
                LCMS_UserLog obj = new LCMS_UserLog();
                obj.CaseId = caseid;
                obj.Remarks = remark;
                obj.CreatedBy = uid;
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.IsDelete = false;
                db.LCMS_UserLog.Add(obj);
                db.SaveChanges();

                var data = db.LCMS_CaseDetails.Where(x => x.CaseId == caseid).FirstOrDefault();
                if (data != null)
                {
                    data.PendingAt = "7";
                    db.SaveChanges();
                }

                return 1;
            }
        }
        public List<SelectListItem> Lcms_user_type()
        {
            var data = db.UserTypes.Where(x => x.Project_Typeid == 2 && x.UserType_Id != 4 && x.Is_Visible == true && x.IsActive == true && x.IsDelete == false).ToList();
            var data1 = db.UserTypes.Where(x => x.Project_Typeid == 2 && x.UserType_Id == 6 && x.IsActive == true && x.IsDelete == false).ToList();
            data.AddRange(data1);

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.User_Name,
                    Value = item.UserType_Id.ToString(),
                });
            }
            return items;
        }
        public List<SelectListItem> Lcms_user_type_ed()
        {
            var data = db.UserTypes.Where(x => x.Project_Typeid == 2 && x.UserType_Id != 7 && x.Is_Visible == true && x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.User_Name,
                    Value = item.UserType_Id.ToString(),
                });
            }
            return items;
        }
        public List<SelectListItem> Lcms_user_type_md()
        {
            var data = db.UserTypes.Where(x => x.Project_Typeid == 2 && x.UserType_Id != 6 && x.Is_Visible == true && x.IsActive == true && x.IsDelete == false).ToList();
            var data1 = db.UserTypes.Where(x => x.Project_Typeid == 2 && x.UserType_Id == 9 && x.IsActive == true && x.IsDelete == false).ToList();
            data.AddRange(data1);
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.User_Name,
                    Value = item.UserType_Id.ToString(),
                });
            }
            return items;
        }
        public List<SelectListItem> Lcms_user_type_department()
        {
            var data = db.UserTypes.Where(x => x.UserType_Id == 4).ToList();
            var data1 = db.UserTypes.Where(x => x.UserType_Id == 9).ToList();
            data.AddRange(data1);
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.User_Name,
                    Value = item.UserType_Id.ToString(),
                });
            }
            return items;
        }
        public List<SelectListItem> Lawer_List()
        {
            var data = db.ManageUsers.Where(x => x.UserType_Id == 5).ToList();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var item in data)
            {
                items.Add(new SelectListItem
                {
                    Text = item.FirstName + " " + item.LastName,
                    Value = item.User_Id.ToString(),
                });
            }
            return items;
        }
        public int assignlawer(int caseid, int lawer, string message)
        {
            LCMS_AssignedLawer obj = new LCMS_AssignedLawer();
            obj.CaseId = caseid;
            obj.UserId = lawer;
            obj.AssignedDate = DateTime.Now;
            obj.Message = message;
            obj.CreatedDate = DateTime.Now;
            obj.IsActive = true;
            obj.IsDeleted = false;
            db.LCMS_AssignedLawer.Add(obj);
            db.SaveChanges();

            return 1;
        }
        public List<sp_case_hearing_detail_Result> sp_Case_Hearing_Detail_Results(int cid, int uid)
        {
            var data = db.sp_case_hearing_detail(cid, uid).ToList();
            return data;
        }
        public List<sp_case_hearing_doc_detail_Result> sp_Case_Hearing_Doc_Detail_Results(int hid, string url)
        {
            var data = db.sp_case_hearing_doc_detail(hid, url).ToList();
            return data;
        }
        public CaseHearingViewModel caseHearingModel(int caseid, string url)
        {
            CaseHearingViewModel obj = new CaseHearingViewModel();
            obj.caseHearingModels = (from a in db.sp_case_hearing_detail(caseid, 0)
                                     select new CaseHearingModel
                                     {
                                         CaseNo = a.CaseNo,
                                         HearingDate = a.HearingDate,
                                         OrderOfCourt = a.OrderOfCourt,
                                         LCMS_Status_Name = a.LCMS_Status_Name,
                                         CaseHearingDocModel = (from b in db.sp_case_hearing_doc_detail(a.Case_Hearing_Id, url)
                                                                select new CaseHearingDocModel
                                                                {
                                                                    DocumentName = b.DocumentName,
                                                                    docfile = b.docfile
                                                                }).ToList()
                                     }).ToList();
            return obj;
        }
        #endregion
    }
}
