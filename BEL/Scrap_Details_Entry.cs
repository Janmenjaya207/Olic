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
    
    public partial class Scrap_Details_Entry
    {
        public int Scrap_Details_Id { get; set; }
        public Nullable<int> Scrap_Code_Autogenerate_Id { get; set; }
        public Nullable<int> Scrap_Category_Id { get; set; }
        public Nullable<int> Scrap_Iteam_Id { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string Approx_Weight { get; set; }
        public string Total_Weight { get; set; }
        public Nullable<System.DateTime> Date_Of_Receipt { get; set; }
        public string No_Of_Scrap { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> Is_Active { get; set; }
        public Nullable<bool> Is_Delete { get; set; }
        public string Modified_By { get; set; }
        public string Modified_On { get; set; }
    }
}
