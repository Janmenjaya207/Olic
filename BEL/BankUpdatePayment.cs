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
    
    public partial class BankUpdatePayment
    {
        public int Bnk_Payment_ID { get; set; }
        public Nullable<int> DeepBorewellRegdId { get; set; }
        public string FD_Number { get; set; }
        public Nullable<System.DateTime> Dateof_FD { get; set; }
        public string Account_Number { get; set; }
        public Nullable<decimal> FD_Amount { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> Is_Active { get; set; }
        public Nullable<bool> Is_Delete { get; set; }
        public Nullable<int> Add_By { get; set; }
        public Nullable<System.DateTime> Add_On { get; set; }
        public Nullable<int> ModBy { get; set; }
        public Nullable<System.DateTime> Mod_On { get; set; }
    }
}