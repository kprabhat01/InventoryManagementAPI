//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IM.Data.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class logsPRStatu
    {
        public int id { get; set; }
        public int prId { get; set; }
        public int statusId { get; set; }
        public string changedBy { get; set; }
        public Nullable<System.DateTime> changedDate { get; set; }
    
        public virtual Status Status { get; set; }
        public virtual prRequest prRequest { get; set; }
    }
}
