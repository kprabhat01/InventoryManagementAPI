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
    
    public partial class prItemLog
    {
        public int id { get; set; }
        public Nullable<int> prid { get; set; }
        public string username { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public string oldRequest { get; set; }
        public string newRequest { get; set; }
    
        public virtual prRequest prRequest { get; set; }
    }
}