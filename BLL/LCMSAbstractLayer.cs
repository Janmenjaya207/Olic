using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BEL;

namespace BLL
{
    public interface LCMSAbstractLayer
    {
        #region---------------------Login------------------------------
        ManageUser Login(string uname, string password, int projectypeid);
        #endregion

        #region---------------------Master------------------------------

        int SaveCourtType(MasterModel CourtMasterModel);
        List<LCMS_Mst_Court> MstCourtType();
        bool DeleteCourtType(int id);

        int SaveCaseType(MasterModel CaseMasterModel);
        List<LCMS_Mst_CaseType> MstCaseType();
        bool DeleteCaseType(int id);

        #endregion

        #region---------------------Input User------------------------------
        List<SelectListItem> CaseType();
        List<SelectListItem> CourtType();
        List<SelectListItem> ComplianceTime();
        List<SelectListItem> LCMS_Status();
        int SaveCaseDetail(CaseViewModel obj, int id, string casedate);
        int SaveCaseHearingDetail(CaseListViewModel obj, int id, string casedate);
        int SaveCaseDoc(List<LCMS_CaseDoc> obj);
        int SaveCaseHearingDoc(List<LCMS_Case_Hearing_Doc> obj);
        List<vw_input_case_list> vw_Input_Case_Lists();
        List<sp_input_case_detail_Result> sp_Input_Case_Detail_Results(int id);
        List<sp_lcms_user_log_Result> sp_lcms_user_log_Result(int id);
        List<sp_input_case_doc_detail_Result> sp_Input_Case_Doc_Detail_Results(int id, string url);
        LCMS_CaseDetails LCMS_CaseDetails(int id);
        int deletedocument(int id);
        int logdetails(int uid, int caseid, string remark);
        int logdetails(int uid, int caseid, string remark, string parameter, string forwardto);
        int logdetails_ed(int uid, int caseid, string remark, string parameter, string forwardto);
        int logdetails_md(int uid, int caseid, string remark, string parameter, string forwardto);
        int logdetails_department(int uid, int caseid, string remark, string parameter, string forwardto);

        List<SelectListItem> Lcms_user_type();
        List<SelectListItem> Lcms_user_type_ed();
        List<SelectListItem> Lcms_user_type_md();
        List<SelectListItem> Lcms_user_type_department();
        List<SelectListItem> Lawer_List();
        int assignlawer(int caseid, int lawer, string message);
        List<sp_case_hearing_detail_Result> sp_Case_Hearing_Detail_Results(int cid, int uid);
        List<sp_case_hearing_doc_detail_Result> sp_Case_Hearing_Doc_Detail_Results(int hid, string url);
        CaseHearingViewModel caseHearingModel(int caseid, string url);
        #endregion

    }
}
