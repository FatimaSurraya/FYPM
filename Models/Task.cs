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
    
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            this.Results = new HashSet<Result>();
        }
    
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public int AssignedTo { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string TaskStatus { get; set; }
    
        public virtual ProjectDetail ProjectDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }
        public virtual User User { get; set; }
    }
}
