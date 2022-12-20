using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BEL

{
    public class MasterModel
    {
        public List<SelectListItem> hcm_Employee { get; set; }
        public DBW_Mst_DPR DBW_Mst_DPR { get; set; }

        public LCMS_Mst_Court Mst_Court { get; set; }

        public LCMS_Mst_CaseType Mst_Case { get; set; }
    }
    public class CaseViewModel
    {
        public List<SelectListItem> CaseType { get; set; }
        public List<SelectListItem> CourtType { get; set; }
        public List<SelectListItem> ComplianceTime { get; set; }
        public List<SelectListItem> Status { get; set; }
        public LCMS_CaseDetails LCMS_CaseDetails { get; set; }
        public List<sp_input_case_doc_detail_Result> sp_Input_Case_Doc_Detail_Results { get; set; }
        public string CaseFileDate { get; set; }
    }
    public class CaseListViewModel
    {
        public LCMS_Case_Hearing_Dtls LCMS_Case_Hearing_Dtls { get; set; }
        public List<vw_input_case_list> vw_Input_Case_Lists { get; set; }
        public List<sp_input_case_detail_Result> sp_Input_Case_Detail_Results { get; set; }
        public List<sp_input_case_doc_detail_Result> sp_Input_Case_Doc_Detail_Results { get; set; }
        public List<sp_lcms_user_log_Result> sp_Lcms_User_Log_Results { get; set; }
        public List<SelectListItem> CaseType { get; set; }
        public List<SelectListItem> ComplianceTime { get; set; }
        public List<SelectListItem> Status { get; set; }
        public List<SelectListItem> Lcms_user_type { get; set; }
        public List<SelectListItem> Lcms_user_type_ed { get; set; }
        public List<SelectListItem> Lcms_user_type_md { get; set; }
        public List<SelectListItem> Lcms_user_type_department { get; set; }
        public List<SelectListItem> Lawer_List { get; set; }
        public int CaseTypeId { get; set; }
        public int CaseId { get; set; }
        public string Statuss { get; set; }
        public string ComplianceTimee { get; set; }
        public List<sp_case_hearing_detail_Result> sp_Case_Hearing_Detail_Results { get; set; }
        public sp_case_hearing_detail_Result Case_Hearing_Detail { get; set; }
        public List<sp_case_hearing_doc_detail_Result> sp_Case_Hearing_Doc_Detail_Results { get; set; }
    }

    public class CaseHearingViewModel
    {
        public List<CaseHearingModel> caseHearingModels { get; set; }
    }
    public class CaseHearingModel
    {
        public string HearingDate { get; set; }
        public string OrderOfCourt { get; set; }
        public string CaseNo { get; set; }
        public string LCMS_Status_Name { get; set; }
        public List<CaseHearingDocModel> CaseHearingDocModel { get; set; }
    }
    public class CaseHearingDocModel
    {
        public string DocumentName { get; set; }
        public string docfile { get; set; }
    }
}
