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
    
    public partial class Hcm_Property_Land
    {
        public int Id { get; set; }
        public string PreciseLocation { get; set; }
        public string ExtentOfInterest { get; set; }
        public Nullable<int> Value { get; set; }
        public string InWhoseName { get; set; }
        public Nullable<System.DateTime> DateAndMannerOfAcquisitionOrDisposal { get; set; }
        public Nullable<int> Property_Id { get; set; }
        public string Files { get; set; }
        public string EmpCode { get; set; }
    
        public virtual Hcm_Property Hcm_Property { get; set; }
    }
}
