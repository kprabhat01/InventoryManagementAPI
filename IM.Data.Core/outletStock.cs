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
    
    public partial class outletStock
    {
        public int id { get; set; }
        public int itemId { get; set; }
        public int outletId { get; set; }
        public decimal currentStock { get; set; }
        public System.DateTime lastUpdated { get; set; }
        public decimal lastrate { get; set; }
    
        public virtual item item { get; set; }
        public virtual outlet outlet { get; set; }
    }
}
