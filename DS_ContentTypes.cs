//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DigitalServices
{
    using System;
    using System.Collections.Generic;
    
    public partial class DS_ContentTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DS_ContentTypes()
        {
            this.DS_Contents = new HashSet<DS_Contents>();
        }
    
        public decimal id { get; set; }
        public string title { get; set; }
        public string file_name { get; set; }
        public string body { get; set; }
        public Nullable<short> status { get; set; }
        public Nullable<short> type { get; set; }
        public Nullable<decimal> companyId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DS_Contents> DS_Contents { get; set; }
    }
}
