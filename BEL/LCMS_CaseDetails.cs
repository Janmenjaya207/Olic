//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BEL
{
    using System;
    using System.Collections.Generic;
    
    public partial class LCMS_CaseDetails
    {
        public int CaseId { get; set; }
        public string CaseNo { get; set; }
        public string PartyName { get; set; }
        public Nullable<int> CourtName { get; set; }
        public Nullable<System.DateTime> CaseFileDate { get; set; }
        public Nullable<System.DateTime> ComplianceDate { get; set; }
        public Nullable<int> CaseTypeId { get; set; }
        public string CaseSubject { get; set; }
        public string CourtOrder { get; set; }
        public string Status { get; set; }
        public string ComplianceTime { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModOn { get; set; }
        public string ModBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string PendingAt { get; set; }
    }
}