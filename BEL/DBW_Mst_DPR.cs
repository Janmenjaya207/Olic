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
    
    public partial class DBW_Mst_DPR
    {
        public int Dpr_Id { get; set; }
        public string Description_of_work { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> Rate_per_Unit { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string Service_Code { get; set; }
    }
}
