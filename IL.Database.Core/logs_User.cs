//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IL.Database.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class logs_User
    {
        public int id { get; set; }
        public int userId { get; set; }
        public Nullable<System.DateTime> loginDate { get; set; }
        public string macAddress { get; set; }
        public string ipAddress { get; set; }
    
        public virtual User User { get; set; }
    }
}
