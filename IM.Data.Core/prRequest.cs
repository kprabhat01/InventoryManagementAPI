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
    
    public partial class prRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public prRequest()
        {
            this.logsPRStatus = new HashSet<logsPRStatu>();
            this.prItemLogs = new HashSet<prItemLog>();
            this.prItems = new HashSet<prItem>();
        }
    
        public int id { get; set; }
        public int fromOutletId { get; set; }
        public int toOutletId { get; set; }
        public System.DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public int statusId { get; set; }
        public string Comment { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<logsPRStatu> logsPRStatus { get; set; }
        public virtual outlet outlet { get; set; }
        public virtual outlet outlet1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prItemLog> prItemLogs { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prItem> prItems { get; set; }
    }
}
