//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FYPM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Invitation
    {
        public int id { get; set; }
        public int sender_id { get; set; }
        public int group_id { get; set; }
        public string status { get; set; }
        public System.DateTime created_at { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
